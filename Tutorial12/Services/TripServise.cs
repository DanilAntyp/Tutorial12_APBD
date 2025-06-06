
using Tutorial12.DTO;
using Tutorial12.Models;
using Tutorial12.Repositories;

namespace Tutorial12.Services;

public class TripServise : ITripServise
{
    private readonly ITrip _tripRepository;
    private readonly IClient _clientRepository;

    public TripServise(ITrip tripRepository, IClient clientRepository)
    {
        _tripRepository = tripRepository;
        _clientRepository = clientRepository;
    }

    public async Task<object> GetTripsAsync(int page, int pageSize)
    {
        var trips = await _tripRepository.GetTripsAsync(page, pageSize);
        var totalCount = await _tripRepository.GetTotalTripsCountAsync();
        var allPages = (int)Math.Ceiling((double)totalCount / pageSize);

        var result = new
        {
            pageNum = page,
            pageSize = pageSize,
            allPages = allPages,
            trips = trips.Select(t => new TripDTO
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.IdCountries.Select(c => new CountryDTO { Name = c.Name }).ToList(),
                Clients = t.Client_Trips.Select(ct => new ClientDTO
                {
                    FirstName = ct.IdClientNavigation.FirstName,
                    LastName = ct.IdClientNavigation.LastName
                }).ToList()
            }).ToList()
        };

        return result;
    }

    public async Task<bool> DeleteClientAsync(int clientId)
    {
        var client = await _clientRepository.GetClientByIdAsync(clientId);
        if (client == null) return false;

        if (await _clientRepository.CheckTripsAsync(clientId))
            return false;

        await _clientRepository.DeleteClientAsync(client);
        await _clientRepository.SaveChangesAsync();
        return true;
    }

    public async Task<string> AssignClientToTripAsync(int idTrip, AddClientDTO dto)
    {
        if (await _clientRepository.ExistsByPeselAsync(dto.Pesel))
            return "Client already exist";

        if (await _clientRepository.IsRegisteredToTripAsync(dto.Pesel, idTrip))
            return "Client is already registered";

        if (!await _tripRepository.CheckIfTripInFuture(idTrip))
            return "Trip not exists";

        var client = new ClientModel
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Telephone = dto.Telephone,
            Pesel = dto.Pesel
        };

        var registration = new Client_TripModel
        {
            IdTrip = idTrip,
            IdClientNavigation = client,
            PaymentDate = dto.PaymentDate,
            RegisteredAt = DateTime.UtcNow
        };

        await _clientRepository.AddClientAsync(client);
        await _tripRepository.RegisterClientToTripAsync(registration);
        await _clientRepository.SaveChangesAsync();

        return "Success";
    }
}