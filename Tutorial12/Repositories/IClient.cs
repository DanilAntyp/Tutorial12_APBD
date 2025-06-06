using Tutorial12.Models;

namespace Tutorial12.Repositories;

public interface IClient
{
    Task<ClientModel?> GetClientByIdAsync(int id);
    Task AddClientAsync(ClientModel client);
    Task DeleteClientAsync(ClientModel client);

    Task<bool> CheckTripsAsync(int clientId);
    Task<bool> ExistsByPeselAsync(string pesel);
    Task<bool> IsRegisteredToTripAsync(string pesel, int tripId);
    
    Task SaveChangesAsync();
}