using System.IO;
using System.Threading.Tasks;
using ProyectoSalud.API.Smtp;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ProyectoSalud.API.Services
{
    public class MailService : IMailService
    {
        private readonly MailSettings _mailSettings;
        private readonly IConfiguration _config;
        public MailService(IOptions<MailSettings> mailSettings, IConfiguration config)
        {
            _mailSettings = mailSettings.Value;
            _config = config;
        }

        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;

            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendRecoveryKeyEmailAsync(RecoveryKeyEmail recoveryKeyEmail)
        {
            string FilePath = Directory.GetCurrentDirectory() + "/Smtp/Templates/RecoveryKeyTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[FirstName]", recoveryKeyEmail.FirstName + " " + recoveryKeyEmail.LastName).Replace("[UrlKey]", recoveryKeyEmail.UrlKey);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(recoveryKeyEmail.ToEmail));
            email.Subject = $"Password Recovery for your Account";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            var logDirectory = _config.GetSection("MailLogger").Value;
            using var smtp = new SmtpClient(new ProtocolLogger(logDirectory));
            smtp.CheckCertificateRevocation = false;

            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendVerifyEmailAsync(VerifyEmail verifyEmail)
        {
            string FilePath = Directory.GetCurrentDirectory() + "/Smtp/Templates/VerifyTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[FirstName]", verifyEmail.FirstName + " " + verifyEmail.LastName).Replace("[UrlKey]", verifyEmail.UrlKey);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(verifyEmail.ToEmail));
            email.Subject = $"Password Recovery for your Account";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;

            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendWelcomeEmailAsync(RegisterEmail request)
        {
            string FilePath = Directory.GetCurrentDirectory() + "/Smtp/Templates/RegisterTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText
            .Replace("[username]", request.UserName)
            .Replace("[email]", request.ToEmail)
            .Replace("[urlKey]", _config.GetSection("HostSettings:WebSiteUrl").Value);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"Welcome {request.FirstName}";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;

            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
            // smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendWelcomeEmailWithPasswordAsync(PreRegisterCourseWithEmail request)
        {
            string FilePath = Directory.GetCurrentDirectory() + "/Smtp/Templates/RegisterWithPasswordTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText
                .Replace("[username]", request.UserName)
                .Replace("[password]", request.Password)
                .Replace("[firstName]", request.FirstName)
                .Replace("[NameCourse]", request.SignedCourseName)
                .Replace("[programId]", request.SignedCourseId.ToString())
                .Replace("[urlKey]", _config.GetSection("HostSettings:WebSiteUrl").Value)
                .Replace("[enrollId]", request.SignedCourseId.ToString());
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"Welcome {request.FirstName}";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;

            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        //
        public async Task WebinarSendWelcomeEmailWithPasswordAsync(WebinarPreRegisterCourseWithEmail request)
        {
            string FilePath = Directory.GetCurrentDirectory() + "/Smtp/Templates/WebinarRegisterWithPasswordTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText
                .Replace("[username]", request.UserName)
                .Replace("[password]", request.Password)
                .Replace("[firstName]", request.FirstName)
                .Replace("[NameCourse]", request.SignedCourseName)
                .Replace("[courseId]", request.SignedCourseId.ToString())
                .Replace("[moduleId]", request.SignedModuleId.ToString())
                .Replace("[urlKey]", _config.GetSection("HostSettings:WebSiteUrl").Value);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"Welcome {request.FirstName}";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;

            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
        //
        public async Task SendRegisterEmailWithPasswordAsync(PreRegisterCourseWithEmail request)
        {
            string FilePath = Directory.GetCurrentDirectory() + "/Smtp/Templates/RegisterEmailWithPasswordTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText
                .Replace("[username]", request.UserName)
                .Replace("[password]", request.Password)
                .Replace("[firstName]", request.FirstName)
                .Replace("[NameCourse]", request.SignedCourseName)
                .Replace("[urlKey]", _config.GetSection("HostSettings:WebSiteUrl").Value);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"Welcome {request.FirstName}";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;

            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public string GetVerifyURL(string verifyKey, string action)
        {
            var completeUrl = _config.GetSection("HostSettings:WebSiteUrl").Value + "/" + action + "/" + verifyKey;
            return completeUrl;
        }

        public async Task SendConfirmationRecoveryEmailAsync(RecoveryKeyEmail recoveryKeyEmail)
        {
            string FilePath = Directory.GetCurrentDirectory() + "/Smtp/Templates/ConfirmRecoveryTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[FirstName]", recoveryKeyEmail.FirstName + " " + recoveryKeyEmail.LastName);
            MailText = MailText.Replace("[WebSiteUrl]", _config.GetSection("HostSettings:WebSiteUrl").Value);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(recoveryKeyEmail.ToEmail));
            email.Subject = $"Recovery Account Confirmation";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;

            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task ProgramPreRegistrationTemplate(PreregistrationEmail PreregistrationRequest)
        {
            string FilePath = Directory.GetCurrentDirectory() + "/Smtp/Templates/PreregistrationTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();

            MailText = MailText
            .Replace("[username]", PreregistrationRequest.UserName)
            .Replace("[email]", PreregistrationRequest.Email)
            .Replace("[NameCourse]", PreregistrationRequest.SignedCourseName)
            .Replace("[programId]", PreregistrationRequest.SignedCourseId.ToString())
            .Replace("[urlKey]", _config.GetSection("HostSettings:WebSiteUrl").Value)
            .Replace("[enrollId]", PreregistrationRequest.PreRegisterQuizId.ToString());
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(PreregistrationRequest.Email));
            email.Subject = $"Successfull pre-registration";

            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;

            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SentInvitations(InvitationRequest invitationRequest)
        {
            string FilePath = Directory.GetCurrentDirectory() + "/Smtp/Templates/InvitationTemplate.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();

            MailText = MailText
            .Replace("[email]", invitationRequest.ToEmail);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(invitationRequest.ToEmail));
            email.Subject = $"You have been invited to a virtual class";

            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.CheckCertificateRevocation = false;

            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.Auto);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}