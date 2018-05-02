using System;
using Stations.Data;
using System.Text;
using Newtonsoft.Json;
using Stations.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using Stations.Models.Enums;
using System.Globalization;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace Stations.DataProcessor
{
	public static class Deserializer
	{
		private const string FailureMessage = "Invalid data format.";
		private const string SuccessMessage = "Record {0} successfully imported.";

		public static string ImportStations(StationsDbContext context, string jsonString)
		{
            StringBuilder sb = new StringBuilder();
            var deserializedStations = JsonConvert.DeserializeObject<Station[]>(jsonString);
            List<Station> stations = new List<Station>();
            foreach (var station in deserializedStations)
            {
                IsValid(station);
                if (IsValid(station) == false)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                if (station.Town == null)
                {
                    station.Town = station.Name;
                }
                var stationAlreadyExists = stations.Any(s => s.Name == station.Name);
                if (stationAlreadyExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var stationV = new Station()
                {
                    Town = station.Town,
                    Name = station.Name
                };
                stations.Add(stationV);
                sb.AppendLine(String.Format(SuccessMessage, stationV.Name));
            }
            context.Stations.AddRange(stations);
            context.SaveChanges();
            return sb.ToString();
		}

        public static string ImportClasses(StationsDbContext context, string jsonString)
		{
            StringBuilder sb = new StringBuilder();
            var deserializedClasses = JsonConvert.DeserializeObject<SeatingClass[]>(jsonString);
            List<SeatingClass> classes = new List<SeatingClass>();
            foreach (var clas in deserializedClasses)
            {
                if (!IsValid(clas))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var seatingClassAlreadyExists = classes
                    .Any(sc => sc.Name == clas.Name || sc.Abbreviation == clas.Abbreviation);
                if (seatingClassAlreadyExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var seatClass = new SeatingClass()
                {
                    Name = clas.Name,
                    Abbreviation = clas.Abbreviation
                };
                classes.Add(seatClass);
                sb.AppendLine(String.Format(SuccessMessage, seatClass.Name));
            }
            context.SeatingClasses.AddRange(classes);
            context.SaveChanges();
            return sb.ToString();
		}

		public static string ImportTrains(StationsDbContext context, string jsonString)
		{
            StringBuilder sb = new StringBuilder();
            List<Train> trains = new List<Train>();
            var deserializedTrains = JsonConvert.DeserializeObject<Train[]>(jsonString, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            foreach (var train in deserializedTrains)
            {
                if (!IsValid(train))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                bool trainExists = trains.Any(t => t.TrainNumber == train.TrainNumber );
                if (trainExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                bool seatsArerVallid = train.TrainSeats.All(IsValid);
                if (!seatsArerVallid)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var seatingClassesAreValid = train.TrainSeats
                    .All(s => context.SeatingClasses.Any(sc => sc.Name == s.SeatingClass.Name && sc.Abbreviation == s.SeatingClass.Abbreviation));
                if (!seatingClassesAreValid)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                var type = Enum.Parse<TrainType>(train.Type.ToString());

                var trainSeats = train.TrainSeats.Select(s => new TrainSeat
                {
                    SeatingClass =
                            context.SeatingClasses.SingleOrDefault(sc => sc.Name == s.SeatingClass.Name && sc.Abbreviation == s.SeatingClass.Abbreviation),
                    Quantity = s.Quantity
                })
                    .ToArray();

                var trainValid = new Train
                {
                    TrainNumber = train.TrainNumber,
                    Type = type,
                    TrainSeats = trainSeats
                };
                trains.Add(trainValid);
                sb.AppendLine(string.Format(SuccessMessage, trainValid.TrainNumber));
            }
            context.Trains.AddRange(trains);
            context.SaveChanges();
            return sb.ToString();
		}

		public static string ImportTrips(StationsDbContext context, string jsonString)
		{
            StringBuilder sb = new StringBuilder();
            List<Trip> trips = new List<Trip>();
            var deserializedTrips = JsonConvert.DeserializeObject<Trip[]>(jsonString, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            foreach (var trip in deserializedTrips)
            {
                if (!IsValid(trip))
                {
                    sb.AppendLine(FailureMessage);
                }
                if (trip.Status == null || trip.TimeDifference == null || trip.ArrivalTime == null || trip.DepartureTime == null)
                {
                    sb.AppendLine(FailureMessage);
                }
                var trainExists = context.Trains.SingleOrDefault(t => t.TrainNumber == trip.Train.TrainNumber);
                var originStationExists = context.Stations.SingleOrDefault(s => s.Name == trip.OriginStation.Name);
                var destinationStationExists = context.Stations.SingleOrDefault(s => s.Name == trip.DestinationStation.Name);
                if (trainExists == null || originStationExists == null || destinationStationExists == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var arrivalTime = DateTime.ParseExact(trip.ArrivalTime.ToString(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                var departureTime = DateTime.ParseExact(trip.DepartureTime.ToString(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                if (arrivalTime < departureTime)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                TimeSpan time;
                if (time != null)
                {
                    time = TimeSpan.ParseExact(time.ToString(), @"hh:mm", CultureInfo.InvariantCulture);
                }
                var status = Enum.Parse<TripStatus>(trip.Status.ToString());
                var tripValid = new Trip()
                {
                    Train = trainExists,
                    OriginStation = originStationExists,
                    DestinationStation = destinationStationExists,
                    DepartureTime = departureTime,
                    ArrivalTime = arrivalTime,
                    Status = status,
                    TimeDifference = time
                };
                trips.Add(tripValid);
                sb.AppendLine($"Trip from {tripValid.OriginStation} to {tripValid.DestinationStation} imported.");
            }
            context.Trips.AddRange(trips);
            context.SaveChanges();
            return sb.ToString();
		}

		public static string ImportCards(StationsDbContext context, string xmlString)
		{
            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(CustomerCard), new XmlRootAttribute("cards"));
            var deserializedCards = (CustomerCard[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));
            List<CustomerCard> cards = new List<CustomerCard>();
            foreach (var card in deserializedCards)
            {
                if (!IsValid(card))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var cardType = Enum.TryParse<CardType>(card.Type.ToString(), out var card1) ? card1 : CardType.Normal;
                var customCard = new CustomerCard()
                {
                    Name = card.Name,
                    Type = cardType,
                    Age = card.Age
                };
                cards.Add(customCard);
                sb.AppendLine(string.Format(SuccessMessage, customCard.Name));
            }
            context.Cards.AddRange(cards);
            context.SaveChanges();
            return sb.ToString();
		}

		public static string ImportTickets(StationsDbContext context, string xmlString)
		{
            StringBuilder sb = new StringBuilder();
            List<Ticket> tickets = new List<Ticket>();
            var serializer = new XmlSerializer(typeof(Ticket), new XmlRootAttribute("tickets"));
            var deserializedTickets = (Ticket[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));
            foreach (var ticket in deserializedTickets)
            {
                if (!IsValid(ticket))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var departureTime = DateTime.ParseExact(ticket.Trip.DepartureTime.ToString(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);

                var trip = context.Trips
                    .Include(t => t.OriginStation)
                    .Include(t => t.DestinationStation)
                    .Include(t => t.Train)
                    .Include(t => t.Train.TrainSeats)
                    .SingleOrDefault(t => t.OriginStation == ticket.Trip.OriginStation && t.DestinationStation == ticket.Trip.DestinationStation && t.DepartureTime == ticket.Trip.DepartureTime);

                if (trip == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }

                CustomerCard card = null;
                if (ticket.CustomerCard != null)
                {
                    card = context.Cards.SingleOrDefault(c => c.Name == ticket.CustomerCard.Name);
                    if (card == null)
                    {
                        sb.AppendLine(FailureMessage);
                        continue;
                    }
                }

                var seatingClassAbbr = ticket.SeatingPlace.Substring(0, 2);
                var quantity = int.Parse(ticket.SeatingPlace.Substring(2));
                var seatExists = trip.Train.TrainSeats.SingleOrDefault(t => t.SeatingClass.Abbreviation == seatingClassAbbr && t.Quantity < quantity);
                if (seatExists == null)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var seat = ticket.SeatingPlace;

                var ticketV = new Ticket
                {
                    Trip = trip,
                    CustomerCard = card,
                    Price = ticket.Price,
                    SeatingPlace = seat
                };

                tickets.Add(ticket);
                sb.AppendLine(string.Format("Ticket from {0} to {1} departing at {2} imported.",
                    trip.OriginStation.Name,
                    trip.DestinationStation.Name,
                    trip.DepartureTime.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)));
            }
            context.Tickets.AddRange(tickets);
            context.SaveChanges();
            return sb.ToString();
		}

        private static bool IsValid(Object obj)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(obj);
            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            return isValid;
        }

    }
}