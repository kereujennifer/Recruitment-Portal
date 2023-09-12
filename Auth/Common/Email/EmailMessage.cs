
using Auth.Models.Database;
using Auth.Models;
using System.Collections.Generic;

namespace Auth.Common.Email
{
	public class EmailMessage
	{
		public EmailMessage()
		{
			ToAddresses = new List<EmailAddress>();
			FromAddresses = new List<EmailAddress>();
		}

		public EmailMessage(ApplicationUser user, string subject, string content)
		{
			ToAddresses = new List<EmailAddress> { new EmailAddress {
				Name=user.Name,
				Address=user.Email
			} };
			Subject = subject;
			Content = content;
		}

		public List<EmailAddress> ToAddresses { get; set; }
		public List<EmailAddress> FromAddresses { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }
	}
}