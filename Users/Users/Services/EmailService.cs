using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Users.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            var mail = new MailMessage(new MailAddress("treetalk95@gmail.com"),new MailAddress("1242450527@qq.com"));
            //MailMessage mail = new MailMessage();
            NetworkCredential cred = new NetworkCredential("treetalk95@gmail.com", "Sedion-pct951006");
            //收件者
            mail.To.Add(new MailAddress("1242450527@qq.com"));
            mail.Subject = message.Subject;
            //寄件者
            mail.From = new System.Net.Mail.MailAddress("treetalk95@gmail.com");
            mail.IsBodyHtml = true;
            mail.Body = message.Body;
            //設定SMTP
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = cred;
            smtp.Port = 587;
            //送出Mail
            await smtp.SendMailAsync(mail);
        }
    }
}