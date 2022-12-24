namespace Powershell.AzureDevOps.Cmdlets.Core;

using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;

[Cmdlet(VerbsCommon.Get, "AzDoProject")]
[OutputType(typeof(TeamProject))]
public class GetAzDoProjectCmdlet : CmdletBase
{
    #region Parameters

    [Parameter(Mandatory = true)]
    public string ProjectId { get; set; }

    #endregion

    #region Cmdlet Methods

    protected override void ProcessRecord()
    {
        WriteObject(ProjectClient.GetProjectAsync(ProjectId).Result);
    }

    #endregion
}