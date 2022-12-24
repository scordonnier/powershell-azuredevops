namespace Powershell.AzureDevOps.Cmdlets.Core;

using System.Management.Automation;

[Cmdlet(VerbsCommon.Remove, "AzDoServiceEndpoint")]
public class RemoveAzDoServiceEndpointCmdlet : CmdletBase
{
    #region Parameters

    [Parameter(Mandatory = true)]
    public Guid EndpointId { get; set; }

    [Parameter(Mandatory = true)]
    public List<string> ProjectIds { get; set; }

    #endregion

    #region Cmdlet Methods

    protected override void ProcessRecord()
    {
        ServiceEndpointClient.RemoveServiceEndpointAsync(EndpointId, ProjectIds).Wait();
    }

    #endregion
}