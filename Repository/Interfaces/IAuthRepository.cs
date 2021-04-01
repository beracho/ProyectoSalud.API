using System.Collections.Generic;
using System.Threading.Tasks;
using ProyectoSalud.API.Dtos;
using ProyectoSalud.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace ProyectoSalud.API.Data
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<User> GetUser(int userID);
        Task<User> UserExists(UserForRegisterDto userForRegisterDto);
        Task<List<Rol>> GetRolsPerUser(int userId);
        string GenerateVerificationKey();
        User CompleteInfoToConfirmVerify(UserForRecoveryVerifyDto userForRecoveryVerifyDto, User user);
        Task<string> ChangePassword(UserForChangePasswordDto userForChangePasswordDto);
        SecurityTokenDescriptor CreateToken(User userToReturn, List<Rol> rols);
        Task<User> GetUserByEmail(string email);
        Task<List<Rol>> AssignRol(int userId, string rolName);
        Task<DataForAuthenticationDto> AuthenticationData(int userId);
    }
}