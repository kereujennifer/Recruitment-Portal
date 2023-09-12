

using Auth.Models.Database;
using Auth.Data;
using Auth.Services;
using System;
using System.Linq;

namespace Auth.Services
{
	public interface IClientSetupService
	{
		ReturnData<Setting> Read();
	}
	public class ClientSetupService : IClientSetupService
	{
		private readonly ApplicationDbContext _context;
		public ClientSetupService(ApplicationDbContext  context)
		{
			_context = context;
		}
		public ReturnData<Setting> Read()
		{
			var res = new ReturnData<Setting>
			{
				Data = new Setting()
			};
			try
			{

				var dbSetting = _context.Settings.FirstOrDefault() ?? new Setting();
				res.Data = dbSetting;
				res.Success = true;
				res.Message = "Found";
				return res;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				res.Message = "Error Occured. Try again";
				res.ErrorMessage = e.Message;
				return res;
			}
		}

	}
}
