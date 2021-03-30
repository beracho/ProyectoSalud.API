using System.Collections.Generic;

namespace ProyectoSalud.API.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Route { get; set; }
        public int Order { get; set; }
        public string Icon { get; set; }
        public bool Hidden { get; set; }
        public bool MethodGet { get; set; }
        public bool MethodPost { get; set; }
        public bool MethodPut { get; set; }
        public bool MethodDelete { get; set; }
        public string ActionRouteState { get; set; }
    }
}