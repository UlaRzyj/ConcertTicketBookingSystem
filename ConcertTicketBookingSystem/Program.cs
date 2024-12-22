// See https://aka.ms/new-console-template for more information

using ConcertTicketBookingSystem;

internal class Program
{
    public static void Main(string[] args)
    {
        
        var firstConcert = new Concert("First Concert", "2022-01-01", "Location 1", 100, "Regular");
        var secondConcert = new Concert("Second Concert", "2022-02-02", "Location 2", 200, "VIP");
        var thirdConcert = new Concert("Third Concert", "2022-03-03", "Location 3", 300, "Regular");
        var fourthConcert = new Concert("Fourth Concert", "2022-03-03", "Location 4", 49, "VIP");
        var fifthConcert = new Concert("Fifth Concert", "2022-05-05", "Location 4", 500, "Regular");
        var admin = new BookingSystem("Admin");
        admin.AddConcert();
        var user = new BookingSystem("User");
        admin.raport();
        user.reserveTicket();
        user.reserveTicket();
        admin.raport();
        user.deleteReservation();
        admin.raport();
    }
}