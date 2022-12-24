namespace Powershell.AzureDevOps.Cmdlets.Core;

using System.Management.Automation;

[Cmdlet(VerbsLifecycle.Invoke, "AzDoShareServiceEndpoint")]
public class ShareAzDoServiceEndpointCmdlet : CmdletBase
{
    #region Parameters

    [Parameter(Mandatory = true)]
    public Guid Id { get; set; }

    [Parameter(Mandatory = true)]
    public string SourceProjectId { get; set; }

    [Parameter(Mandatory = true)]
    public string TargetProjectId { get; set; }

    #endregion

    #region Cmdlet Methods

    protected override void ProcessRecord()
    {
        ServiceEndpointClient.ShareServiceEndpointAsync(Id, SourceProjectId, TargetProjectId).Wait();
    }

    #endregion
}