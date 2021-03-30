namespace ProyectoSalud.API.Smtp
{
    public class WebinarPreRegisterCourseWithEmail
    {
        public string ToEmail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public int PreRegisterQuizId { get; set; }
        public int SignedCourseId { get; set; }
        public string SignedCourseName { get; set; }
        public int SignedModuleId { get; set; }
    }
}