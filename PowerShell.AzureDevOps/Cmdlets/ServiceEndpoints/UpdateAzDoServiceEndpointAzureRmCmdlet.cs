namespace PowerShell.AzureDevOps.Cmdlets.Core;

using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceEndpoints.WebApi;
using PowerShell.AzureDevOps.Clients.ServiceEndpoints;

[Cmdlet(VerbsData.Update, "AzDoServiceEndpointAzureRm")]
[OutputType(typeof(ServiceEndpoint))]
public class UpdateAzDoServiceEndpointAzureRmCmdlet : CmdletBase
{
    #region Parameters

    [Parameter(Mandatory = true)]
    public Guid Id { get; set; }

    [Parameter(Mandatory = true)]
    public CreateOrUpdateServiceEndpointAzureRmArgs ServiceEndpoint { get; set; }

    #endregion

    #region Cmdlet Methods

    protected override void ProcessRecord()
    {
        WriteObject(ServiceEndpointClient.UpdateServiceEndpointAzureRmAsync(Id, ServiceEndpoint).Result);
    }

    #endregion
}