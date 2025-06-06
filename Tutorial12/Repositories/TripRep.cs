using Microsoft.EntityFrameworkCore;
using Tutorial12.Data;
using Tutorial12.Models;

namespace Tutorial12.Repositories;

public class TripRep : ITrip
{
    private readonly MasterContext _context;

    public TripRep(MasterContext context)
    {
        _context = context;
    }
    
    public async Task<TripModel?> GetTripByIdAsync(int id)
        => await _context.Trip.FindAsync(id);

    public async Task<List<TripModel>> GetTripsAsync(int page, int pageSize)
        => await _context.Trip
            .Include(t => t.Client_Trips).ThenInclude(ct => ct.IdClientNavigation)
            .Include(t => t.IdCountries)
            .OrderByDescending(t => t.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task<int> GetTotalTripsCountAsync()
        => await _context.Trip.CountAsync();

    

    public async Task<bool> CheckIfTripInFuture(int tripId)
    {
        var trip = await _context.Trip.FindAsync(tripId);
        return trip != null && trip.DateFrom > DateTime.UtcNow;
    }

    public async Task RegisterClientToTripAsync(Client_TripModel registration)
    {
        await _context.ClientTrip.AddAsync(registration);
    }
}