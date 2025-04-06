using System.Net;
using System.Net.Mail;

namespace Company.Day02.PL.Healpers
{
	public class EmailSettings
	{
        public static bool SendEmail(Email email)
		{

			try
			{
				var client = new SmtpClient("smtp.gmail.com", 587);
				client.EnableSsl = true;
				client.Credentials = new NetworkCredential("muhmdmagdy772@gmail.com", "myqvfsqculunnufp");
				client.Send("muhmdmagdy772@gmail.com", email.To, email.Subject, email.Body);


				return true;
			}
			catch(Exception e)
			{
				return false;
			}
		}
		
    }
}
