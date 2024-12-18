using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Services
{
    public class RoleService
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<List<RoleVM>> GetAllAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var rolesVM = new List<RoleVM>();

            foreach (var role in roles)
            {
                var roleVM = Mapper.ToRoleVM(role);

                if (roleVM != null)
                {
                    rolesVM.Add(roleVM);
                }
            }

            return rolesVM;
        }

        public async Task<RoleVM?> GetByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            return Mapper.ToRoleVM(role);
        }

        public async Task<bool> AddAsync(RoleVM roleVM)
        {
            if (roleVM != null)
            {
                var role = Mapper.ToRole(roleVM);
                var result = await _roleManager.CreateAsync(role);

                return result.Succeeded;
            }

            return false;
        }

        public async Task<bool> UpdateAsync(RoleVM roleVM)
        {
            var role = await _roleManager.FindByIdAsync(roleVM.Id);

            if (role != null)
            {
                role.Name = roleVM.Name;

                var result = await _roleManager.UpdateAsync(role);

                return result.Succeeded;
            }

            return false;
        }

        public async Task<bool> RemoveAsync(RoleVM roleVM)
        {
            var role = await _roleManager.FindByNameAsync(roleVM.Name);

            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);

                return result.Succeeded;
            }

            return false;
        }

        public async Task<bool> IsExistsAsync(RoleVM roleVM)
        {
            return await _roleManager.Roles.AnyAsync(x => x.Name == roleVM.Name);
        }
    }
}
