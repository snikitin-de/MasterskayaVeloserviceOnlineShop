using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Services
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IWebHostEnvironment _appEnvironment;
        private const string AvatarsPath = @"/images/avatars";

        public UserService(UserManager<User> userManager, IHttpContextAccessor httpContext, IWebHostEnvironment appEnvironment)
        {
            _userManager = userManager;
            _httpContext = httpContext;
            _appEnvironment = appEnvironment;
        }

        public async Task<UserVM?> GetAuthorizedUserAsync()
        {
            var user = _httpContext.HttpContext?.User;

            if (user != null)
            {
                if (user.Identity.IsAuthenticated)
                {
                    var userId = _userManager.GetUserId(user);
                    var authorizedUser = await _userManager.Users
                       .Include(u => u.Avatar)
                       .FirstOrDefaultAsync(u => u.Id == userId);

                    return Mapper.ToUserVM(authorizedUser);
                }
            }

            return null;
        }

        public async Task<UserVM?> GetAsync(string login)
        {
            var user = await _userManager.Users
                .Include(u => u.Avatar)
                .FirstOrDefaultAsync(u => u.UserName == login);
            var userVM = Mapper.ToUserVM(user);
            var currentUserRoles = await _userManager.GetRolesAsync(user);

            if (currentUserRoles.Any())
            {
                var roleVM = new RoleVM()
                {
                    Name = currentUserRoles.FirstOrDefault()
                };

                userVM.Role = roleVM;
            }

            return userVM;
        }

        public async Task<List<UserVM>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var usersVM = new List<UserVM>();

            foreach (var user in users)
            {
                var userVM = Mapper.ToUserVM(user);

                if (userVM != null)
                {
                    usersVM.Add(userVM);
                }
            }

            return usersVM;
        }

        public async Task<bool> AddAsync(UserVM userVM)
        {
            if (userVM != null)
            {
                var user = Mapper.ToUser(userVM);
                var result = await _userManager.CreateAsync(user, userVM.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Constants.UserRoleName);

                    return result.Succeeded;
                }
            }

            return false;
        }

        public async Task<bool> UpdateAsync(UserVM userVM)
        {
            var user = await _userManager.FindByNameAsync(userVM.Login);

            if (user != null)
            {
                user.UserName = userVM.Login;
                user.Firstname = userVM.Firstname;
                user.Lastname = userVM.Lastname;
                user.Middlename = userVM.Middlename;
                user.Email = userVM.Login;
                user.PhoneNumber = userVM.Phone;
                user.Address = userVM.Address;
                user.Avatar = userVM.Avatar;

                var result = await _userManager.UpdateAsync(user);

                return result.Succeeded;
            }

            return false;
        }

        public async Task<bool> RemoveAsync(UserVM userVM)
        {
            var user = await _userManager.FindByNameAsync(userVM.Login);

            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);

                return result.Succeeded;
            }

            return false;
        }

        public async Task<bool> ChangePasswordAsync(UserVM userVM)
        {
            var user = await _userManager.FindByNameAsync(userVM.Login);

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, token, userVM.Password);

                return result.Succeeded;
            }

            return false;
        }

        public async Task<bool> ChangeRoleAsync(UserVM userVM, string roleName)
        {
            var user = await _userManager.FindByNameAsync(userVM.Login);
            var currentRoles = await _userManager.GetRolesAsync(user);

            await _userManager.RemoveFromRoleAsync(user, currentRoles.First());

            var addRoleResult = await _userManager.AddToRoleAsync(user, roleName);

            return addRoleResult.Succeeded;
        }

        public async Task<bool> IsExistsAsync(UserVM user)
        {
            return await _userManager.Users.AnyAsync(x => x.UserName == user.Login);
        }

        public async Task<Image> SaveAvatarAsync(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                var productImagePath = Path.Combine(_appEnvironment.WebRootPath + AvatarsPath);

                if (!Directory.Exists(productImagePath))
                {
                    Directory.CreateDirectory(productImagePath);
                }

                var fileName = Guid.NewGuid() + "." + uploadedFile.FileName.Split('.').Last();

                using (var fileStream = new FileStream(productImagePath + fileName, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                return new Image { Path = AvatarsPath + fileName };
            }

            return new Image { Path = Constants.NoAvatarImagePath };
        }
    }
}
