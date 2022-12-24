namespace Powershell.AzureDevOps.Clients.ServiceEndpoints;

using System.Collections.ObjectModel;
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

    public Task<ServiceEndpoint> CreateServiceEndpointAzureRmAsync(CreateServiceEndpointAzureRmArgs args, CancellationToken cancellationToken = default)
    {
        var serviceEndpoint = BuildServiceEndpoint(args);
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
        return client.CreateServiceEndpointAsync(serviceEndpoint, cancellationToken: cancellationToken);
    }

    public Task<ServiceEndpoint> CreateServiceEndpointBitbucketAsync(CreateServiceEndpointBitbucketArgs args, CancellationToken cancellationToken = default)
    {
        var serviceEndpoint = BuildServiceEndpoint(args);
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
        return client.CreateServiceEndpointAsync(serviceEndpoint, cancellationToken: cancellationToken);
    }

    public Task<List<ServiceEndpoint>> GetServiceEndpointsAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        return client.GetServiceEndpointsAsync(projectId, cancellationToken: cancellationToken);
    }

    #endregion

    #region Private Methods

    private static ServiceEndpoint BuildServiceEndpoint(CreateServiceEndpointArgs args)
    {
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

    #endregion
}