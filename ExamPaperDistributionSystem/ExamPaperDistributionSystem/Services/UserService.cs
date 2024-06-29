using System;
using System.Collections.Generic;
using ExamPaperDistributionSystem.Models;
using ExamPaperDistributionSystem.Repositories;

namespace ExamPaperDistributionSystem.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public User Authenticate(string username, string password)
        {
            // Implement authentication logic (password hashing should be added for security)
            var user = _userRepository.GetUserByUsername(username);
            if (user != null && user.Password == password)
            {
                return user;
            }
            return null;
        }

        public User GetUserById(int userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public void AddUser(User user)
        {
            // Add validation and business logic as needed
               _userRepository.AddUser(user);
        }

        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
        }

        public void DeleteUser(int userId)
        {
            _userRepository.DeleteUser(userId);
        }
    }
}
