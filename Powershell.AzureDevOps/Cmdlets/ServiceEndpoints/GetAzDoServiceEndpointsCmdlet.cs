namespace Powershell.AzureDevOps.Cmdlets.Core;

using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceEndpoints.WebApi;

[Cmdlet(VerbsCommon.Get,"AzDoServiceEndpoints")]
[OutputType(typeof(List<ServiceEndpoint>))]
public class GetAzDoServiceEndpointsCmdlet : CmdletBase
{
    #region Parameters

    [Parameter(Mandatory = true)]
    public Guid ProjectId { get; set; }

    #endregion

    #region Cmdlet Methods

    protected override void ProcessRecord()
    {
        WriteObject(ServiceEndpointClient.GetServiceEndpointsAsync(ProjectId).Result);
    }

    #endregion
}