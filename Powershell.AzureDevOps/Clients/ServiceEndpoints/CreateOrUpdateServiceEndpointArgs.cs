namespace Powershell.AzureDevOps.Clients.ServiceEndpoints;

public class CreateOrUpdateServiceEndpointArgs
{
    public string Description { get; set; }

    public string Name { get; set; }

    public Guid ProjectId { get; set; }
}

public class CreateOrUpdateServiceEndpointAzureRmArgs : CreateOrUpdateServiceEndpointArgs
{
    public string ServicePrincipalId { get; set; }

    public string ServicePrincipalKey { get; set; }

    public string SubscriptionId { get; set; }

    public string SubscriptionName { get; set; }

    public string TenantId { get; set; }
}

public class CreateOrUpdateServiceEndpointBitbucketArgs : CreateOrUpdateServiceEndpointArgs
{
    public string Password { get; set; }

    public string UserName { get; set; }
}