using System.Threading.Tasks;
using ProyectoSalud.API.Smtp;

namespace ProyectoSalud.API.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
        Task SendWelcomeEmailAsync(RegisterEmail mailRequest);
        Task SendWelcomeEmailWithPasswordAsync(PreRegisterCourseWithEmail mailRequest);
        Task SendRegisterEmailWithPasswordAsync(PreRegisterCourseWithEmail mailRequest);
        Task SendRecoveryKeyEmailAsync(RecoveryKeyEmail recoveryKeyEmail);
        string GetVerifyURL(string verifyKey, string action);
        Task SendConfirmationRecoveryEmailAsync(RecoveryKeyEmail recoveryKeyEmail);
        Task SendVerifyEmailAsync(VerifyEmail verifyEmail);
        Task ProgramPreRegistrationTemplate(PreregistrationEmail PreregistrationRequest);
        Task SentInvitations(InvitationRequest invitationRequest);
        Task WebinarSendWelcomeEmailWithPasswordAsync(WebinarPreRegisterCourseWithEmail request);
    }
}