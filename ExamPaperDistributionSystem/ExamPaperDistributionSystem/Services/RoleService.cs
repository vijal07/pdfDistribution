using System;
using System.Collections.Generic;
using ExamPaperDistributionSystem.Models;
using ExamPaperDistributionSystem.Repositories;

namespace ExamPaperDistributionSystem.Services
{
    public class RoleService
    {
        private readonly RoleRepository _roleRepository;

        public RoleService()
        {
            _roleRepository = new RoleRepository();
        }

        public List<Role> GetAllRoles()
        {
            return _roleRepository.GetAllRoles();
        }

        public Role GetRoleById(int roleId)
        {
            return _roleRepository.GetRoleById(roleId);
        }

        public void AddRole(Role role)
        {
            // Add business logic here if needed
            _roleRepository.AddRole(role);
        }

        public void UpdateRole(Role role)
        {
            // Add business logic here if needed
            _roleRepository.UpdateRole(role);
        }

        public void DeleteRole(int roleId)
        {
            // Add business logic here if needed
            _roleRepository.DeleteRole(roleId);
        }
    }
}
