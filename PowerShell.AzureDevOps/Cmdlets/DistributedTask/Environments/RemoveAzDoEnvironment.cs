namespace PowerShell.AzureDevOps.Cmdlets.DistributedTask;

using System.Management.Automation;

[Cmdlet(VerbsCommon.Remove, "AzDoEnvironment")]
public class RemoveAzDoEnvironment : CmdletBase
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
        DistributedTaskClient.DeleteEnvironmentAsync(Id, Name, ProjectId).Wait();
    }

    #endregion
}