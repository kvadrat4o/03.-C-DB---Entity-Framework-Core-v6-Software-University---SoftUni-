using AutoMapper;
using Mapping.App.Models;
using Mapping.Data;
using Mapping.Models;
using System;

namespace Mapping.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            //ResetDatabase();
            Initializer();
            CommandDispatcher commandDispatcher = new CommandDispatcher();
            Engine engine = new Engine(commandDispatcher);
            engine.Run();
            
        }

        private static void Initializer()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeDTO>();
                cfg.CreateMap<EmployeeDTO, Employee>();
                cfg.CreateMap<Employee, ManagerDTO>();
                cfg.CreateMap<Employee, ListEmployeesOlderThanDTO>()
                    .ForMember(e => e.Years, em => em.MapFrom(emp => DateTime.Today.Year - emp.Birthday.Value.Year))
                    .ForMember(a => a.Manager, am => am.MapFrom(e => e.Manager));
            });
        }

        private static void ResetDatabase()
        {
            using (var db = new EmployeeContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
        }
    }
}
