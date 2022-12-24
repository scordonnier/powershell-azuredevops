namespace Powershell.AzureDevOps.Clients.ServiceEndpoints;

public class CreateServiceEndpointArgs
{
    public string Description { get; set; }

    public string Name { get; set; }

    public Guid ProjectId { get; set; }
}

public class CreateServiceEndpointAzureRmArgs : CreateServiceEndpointArgs
{
    public string ServicePrincipalId { get; set; }

    public string ServicePrincipalKey { get; set; }

    public string SubscriptionId { get; set; }

    public string SubscriptionName { get; set; }

    public string TenantId { get; set; }
}

public class CreateServiceEndpointBitbucketArgs : CreateServiceEndpointArgs
{
    public string Password { get; set; }

    public string UserName { get; set; }
}