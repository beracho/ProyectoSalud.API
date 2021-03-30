namespace ProyectoSalud.API.Smtp
{
    public class RecoveryKeyEmail
    {
        public string ToEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UrlKey { get; set; }

        public RecoveryKeyEmail(string toEmail,string firstName,string lastName,string urlKey){
            ToEmail = toEmail;
            FirstName = firstName;
            LastName = lastName;
            UrlKey = urlKey;
        }
    }
}