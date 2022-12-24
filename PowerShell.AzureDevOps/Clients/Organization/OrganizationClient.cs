namespace PowerShell.AzureDevOps.Clients.Organization;

using Microsoft.VisualStudio.Services.Organization.Client;

internal class OrganizationClient
{
    #region Private Variables

    private readonly OrganizationHttpClient client;

    #endregion

    #region Initialization / Memory Management

    public OrganizationClient(OrganizationHttpClient client)
    {
        this.client = client;
    }

    #endregion

    #region Public Methods

    public Task<Organization> GetOrganizationAsync(CancellationToken cancellationToken = default)
    {
        return client.GetOrganizationAsync("Me", cancellationToken: cancellationToken);
    }

    #endregion
}