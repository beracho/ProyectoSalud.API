namespace ProyectoSalud.API.Dtos
{
    public class UserForRecoveryVerifyDto
    {
        public string Email { get; set; } 
        public string Password { get; set; } 
        public string VerifyKey { get; set; } 
    }
}