namespace ConcertTicketBookingSystem;

public class Ticket
{
    public string Concert { get; set; }
    public string Price { get; set; }
    public int SeatNumber { get; set; }
    public static List<Ticket> Tickets = new List<Ticket>();
    public Ticket(string concert, string price, int seatNumber)
    {
        Concert = concert;
        Price = price;
        SeatNumber = seatNumber;
        Tickets.Add(this);
    }
}