using AutoMapper;
using FastFood.DataProcessor.Dto.Import;
using FastFood.Models;

namespace FastFood.App
{
	public class FastFoodProfile : Profile
	{
		// Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
		public FastFoodProfile()
		{
            //CreateMap<EmployeeDto, Employee>();
            //.ForMember(f => f.Position, ff => ff.MapFrom(u => u.Position))
            //.ForMember(dto => dto.PositionId, ff => ff.Ignore())
            //.ForMember(dto => dto.Orders, ff => ff.Ignore());
            //CreateMap<ItemDto, Item>();
                //.ForMember(f => f.Id, ff => ff.Ignore())
                //.ForMember(dto => dto.OrderItems, ff => ff.Ignore())
                //.ForMember(dto => dto.Name, ff => ff.Ignore());
		}
	}
}
