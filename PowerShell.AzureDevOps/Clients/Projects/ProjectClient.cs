namespace PowerShell.AzureDevOps.Clients.Projects;

using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.WebApi;

internal class ProjectClient
{
    #region Private Variables

    private readonly ProjectHttpClient client;

    #endregion

    #region Initialization / Memory Management

    public ProjectClient(VssConnection connection)
    {
        client = connection.GetClient<ProjectHttpClient>();
    }

    #endregion

    #region Public Methods

    public Task<TeamProject> GetProjectAsync(string projectId, CancellationToken cancellationToken = default)
    {
        return client.GetProject(projectId);
    }

    #endregion
}