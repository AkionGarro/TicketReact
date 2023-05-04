using System.Net;
using System.Net.Mail;

namespace BZPAY_BE.Helpers
{
    public class MailHelper
    {
        public static void RecoverPasswordSendMail(string targetMail, string subject, string body)
        {
            try
            {
                var smtp = new SmtpClient("smtp.outlook.office365.com");
                var mail = new MailMessage();
                string userFrom = "lleiton@bzpay.com.mx"; //system account.
                mail.From = new MailAddress(userFrom);
                mail.To.Add(new MailAddress(targetMail));//target account
                mail.Subject = subject;     //subject
                mail.Body = body;           //body message
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(userFrom, "Cost@Ry22!");
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
