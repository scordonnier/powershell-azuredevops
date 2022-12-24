namespace PowerShell.AzureDevOps;

using System.Management.Automation;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.Organization.Client;
using Microsoft.VisualStudio.Services.ServiceEndpoints.WebApi;
using Microsoft.VisualStudio.Services.WebApi;
using PowerShell.AzureDevOps.Clients.Organization;
using PowerShell.AzureDevOps.Clients.Projects;
using PowerShell.AzureDevOps.Clients.ServiceEndpoints;

public class CmdletBase : PSCmdlet
{
    #region Internal Properties

    internal OrganizationClient OrganizationClient => SessionState.PSVariable.GetValue(VariableNames.OrganizationClient) as OrganizationClient;

    internal ProjectClient ProjectClient => SessionState.PSVariable.GetValue(VariableNames.ProjectClient) as ProjectClient;

    internal ServiceEndpointClient ServiceEndpointClient => SessionState.PSVariable.GetValue(VariableNames.ServiceEndpointClient) as ServiceEndpointClient;

    #endregion

    #region Protected Methods

    protected void Configure(string organization, string personalAccessToken)
    {
        var vssConnection = new VssConnection(new Uri($"https://dev.azure.com/{organization}/"), new VssBasicCredential(string.Empty, personalAccessToken));
        SessionState.PSVariable.Set(VariableNames.OrganizationClient, new OrganizationClient(vssConnection.GetClient<OrganizationHttpClient>()));
        SessionState.PSVariable.Set(VariableNames.ProjectClient, new ProjectClient(vssConnection.GetClient<ProjectHttpClient>()));
        SessionState.PSVariable.Set(VariableNames.ServiceEndpointClient, new ServiceEndpointClient(vssConnection.GetClient<ServiceEndpointHttpClient>()));
    }

    #endregion
}