namespace Powershell.AzureDevOps.Clients.Projects;

using Microsoft.TeamFoundation.Core.WebApi;

internal class ProjectClient
{
    #region Private Variables

    private readonly ProjectHttpClient client;

    #endregion

    #region Initialization / Memory Management

    public ProjectClient(ProjectHttpClient client)
    {
        this.client = client;
    }

    #endregion

    #region Public Methods

    public Task<TeamProject> GetProjectAsync(string projectId, CancellationToken cancellationToken = default)
    {
        return client.GetProject(projectId);
    }

    #endregion
}