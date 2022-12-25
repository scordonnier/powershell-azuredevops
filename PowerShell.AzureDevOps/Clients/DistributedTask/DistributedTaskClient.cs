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

    #region Public Methods

    #region Environments

    public Task<EnvironmentInstance> CreateEnvironmentAsync(string name, string description, string projectId, CancellationToken cancellationToken = default)
    {
        var environment = new EnvironmentCreateParameter { Description = description, Name = name };
        return client.AddEnvironmentAsync(projectId, environment, null, cancellationToken);
    }

    public async Task DeleteEnvironmentAsync(int id, string name, string projectId, CancellationToken cancellationToken = default)
    {
        if ((string.IsNullOrWhiteSpace(name) && id == 0) || (!string.IsNullOrWhiteSpace(name) && id != 0))
        {
            throw new ArgumentException("You must provide either 'Id' or 'Name' parameters, but not both together.");
        }

        if (id != 0)
        {
            await client.DeleteEnvironmentAsync(projectId, id, null, cancellationToken);
            return;
        }

        var environment = await GetEnvironmentAsync(id, name, projectId, cancellationToken);
        await client.DeleteEnvironmentAsync(projectId, environment.Id, null, cancellationToken);
    }

    public async Task<EnvironmentInstance> GetEnvironmentAsync(int id, string name, string projectId, CancellationToken cancellationToken = default)
    {
        if ((string.IsNullOrWhiteSpace(name) && id == 0) || (!string.IsNullOrWhiteSpace(name) && id != 0))
        {
            throw new ArgumentException("You must provide either 'Id' or 'Name' parameters, but not both together.");
        }

        if (id != 0)
        {
            var environment = await client.GetEnvironmentByIdAsync(projectId, id, cancellationToken: cancellationToken);
            return environment ?? throw new Exception($"Environment '{id}' not found in project '{projectId}'");
        }

        var environments = await client.GetEnvironmentsAsync(projectId, name, cancellationToken: cancellationToken);
        return environments.FirstOrDefault() ?? throw new Exception($"Environment '{name}' not found in project '{projectId}'");
    }

    public async Task<EnvironmentInstance> UpdateEnvironmentAsync(int id, string currentName, string name, string description, string projectId, CancellationToken cancellationToken = default)
    {
        if ((string.IsNullOrWhiteSpace(currentName) && id == 0) || (!string.IsNullOrWhiteSpace(currentName) && id != 0))
        {
            throw new ArgumentException("You must provide either 'Id' or 'CurrentName' parameters, but not both together.");
        }

        var environmentUpdate = new EnvironmentUpdateParameter { Description = description, Name = name };
        if (id != 0)
        {
            return await client.UpdateEnvironmentAsync(projectId, id, environmentUpdate, null, cancellationToken);
        }

        var environment = await GetEnvironmentAsync(id, currentName, projectId, cancellationToken);
        return await client.UpdateEnvironmentAsync(projectId, environment.Id, environmentUpdate, null, cancellationToken);
    }

    #endregion

    #endregion
}