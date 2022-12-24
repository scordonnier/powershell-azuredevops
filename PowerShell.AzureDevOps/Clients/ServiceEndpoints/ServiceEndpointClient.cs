namespace PowerShell.AzureDevOps.Clients.ServiceEndpoints;

using System.Collections.ObjectModel;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.ServiceEndpoints;
using Microsoft.VisualStudio.Services.ServiceEndpoints.WebApi;

internal class ServiceEndpointClient
{
    #region Private Variables

    private readonly ServiceEndpointHttpClient client;

    #endregion

    #region Initialization / Memory Management

    public ServiceEndpointClient(ServiceEndpointHttpClient client)
    {
        this.client = client;
    }

    #endregion

    #region Public Methods

    public Task<ServiceEndpoint> CreateServiceEndpointAzureRmAsync(CreateOrUpdateServiceEndpointAzureRmArgs args, CancellationToken cancellationToken = default)
    {
        var serviceEndpoint = BuildOrUpdateServiceEndpointAzureRm(null, args);
        return client.CreateServiceEndpointAsync(serviceEndpoint, cancellationToken: cancellationToken);
    }

    public Task<ServiceEndpoint> CreateServiceEndpointBitbucketAsync(CreateOrUpdateServiceEndpointBitbucketArgs args, CancellationToken cancellationToken = default)
    {
        var serviceEndpoint = BuildOrUpdateServiceEndpointBitbucket(null, args);
        return client.CreateServiceEndpointAsync(serviceEndpoint, cancellationToken: cancellationToken);
    }

    public async Task<ServiceEndpoint> GetServiceEndpointAsync(Guid id, string name, Guid projectId, CancellationToken cancellationToken = default)
    {
        if ((string.IsNullOrWhiteSpace(name) && id == Guid.Empty) || (!string.IsNullOrWhiteSpace(name) && id != Guid.Empty))
        {
            throw new ArgumentException("You must provide either 'Id' or 'Name' parameters, but not both together.");
        }

        if (id != Guid.Empty)
        {
            var serviceEndpointDetails = await client.GetServiceEndpointDetailsAsync(projectId, id, cancellationToken: cancellationToken);
            return serviceEndpointDetails ?? throw new Exception($"Endpoint '{id}' not found in project '{projectId}'");
        }

        var serviceEndpointsByNames = await client.GetServiceEndpointsByNamesAsync(projectId, new List<string> { name }, cancellationToken: cancellationToken);
        return serviceEndpointsByNames.FirstOrDefault() ?? throw new Exception($"Endpoint '{name}' not found in project '{projectId}'");
    }

    public Task<List<ServiceEndpoint>> GetServiceEndpointsAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return client.GetServiceEndpointsAsync(projectId, cancellationToken: cancellationToken);
    }

    public Task RemoveServiceEndpointAsync(Guid id, IEnumerable<string> projectIds, CancellationToken cancellationToken = default)
    {
        return client.DeleteServiceEndpointAsync(id, projectIds, cancellationToken: cancellationToken);
    }

    public Task ShareServiceEndpointAsync(Guid id, string sourceProjectId, string targetProjectId, CancellationToken cancellationToken = default)
    {
        return client.ShareEndpointWithProjectAsync(id, sourceProjectId, targetProjectId, cancellationToken: cancellationToken);
    }

    public async Task<ServiceEndpoint> UpdateServiceEndpointAzureRmAsync(Guid id, CreateOrUpdateServiceEndpointAzureRmArgs args, CancellationToken cancellationToken = default)
    {
        var serviceEndpoint = await GetServiceEndpointAsync(id, null, args.ProjectId, cancellationToken);
        serviceEndpoint = BuildOrUpdateServiceEndpointAzureRm(serviceEndpoint, args);
        return await client.UpdateServiceEndpointAsync(id, serviceEndpoint, cancellationToken: cancellationToken);
    }

    public async Task<ServiceEndpoint> UpdateServiceEndpointBitbucketAsync(Guid id, CreateOrUpdateServiceEndpointBitbucketArgs args, CancellationToken cancellationToken = default)
    {
        var serviceEndpoint = await GetServiceEndpointAsync(id, null, args.ProjectId, cancellationToken);
        serviceEndpoint = BuildOrUpdateServiceEndpointBitbucket(serviceEndpoint, args);
        return await client.UpdateServiceEndpointAsync(id, serviceEndpoint, cancellationToken: cancellationToken);
    }

    #endregion

    #region Private Methods

    private static ServiceEndpoint BuildOrUpdateServiceEndpoint(ServiceEndpoint currentServiceEndpoint, CreateOrUpdateServiceEndpointArgs args)
    {
        if (currentServiceEndpoint != null)
        {
            currentServiceEndpoint.Description = args.Description;
            currentServiceEndpoint.Name = args.Name;
            currentServiceEndpoint.ServiceEndpointProjectReferences.ForEach(p =>
            {
                p.Description = args.Description;
                p.Name = args.Name;
            });
            return currentServiceEndpoint;
        }

        return new ServiceEndpoint
        {
            Description = args.Description,
            Name = args.Name,
            Owner = ServiceEndpointOwner.Library,
            ServiceEndpointProjectReferences = new Collection<ServiceEndpointProjectReference>
            {
                new()
                {
                    Description = args.Description,
                    Name = args.Name,
                    ProjectReference = new ProjectReference
                    {
                        Id = args.ProjectId
                    }
                }
            }
        };
    }

    private static ServiceEndpoint BuildOrUpdateServiceEndpointAzureRm(ServiceEndpoint currentServiceEndpoint, CreateOrUpdateServiceEndpointAzureRmArgs args)
    {
        var serviceEndpoint = BuildOrUpdateServiceEndpoint(currentServiceEndpoint, args);
        serviceEndpoint.Authorization = new EndpointAuthorization
        {
            Parameters = new Dictionary<string, string>
            {
                { "authenticationType", "spnKey" },
                { "serviceprincipalid", args.ServicePrincipalId },
                { "serviceprincipalkey", args.ServicePrincipalKey },
                { "tenantid", args.TenantId }
            },
            Scheme = EndpointAuthorizationSchemes.ServicePrincipal
        };
        serviceEndpoint.Data = new Dictionary<string, string>
        {
            { "creationMode", "Manual" },
            { "environment", "AzureCloud" },
            { "scopeLevel", "Subscription" },
            { "subscriptionId", args.SubscriptionId },
            { "subscriptionName", args.SubscriptionName }
        };
        serviceEndpoint.Type = ServiceEndpointTypes.AzureRM;
        serviceEndpoint.Url = new Uri("https://management.azure.com/");
        return serviceEndpoint;
    }

    private static ServiceEndpoint BuildOrUpdateServiceEndpointBitbucket(ServiceEndpoint currentServiceEndpoint, CreateOrUpdateServiceEndpointBitbucketArgs args)
    {
        var serviceEndpoint = BuildOrUpdateServiceEndpoint(currentServiceEndpoint, args);
        serviceEndpoint.Authorization = new EndpointAuthorization
        {
            Parameters = new Dictionary<string, string>
            {
                { "username", args.UserName },
                { "password", args.Password }
            },
            Scheme = EndpointAuthorizationSchemes.UsernamePassword
        };
        serviceEndpoint.Type = ServiceEndpointTypes.Bitbucket;
        serviceEndpoint.Url = new Uri("https://api.bitbucket.org/");
        return serviceEndpoint;
    }

    #endregion
}