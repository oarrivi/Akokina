using Akokina.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akokina.Data
{
    public interface ILocalDataManager
    {
        IEnumerable<User> GetUsers();
        void AddUser(User entity);
        void AddUsers(IEnumerable<User> entities);
        void DeleteUsers();
        IEnumerable<User> NotSynchronizedUsers { get; }
    }

    public class LocalDataManager : ILocalDataManager
    {
        Dictionary<int, User> _data;
        Dictionary<int, User> _sync;

        public LocalDataManager()
        {
            _data = new Dictionary<int, User>();
        }

        public IEnumerable<User> GetUsers()
        {
            return _data.Values;
        }

        public void AddUser(User entity)
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

        public void AddUsers(IEnumerable<User> entities)
        {
            foreach(var entity in entities)
            {
                AddUser(entity);
            }
        }

        public void DeleteUsers()
        {
            _data.Clear();
        }

        public IEnumerable<User> NotSynchronizedUsers
        {
            get { return _sync.Values; }
        }
    }
}
