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
        static MockDataFactory _instance = new MockDataFactory();

        public static MockDataFactory Default
        {
             get { return _instance; }
        }

        readonly IList<User> _users = null;
        readonly IList<Group> _groups = null;
        readonly IDictionary<int, IList<int>> _groupToUsers = null;

        private MockDataFactory()
        {
            _groups = new List<Group>();
            _users = new List<User>();
            _groupToUsers = new Dictionary<int, IList<int>>();

            CreateGroups();
            CreateUsers();
            PopulateGroupsWithFriends();
        }

        void CreateUsers()
        {
            _users.Add(new User { Id = 1, Username = "Oscar Arrivi", Avatar = 11 });
            _users.Add(new User { Id = 2, Username = "Luis Astorga", Avatar = 10 });
            _users.Add(new User { Id = 3, Username = "Jose Carlos", Avatar = 9 });
            _users.Add(new User { Id = 4, Username = "Alejandro Carpena", Avatar = 8 });
            _users.Add(new User { Id = 5, Username = "Victor Muñoz", Avatar = 7 });
            _users.Add(new User { Id = 6, Username = "Ismael Moreno", Avatar = 6 });
            _users.Add(new User { Id = 7, Username = "Maruxa", Avatar = 5 });
        }

        void CreateGroups()
        {
            _groups.Add(new Group { Id = 1, ImageId = 0, SettledUp = false, Name = "Gastos comunes" });
            _groups.Add(new Group { Id = 2, ImageId = 4, SettledUp = false, Name = "Viaje a Granada" });
            _groups.Add(new Group { Id = 3, ImageId = 7, SettledUp = false, Name = "Regalo de Chicui" });
        }

        void PopulateGroupsWithFriends()
        {
            _groupToUsers.Add(1, new List<int>(new int[] { 1, 7 }));
            _groupToUsers.Add(2, new List<int>(new int[] { 1, 2, 3, 6 }));
            _groupToUsers.Add(3, new List<int>(new int[] { 1, 3, 4, 5, 6 }));
        }

        IEnumerable<ObservableUserSummary> GetFriends()
        {
            Random rand = new Random();
            return _users
                .Select(user => new ObservableUserSummary
                {
                    UserId = user.Id,
                    AvatarId = user.Avatar,
                    Username = user.Username,
                    AmountOwed = Convert.ToDecimal((rand.NextDouble() - 0.5) * 100)
                })
                .ToList();
        }

        public IEnumerable<ObservableGroupSummary> GetGroups()
        {
            return _groups
                .Select(g => new ObservableGroupSummary
                {
                    Id = g.Id,
                    Name = g.Name,
                    ImageId = g.ImageId,
                    SettledUp = g.SettledUp,
                    AmountOwed = g.SettledUp ? decimal.Zero : Convert.ToDecimal(new Random().NextDouble() -0.5 * 100)
                })
                .ToList();
        }

        public IEnumerable<ObservableUserSummary> GetFriendsOf(int id)
        {
            return GetFriends()
                .Where(CreateFriends => CreateFriends.UserId != id)
                .ToList();
        }

        public ObservableUserSummary GetFriendSummary(int id)
        {
            var friends = GetFriends();

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

        public ObservableGroupSummary GetGroupSummary(int id)
        {
            return GetGroups().FirstOrDefault(g => g.Id == id);
        }
    }
}
