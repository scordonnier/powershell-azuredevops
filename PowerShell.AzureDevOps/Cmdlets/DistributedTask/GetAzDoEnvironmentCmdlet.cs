namespace PowerShell.AzureDevOps.Cmdlets.Core;

using System.Management.Automation;
using Microsoft.TeamFoundation.DistributedTask.WebApi;

[Cmdlet(VerbsCommon.Get, "AzDoEnvironment")]
[OutputType(typeof(EnvironmentInstance))]
public class GetAzDoEnvironmentCmdlet : CmdletBase
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
        WriteObject(DistributedTaskClient.GetEnvironmentAsync(Id, Name, ProjectId).Result);
    }

    #endregion
}