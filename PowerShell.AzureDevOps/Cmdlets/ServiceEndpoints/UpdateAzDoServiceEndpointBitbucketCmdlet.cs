namespace PowerShell.AzureDevOps.Cmdlets.Core;

using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceEndpoints.WebApi;
using PowerShell.AzureDevOps.Clients.ServiceEndpoints;

[Cmdlet(VerbsData.Update, "AzDoServiceEndpointBitbucket")]
[OutputType(typeof(ServiceEndpoint))]
public class UpdateAzDoServiceEndpointBitbucketCmdlet : CmdletBase
{
    #region Parameters

    [Parameter(Mandatory = true)]
    public Guid Id { get; set; }

    [Parameter(Mandatory = true)]
    public CreateOrUpdateServiceEndpointBitbucketArgs ServiceEndpoint { get; set; }

    #endregion

    #region Cmdlet Methods

    protected override void ProcessRecord()
    {
        WriteObject(ServiceEndpointClient.UpdateServiceEndpointBitbucketAsync(Id, ServiceEndpoint).Result);
    }

    #endregion
}