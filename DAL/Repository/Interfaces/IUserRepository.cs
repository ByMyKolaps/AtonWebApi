using AtonWebApi.DAL.DTO;
using AtonWebApi.DTO;
using AtonWebApi.Models;

namespace AtonWebApi.DAL.Repository.Interfaces
{
    public interface IUserRepository
    {
        public Maybe TryCreate(string creatorLogin, UserCreateDTO userDTO);
        public Maybe TryUpdate(string updatorLogin, string userLogin, UserUpdateDTO userDTO);
        public Maybe ChangePassword(string updatorLogin, ChangeUserPasswordDTO passwordDTO);
        public Maybe ChangeLogin(string updatorLogin, ChangeUserLoginDTO loginDTO);
        public IEnumerable<User> GetActiveUsers();
        public TMaybe<User> GetUserByLogin(string userLogin);
        public TMaybe<User> GetUserByLoginAndPassword(string userLogin, string userPassword);
        public IEnumerable<User> GetUsersOlderThan(int age);
        public Maybe DeleteUserSoftly(string updatorLogin, string userLogin);
        public Maybe DeleteUserCompletely(string userLogin);
        public Maybe RestoreUser(string userLogin);
        public Task<User?> Authenticate(string userLogin, string userPassword);
    }
}
