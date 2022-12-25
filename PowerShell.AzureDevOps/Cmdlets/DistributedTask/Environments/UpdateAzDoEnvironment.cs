namespace PowerShell.AzureDevOps.Cmdlets.DistributedTask;

using System.Management.Automation;
using Microsoft.TeamFoundation.DistributedTask.WebApi;

[Cmdlet(VerbsData.Update, "AzDoEnvironment")]
[OutputType(typeof(EnvironmentInstance))]
public class UpdateAzDoEnvironment : CmdletBase
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
        WriteObject(DistributedTaskClient.UpdateEnvironmentAsync(Id, CurrentName, Name, Description, ProjectId).Result);
    }

    #endregion
}