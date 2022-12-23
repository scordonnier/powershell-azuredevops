namespace Powershell.AzureDevOps.Cmdlets;

using System.Management.Automation;

[Cmdlet(VerbsLifecycle.Invoke,"AzDoConfigure")]
public class InvokeAzDoConfigureCmdlet : CmdletBase
{
    #region Parameters
    
    [Parameter(Mandatory = true)]
    public string Organization { get; set; }

    [Parameter(Mandatory = true)]
    public string PersonalAccessToken { get; set; }

    #endregion

    #region Cmdlet Methods

    protected override void ProcessRecord()
    {
        Configure(Organization, PersonalAccessToken);
    }

    #endregion
}