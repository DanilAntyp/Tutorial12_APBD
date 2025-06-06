namespace Tutorial12.Models;

public partial class Client_TripModel
{
    public int IdClient { get; set; }
    public int IdTrip { get; set; }
    public DateTime RegisteredAt { get; set; }
    public DateTime? PaymentDate { get; set; }
    public virtual ClientModel IdClientNavigation { get; set; } = null!;
    public virtual TripModel IdTripNavigation { get; set; } = null!;

}