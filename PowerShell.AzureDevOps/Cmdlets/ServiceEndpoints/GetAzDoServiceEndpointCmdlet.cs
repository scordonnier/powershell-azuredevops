namespace PowerShell.AzureDevOps.Cmdlets.Core;

using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceEndpoints.WebApi;

[Cmdlet(VerbsCommon.Get, "AzDoServiceEndpoint")]
[OutputType(typeof(ServiceEndpoint))]
public class GetAzDoServiceEndpointCmdlet : CmdletBase
{
    #region Parameters

    [Parameter]
    public Guid Id { get; set; }

    [Parameter]
    public string Name { get; set; }

    [Parameter(Mandatory = true)]
    public Guid ProjectId { get; set; }

    #endregion

    #region Cmdlet Methods

    protected override void ProcessRecord()
    {
        WriteObject(ServiceEndpointClient.GetServiceEndpointAsync(Id, Name, ProjectId).Result);
    }

    #endregion
}