namespace PowerShell.AzureDevOps.Cmdlets.DistributedTask;

using System.Management.Automation;

[Cmdlet(VerbsCommon.Remove, "AzDoVariableGroup")]
public class RemoveAzDoVariableGroupCmdlet : CmdletBase
{
    #region Parameters

    [Parameter]
    public int Id { get; set; }

    [Parameter]
    public string Name { get; set; }

    [Parameter(Mandatory = true)]
    public string ProjectId { get; set; }

    #endregion

    #region Cmdlet Methods

    protected override void ProcessRecord()
    {
        DistributedTaskClient.DeleteVariableGroupAsync(Id, Name, ProjectId).Wait();
    }

    #endregion
}