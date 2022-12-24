namespace Powershell.AzureDevOps.Cmdlets.Core;

using System.Management.Automation;
using Microsoft.VisualStudio.Services.ServiceEndpoints.WebApi;
using Powershell.AzureDevOps.Clients.ServiceEndpoints;

[Cmdlet(VerbsCommon.New, "AzDoServiceEndpointBitbucket")]
[OutputType(typeof(ServiceEndpoint))]
public class NewAzDoServiceEndpointBitbucketCmdlet : CmdletBase
{
    #region Parameters

    [Parameter(Mandatory = true)]
    public CreateOrUpdateServiceEndpointBitbucketArgs ServiceEndpoint { get; set; }

    #endregion

    #region Cmdlet Methods

    protected override void ProcessRecord()
    {
        WriteObject(ServiceEndpointClient.CreateServiceEndpointBitbucketAsync(ServiceEndpoint).Result);
    }

    #endregion
}