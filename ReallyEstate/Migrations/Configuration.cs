using ReallyEstate.Models;

namespace ReallyEstate.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ReallyEstateContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ReallyEstateContext context)
        {
            context.Listings.AddOrUpdate(x => x.Id,
                new Listing
                {
                    Id = 1,
                    Address = "12345 Happy Street",
                    Description = "A beautiful two-story home in Northern Virginia surrounded by lush ...",
                    Price = 225000
                },
                new Listing
                {
                    Id = 2,
                    Address = "15061 W 138th St, Olathe, KS 66062",
                    Description = "A luxury town home featuring attractive brick exterior, attached basement & garage, fireplace ...",
                    Price = 150000
                },
                new Listing
                {
                    Id = 3,
                    Address = "13826 S Kaw St., Olathe, KS 66062",
                    Description = "Terrific home on large corner lot in very desirable Wexford subdivision. Lots of updates ...",
                    Price = 246500
                },
                new Listing
                {
                    Id = 4,
                    Address = "13949 Canterbury Circle, Leawood, KS 66224",
                    Description = "Someone who wants the ultimate in care free living and feel of vacation every day of the year ...",
                    Price = 1095000
                },
                new Listing
                {
                    Id = 5,
                    Address = "2055 W. 162nd Terr, Stillwell, KS 66085",
                    Description = "Country meets city. Fantastic 2 story home. 4 bedrooms, 3.5 baths",
                    Price = 350000
                },
                new Listing
                {
                    Id = 6,
                    Address = "11100 Ash Street, Suite 101, Leawood, KS 66211",
                    Description = "Essenza's Headquarters. Bargain offer!",
                    Price = 11100000
                });
        }
    }
}
