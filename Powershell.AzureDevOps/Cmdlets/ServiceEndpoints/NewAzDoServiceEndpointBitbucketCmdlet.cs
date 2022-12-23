namespace Powershell.AzureDevOps.Cmdlets.Core;

using System.Collections.ObjectModel;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceEndpoints;
using Microsoft.VisualStudio.Services.ServiceEndpoints.WebApi;

[Cmdlet(VerbsCommon.New,"AzDoServiceEndpointBitbucket")]
[OutputType(typeof(ServiceEndpoint))]
public class NewAzDoServiceEndpointBitbucketCmdlet : CmdletBase
{
    #region Parameters

    [Parameter]
    public string Description { get; set; }

    [Parameter(Mandatory = true)]
    public string Name { get; set; }

    [Parameter(Mandatory = true)]
    public string Password { get; set; }

    [Parameter(Mandatory = true)]
    public Guid ProjectId { get; set; }

    [Parameter(Mandatory = true)]
    public string UserName { get; set; }

    #endregion

    #region Cmdlet Methods

    protected override void ProcessRecord()
    {
        using var client = ServiceEndpointClient;
        var serviceEndpoint = new ServiceEndpoint
        {
            Authorization = new EndpointAuthorization
            {
                Parameters = new Dictionary<string, string>
                {
                    { "username", UserName },
                    { "password", Password }
                },
                Scheme = "UsernamePassword"
            },
            Description = Description,
            Name = Name,
            Owner = "library",
            ServiceEndpointProjectReferences = new Collection<ServiceEndpointProjectReference>
            {
                new()
                {
                    Description = Description,
                    Name = Name,
                    ProjectReference = new ProjectReference
                    {
                        Id = ProjectId
                    }
                }
            },
            Type = "Bitbucket",
            Url = new Uri("https://api.bitbucket.org/")
        };
        var result = client.CreateServiceEndpointAsync(serviceEndpoint).Result;
        WriteObject(result);
    }

    #endregion
}