namespace PowerShell.AzureDevOps.Cmdlets.Core;

using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceEndpoints.WebApi;
using PowerShell.AzureDevOps.Clients.ServiceEndpoints;

[Cmdlet(VerbsCommon.New, "AzDoServiceEndpointAzureRm")]
[OutputType(typeof(ServiceEndpoint))]
public class NewAzDoServiceEndpointAzureRmCmdlet : CmdletBase
{
    #region Parameters

    [Parameter(Mandatory = true)]
    public CreateOrUpdateServiceEndpointAzureRmArgs ServiceEndpoint { get; set; }

    #endregion

    #region Cmdlet Methods

    protected override void ProcessRecord()
    {
        WriteObject(ServiceEndpointClient.CreateServiceEndpointAzureRmAsync(ServiceEndpoint).Result);
    }

    #endregion
}