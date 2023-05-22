using System.Net.Mail;
using System.Configuration;
using NLog;

namespace EmailSendApp
{
    internal class Program
    {

        private static Logger logger = LogManager.GetLogger("nlog.config");

        static void Main(string[] args)
        {
            try
            {
                MailMessage mail = new MailMessage();

                SmtpClient smtpClient = new SmtpClient(GetConfigurationValue("smtpClient"));
                mail.From = new MailAddress(GetConfigurationValue("mailFrom")); 
                mail.To.Add(GetConfigurationValue("mailTo"));
                mail.Subject = "Test";
                mail.Body = "Test Body";
                
                logger.Info("Email is preparing...");
                logger.Info(mail.Subject);
                logger.Info(mail.Body);

                smtpClient.Port = int.Parse(GetConfigurationValue("smtpClientPort"));
                smtpClient.Credentials = new System.Net.NetworkCredential(GetConfigurationValue("smtpClientCredentialsUsername"), GetConfigurationValue("smtpClientCredentialsPassword"));
                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);

                logger.Info("Email sent successfully");

            }
           catch (Exception ex)
            {
                logger.Error(ex);

            }
        }

        public static string GetConfigurationValue(string key)
        {
            return ConfigurationManager.AppSettings.Get(key) ?? "Default Value";
        }
    }
}

