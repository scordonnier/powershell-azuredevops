namespace PowerShell.AzureDevOps.Cmdlets.DistributedTask;

using System.Management.Automation;
using Microsoft.TeamFoundation.DistributedTask.WebApi;

[Cmdlet(VerbsData.Update, "AzDoVariableGroup")]
[OutputType(typeof(VariableGroup))]
public class UpdateAzDoVariableGroup : CmdletBase
{
    #region Parameters

    [Parameter]
    public string CurrentName { get; set; }

    [Parameter]
    public string Description { get; set; }

    [Parameter]
    public int Id { get; set; }

    [Parameter(Mandatory = true)]
    public string Name { get; set; }

    [Parameter(Mandatory = true)]
    public string ProjectId { get; set; }

    #endregion

    #region Cmdlet Methods

    protected override void ProcessRecord()
    {
        WriteObject(DistributedTaskClient.UpdateVariableGroupAsync(Id, CurrentName, Name, Description, ProjectId).Result);
    }

    #endregion
}