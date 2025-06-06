using Tutorial12.Models;

namespace Tutorial12.Repositories;

public interface ITrip
{
    Task<List<TripModel>> GetTripsAsync(int page, int pageSize);
    Task<int> GetTotalTripsCountAsync();
    Task<TripModel?> GetTripByIdAsync(int id);
    Task<bool> CheckIfTripInFuture(int tripId);
    Task RegisterClientToTripAsync(Client_TripModel registration);

}