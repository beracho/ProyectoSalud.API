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
        Task<string> UserExists(UserForRegisterDto userForRegisterDto);
        Task<IEnumerable<UserRol>> GetRolsPerUser(int userId);
        string GenerateVerificationKey();
        User CompleteInfoToConfirmVerify(UserForRecoveryVerifyDto userForRecoveryVerifyDto, User user);
        Task<string> ChangePassword(UserForChangePasswordDto userForChangePasswordDto);
        SecurityTokenDescriptor CreateToken(User userToReturn);
        Task<User> GetUserByEmail(string email);
        Task<DataForAuthenticationDto> AuthenticationData(int userId);
    }
}