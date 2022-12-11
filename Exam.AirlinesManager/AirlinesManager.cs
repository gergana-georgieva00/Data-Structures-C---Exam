using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.DeliveriesManager
{
    public class AirlinesManager : IAirlinesManager
    {
        private Dictionary<string, Airline> airlinesById = new Dictionary<string, Airline>();
        private Dictionary<string, Flight> flightsById = new Dictionary<string, Flight>();

        public void AddAirline(Airline airline)
        {
            airlinesById.Add(airline.Id, airline);
        }

        public void AddFlight(Airline airline, Flight flight)
        {
            if (!airlinesById.ContainsKey(airline.Id))
            {
                throw new ArgumentException();
            }

            airline.Flights.Add(flight);
            flight.Airline = airline;
            flightsById.Add(flight.Id, flight);
        }

        public bool Contains(Airline airline)
            => airlinesById.ContainsKey(airline.Id);

        public bool Contains(Flight flight)
            => flightsById.ContainsKey(flight.Id);

        public void DeleteAirline(Airline airline)
        {
            if (!airlinesById.ContainsKey(airline.Id))
            {
                throw new ArgumentException();
            }

            var flightsToRemove = airlinesById[airline.Id].Flights;
            airlinesById.Remove(airline.Id);

            foreach (var flight in flightsToRemove)
            {
                flightsById.Remove(flight.Id);
            }
        }

        public IEnumerable<Airline> GetAirlinesOrderedByRatingThenByCountOfFlightsThenByName()
            => airlinesById.Values
            .OrderByDescending(a => a.Rating)
            .ThenByDescending(a => a.Flights.Count)
            .ThenBy(a => a.Name);

        public IEnumerable<Airline> GetAirlinesWithFlightsFromOriginToDestination(string origin, string destination)
        //=> airlinesById.Values
        //.Where(a => a.Flights
        //.Any(f => f.IsCompleted == false && f.Origin == origin && f.Destination == destination));
        {
            var list = new List<Airline>();
            foreach (var airline in airlinesById.Values)
            {
                if (airline.Flights.Any(f => f.IsCompleted == false && f.Origin == origin && f.Destination == destination))
                {
                    list.Add(airline);
                }
            }

            return list;
        }

        public IEnumerable<Flight> GetAllFlights()
            => flightsById.Values;

        public IEnumerable<Flight> GetCompletedFlights()
            => flightsById.Values.Where(f => f.IsCompleted == true);

        public IEnumerable<Flight> GetFlightsOrderedByCompletionThenByNumber()
            => flightsById.Values
            .OrderBy(f => f.IsCompleted)
            .ThenBy(f => f.Number);

        public Flight PerformFlight(Airline airline, Flight flight)
        {
            if (!airlinesById.ContainsKey(airline.Id))
            {
                throw new ArgumentException();
            }
            if (!flightsById.ContainsKey(flight.Id))
            {
                throw new ArgumentException();
            }

            //var flightToReturn = flight;
            //flight.IsCompleted = true;
            //return flightToReturn;

            airline.Flights.Remove(flight);
            flight.IsCompleted = true;
            flight.Airline = null;
            return flight;
        }
    }
}
