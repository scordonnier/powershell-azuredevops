namespace PowerShell.AzureDevOps.Clients.DistributedTask;

using Microsoft.TeamFoundation.DistributedTask.WebApi;

internal class DistributedTaskClient
{
    #region Private Variables

    private readonly TaskAgentHttpClient client;

    #endregion

    #region Initialization / Memory Management

    public DistributedTaskClient(TaskAgentHttpClient client)
    {
        this.client = client;
    }

    #endregion
}