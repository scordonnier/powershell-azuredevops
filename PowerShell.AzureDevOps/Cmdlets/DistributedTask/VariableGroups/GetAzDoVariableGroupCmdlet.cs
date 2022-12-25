namespace PowerShell.AzureDevOps.Cmdlets.DistributedTask;

using System.Management.Automation;
using Microsoft.TeamFoundation.DistributedTask.WebApi;

[Cmdlet(VerbsCommon.Get, "AzDoVariableGroup")]
[OutputType(typeof(VariableGroup))]
public class GetAzDoVariableGroupCmdlet : CmdletBase
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
        WriteObject(DistributedTaskClient.GetVariableGroupAsync(Id, Name, ProjectId).Result);
    }

    #endregion
}