namespace PowerShell.AzureDevOps.Cmdlets.DistributedTask;

using System.Management.Automation;
using Microsoft.TeamFoundation.DistributedTask.WebApi;

[Cmdlet(VerbsCommon.New, "AzDoEnvironment")]
[OutputType(typeof(EnvironmentInstance))]
public class NewAzDoEnvironmentCmdlet : CmdletBase
{
    #region Parameters

    [Parameter]
    public string Description { get; set; }

    [Parameter(Mandatory = true)]
    public string Name { get; set; }

    [Parameter(Mandatory = true)]
    public string ProjectId { get; set; }

    #endregion

    #region Cmdlet Methods

    protected override void ProcessRecord()
    {
        WriteObject(DistributedTaskClient.CreateEnvironmentAsync(Name, Description, ProjectId).Result);
    }

    #endregion
}