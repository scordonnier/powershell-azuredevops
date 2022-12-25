namespace PowerShell.AzureDevOps.Cmdlets.Core;

using System.Management.Automation;

[Cmdlet(VerbsData.Update, "AzDoEnvironment")]
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