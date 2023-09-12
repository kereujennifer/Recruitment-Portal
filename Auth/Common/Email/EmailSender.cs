using Auth.Models.Database;
using Auth.Data;
using Auth.Services;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Auth.Common.Email
{
    public interface IEmailSender
	{
		Task<ReturnData<string>> SendEmailAsync(string email, string subject, string message);
	}

	public class EmailSender : IEmailSender
	{
		private readonly ApplicationDbContext _context;
		private readonly IClientSetupService _settingsService;
		public EmailSender(ApplicationDbContext context,
			IClientSetupService settingsService)
		{
			_context = context;
			_settingsService = settingsService;
		}
		public async Task<ReturnData<string>> SendEmailAsync(string email, string subject, string message)
		{
			var emailMessage = new EmailMessage
			{
				ToAddresses = new List<EmailAddress> { new EmailAddress { Address = email } },
				Subject = subject,
				Content = message
			};
			var res = await SendEmailAsync(emailMessage, EmailSetting());
			return res;
		}

		private Setting EmailSetting(string senderName = "", string senderEmail = "")
		{
			var settings = _context.Settings
				   .FirstOrDefault();
			if (settings == null)
			{
				return null;
			}
			if (!string.IsNullOrEmpty(senderName))
				settings.SMTPUserName = senderName;
			if (!string.IsNullOrEmpty(senderEmail))
				settings.SenderFromEmail = senderEmail;
			return settings;
		}

		private async Task<ReturnData<string>> SendEmailAsync(EmailMessage emailMessage, Setting settings)
		{
			var res = new ReturnData<string>();
			try
			{
				if (settings == null)
				{
					res.Message = "Email settings not set";
					return res;
				}
				emailMessage.Content = $"{emailMessage.Content}";
				var message = new MimeMessage();
				message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
				message.From.Add(new MailboxAddress(settings.SMTPUserName, settings.SenderFromEmail));
				message.Subject = emailMessage.Subject;

				var clSetings = _settingsService.Read().Data;
				var builder = new BodyBuilder();

				builder.HtmlBody = emailMessage.Content;
				message.Body = builder.ToMessageBody();
				using (var emailClient = new SmtpClient())
				{
					var smtpServer = settings.SMTServer;
					var smtpPort = int.Parse(settings.SMTPPort);
					var smtpUsername = settings.SMTPUserName;
					var smtpPassword = settings.SMTPPassword;
					//try
					//{
					//	emailClient.Connect(smtpServer, smtpPort);
					//	emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
					//	emailClient.Authenticate(smtpUsername, smtpPassword);
					//}
					//catch (Exception ex)
					//{
					//	Console.WriteLine(ex);
					//	res.Message = "Mail Error occured. Try again later";
					//	res.ErrorMessage = ex.Message;
					//	return res;
					//}
					//await emailClient.SendAsync(message);
					//await emailClient.DisconnectAsync(true);
				}

				res.Success = true;
				res.Message = "Sent";
				res.Data = message.MessageId;
				return res;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				res.Message = "Error occured. Try again later";
				res.ErrorMessage = ex.Message;
				return res;
			}
		}

	}


}
