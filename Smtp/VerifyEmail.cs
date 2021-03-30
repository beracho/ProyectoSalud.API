namespace ProyectoSalud.API.Smtp
{
    public class VerifyEmail
    {
        public string ToEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string VerifyKey { get; set; }
        public string UrlKey { get; set; }

        public VerifyEmail(string toEmail){
            ToEmail = toEmail;
        }
    }
}