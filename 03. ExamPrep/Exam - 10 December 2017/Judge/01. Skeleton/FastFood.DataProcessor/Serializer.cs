using System;
using System.IO;
using FastFood.Data;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using FastFood.Models;

namespace FastFood.DataProcessor
{
	public class Serializer
	{
        public static string ExportOrdersByEmployee(FastFoodDbContext context, string employeeName, string orderType)
        {
            StringBuilder sb = new StringBuilder();
            var otype = context.Orders.Select(e => e.Type).ToString();
            var parsed = Enum.Parse<OrderType>(otype);
            var mm = Enum.Parse<OrderType>(orderType);
            var employees = context.Employees.Where(e => e.Name == employeeName && e.Orders.Any(o => o.Type == mm))
                .Include(e => e.Orders)
                .Select(e => new
                {
                    Name = e.Name,
                    Orders = e.Orders.Select(em => new
                    {
                        Customer = em.Customer,
                        Items = em.OrderItems.Select(oi => new
                        {
                            Name = oi.Item.Name,
                            Price = oi.Item.Price,
                            Quantity = oi.Quantity
                        }),
                        TotalPrice = decimal.Parse(em.OrderItems.Select(om => om.Quantity).ToString()) * decimal.Parse(em.OrderItems.Select(oe => oe.Item.Price).ToString())
                    })
                })
                .OrderByDescending(o => decimal.Parse(o.Orders.Select(om => om.Items.Select(pp => pp.Quantity)).ToString()) * decimal.Parse(o.Orders.Select(oe => oe.Items.Select(wp => wp.Price)).ToString()))
                .ThenByDescending(e => e.Orders.Select(pa => pa.Items.Count()))
                .Select(op => new {
                    TotalMade = int.Parse(op.Orders.Select(p => p.Items.Select(i => i.Quantity).Sum()).ToString()) * int.Parse(op.Orders.Select(p => p.Items.Select(i => i.Price).Sum()).ToString()) })
                .ToString();
            sb.AppendLine(employees);
            return sb.ToString();
		}

		public static string ExportCategoryStatistics(FastFoodDbContext context, string categoriesString)
		{
            StringBuilder sb = new StringBuilder();
            var cats = categoriesString.Split(',').ToList();
            //var categories = context.Categories.Any(e => e.Name = cats[)
            foreach (var cc in cats)
            {
                if (context.Categories.Any(e => e.Name == cc))
                {
                    
                }
                var category = context.Categories.Where(c => c.Name == cc);

            }
            


            return sb.ToString();

		}
	}
}