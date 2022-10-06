using MimeKit.Text;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace MockProject.Service
{
	public class MailKitService
	{
		public Task SendEmail(string emailAddress, string body, string subject)
		{
			var message = new MimeMessage();
			message.From.Add(new MailboxAddress("ITERNTH MockProject Service", "trunghauchannhan@gmail.com"));

			message.To.Add(new MailboxAddress(subject, emailAddress));

			message.Subject = subject;

			message.Body = new TextPart(TextFormat.Html) { Text = body };

			try
			{
				using (var client = new MailKit.Net.Smtp.SmtpClient())
				{
					client.Connect("smtp.gmail.com", 587);
					client.Authenticate("trunghauchannhan@gmail.com", "jaqfckhooorfkpvj");
					client.Send(message);
					client.Disconnect(true);
				}

			}
			catch (Exception e)
			{
				throw;
			}

			return Task.CompletedTask;
		}
	}
}
