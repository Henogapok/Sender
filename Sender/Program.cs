using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace Sender
{
    internal class Program
    {
        static readonly string attPath = "C:\\Attachments(MailSender)\\";
        static void Main(string[] args)
        {
            
            string senderMail = "kskfbdoc@gmail.com";
            string senderAppPass = "apabuqxfoqdhtlzc";
            SmtpClient smtpClient = SetSmtpClient("smtp.gmail.com", 25, senderMail, senderAppPass);


            MailAddress from = new MailAddress(senderMail, "Death");
            Console.WriteLine("Введите Адрес получателя: ");
            string recipient = null;
            while (true)
            {
                recipient = Console.ReadLine();
                if (recipient.Contains("@"))
                {
                    break;
                }
                else Console.WriteLine("Вы ввели некорректный адрес!");
            }
            MailAddress to = new MailAddress(recipient);
            string body = "Test";
            string subject = "Test Subject";
            MailMessage mail = new MailMessage(from.Address, to.Address, body, subject);
            mail = AddAttachments(mail, attPath);

            
            smtpClient.Send(mail);
        }

        public static SmtpClient SetSmtpClient(string host, int port, string sender, string senderAppPass)
        {
            SmtpClient smtpClient = null;
            try
            {
                smtpClient = new SmtpClient("smtp.gmail.com", 25);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(sender, senderAppPass);

            }
            catch(SmtpException SE)
            {
                Console.WriteLine(SE.Message);
            }
            catch(Exception E)
            {
                Console.WriteLine(E.Message);
            }

            return smtpClient;
        }
        public static MailMessage AddAttachments(MailMessage mail, string attachmentPath)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(attachmentPath);
                FileInfo[] attachments = di.GetFiles();
                foreach (var x in attachments)
                    mail.Attachments.Add(new Attachment(x.FullName));
            }
            catch(Exception E)
            {
                Console.WriteLine(E.Message);
            }
            return mail;
        }
    }
}
