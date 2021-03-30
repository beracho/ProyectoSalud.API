namespace ProyectoSalud.API.Smtp
{
    public class ConfirmRegistration
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UrlKey { get; set; }
        public string SignedCourseId { get; set; }
    }
}