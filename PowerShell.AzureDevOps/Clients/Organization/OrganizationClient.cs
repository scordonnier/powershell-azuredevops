namespace PowerShell.AzureDevOps.Clients.Organization;

using Microsoft.VisualStudio.Services.Organization.Client;
using Microsoft.VisualStudio.Services.WebApi;

internal class OrganizationClient
{
    #region Private Variables

    private readonly OrganizationHttpClient client;

    #endregion

    #region Initialization / Memory Management

    public OrganizationClient(VssConnection connection)
    {
        client = connection.GetClient<OrganizationHttpClient>();
    }

    #endregion

    #region Public Methods

    public Task<Organization> GetOrganizationAsync(CancellationToken cancellationToken = default)
    {
        return client.GetOrganizationAsync("Me", cancellationToken: cancellationToken);
    }

    #endregion
}