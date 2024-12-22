namespace ConcertTicketBookingSystem;

public class Concert
{
    public string Name { get; set; }
    public string Date { get; set; }
    public string Location { get; set; }
    public int AvailableSeats { get; set; }
    public static List<Concert> Concerts = new List<Concert>();
    public static List<Concert> RegularConcerts = new List<Concert>();
    public static List<Concert> VipConcerts = new List<Concert>();
    
    public Concert(string name, string date, string location, int availableSeats, string type)
    {
        Name = name;
        Date = date;
        Location = location;
        AvailableSeats = availableSeats;
        Concerts.Add(this);
        if(type == "Regular")
        {
            RegularConcerts.Add(this);
        }
        else if(type == "VIP")
        {
            VipConcerts.Add(this);
        }
        
        var seats = new Seat(name, availableSeats);
    }
}