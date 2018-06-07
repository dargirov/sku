using Administration.Entities;
using Infrastructure.Data.Common;
using Infrastructure.Services.Common;
using System;
using System.Threading.Tasks;

namespace Administration.Bll
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly IUserServices _userServices;
        private readonly ICacheServices _cacheServices;

        public AuthenticationServices(IUserServices userServices, ICacheServices cacheServices)
        {
            _userServices = userServices;
            _cacheServices = cacheServices;
        }

        public async Task<bool> LoginAsync(string email, string password, Messages messages)
        {
            if (!ValidateInputFormat(email, password, messages))
            {
                return false;
            }

            var user = await _userServices.GetByEmailAndPasswordAsync(email, password);
            if (user == null)
            {
                messages.AddWarning("Invalid email address or password");
                return false;
            }

            SetupCacheData(user);
            return await UpdateActivity(user, messages);
        }

        public void Logout()
        {
            CleanupCacheData();
        }

        public Task<User> GetCurrentUserAsync()
        {
            var userId = _cacheServices.Get<int>("user_id");
            return _userServices.GetByIdAsync(userId);
        }

        private bool ValidateInputFormat(string email, string password, Messages messages)
        {
            bool isValid = true;

            if (string.IsNullOrWhiteSpace(email) || email.Length < 5 || email.Length > 256)
            {
                messages.AddWarning("Invalid email length");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(password) || password.Length < 3 || password.Length > 30)
            {
                messages.AddWarning("Invalid password length");
                isValid = false;
            }

            return isValid;
        }

        private void SetupCacheData(User user)
        {
            _cacheServices.Set<bool>("logged_in", true);
            _cacheServices.Set<string>("email", user.Email);
            _cacheServices.Set<DateTime>("loggin_date", DateTime.Now);
            _cacheServices.Set<int>("organization_id", user.OrganizationId);
            _cacheServices.Set<int>("user_id", user.Id);
            _cacheServices.Set<string>("user_name", $"{user.FirstName} {user.LastName}");
            _cacheServices.Set<bool>("is_admin", user.IsAdmin);
            _cacheServices.Set<string>("organization_name", user.Organization.Name);
            _cacheServices.Set<string>("tenant_id", user.TenantId.ToString());
        }

        private void CleanupCacheData()
        {
            _cacheServices.Remove("logged_in");
            _cacheServices.Remove("email");
            _cacheServices.Remove("loggin_date");
            _cacheServices.Remove("organization_id");
            _cacheServices.Remove("user_id");
            _cacheServices.Remove("user_name");
            _cacheServices.Remove("is_admin");
            _cacheServices.Remove("organization_name");
        }

        private Task<bool> UpdateActivity(User user, Messages messages)
        {
            user.LastLogIn = DateTime.Now;
            user.LastActivity = DateTime.Now;
            return _userServices.EditAsync(user, messages);
        }
    }
}
