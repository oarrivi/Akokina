using Akokina.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akokina.Model
{
    public class MockDataFactory
    {
        internal IEnumerable<User> CreateUsers()
        {
            return new User[]
            {
                new User { Id = 1, Username = "Oscar Arrivi", Avatar = 11 },
                new User { Id = 2, Username = "Luis Astorga", Avatar = 10 },
                new User { Id = 3, Username = "Jose Carlos", Avatar = 9 },
                new User { Id = 4, Username = "Alejandro Carpena", Avatar = 8 },
                new User { Id = 5, Username = "Victor Muñoz", Avatar = 7 },
                new User { Id = 6, Username = "Ismael Moreno", Avatar = 6 },
            };
        }

        internal IEnumerable<Group> CreateGroups()
        {
            return new Group[]
            {
                new Group { Id = 1, ImageId = 0, SettledUp = false, Name = "Gastos comunes" },
                new Group { Id = 2, ImageId = 1, SettledUp = false, Name = "Viaje a Granada" },
                new Group { Id = 3, ImageId = 2, SettledUp = false, Name = "Regalo de Chicui" }
            };
        }

        internal IEnumerable<ObservableUserSummary> CreateFriends()
        {
            Random rand = new Random();
            return CreateUsers()
                .Select(user => new ObservableUserSummary
                {
                    UserId = user.Id,
                    AvatarId = user.Avatar,
                    Username = user.Username,
                    AmountOwed = Convert.ToDecimal((rand.NextDouble() - 0.5) * 100)
                });
        }

        public IEnumerable<ObservableUserSummary> GetFriends(int id)
        {
            return CreateFriends()
                .Where(CreateFriends => CreateFriends.UserId != id)
                .ToList();
        }

        public ObservableUserSummary GetFriendSummary(int id)
        {
            var friends = CreateFriends();

            var friend = friends.FirstOrDefault(f => f.UserId == id);

            if (friend != null)
            {
                return new ObservableUserSummary
                {
                    UserId = friend.UserId,
                    Username = friend.Username,
                    AvatarId = friend.AvatarId,
                    AmountOwed = friends.Where(f => f.UserId != id).Sum(f => f.AmountOwed)
                };
            }
            return null;
        }

    }
}
