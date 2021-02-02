using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace SportsStore.Models {
    public static class IdentitySeedData {
        private const string AdminLogin = "Admein";
        private const string AdminPassword = "Admin123$";

        public static async void EnsurePopulated(IApplicationBuilder builder) {
            var manager = builder.ApplicationServices.GetRequiredService<UserManager<IdentityUser>>();
            var user = await manager.FindByIdAsync(AdminLogin);
            if (user == null) {
                user = new IdentityUser("Admin");
                await manager.CreateAsync(user, AdminPassword);
            }
        }
    }
}