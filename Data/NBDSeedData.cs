using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NbdAplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace NbdAplication.Data
{
    public static class NBDSeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new NbdContext(
                serviceProvider.GetRequiredService<DbContextOptions<NbdContext>>()))
            {

                if (!context.Client.Any())
                {
                    context.Client.AddRange(
                        new Client
                        {
                            ClientName = "Client1",
                            ContactFirst = "FirstName1",
                            ContactLast = "LastName1",
                            ContactPos = "Position1",
                            Address = "Address for client1",
                            ContactEmail = "emailofclient1@gmail.com",
                            ContactPhone = "6475639856"
                        },
                        new Client
                        {
                            ClientName = "Client2",
                            ContactFirst = "FirstName2",
                            ContactLast = "LastName2",
                            ContactPos = "Position2",
                            Address = "Address for client2",
                            ContactEmail = "emailofclient2@gmail.com",
                            ContactPhone = "6475639856"
                        },
                        new Client
                        {
                            ClientName = "Client3",
                            ContactFirst = "FirstName3",
                            ContactLast = "LastName3",
                            ContactPos = "Position1",
                            Address = "Address for client3",
                            ContactEmail = "emailofclient3@gmail.com",
                            ContactPhone = "6475639856"
                        },
                        new Client
                        {
                            ClientName = "Client4",
                            ContactFirst = "FirstName4",
                            ContactLast = "LastName4",
                            ContactPos = "Position4",
                            Address = "Address for client4",
                            ContactEmail = "emailofclient4@gmail.com",
                            ContactPhone = "6475639856"
                        },
                        new Client
                        {
                            ClientName = "Client5",
                            ContactFirst = "FirstName5",
                            ContactLast = "LastName5",
                            ContactPos = "Position5",
                            Address = "Address for client5",
                            ContactEmail = "emailofclient5@gmail.com",
                            ContactPhone = "6475639856"
                        }
                        );
                    context.SaveChanges();
                }
                    
                

                if (!context.Projects.Any())
                {

                    context.Projects.AddRange(
                        new Projects
                        {
                            PName = "Project1Name",
                            EstStart = DateTime.Parse("2020-01-31"),
                            EstEnd = DateTime.Parse("2020-03-31"),
                            PSite = "Project1Site",
                            ActStartDate = DateTime.Parse("2020-01-31"),
                            ActEndDate = DateTime.Parse("2020-03-31"),
                            BidAmount = 5000,
                            ClientID = context.Client.FirstOrDefault(c => c.ClientName == "Client1" && c.ContactFirst == "FirstName1" && c.ContactLast == "LastName1" && c.ContactPos == "Position1" && c.Address == "Address for client1" && c.ContactEmail == "emailofclient1@gmail.com" && c.ContactPhone == "6475639856").ClientId
                        },
                        new Projects
                        {
                            PName = "Project2Name",
                            EstStart = DateTime.Parse("2020-01-31"),
                            EstEnd = DateTime.Parse("2020-03-31"),
                            PSite = "Project2Site",
                            ActStartDate = DateTime.Parse("2020-01-31"),
                            ActEndDate = DateTime.Parse("2020-03-31"),
                            BidAmount = 5000,
                            ClientID = context.Client.FirstOrDefault(c => c.ClientName == "Client2" && c.ContactFirst == "FirstName2" && c.ContactLast == "LastName2" && c.ContactPos == "Position2" && c.Address == "Address for client2" && c.ContactEmail == "emailofclient2@gmail.com" && c.ContactPhone == "6475639856").ClientId
                        }
                        //new Projects
                        //{
                        //    PName = "Project3Name",
                        //    EstStart = DateTime.Parse("2020-01-31"),
                        //    EstEnd = DateTime.Parse("2020-03-31"),
                        //    PSite = "Project3Site",
                        //    ActStartDate = DateTime.Parse("2020-01-31"),
                        //    ActEndDate = DateTime.Parse("2020-03-31"),
                        //    BidAmount = 5000,
                        //    ClientID = context.Client.FirstOrDefault(c => c.ClientName == "Client3" && c.ContactFirst == "FirstName3" && c.ContactLast == "LastName3" && c.ContactPos == "Position3" && c.Address == "Address for client3" && c.ContactEmail == "emailofclient3@gmail.com" && c.ContactPhone == "6475639856").ClientId
                        //}
                        //new Projects
                        //{
                        //    PName = "Project4Name",
                        //    EstStart = DateTime.Parse("2020-01-31"),
                        //    EstEnd = DateTime.Parse("2020-03-31"),
                        //    PSite = "Project4Site",
                        //    ActStartDate = DateTime.Parse("2020-01-31"),
                        //    ActEndDate = DateTime.Parse("2020-03-31"),
                        //    BidAmount = 5000,
                        //    ClientID = context.Client.FirstOrDefault(c => c.ClientName == "Client4" && c.ContactFirst == "FirstName4" && c.ContactLast == "LastName4" && c.ContactPos == "Position4" && c.Address == "Address for client4" && c.ContactEmail == "emailofclient4@gmail.com" && c.ContactPhone == "6475639856").ClientId
                        //},
                        //new Projects
                        //{
                        //    PName = "Project5Name",
                        //    EstStart = DateTime.Parse("2020-01-31"),
                        //    EstEnd = DateTime.Parse("2020-03-31"),
                        //    PSite = "Project5Site",
                        //    ActStartDate = DateTime.Parse("2020-01-31"),
                        //    ActEndDate = DateTime.Parse("2020-03-31"),
                        //    BidAmount = 5000,
                        //    ClientID = context.Client.FirstOrDefault(c => c.ClientName == "Client5" && c.ContactFirst == "FirstName5" && c.ContactLast == "LastName5" && c.ContactPos == "Position5" && c.Address == "Address for client5" && c.ContactEmail == "emailofclient5@gmail.com" && c.ContactPhone == "6475639856").ClientId
                        //}
                        );
                    context.SaveChanges();
                }


                if (!context.Tasks.Any())
                {

                    context.Tasks.AddRange(
                        new Models.Task
                        {
                            TaskDescription = "Task1Description", 
                            EstCompleteDate = DateTime.Parse("2021-04-10")
                        },
                        new Models.Task
                        {
                            TaskDescription = "Task2Description",
                            EstCompleteDate = DateTime.Parse("2021-04-15")
                        },
                        new Models.Task
                        {
                            TaskDescription = "Task3Description",
                            EstCompleteDate = DateTime.Parse("2021-04-20")
                        }
                        );
                    context.SaveChanges();
                }
                if (!context.Sale.Any())
                {

                    context.Sale.AddRange(
                        new Sale
                        {
                            Sales = "Sale1"
                        }
                        );
                    context.SaveChanges();
                }
                if (!context.Designer.Any())
                {

                    context.Designer.AddRange(
                        new Designer
                        {
                            Designers = "Designer1"
                        }
                        );
                    context.SaveChanges();
                }
                if (!context.Occupations.Any())
                {

                    context.Occupations.AddRange(
                        new Occupation
                        {
                            Position = "OccupationPosition1",
                            Description = "OccupationDescription1",
                            PricePerHour = 14
                        }, 
                        new Occupation
                        {
                            Position = "OccupationPosition2",
                            Description = "OccupationDescription2",
                            PricePerHour = 14
                        }
                        );
                    context.SaveChanges();
                }
                if (!context.Nbdstaffs.Any())
                {

                    context.Nbdstaffs.AddRange(
                        new Nbdstaff
                        {
                            StaffFirst = "Staff1FirstName",
                            StaffLast = "Staff1LastName",
                            UserName = "Staff1UserName",
                            Password = "Staff1Password",
                            Phone = 1112223334,
                            SaleID = context.Sale.FirstOrDefault(s => s.Sales == "Sale1").ID,
                            DesignerID = context.Designer.FirstOrDefault(s => s.Designers == "Designer1").ID,
                            OccupationID = context.Occupations.FirstOrDefault(s => s.Position == "OccupationPosition1" && s.Description == "OccupationDescription1" && s.PricePerHour == 14).ID
                        },
                        new Nbdstaff
                        {
                            StaffFirst = "Staff2FirstName",
                            StaffLast = "Staff2LastName",
                            UserName = "Staff2UserName",
                            Password = "Staff2Password",
                            Phone = 1112223334,
                            SaleID = context.Sale.FirstOrDefault(s => s.Sales == "Sale1").ID,
                            DesignerID = context.Designer.FirstOrDefault(s => s.Designers == "Designer1").ID,
                            OccupationID = context.Occupations.FirstOrDefault(s => s.Position == "OccupationPosition1" && s.Description == "OccupationDescription1" && s.PricePerHour == 14).ID
                        }
                        );
                    context.SaveChanges();
                }
                if (!context.Bids.Any())
                {

                    context.Bids.AddRange(
                        new Bids
                        {
                            //Amount = 5000,
                            EstAmount = 4900,
                            ActAmount = 5000,
                            IsApprovedByClient = true,
                            IsApprovedByNBD = true,
                            RevisionCheck = false,
                            SaleID = context.Sale.FirstOrDefault(s => s.Sales == "Sale1").ID,
                            DesignerID = context.Designer.FirstOrDefault(s => s.Designers == "Designer1").ID,
                            OccupationID = context.Occupations.FirstOrDefault(s => s.Position == "OccupationPosition1" && s.Description == "OccupationDescription1" && s.PricePerHour == 14).ID,
                            NbdstaffID = context.Nbdstaffs.FirstOrDefault(s => s.StaffFirst == "Staff1FirstName" && s.StaffLast == "Staff1LastName" && s.UserName == "Staff1UserName" && s.Password == "Staff1Password" && s.Phone == 1112223334).ID,
                            ProjectsID = context.Projects.FirstOrDefault(s => s.PName == "Project1Name" && s.EstStart == DateTime.Parse("2020-01-31") && s.EstEnd == DateTime.Parse("2020-03-31") &&
                            s.PSite == "Project1Site" && s.ActStartDate == DateTime.Parse("2020-01-31") && s.ActEndDate == DateTime.Parse("2020-03-31") && s.BidAmount == 5000).ID
                        },
                        new Bids
                        {
                            //Amount = 5500,
                            EstAmount = 5100,
                            ActAmount = 5500,
                            IsApprovedByClient = true,
                            IsApprovedByNBD = true,
                            RevisionCheck = false,
                            SaleID = context.Sale.FirstOrDefault(s => s.Sales == "Sale1").ID,
                            DesignerID = context.Designer.FirstOrDefault(s => s.Designers == "Designer1").ID,
                            OccupationID = context.Occupations.FirstOrDefault(s => s.Position == "OccupationPosition1" && s.Description == "OccupationDescription1" && s.PricePerHour == 14).ID,
                            NbdstaffID = context.Nbdstaffs.FirstOrDefault(s => s.StaffFirst == "Staff1FirstName" && s.StaffLast == "Staff1LastName" && s.UserName == "Staff1UserName" && s.Password == "Staff1Password" && s.Phone == 1112223334).ID,
                            ProjectsID = context.Projects.FirstOrDefault(s => s.PName == "Project1Name" && s.EstStart == DateTime.Parse("2020-01-31") && s.EstEnd == DateTime.Parse("2020-03-31") &&
                            s.PSite == "Project1Site" && s.ActStartDate == DateTime.Parse("2020-01-31") && s.ActEndDate == DateTime.Parse("2020-03-31") && s.BidAmount == 5000).ID
                        }
                        );
                    context.SaveChanges();
                }
                if (!context.Labours.Any())
                {

                    context.Labours.AddRange(
                        new Labour
                        {
                            Hours = 30,
                            LabourDesc = "LabourDescription1",
                            TaskID = context.Tasks.FirstOrDefault(s => s.TaskDescription == "Task1Description").ID,
                            OccupationID = context.Occupations.FirstOrDefault(s => s.Position == "OccupationPosition1" && s.Description == "OccupationDescription1" && s.PricePerHour == 14).ID,
                            NbdstaffID = context.Nbdstaffs.FirstOrDefault(s => s.StaffFirst == "Staff1FirstName" && s.StaffLast == "Staff1LastName" && s.UserName == "Staff1UserName" && s.Password == "Staff1Password" && s.Phone == 1112223334).ID,
                            BidsID = context.Bids.FirstOrDefault(s => s.EstAmount == 4900 && s.ActAmount == 5000 && s.IsApprovedByClient == true && s.IsApprovedByNBD == true && s.RevisionCheck == false).ID             
                        },
                        new Labour
                        {
                            Hours = 30,
                            LabourDesc = "LabourDescription2",
                            TaskID = context.Tasks.FirstOrDefault(s => s.TaskDescription == "Task2Description").ID,
                            OccupationID = context.Occupations.FirstOrDefault(s => s.Position == "OccupationPosition2" && s.Description == "OccupationDescription2" && s.PricePerHour == 14).ID,
                            NbdstaffID = context.Nbdstaffs.FirstOrDefault(s => s.StaffFirst == "Staff2FirstName" && s.StaffLast == "Staff2LastName" && s.UserName == "Staff2UserName" && s.Password == "Staff2Password" && s.Phone == 1112223334).ID,
                            BidsID = context.Bids.FirstOrDefault(s => s.EstAmount == 4900 && s.ActAmount == 5000 && s.IsApprovedByClient == true && s.IsApprovedByNBD == true && s.RevisionCheck == false).ID
                        }
                        );
                    context.SaveChanges();
                }
                if (!context.Materials.Any())
                {

                    context.Materials.AddRange(
                        new Material
                        {
                            MaterialName = "Material1Name",
                            //BidsID = context.Bids.FirstOrDefault(s => s.Amount == 5000 && s.EstAmount == 4900 && s.ActAmount == 5000 && s.IsApprovedByClient == true && s.IsApprovedByNBD == true && s.RevisionCheck == false).ID
                        },
                        new Material
                        {
                            MaterialName = "Material2Name",
                            //BidsID = context.Bids.FirstOrDefault(s => s.Amount == 5000 && s.EstAmount == 4900 && s.ActAmount == 5000 && s.IsApprovedByClient == true && s.IsApprovedByNBD == true && s.RevisionCheck == false).ID
                        }
                        );
                    context.SaveChanges();
                }
                if (!context.Inventories.Any())
                {

                    context.Inventories.AddRange(
                        new Inventory
                        {
                            Code = "InventoryCode1",
                            ItemDescription = "InventoryItem1Description",
                            List = 24,
                            Size = "39 Kg",
                            MaterialID = context.Materials.FirstOrDefault(s => s.MaterialName == "Material1Name").ID,
                            //BidsID = context.Bids.FirstOrDefault(s => s.Amount == 5000 && s.EstAmount == 4900 && s.ActAmount == 5000 && s.IsApprovedByClient == true && s.IsApprovedByNBD == true && s.RevisionCheck == false).ID
                        },
                        new Inventory
                        {
                            Code = "InventoryCode2",
                            ItemDescription = "InventoryItem2Description",
                            List = 30,
                            Size = "20 Kg",
                            MaterialID = context.Materials.FirstOrDefault(s => s.MaterialName == "Material2Name").ID,
                            //BidsID = context.Bids.FirstOrDefault(s => s.Amount == 5000 && s.EstAmount == 4900 && s.ActAmount == 5000 && s.IsApprovedByClient == true && s.IsApprovedByNBD == true && s.RevisionCheck == false).ID
                        }
                        );
                    context.SaveChanges();
                }
            }
        }

        internal static object SeedAsync(ApplicationDbContext identityContext, IServiceProvider services)
        {
            throw new NotImplementedException();
        }
    }
}
