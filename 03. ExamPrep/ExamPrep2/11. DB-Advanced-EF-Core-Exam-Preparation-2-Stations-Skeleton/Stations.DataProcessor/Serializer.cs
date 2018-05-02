using System;
using Stations.Data;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using System.Xml.Serialization;
using Stations.Models;
using System.IO;
using System.Xml;
using Stations.Models.Enums;

namespace Stations.DataProcessor
{
	public class Serializer
	{
		public static string ExportDelayedTrains(StationsDbContext context, string dateAsString)
		{
            DateTime date = DateTime.ParseExact(dateAsString, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            var trains = context.Trains.Where(t => t.Trips.Any(tr => tr.Status == Models.Enums.TripStatus.Delayed) && t.Trips.Any(te => te.DepartureTime < date)).Select(t => new
            {
                t.TrainNumber,
                DelayedTrips = t.Trips.Where(tr => tr.Status == Models.Enums.TripStatus.Delayed && tr.DepartureTime <= date).ToArray()
            })
                .Select(t => new 
                {
                    TrainNumber = t.TrainNumber,
                    DelayedTimes = t.DelayedTrips.Length,
                    MaxDelayedTime = t.DelayedTrips.Max(tr => tr.TimeDifference).ToString()
                })
                .OrderByDescending(t => t.DelayedTimes)
                .ThenByDescending(t => t.MaxDelayedTime)
                .ThenBy(t => t.TrainNumber)
                .ToArray();
            var jsonstring = JsonConvert.SerializeObject(trains, Newtonsoft.Json.Formatting.Indented);
            return jsonstring;
		}

		public static string ExportCardsTicket(StationsDbContext context, string cardType)
		{
            var sb = new StringBuilder();
            var type = Enum.Parse<CardType>(cardType);
            var cards = context.Cards.Where(c => c.Type == type && c.BoughtTickets.Any())
                .Select(c => new
                {
                    name = c.Name,
                    type = c.Type,
                    Tickets = c.BoughtTickets.Select(tb => new
                    {
                        OriginStation = tb.Trip.OriginStation,
                        DestinationStation = tb.Trip.DestinationStation,
                        DepartureTime = DateTime.ParseExact(tb.Trip.DepartureTime.ToString(),"",CultureInfo.InvariantCulture)
                    })
                })
                .OrderBy(c => c.name)
                .ToArray();

            var serializer = new XmlSerializer(typeof(CustomerCard[]), new XmlRootAttribute("Cards"));
            serializer.Serialize(new StringWriter(sb), cards, new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty }));
            return sb.ToString();
		}
	}
}