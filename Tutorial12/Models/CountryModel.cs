namespace Tutorial12.Models;

public class CountryModel
{
    public string Name { get; set; } = null!;
    public int IdCountry { get; set; }
    public virtual ICollection<TripModel> IdTrips { get; set; } = new List<TripModel>();

}