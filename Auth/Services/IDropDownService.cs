using Auth.Models.Database;
using Auth.Data;
using Auth.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services
{
	public interface IDropDownService
	{
        Task<List<Product>> products();
    }

	public class DropDownService : IDropDownService
	{
		private readonly ApplicationDbContext _context;
		public DropDownService(ApplicationDbContext context)
		{
			_context = context;
		}

        public async Task<List<RoleVM>> Roles()
        {
            try
            {
                var data = new List<RoleVM>();
              
                    data.Add(new RoleVM
                    {
                        enumCode = 1,
                        Role = "Admin"

                    });
                    data.Add(new RoleVM
                    {
                        enumCode = 2,
                        Role = "Users"

                    });
              

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting roles :- {ex}");
                return new List<RoleVM>();
            }
        }
        public Task<List<Product>> Product()
        {
            try
            {
                var data = _context.Product.ToList();

                return Task.FromResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting Event categories :- {ex}");
                return Task.FromResult(new List<Product>());
            }
        }

        public Task<List<Product>> products()
        {
            try
            {
                var data = _context.Product.ToList();

                return Task.FromResult(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting Event categories :- {ex}");
                return Task.FromResult(new List<Product>());
            }
        }
    }
}
