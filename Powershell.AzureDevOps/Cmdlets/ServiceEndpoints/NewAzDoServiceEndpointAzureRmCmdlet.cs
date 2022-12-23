namespace Powershell.AzureDevOps.Cmdlets.Core;

using System.Collections.ObjectModel;
using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceEndpoints;
using Microsoft.VisualStudio.Services.ServiceEndpoints.WebApi;

[Cmdlet(VerbsCommon.New, "AzDoServiceEndpointAzureRm")]
[OutputType(typeof(ServiceEndpoint))]
public class NewAzDoServiceEndpointAzureRmCmdlet : CmdletBase
{
    #region Parameters

    [Parameter]
    public string Description { get; set; }

    [Parameter(Mandatory = true)]
    public string Name { get; set; }

    [Parameter(Mandatory = true)]
    public Guid ProjectId { get; set; }

    [Parameter(Mandatory = true)]
    public string ServicePrincipalId { get; set; }

    [Parameter(Mandatory = true)]
    public string ServicePrincipalKey { get; set; }

    [Parameter(Mandatory = true)]
    public string SubscriptionId { get; set; }

    [Parameter(Mandatory = true)]
    public string SubscriptionName { get; set; }

    [Parameter(Mandatory = true)]
    public string TenantId { get; set; }

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
                    { "authenticationType", "spnKey" },
                    { "serviceprincipalid", ServicePrincipalId },
                    { "serviceprincipalkey", ServicePrincipalKey },
                    { "tenantid", TenantId }
                },
                Scheme = "ServicePrincipal"
            },
            Data = new Dictionary<string, string>
            {
                { "creationMode", "Manual" },
                { "environment", "AzureCloud" },
                { "scopeLevel", "Subscription" },
                { "subscriptionId", SubscriptionId },
                { "subscriptionName", SubscriptionName }
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
            Type = "azurerm",
            Url = new Uri("https://management.azure.com/")
        };
        var result = client.CreateServiceEndpointAsync(serviceEndpoint).Result;
        WriteObject(result);
    }

    #endregion
}