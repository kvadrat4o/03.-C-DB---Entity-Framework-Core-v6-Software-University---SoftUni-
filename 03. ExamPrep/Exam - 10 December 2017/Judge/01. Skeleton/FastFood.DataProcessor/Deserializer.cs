using System;
using FastFood.Data;
using System.Text;
using Newtonsoft.Json;
using FastFood.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FastFood.DataProcessor.Dto.Import;
using AutoMapper;
using System.Xml.Serialization;
using System.IO;
using System.Globalization;

namespace FastFood.DataProcessor
{
	public static class Deserializer
	{
		private const string FailureMessage = "Invalid data format.";
		private const string SuccessMessage = "Record {0} successfully imported.";

        public static string ImportEmployees(FastFoodDbContext context, string jsonString)
		{
            StringBuilder sb = new StringBuilder();
            var deserializedEmployees = JsonConvert.DeserializeObject<Employee[]>(jsonString, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            List<Employee> employees = new List<Employee>();
            foreach (var empl in deserializedEmployees)
            {
                if (!IsValid(empl))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                bool positionIsValid = IsValid(empl.Position);
                bool positionExists = context.Positions.Any(p => p.Name == empl.Position.Name) || employees.Any(e => e.Position.Name == empl.Position.Name);
                if (positionIsValid && IsValid(empl) && !positionExists)
                {
                    var position = new Position()
                    {
                        Name = empl.Position.Name
                    };
                }
                //var employee = Mapper.Map<Employee>(empl);
                var employee = new Employee()
                {
                    Name = empl.Name,
                    Position = empl.Position,
                    Age = empl.Age
                };
                employees.Add(employee);
                sb.AppendLine(String.Format(SuccessMessage, employee.Name));
            }
            context.Employees.AddRange(employees);
            context.SaveChanges();
            return sb.ToString();
		}
        

        public static string ImportItems(FastFoodDbContext context, string jsonString)
		{
            StringBuilder sb = new StringBuilder();
            var deserializedItems = JsonConvert.DeserializeObject<Item[]>(jsonString, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include
            });
            List<Item> items = new List<Item>();
            foreach (var item in deserializedItems)
            {
                if (!IsValid(item))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                bool itemExits = context.Items.Any(i => i.Name == item.Name) || items.Any(i => i.Name == item.Name);
                if (itemExits)
                {
                    continue;
                }
                bool categoryExists = context.Categories.Any(c => c.Name == item.Category.Name);
                if (!categoryExists)
                {
                    var category = new Category()
                    {
                        Name = item.Category.Name,
                    };
                }
                var itemV = new Item()
                {
                    Name = item.Name,
                    Price = item.Price,
                    Category = item.Category
                };
                //var itemV = Mapper.Map<Item>(item);
                items.Add(itemV);
                sb.AppendLine(String.Format(SuccessMessage, itemV.Name));
            }
            context.Items.AddRange(items);
            return sb.ToString();
        }

		public static string ImportOrders(FastFoodDbContext context, string xmlString)
		{
            StringBuilder sb = new StringBuilder();
            var serializer = new XmlSerializer(typeof(Order[]), new XmlRootAttribute("Orders"));
            var deserialisedOrders = (Order[])serializer.Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(xmlString)));
            List<Order> orders = new List<Order>();
            foreach (var order in deserialisedOrders)
            {
                if (!IsValid(order))
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var orderDate = DateTime.ParseExact(order.DateTime.ToString(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                bool employeeExists = context.Employees.Any(e => e.Name == order.Employee.Name);
                if (!employeeExists)
                {
                    sb.AppendLine(FailureMessage);
                    continue;
                }
                var orderV = new Order()
                {
                    Employee = order.Employee,
                    DateTime = order.DateTime,
                    Customer = order.Customer,
                    OrderItems = order.OrderItems
                };
                orders.Add(orderV);
                sb.AppendLine(string.Format($"Order for {orderV.Employee.Name} on {orderV.DateTime} added"));
            }
            context.Orders.AddRange(orders);
            context.SaveChanges();
            return sb.ToString();
        }

        private static bool IsValid(Object obj)
        {
            var validationcontext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, validationcontext, validationResults, true);
            return isValid;
        }
    }
}