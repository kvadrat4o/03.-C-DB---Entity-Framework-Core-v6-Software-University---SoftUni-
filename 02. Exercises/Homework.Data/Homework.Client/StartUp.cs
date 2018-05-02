using System;
using Microsoft.EntityFrameworkCore;
using Homework.Data;
using AutoMapper;
using Homework.Client.ModelsDBO;
using Homework.Models;

namespace Homework.Client
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            //ResetDatabase();
            InitializeAutoMapper();

            var commandDispatch = new CommandDispatcher();

            var engine = new Engine(commandDispatch);

            engine.Run();
        }

        private static void InitializeAutoMapper()
        {
            Mapper.Initialize(a=>
            {
                a.CreateMap<Employee, EmployeeDto>();
                a.CreateMap<EmployeeDto, Employee>();
                a.CreateMap<Employee, ManagerDto>();
                a.CreateMap<Employee, ListEmployeesOlderThanDto>()
                    .ForMember(b => b.Years, c => c.MapFrom(e => DateTime.Today.Year - e.BirthDay.Value.Year))
                    .ForMember(b => b.Manager, c => c.MapFrom(e => e.Manager));
            });
        }

        private static void ResetDatabase()
        {
            using (var db = new HomeworkDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
