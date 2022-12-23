namespace Powershell.AzureDevOps.Cmdlets.Core;

using System.Management.Automation;
using Microsoft.VisualStudio.Services.Organization.Client;

[Cmdlet(VerbsCommon.Get,"AzDoOrganization")]
[OutputType(typeof(Organization))]
public class GetAzDoOrganizationCmdlet : CmdletBase
{
    #region Cmdlet Methods

    protected override void ProcessRecord()
    {
        using var client = OrganizationClient;
        var organization = client.GetOrganizationAsync("Me").Result;
        WriteObject(organization);
    }

    #endregion
}