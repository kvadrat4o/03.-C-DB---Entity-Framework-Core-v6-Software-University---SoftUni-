using System;
using FastFood.Data;
using System.Text;
using System.Linq;

namespace FastFood.DataProcessor
{
    public static class Bonus
    {
	    public static string UpdatePrice(FastFoodDbContext context, string itemName, decimal newPrice)
	    {
            StringBuilder sb = new StringBuilder();
            var item = context.Items.Where(i => i.Name == itemName).SingleOrDefault();
            bool itemExists = context.Items.Any(e => e.Name == itemName);
            if (!itemExists)
            {
                sb.AppendLine($"Item { item.Name} not found!");
            }
            var oldPrice = item.Price;
            item.Price = newPrice;
            context.SaveChanges();
            sb.AppendLine($"“{item.Name} Price updated from ${oldPrice:F2} to ${newPrice:F2}");
            return sb.ToString();
			throw new NotImplementedException();
	    }
    }
}
