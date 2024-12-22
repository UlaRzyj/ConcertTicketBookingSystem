using System.Data;

namespace ConcertTicketBookingSystem;

public class BookingSystem
{
    public string Role;
    public BookingSystem(string role)
    {
        Role = role;
        BookingSystem.lowAvailable();
    }
    public void AddConcert()
    {
        if(this.Role != "Admin")
        {
            Console.WriteLine("You are not authorized to add a concert");
        }
        else
        {
            Console.WriteLine("Podaj nazwe koncertu: ");
            var name = Console.ReadLine();
            Console.WriteLine("Podaj date koncertu: ");
            String date = Console.ReadLine();
            Console.WriteLine("Podaj lokalizacje koncertu: ");
            var location = Console.ReadLine();
            Console.WriteLine("Podaj ilosc dostepnych miejsc: ");
            String availableSeatsString = Console.ReadLine();
            int availableSeats = int.Parse(availableSeatsString);
            Console.WriteLine("Podaj typ koncertu (Regular/VIP): ");
            var type = Console.ReadLine();
            Concert concert = new Concert(name, date, location, availableSeats, type);
        }

        
        
    }
    

    public void checkConcerst(string Filter)
    {
        if (this.Role == "User")
        {
            if (Filter == "date")
            {
                Console.WriteLine("Podaj date koncertu: ");
                var date = Console.ReadLine();
                var concerts = Concert.Concerts.Where(concert => concert.Date == date);
                foreach (var concert in concerts)
                {
                    Console.WriteLine("Koncert: " + concert.Name + " Data: " + concert.Date + " Lokalizacja: " + concert.Location + " Dostepne miejsca: " + concert.AvailableSeats);
                }
            }
            else if(Filter == "location")
            {
                Console.WriteLine("Podaj lokalizacje koncertu: ");
                var location = Console.ReadLine();
                var concerts = Concert.Concerts.Where(concert => concert.Location == location);
                foreach (var concert in concerts)
                {
                    Console.WriteLine("Koncert: " + concert.Name + " Data: " + concert.Date + " Lokalizacja: " + concert.Location + " Dostepne miejsca: " + concert.AvailableSeats);
                }
            }
        }
    }
    public void reserveTicket()
    {
        if(this.Role != "User")
        {
            Console.WriteLine("You are not authorized to reserve a ticket");
        }
        else
        {
            Console.WriteLine("Podaj nazwe koncertu:");
            var concertName = Console.ReadLine();
            foreach (var concert in Concert.VipConcerts)
            {
                if(concert.Name == concertName)
                {
                    Console.WriteLine("Wybrales koncert VIP, podaj hasło dostępu:");
                    var password = Console.ReadLine();
                    if (password == "VIP")
                    {
                        var vipConcert = new VipConcert();
                        vipConcert.Reservation(concertName);
                    }
                    else if(password != "VIP")
                    {
                        Console.WriteLine("Błędne hasło");
                        return;
                    }
                }
            }
            foreach (var concert in Concert.RegularConcerts)
            {
                if(concert.Name == concertName)
                {
                    var regularConcert = new RegularConcert();
                    regularConcert.Reservation(concertName);
                }
            }
        }
    }

    public void raport()
    {
        if(this.Role != "Admin")
        {
            Console.WriteLine("You are not authorized to generate a report");
        }
        else
        {
            var concertsNumber = Concert.Concerts.Count;
            for (int i = 0; i < concertsNumber; i++)
            {
                Console.WriteLine("Koncert: " + Concert.Concerts[i].Name + " Data: " + Concert.Concerts[i].Date + " Lokalizacja: " + Concert.Concerts[i].Location + " Dostepne miejsca: " + Concert.Concerts[i].AvailableSeats);
            }
        }
    }
    
    public static void lowAvailable() 
    {
        Console.WriteLine("Niska dostępność miejsc:");
        foreach (var concert in Concert.Concerts)
        {
            if (concert.AvailableSeats < 50)
            {
                Console.WriteLine("Koncert: " + concert.Name + " Data: " + concert.Date + " Lokalizacja: " + concert.Location + " Dostepne miejsca: " + concert.AvailableSeats);
            }
        }
    }

    public void deleteReservation()
    {
        if (this.Role == "User")
        {
            Console.WriteLine("Podaj nazwe koncertu: ");
            var concertName = Console.ReadLine();
            foreach (var tickets in Ticket.Tickets)
            {
                if (tickets.Concert == concertName)
                {
                    Console.WriteLine("Bilet: " + tickets.Concert +
                                      " Cena: " + tickets.Price + " Numer miejsca: " + tickets.SeatNumber);
                }
            }
            Console.WriteLine("Podaj numer miejsca, który chcesz anulować: ");
            var seatNumberString = Console.ReadLine();
            var seatNumber = int.Parse(seatNumberString);
            for (var x = 0; x < Ticket.Tickets.Count; x++)
            {
                if (Ticket.Tickets[x].Concert == concertName && Ticket.Tickets[x].SeatNumber == seatNumber)
                {
                    Ticket.Tickets.RemoveAt(x);
                }
            }
            var concertSeats = Seat.concerstSeatsList.Where(seat => seat.ConcertName == concertName);
            foreach (var seat in concertSeats)
            {
                seat.Seats.Add(seatNumber);
            }
            var concertsNumber = Concert.Concerts.Count;
            for (int i = 0; i < concertsNumber; i++)
            {
                if (Concert.Concerts[i].Name == concertName)
                {
                    Concert.Concerts[i].AvailableSeats += 1;
                }
            }
        }
    }
    
}