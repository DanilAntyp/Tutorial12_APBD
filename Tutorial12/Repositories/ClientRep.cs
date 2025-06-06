using Microsoft.EntityFrameworkCore;
using Tutorial12.Data;
using Tutorial12.Models;

namespace Tutorial12.Repositories;

public class ClientRep : IClient
{
    private readonly MasterContext _context;

    public ClientRep(MasterContext context)
    {
        _context = context;
    }

    public async Task<ClientModel?> GetClientByIdAsync(int id)
        => await _context.Client.FindAsync(id);
    public async Task AddClientAsync(ClientModel client)
    {
        await _context.Client.AddAsync(client);
    }
    public async Task DeleteClientAsync(ClientModel client)
    {
        _context.Client.Remove(client);
    }
    
    public async Task<bool> IsRegisteredToTripAsync(string pesel, int tripId)
        => await _context.ClientTrip
            .AnyAsync(ct => ct.IdTrip == tripId && ct.IdClientNavigation.Pesel == pesel);
    public async Task<bool> CheckTripsAsync(int clientId)
        => await _context.ClientTrip.AnyAsync(ct => ct.IdClient == clientId);
    public async Task<bool> ExistsByPeselAsync(string pesel)
        => await _context.Client.AnyAsync(c => c.Pesel == pesel);
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}