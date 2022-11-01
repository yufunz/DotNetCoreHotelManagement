using Microsoft.EntityFrameworkCore;
using IT703_A2.Data;
using IT703_A2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace IT703_A2.Extensions
{
    public static class AppBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<ApplicationDbContext>();
            var services = scopedServices.ServiceProvider;

            data.Database.Migrate();

            SeedCompanies(data);
            SeedAgencies(data);
            SeedHotels(data);
            SeedGuests(data);
            SeedCarparks(data);
            SeedRoomTypes(data);
            SeedRooms(data);
            SeedRoles(data, services);
            SeedManager(services);
            SeedReception(services);
            SeedHousekeeper(services);

            return app;
        }

        public static void SeedRoles(ApplicationDbContext db, IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync("Manager"))
                    {
                        return;
                    }

                    var manager = new IdentityRole { Name = "Manager" };
                    var reception = new IdentityRole { Name = "Reception" };
                    var housekeeper = new IdentityRole { Name = "Housekeeper" };

                    await roleManager.CreateAsync(manager);
                    await roleManager.CreateAsync(reception);
                    await roleManager.CreateAsync(housekeeper);
                })
                .GetAwaiter()
                .GetResult();
        }

        public static void SeedReception(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    var curUser = await userManager.FindByEmailAsync("reception@tomive.com");

                    if (curUser != null)
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = "Reception" };

                    const string recepEmail = "reception@tomive.com";
                    const string recepPassword = "password!";
                    const string recepName = "Yuhei Fujisawa";

                    var user = new User
                    {
                        Email = recepEmail,
                        UserName = recepEmail,
                        FullName = recepName
                    };

                    await userManager.CreateAsync(user, recepPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }

        public static void SeedHousekeeper(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    var curUser = await userManager.FindByEmailAsync("housekeeper@tomive.com");

                    if (curUser != null)
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = "Housekeeper" };

                    const string hkEmail = "housekeeper@tomive.com";
                    const string hkPassword = "password!";
                    const string hkName = "Misandu Perera";

                    var user = new User
                    {
                        Email = hkEmail,
                        UserName = hkEmail,
                        FullName = hkName
                    };

                    await userManager.CreateAsync(user, hkPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }

        public static void SeedManager(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    var curUser =  await userManager.FindByEmailAsync("manager@tomive.com");

                    if(curUser != null)
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = "Manager" };

                    const string managerEmail = "manager@tomive.com";
                    const string managerPassword = "password!";
                    const string fullName = "Abdul Rehman";

                    var user = new User
                    {
                        Email = managerEmail,
                        UserName = managerEmail,
                        FullName = fullName
                    };

                    await userManager.CreateAsync(user, managerPassword);

                    await userManager.AddToRoleAsync(user, role.Name);
                })
                .GetAwaiter()
                .GetResult();
        }

        public static void SeedGuests(ApplicationDbContext db)
        {
            if(db.Guests.Any())
            {
                return;
            }

            var guests = new[]
            {
                new Guest { FirstName = "Sofia", LastName = "Hristov", Address = "80384 Dane Viaduct", Email = "sofia@example.com", Phone = "23049809", CreatedAt = DateTime.Now },
                new Guest { FirstName = "Kayden", LastName = "Ibrra", Address = "28324 Stefan Rapids", Email = "kayden@example.com", Phone = "4500980", CreatedAt = DateTime.Now },
                new Guest { FirstName = "Clayton", LastName = "Delacruz", Address = "56770 Hartmann Crossing", Email = "clayton@example.com", Phone = "450234582", CreatedAt = DateTime.Now },
                new Guest { FirstName = "Izabella", LastName = "Dawson", Address = "0388 Austyn Views", Email = "izabella@example.com", Phone = "99342154", CreatedAt = DateTime.Now },
                new Guest { FirstName = "Nigel", LastName = "Guerra", Address = "155 Rafael Manor", Email = "nigel@example.com", Phone = "12100008", CreatedAt = DateTime.Now },
                new Guest { FirstName = "Conor", LastName = "Meyer", Address = "036 Marcella Field", Email = "conor@example.com", Phone = "9905431", CreatedAt = DateTime.Now },
            };

            db.Guests.AddRange(guests);
            db.SaveChanges();
        }

        public static void SeedCompanies(ApplicationDbContext db)
        {
            if(db.Companys.Any())
            {
                return;
            }

            var company = new Company
            {
                Name = "SIT Travel",
                Email = "office@sit.com",
                Address = "133 Tay Street",
                Phone = "6432112699",
            };

            db.Companys.Add(company);
            db.SaveChanges();
        }

        public static void SeedAgencies(ApplicationDbContext db)
        {
            if(db.Agencies.Any())
            {
                return;
            }

            var agency = new Agency
            {
                Name = "ABC Travel Agency",
                Email = "office@abc.com",
                Address = "113 Tay Street",
                Phone = "649858554",
            };

            db.Agencies.Add(agency);
            db.SaveChanges();
        }

        public static void SeedHotels(ApplicationDbContext db)
        {
            if (db.Hotels.Any())
            {
                return;
            }

            var hotels = new[]
            {
                new Hotel { Name = "Tomive Hotel", Address = "111 Dee Street", Phone = "644857983", Email = "office@tomive.com" },
                new Hotel { Name = "SIT Hotel", Address = "133 Tay Street", Phone = "6432112699", Email = "office@sit.com" },
            };

            db.Hotels.AddRange(hotels);
            db.SaveChanges();
        }

        public static void SeedRoomTypes(ApplicationDbContext db)
        {
            if(db.RoomTypes.Any())
            {
                return;
            }

            var roomTypes = new[]
            {
                new RoomType { Name = "Single", NumOfBeds = 1, Rate = 50, Image = @"https://live.staticflickr.com/5150/5552156843_4c478eb79c_b.jpg" },
                new RoomType { Name = "Double", NumOfBeds = 2, Rate = 80, Image = @"https://upload.wikimedia.org/wikipedia/commons/thumb/e/e7/Double_room_1_-_Paris_Opera_Cadet_Hotel.jpg/800px-Double_room_1_-_Paris_Opera_Cadet_Hotel.jpg" },
                new RoomType { Name = "Superior", NumOfBeds = 1, Rate = 100, Image = @"https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Flive.staticflickr.com%2F952%2F42102542482_a70235625c_b.jpg&f=1&nofb=1&ipt=f62fdcb31e6dae333dc37e0f480c968b86e2c70f43b00b0b4ea4e8ebd9d67f2c&ipo=images" }
            };

            db.AddRange(roomTypes);
            db.SaveChanges();
        }

        public static void SeedRooms(ApplicationDbContext db)
        {
            if(db.Rooms.Any())
            {
                return;
            }

            var currentHotel = db.Hotels.OrderBy(h => h.Name).OrderBy(c => c.Name).FirstOrDefault();
            var singleRoom = db.RoomTypes.OrderBy(h => h.Name).OrderBy(c => c.Name).FirstOrDefault(rt => rt.Name == "Single");
            var doubleRoom = db.RoomTypes.OrderBy(h => h.Name).OrderBy(c => c.Name).FirstOrDefault(rt => rt.Name == "Double");
            var superiorRoom = db.RoomTypes.OrderBy(h => h.Name).OrderBy(c => c.Name).FirstOrDefault(rt => rt.Name == "Superior");

            var allRooms = new[]
            {
                new Room { FloorNum = 1, Hotel = currentHotel, RoomNum = "Room 101", RoomType = singleRoom },
                new Room { FloorNum = 1, Hotel = currentHotel, RoomNum = "Room 102", RoomType = singleRoom },
                new Room { FloorNum = 1, Hotel = currentHotel, RoomNum = "Room 103", RoomType = doubleRoom },
                new Room { FloorNum = 1, Hotel = currentHotel, RoomNum = "Room 104", RoomType = doubleRoom },
                new Room { FloorNum = 1, Hotel = currentHotel, RoomNum = "Room 105", RoomType = superiorRoom },
                new Room { FloorNum = 1, Hotel = currentHotel, RoomNum = "Room 106", RoomType = singleRoom },
                new Room { FloorNum = 2, Hotel = currentHotel, RoomNum = "Room 201", RoomType = singleRoom },
                new Room { FloorNum = 2, Hotel = currentHotel, RoomNum = "Room 202", RoomType = doubleRoom  },
                new Room { FloorNum = 2, Hotel = currentHotel, RoomNum = "Room 203", RoomType = doubleRoom  },
                new Room { FloorNum = 2, Hotel = currentHotel, RoomNum = "Room 204", RoomType = superiorRoom },
                new Room { FloorNum = 2, Hotel = currentHotel, RoomNum = "Room 205", RoomType = singleRoom },
                new Room { FloorNum = 3, Hotel = currentHotel, RoomNum = "Room 301", RoomType = singleRoom },
                new Room { FloorNum = 3, Hotel = currentHotel, RoomNum = "Room 302", RoomType = doubleRoom },
                new Room { FloorNum = 3, Hotel = currentHotel, RoomNum = "Room 303", RoomType = doubleRoom },
                new Room { FloorNum = 3, Hotel = currentHotel, RoomNum = "Room 304", RoomType = superiorRoom },
                new Room { FloorNum = 3, Hotel = currentHotel, RoomNum = "Room 305", RoomType = singleRoom },
            };

            db.Rooms.AddRange(allRooms);
            db.SaveChanges();
        }

        public static void SeedCarparks(ApplicationDbContext db)
        {
            if (db.Carparks.Any())
            {
                return;
            }

            var carparks = new[]
            {
                new Carpark { Block = "A", IsAvailable = true },
                new Carpark { Block = "B", IsAvailable = true },
                new Carpark { Block = "C", IsAvailable = true },
                new Carpark { Block = "D", IsAvailable = true },
                new Carpark { Block = "E", IsAvailable = true },
            };

            db.Carparks.AddRange(carparks);
            db.SaveChanges();
        }

    }
}
