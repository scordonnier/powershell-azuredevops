namespace PowerShell.AzureDevOps.Clients;

internal abstract class AzDoClientBase
{
    #region Protected Methods

    protected static void EnsureIdOrName(int id, string name)
    {
        if ((string.IsNullOrWhiteSpace(name) && id == 0) || (!string.IsNullOrWhiteSpace(name) && id != 0))
        {
            throw new ArgumentException("You must provide either 'Id' or 'Name' parameters, but not both together.");
        }
    }

    #endregion
}