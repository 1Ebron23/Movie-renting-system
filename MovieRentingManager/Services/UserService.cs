using MovieRentingManager.Interfaces;
using MovieRentingManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRentingManager.Services
{
    public class UserService : IUserService
    {
        private static UserService _instance;
        private List<User> _users = new List<User>();
        private int _nextId = 1; // Start with ID 1

        private UserService()
        {
            // Initialize with test users
            _users.Add(new User { Id = _nextId++, Name = "John Doe", Email = "" });
        }

        // Singleton access method
        public static UserService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserService();
                }
                return _instance;
            }

        }
        public int GetNextId()
        {
            return _nextId++;
        }

        public bool RemoveUser(int userId)
        {
            User? userToRemove = _users.FirstOrDefault(u => u.Id == userId);

            if (userToRemove == null)
            {
                return false;
            }

            _users.Remove(userToRemove);
            return true;
        }

        public bool UpdateUser(User user)
        {
            User? userToUpdate = _users.FirstOrDefault(u => u.Id == user.Id);

            if (userToUpdate == null)
            {
                return false;
            }

            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;
            return true;
        }

        public bool AddUser(User user)
        {
            user.Id = GetNextId();
            _users.Add(user);
            return true;
        }

        public IEnumerable<User> GetUsers()
        {
            return _users;
        }
    }

}
