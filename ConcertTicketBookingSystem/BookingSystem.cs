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
            Concert concert = new Concert(name, date, location, availableSeats);
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
            Console.WriteLine("Podaj nazwe koncertu: ");
            var concertName = Console.ReadLine();
            var random = new Random();
            var firstPrice = random.Next(50, 100);
            var secondPrice = random.Next(100, 200);
            var thirdPrice = random.Next(200, 500);
            Console.WriteLine($"Wybierz cene z podanych - $ {firstPrice}, $ {secondPrice}, $ {thirdPrice}");
            var price = Console.ReadLine();
            Console.WriteLine("Podaj numer miejsca: ");
            var concertSeats = Seat.concerstSeatsList.Where(seat => seat.ConcertName == concertName);
            foreach (var seat in concertSeats)
            {
                for(var i = 0; i < seat.Seats.Count; i++)
                {
                    Console.Write(seat.Seats[i] + ", ");
                }
            }
            var seatNumberString = Console.ReadLine();
            var seatNumber = int.Parse(seatNumberString);
            Ticket ticket = new Ticket(concertName, price, seatNumber);
            foreach (var seat in concertSeats)
            {
                for(var i = 0; i < seat.Seats.Count; i++)
                {
                    if(seat.Seats[i] == seatNumber)
                    {
                        seat.Seats.RemoveAt(i);
                    }
                }
            }

            var concertsNumber = Concert.Concerts.Count;
            for (int i = 0; i < concertsNumber; i++)
            {
                if (Concert.Concerts[i].Name == concertName)
                {
                    Concert.Concerts[i].AvailableSeats -= 1;
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
            foreach (var ticket in Ticket.Tickets)
            {
                if (ticket.Concert == concertName && ticket.SeatNumber == seatNumber)
                {
                    Ticket.Tickets.Remove(ticket);
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
    }
}