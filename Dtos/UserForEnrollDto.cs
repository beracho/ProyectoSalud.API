namespace ProyectoSalud.API.Dtos
{
    public class UserForEnrollDto
    {
        public string LastName { get; set; }
        public string Name { get; set; }
        public int Ci { get; set; }
        public string ExpeditionCi { get; set; }
        public string Email { get; set; }
        public int Telephone { get; set; }
        public int ProgramId { get; set; }
    }
}