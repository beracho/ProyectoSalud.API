using System;
using System.Text;
using ProyectoSalud.API.Smtp;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ProyectoSalud.API.Helpers
{
    public static class Extensions
    {
        public static int CalculateAge(this DateTime theDateTime)
        {
            var age = DateTime.Today.Year - theDateTime.Year;
            if (theDateTime.AddYears(age) > DateTime.Today)
            {
                age--;
            }
            return age;
        }

        public static void AddPagination(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }

        public static string GenerateUsername(string FirstName, string LastName)
        {
            string[] namecute = FirstName.Split(' ');
            string namefirst = namecute[0];

            string[] lastnamecute = LastName.Split(' ');
            string lastnamefirst = lastnamecute[0];

            string username = namefirst + "." + lastnamefirst;
            byte[] tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(username.ToLower());
            username = System.Text.Encoding.UTF8.GetString(tempBytes);

            return username;
        }

        public static string GeneratePassword(PasswordOptions options = null)
        {
            if (options == null) options = new PasswordOptions()
            {
                RequiredLength = 8,
                RequireNonAlphanumeric = false,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < options.RequiredLength)
            {
                char c = (char)random.Next(48, 58);

                password.Append(c);

                if (char.IsDigit(c))
                    options.RequireDigit = false;
                else if (char.IsLower(c))
                    options.RequireLowercase = false;
                else if (char.IsUpper(c))
                    options.RequireUppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    options.RequireNonAlphanumeric = false;
            }

            if (options.RequireNonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (options.RequireDigit)
                password.Append((char)random.Next(48, 58));
            if (options.RequireLowercase)
                password.Append((char)random.Next(97, 123));
            if (options.RequireUppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }

        public static string GenerateCoursesPath(int courseId, string type)
        {
            string route = "/courses/" + courseId + "/" + type + "/";

            return route;
        }
        // UsersPath
    }
}