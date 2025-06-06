namespace Tutorial12.Models;
using System.Collections.Generic;

public class TripModel
{
    public int IdTrip { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int MaxPeople { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public virtual ICollection<Client_TripModel> Client_Trips { get; set; } = new List<Client_TripModel>();
    public virtual ICollection<CountryModel> IdCountries { get; set; } = new List<CountryModel>();

}