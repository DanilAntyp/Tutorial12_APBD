using Tutorial12.DTO;

namespace Tutorial12.Services;

public interface ITripServise
{
    Task<object> GetTripsAsync(int page, int pageSize);
    Task<bool> DeleteClientAsync(int idClient);
    Task<string> AssignClientToTripAsync(int idTrip, AddClientDTO dto);

}