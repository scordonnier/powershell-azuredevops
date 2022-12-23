namespace Powershell.AzureDevOps;

using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.Organization.Client;
using Microsoft.VisualStudio.Services.ServiceEndpoints.WebApi;
using Microsoft.VisualStudio.Services.WebApi;

public class CmdletBase : PSCmdlet
{
    #region Private Properties

    private VssConnection VssConnection => SessionState.PSVariable.GetValue(VariableNames.VssConnection) as VssConnection;

    #endregion

    #region Protected Properties

    protected OrganizationHttpClient OrganizationClient => VssConnection.GetClient<OrganizationHttpClient>();

    protected ProjectHttpClient ProjectClient => VssConnection.GetClient<ProjectHttpClient>();

    protected ServiceEndpointHttpClient ServiceEndpointClient => VssConnection.GetClient<ServiceEndpointHttpClient>();

    #endregion

    #region Protected Methods

    protected void Configure(string organization, string personalAccessToken)
    {
        SessionState.PSVariable.Set(VariableNames.Organization, organization);
        SessionState.PSVariable.Set(VariableNames.PersonalAccessToken, personalAccessToken);
        var connection = new VssConnection(new Uri($"https://dev.azure.com/{organization}/"), new VssBasicCredential(string.Empty, personalAccessToken));
        SessionState.PSVariable.Set(VariableNames.VssConnection, connection);
    }

    #endregion
}