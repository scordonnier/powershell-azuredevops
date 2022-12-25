namespace PowerShell.AzureDevOps.Clients.DistributedTask;

using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.DistributedTask.WebApi;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;

internal class DistributedTaskClient : AzDoClientBase
{
    #region Private Variables

    private readonly ProjectHttpClient projectClient;
    private readonly TaskAgentHttpClient taskAgentClient;

    #endregion

    #region Initialization / Memory Management

    public DistributedTaskClient(VssConnection connection)
    {
        projectClient = connection.GetClient<ProjectHttpClient>();
        taskAgentClient = connection.GetClient<TaskAgentHttpClient>();
    }

    #endregion

    #region Public Methods

    #region Environments

    public Task<EnvironmentInstance> CreateEnvironmentAsync(string name, string description, string projectId, CancellationToken cancellationToken = default)
    {
        var environment = new EnvironmentCreateParameter { Description = description, Name = name };
        return taskAgentClient.AddEnvironmentAsync(projectId, environment, null, cancellationToken);
    }

    public async Task DeleteEnvironmentAsync(int id, string name, string projectId, CancellationToken cancellationToken = default)
    {
        EnsureIdOrName(id, name);

        if (id != 0)
        {
            await taskAgentClient.DeleteEnvironmentAsync(projectId, id, null, cancellationToken);
            return;
        }

        var environment = await GetEnvironmentAsync(id, name, projectId, cancellationToken);
        await taskAgentClient.DeleteEnvironmentAsync(projectId, environment.Id, null, cancellationToken);
    }

    public async Task<EnvironmentInstance> GetEnvironmentAsync(int id, string name, string projectId, CancellationToken cancellationToken = default)
    {
        EnsureIdOrName(id, name);

        if (id != 0)
        {
            var environment = await taskAgentClient.GetEnvironmentByIdAsync(projectId, id, cancellationToken: cancellationToken);
            return environment ?? throw new Exception($"Environment '{id}' not found in project '{projectId}'");
        }

        var environments = await taskAgentClient.GetEnvironmentsAsync(projectId, name, cancellationToken: cancellationToken);
        return environments.FirstOrDefault() ?? throw new Exception($"Environment '{name}' not found in project '{projectId}'");
    }

    public async Task<EnvironmentInstance> UpdateEnvironmentAsync(int id, string currentName, string name, string description, string projectId, CancellationToken cancellationToken = default)
    {
        EnsureIdOrName(id, currentName);

        var environmentUpdate = new EnvironmentUpdateParameter { Description = description, Name = name };
        if (id != 0)
        {
            return await taskAgentClient.UpdateEnvironmentAsync(projectId, id, environmentUpdate, null, cancellationToken);
        }

        var environment = await GetEnvironmentAsync(id, currentName, projectId, cancellationToken);
        return await taskAgentClient.UpdateEnvironmentAsync(projectId, environment.Id, environmentUpdate, null, cancellationToken);
    }

    #endregion

    #region Variable Groups

    public Task<VariableGroup> CreateVariableGroupAsync(string name, string description, string projectId, CancellationToken cancellationToken = default)
    {
        var variableGroup = BuildOrUpdateVariableGroupParameters(null, name, description, projectId);
        return taskAgentClient.AddVariableGroupAsync(variableGroup, null, cancellationToken);
    }

    public async Task DeleteVariableGroupAsync(int id, string name, string projectId, CancellationToken cancellationToken = default)
    {
        EnsureIdOrName(id, name);

        if (!Guid.TryParse(projectId, out var guidProjectId))
        {
            guidProjectId = (await projectClient.GetProject(projectId)).Id;
        }

        if (id != 0)
        {
            await taskAgentClient.DeleteVariableGroupAsync(id, new[] { guidProjectId.ToString() }, null, cancellationToken);
            return;
        }

        var variableGroup = await GetVariableGroupAsync(id, name, projectId, cancellationToken);
        await taskAgentClient.DeleteVariableGroupAsync(variableGroup.Id, new[] { guidProjectId.ToString() }, null, cancellationToken);
    }

    public async Task<VariableGroup> GetVariableGroupAsync(int id, string name, string projectId, CancellationToken cancellationToken = default)
    {
        EnsureIdOrName(id, name);

        if (id != 0)
        {
            var variableGroup = await taskAgentClient.GetVariableGroupAsync(projectId, id, null, cancellationToken);
            return variableGroup ?? throw new Exception($"Variable group '{id}' not found in project '{projectId}'");
        }

        var variableGroups = await taskAgentClient.GetVariableGroupsAsync(projectId, name, cancellationToken: cancellationToken);
        return variableGroups.FirstOrDefault() ?? throw new Exception($"Variable group '{name}' not found in project '{projectId}'");
    }

    public async Task<VariableGroup> UpdateVariableGroupAsync(int id, string currentName, string name, string description, string projectId, CancellationToken cancellationToken = default)
    {
        EnsureIdOrName(id, currentName);

        var variableGroup = await GetVariableGroupAsync(id, currentName, projectId, cancellationToken);
        var variableGroupUpdate = BuildOrUpdateVariableGroupParameters(variableGroup, name, description, projectId);
        return await taskAgentClient.UpdateVariableGroupAsync(variableGroup.Id, variableGroupUpdate, null, cancellationToken);
    }

    #endregion

    #endregion

    #region Private Methods

    private static VariableGroupParameters BuildOrUpdateVariableGroupParameters(VariableGroup variableGroup, string name, string description, string projectId)
    {
        var variableGroupProjectReferences = variableGroup?.VariableGroupProjectReferences;
        variableGroupProjectReferences?.ForEach(v =>
        {
            v.Description = description;
            v.Name = name;
        });
        return new VariableGroupParameters
        {
            Description = description,
            Name = name,
            VariableGroupProjectReferences = variableGroupProjectReferences ?? new List<VariableGroupProjectReference>
            {
                new()
                {
                    Description = description,
                    Name = name,
                    ProjectReference = BuildProjectReference(projectId)
                }
            },
            Variables = variableGroup?.Variables ?? new Dictionary<string, VariableValue>
            {
                { "Sample Variable", new VariableValue { Value = string.Empty } }
            }
        };
    }

    private static ProjectReference BuildProjectReference(string projectId)
    {
        return Guid.TryParse(projectId, out var guid) ?
            new ProjectReference { Id = guid } :
            new ProjectReference { Name = projectId };
    }

    #endregion
}