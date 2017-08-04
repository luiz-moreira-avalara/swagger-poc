using System.Collections.Generic;
using Bogus;
using Swagger.PoC.ViewModels;

namespace Swagger.PoC.Extension
{
    public static class FakeViewModels
    {
        public static CategoryViewModel Category
        {
            get
            {
                var fake = new Faker<CategoryViewModel>()
                    .RuleFor(u => u.Id, f => f.UniqueIndex)
                    .RuleFor(u => u.Name, f => f.Lorem.Text());

                return fake.Generate();
            }
        }
        public static OrderViewModel Order
        {
            get
            {
                var fake = new Faker<OrderViewModel>()
                    .RuleFor(u => u.Id, f => f.UniqueIndex)
                    .RuleFor(u => u.Complete, false)
                    .RuleFor(u => u.PetId, f => f.UniqueIndex)
                    .RuleFor(u => u.Quantity, f => f.Random.Int())
                    .RuleFor(u => u.ShipDate, f => f.Date.Past())
                    .RuleFor(u => u.Status, f => f.Lorem.Text());
                return fake.Generate();
            }
        }

        public static PetViewModel Pet
        {
            get
            {
                var fake = new Faker<PetViewModel>()
                    .RuleFor(u => u.Id, f => f.UniqueIndex)
                    .RuleFor(u => u.Category, f => Category)
                    .RuleFor(u => u.Name, f => f.Name.FirstName())
                    .RuleFor(u => u.PhotoUrls, f => new List<string> { "https://www.google.com.br" })
                    .RuleFor(u => u.Status, f => f.Lorem.Text())
                    .RuleFor(u => u.Tags, f => new List<TagViewModel> { Tag, Tag, Tag });
                return fake.Generate();
            }
        }

        public static TagViewModel Tag
        {
            get
            {
                var fake = new Faker<TagViewModel>()
                    .RuleFor(u => u.Id, f => f.UniqueIndex)
                    .RuleFor(u => u.Name, f => f.Lorem.Text());
                return fake.Generate();
            }
        }

        public static UserViewModel User
        {
            get
            {
                var fake = new Faker<UserViewModel>()
                    .RuleFor(u => u.Id, f => f.UniqueIndex)
                    .RuleFor(u => u.Email, f => f.Internet.Email())
                    .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                    .RuleFor(u => u.LastName, f => f.Name.LastName())
                    .RuleFor(u => u.Password, f => f.Hashids.Encode(1, 2, 3, 4))
                    .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
                    .RuleFor(u => u.UserStatus, f => f.Random.Int())
                    .RuleFor(u => u.Username, f => f.Internet.UserName());
                return fake.Generate();
            }
        }
    }
}
