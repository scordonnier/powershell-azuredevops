namespace PowerShell.AzureDevOps;

using System.Management.Automation;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using PowerShell.AzureDevOps.Clients.DistributedTask;
using PowerShell.AzureDevOps.Clients.Organization;
using PowerShell.AzureDevOps.Clients.Projects;
using PowerShell.AzureDevOps.Clients.ServiceEndpoints;

public abstract class CmdletBase : PSCmdlet
{
    #region Internal Properties

    internal DistributedTaskClient DistributedTaskClient => SessionState.PSVariable.GetValue(VariableNames.DistributedTaskClient) as DistributedTaskClient;

    internal OrganizationClient OrganizationClient => SessionState.PSVariable.GetValue(VariableNames.OrganizationClient) as OrganizationClient;

    internal ProjectClient ProjectClient => SessionState.PSVariable.GetValue(VariableNames.ProjectClient) as ProjectClient;

    internal ServiceEndpointClient ServiceEndpointClient => SessionState.PSVariable.GetValue(VariableNames.ServiceEndpointClient) as ServiceEndpointClient;

    #endregion

    #region Protected Methods

    protected void Configure(string organization, string personalAccessToken)
    {
        var vssConnection = new VssConnection(new Uri($"https://dev.azure.com/{organization}/"), new VssBasicCredential(string.Empty, personalAccessToken));
        SessionState.PSVariable.Set(VariableNames.DistributedTaskClient, new DistributedTaskClient(vssConnection));
        SessionState.PSVariable.Set(VariableNames.OrganizationClient, new OrganizationClient(vssConnection));
        SessionState.PSVariable.Set(VariableNames.ProjectClient, new ProjectClient(vssConnection));
        SessionState.PSVariable.Set(VariableNames.ServiceEndpointClient, new ServiceEndpointClient(vssConnection));
    }

    #endregion
}