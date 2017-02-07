using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Akokina.Data.Entity;

namespace Akokina.Data.Tests
{
    [TestClass]
    public class UsersDataTests
    {
        [TestMethod]
        public void EmptyLocalAndRemoteRepositoryReturnsAnEmptyList()
        {
            ILocalDataManager localRepo = new LocalDataManager();
            IRemoteDataManager remoteRepo = new RemoteDataManager();
            IDataManager dataManager = new DataManager(localRepo, remoteRepo);

            IEnumerable<User> users = dataManager.GetUsers();

            Assert.IsFalse(users.Any());
        }

        [TestMethod]
        public void LoadUsersOffline_ReturnDataFromLocalRepo()
        {
            // Local data is empty
            ILocalDataManager localData = new LocalDataManager();
            localData.AddUser(new User
            {
                Id = 1,
                Username = "username",
                Initials = "US",
                Email = "username@email.com",
                Avatar = 0,
                //Color = "Yellow"
            });

            // Remote data has some entities
            IRemoteDataManager remoteData = new RemoteDataManager(); 

            IDataManager dataManager = new DataManager(localData, remoteData);

            Assert.IsFalse(remoteData.IsOnline());
            IEnumerable<User> users = dataManager.GetUsers();

            Assert.IsTrue(users.Any());
            Assert.AreEqual(1, users.First().Id);
        }

        [TestMethod]
        public void LoadUsersOnline_SyncAndReturnDataFromLocalRepo()
        {
            var user1 = new User { Id = 1, Username = "user1" };
            var user2 = new User { Id = 2, Username = "user2" };

            // Local data is empty
            ILocalDataManager localData = new LocalDataManager();
            localData.AddUser(user1);

            // Remote data has some entities
            IRemoteDataManager remoteData = new RemoteDataManager(true);
            remoteData.AddUser(user1);
            remoteData.AddUser(user2);

            IDataManager dataManager = new DataManager(localData, remoteData);

            Assert.IsTrue(remoteData.IsOnline());
            var lastSyncBefore = dataManager.UsersLastSynchronizationUTC;

            IEnumerable<User> users = dataManager.GetUsers();

            Assert.IsTrue(users.Any());
            Assert.AreEqual(2, users.Count());
            Assert.IsTrue(users.Any(u => u.Id == 1));
            Assert.IsTrue(users.Any(u => u.Id == 2));

            var lastSyncAfter = dataManager.UsersLastSynchronizationUTC;

            Assert.AreNotEqual(lastSyncBefore, lastSyncAfter);
        }

        [TestMethod]
        public void AddUserOffline_UpdateLocalRepository()
        {
            var user1 = new User { Id = 1, Username = "user1" };

            ILocalDataManager localData = new LocalDataManager();
            IRemoteDataManager remoteData = new RemoteDataManager(false);
            IDataManager dataManager = new DataManager(localData, remoteData);
            localData.AddUser(user1);
            remoteData.AddUser(user1);

            Assert.IsFalse(remoteData.IsOnline());

            dataManager.AddUser(new User { Username = "new" });

            Assert.AreEqual(2, localData.GetUsers().Count(), "Local repository doesn't have expected number of users.");
            Assert.IsNotNull(localData.GetUsers().FirstOrDefault(user => user.Username == "new"));

            // remote repository Offline always return empty data.
            Assert.AreEqual(0, remoteData.GetUsers().Count(), "Remote repository doesn't have the expected number of users.");
        }

        [TestMethod]
        public void AddUserOnline_UpdateLocalAndRemoteRepository()
        {
            var user1 = new User { Id = 1, Username = "user1" };

            ILocalDataManager localData = new LocalDataManager();
            IRemoteDataManager remoteData = new RemoteDataManager(true);
            IDataManager dataManager = new DataManager(localData, remoteData);
            localData.AddUser(user1);
            remoteData.AddUser(user1);

            Assert.IsTrue(remoteData.IsOnline());

            dataManager.AddUser(new User { Username = "new" });

            Assert.AreEqual(2, localData.GetUsers().Count(), "Local repository doesn't have expected number of users.");
            Assert.IsNotNull(localData.GetUsers().FirstOrDefault(user => user.Username == "new"));

            Assert.AreEqual(2, remoteData.GetUsers().Count(), "Remote repository doesn't have the expected number of users.");
            Assert.IsNotNull(remoteData.GetUsers().FirstOrDefault(user => user.Username == "new"));
        }

        [TestMethod]
        public void AddUserOnline_ReportsNoPendingSync()
        {
            var user1 = new User { Id = 1, Username = "user1" };

            ILocalDataManager localData = new LocalDataManager();
            IRemoteDataManager remoteData = new RemoteDataManager(true);
            IDataManager dataManager = new DataManager(localData, remoteData);
            localData.AddUser(user1);
            remoteData.AddUser(user1);

            Assert.IsTrue(remoteData.IsOnline());

            dataManager.AddUser(new User { Username = "new" });

            Assert.AreEqual(2, localData.GetUsers().Count(), "Local repository doesn't have expected number of users.");
            Assert.IsNotNull(localData.GetUsers().FirstOrDefault(user => user.Username == "new"));

            Assert.AreEqual(2, remoteData.GetUsers().Count(), "Remote repository doesn't have the expected number of users.");
            Assert.IsNotNull(remoteData.GetUsers().FirstOrDefault(user => user.Username == "new"));

            Assert.IsFalse(dataManager.UsersToBeSynchronized.Any());
        }


    }
}
