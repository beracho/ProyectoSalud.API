namespace ProyectoSalud.API.Smtp
{
    public class RegisterEmail
    {
        public string ToEmail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
    }
}