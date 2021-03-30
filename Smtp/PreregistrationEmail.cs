namespace ProyectoSalud.API.Smtp
{
    public class PreregistrationEmail
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public int PreRegisterQuizId { get; set; }
        public int SignedCourseId { get; set; }
        public string SignedCourseName { get; set; }
    }
}