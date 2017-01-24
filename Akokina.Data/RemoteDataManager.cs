using Akokina.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akokina.Data
{
    public interface IRemoteDataManager
    {
        bool IsOnline();
        DateTime UsersLastUpdateUTC { get; }
        IEnumerable<User> GetUsers();
        User AddUser(User entity);
    }

    public class RemoteDataManager : IRemoteDataManager
    {
        Dictionary<int, User> _data;
        bool _isOnline = false;

        public RemoteDataManager()
        {
            _data = new Dictionary<int, User>();
            this.UsersLastUpdateUTC = new DateTime(2000, 1, 1);
        }

        public DateTime UsersLastUpdateUTC { get; private set; }

        public RemoteDataManager(bool online) : this()
        {
            _isOnline = online;
        }

        public bool IsOnline()
        {
            return _isOnline;
        }

        public IEnumerable<User> GetUsers()
        {
            if (!IsOnline())
            {
                return new List<User>();
            }

            this.UsersLastUpdateUTC = DateTime.UtcNow;
            return _data.Values;
        }

        public User AddUser(User entity)
        {
            if (entity.Id == 0)
            {
                int newId = CalculateNextId();
                entity.Id = newId;
                _data.Add(newId, entity);
            }
            else
            {
                if (_data.ContainsKey(entity.Id))
                {
                    _data[entity.Id] = entity;
                }
                else
                {
                    _data.Add(entity.Id, entity);
                }
            }
            return entity;
        }

        int CalculateNextId()
        {
            if (_data.Any())
            {
                return _data.Keys.Max(key => key) + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}
