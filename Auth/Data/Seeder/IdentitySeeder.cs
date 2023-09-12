using Auth.Models;
using Auth.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auth.Data.Seeder
{
    public static class IdentitySeeder
	{

		public static async Task IdentitySeed(IServiceProvider serviceProvider, ApplicationDbContext context)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			var roleNames = new List<RoleVM>();
			roleNames.Add(new RoleVM()
			{
				enumCode = 1,
				Role = "Admin"
			});
			roleNames.Add(new RoleVM()
			{
				enumCode = 2,
				Role = "User"
			});

			IdentityResult roleResult;
			foreach (var roleName in roleNames)
			{
				var roleExist = await roleManager.RoleExistsAsync(roleName.Role.ToString());
				if (!roleExist)
				{
					roleResult = await roleManager.CreateAsync(new IdentityRole(roleName.Role.ToString()));
				}
			}
			var userEmail = "admin@abno.com";
			var poweruser = new ApplicationUser
			{
				UserName = userEmail,
				Email = userEmail,
				PhoneNumber = "0712345678",
				EmailConfirmed = true,
				Active = true,
				Name = "ABN ADMIN",
				ActivationStatus=true
			

			};

			string userPassword = "123456";
			var user = await userManager.FindByEmailAsync(userEmail);
			if (user == null)
			{
				var createPowerUser = await userManager.CreateAsync(poweruser, userPassword);
				if (createPowerUser.Succeeded)
				{
					await userManager.AddToRoleAsync(poweruser, "Admin");	
				}
				
			}
			
			




		}

	}
}
