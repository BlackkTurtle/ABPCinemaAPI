using ABPCinemaAPI.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPCinemaAPI.DAL.Persistence.Seeding
{
    public static class CinemaSeeding
    {
        public static List<Hall> Halls { get; set; } = new List<Hall>();

        public static void SeedingInit()
        {
            SeedHallEntities();
        }

        #region SeedHalls
        private static void SeedHallEntities()
        {
            Halls.Add(
                new Hall {
                    Id = Guid.NewGuid(), 
                    Name = "Hall 1", 
                    Capacity = 50,
                    Price = 2000,
                    Services = new List<Service>
                    {
                        new Service { Id = Guid.NewGuid(),  Name = "Projector", Price = 500 },
                        new Service { Id = Guid.NewGuid(),  Name = "Wifi", Price = 300 },
                        new Service { Id = Guid.NewGuid(),  Name = "Sound", Price = 700 }
                    }
                });

            Halls.Add(
                new Hall {
                    Id = Guid.NewGuid(), 
                    Name = "Hall 2", 
                    Capacity = 100,
                    Price = 3500,
                    Services = new List<Service>
                    {
                        new Service { Id = Guid.NewGuid(),  Name = "Popcorn", Price = 500 },
                        new Service { Id = Guid.NewGuid(),  Name = "Soda", Price = 300 },
                        new Service { Id = Guid.NewGuid(),  Name = "Nachos", Price = 400 }
                    }
                });

            Halls.Add(
                new Hall {
                    Id = Guid.NewGuid(), 
                    Name = "Hall 3", 
                    Capacity = 30,
                    Price = 1500,
                    Services = new List<Service>
                    {
                        new Service { Id = Guid.NewGuid(), Name = "VIP Seats", Price = 1000 },
                        new Service { Id = Guid.NewGuid(), Name = "Private Screening", Price = 2000 },
                        new Service { Id = Guid.NewGuid(), Name = "Catering", Price = 1500 }
                    }
                });
        }
        #endregion
    }
}
