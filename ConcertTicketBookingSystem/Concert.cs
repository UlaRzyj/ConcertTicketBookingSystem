namespace ConcertTicketBookingSystem;

public class Concert
{
    public string Name { get; set; }
    public string Date { get; set; }
    public string Location { get; set; }
    public int AvailableSeats { get; set; }
    public static List<Concert> Concerts = new List<Concert>();
    
    public Concert(string name, string date, string location, int availableSeats)
    {
        Name = name;
        Date = date;
        Location = location;
        AvailableSeats = availableSeats;
        
        Concerts.Add(this);
        
        var seats = new Seat(name, availableSeats);
    }
}