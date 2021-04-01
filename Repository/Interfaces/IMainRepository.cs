using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoSalud.API.Dtos;
using ProyectoSalud.API.Helpers;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Data
{
    public interface IMainRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<User> GetUser(int id);
        Task<User> GetUser(string usernameOrEmail);
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetMainPhotoForUser(int userId);
        int GetCountries();
        Task<Country> GetCountry(int id);
        Task<City> GetCity(int id);
        Task<string> ChangeUserRegister(string username);
    }
}