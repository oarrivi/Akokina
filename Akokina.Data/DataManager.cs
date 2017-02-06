using Akokina.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akokina.Data
{
    public interface IDataManager
    {
        DateTime UsersLastSynchronizationUTC { get; }
        IEnumerable<User> UsersToBeSynchronized { get; }
        IEnumerable<User> GetUsers();
        User AddUser(User entity);
    }

    public class DataManager : IDataManager
    {
        readonly ILocalDataManager _userLocalRepository = null;
        readonly IRemoteDataManager _userRemoteRepository = null;

        public DataManager(ILocalDataManager userLocalRepository,
                           IRemoteDataManager userRemoteRepository)
        {
            _userLocalRepository = userLocalRepository;
            _userRemoteRepository = userRemoteRepository;
            UsersLastSynchronizationUTC = new DateTime(2000, 1, 1);
        }

        public DateTime UsersLastSynchronizationUTC
        {
            get;
            private set;
        }

        public IEnumerable<User> UsersToBeSynchronized
        {
            get
            {
                return _userLocalRepository.NotSynchronizedUsers;
            }
        }

        public IEnumerable<User> GetUsers()
        {
            TrySynchronizeUsers();

            return _userLocalRepository.GetUsers();
        }

        void TrySynchronizeUsers()
        {
            if (_userRemoteRepository.IsOnline())
            {
                if (_userRemoteRepository.UsersLastUpdateUTC >= this.UsersLastSynchronizationUTC)
                {
                    var users = _userRemoteRepository.GetUsers();

                    if (users != null || users.Any())
                    {
                        _userLocalRepository.DeleteUsers();
                        _userLocalRepository.AddUsers(users);
                        this.UsersLastSynchronizationUTC = DateTime.UtcNow;
                    }
                }
            }
        }

        public User AddUser(User entity)
        {
            if (_userRemoteRepository.IsOnline())
            {
                entity = _userRemoteRepository.AddUser(entity);
                TrySynchronizeUsers();
            }
            else
            {
                _userLocalRepository.AddUser(entity);
            }

            return entity;
        }

    }
}
