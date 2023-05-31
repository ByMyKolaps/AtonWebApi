using AtonWebApi.DAL.DTO;
using AtonWebApi.DAL.Repository.Interfaces;
using AtonWebApi.DTO;
using AtonWebApi.Models;

namespace AtonWebApi.DAL.Repository.Realization
{
    public class UserRepository : IUserRepository
    {
        public async Task<User?> Authenticate(string userLogin, string userPassword)
        {
            return await Task.Run(() =>
            {
                if (!DatabaseMock.UsersTable.ContainsKey(userLogin))
                    return null;
                else
                {
                    User user = DatabaseMock.UsersTable[userLogin];
                    return user.Password == userPassword ? user : null;
                }
            });
        }

        public Maybe ChangeLogin(string updatorLogin, ChangeUserLoginDTO changeUserLoginDTO)
        {
            if (DatabaseMock.UsersTable.ContainsKey(changeUserLoginDTO.OldLogin))
            {
                if (DatabaseMock.UsersTable.ContainsKey(changeUserLoginDTO.NewLogin))
                    return new Maybe(false, "User with this login already exists");
                var user = DatabaseMock.UsersTable[updatorLogin];
                if (!user.IsAdmin)
                {
                    if (user.Login != changeUserLoginDTO.OldLogin)
                        return new Maybe(false, "You can't change logins for other users");
                    if (user.RevokedOn != null)
                        return new Maybe(false, "User deleted");
                }
                var userToUpdate = DatabaseMock.UsersTable[changeUserLoginDTO.OldLogin];
                DatabaseMock.UsersTable.Remove(userToUpdate.Login);
                userToUpdate.Login = changeUserLoginDTO.NewLogin;
                DatabaseMock.UsersTable.Add(userToUpdate.Login, userToUpdate);
                ModifyUser(updatorLogin, changeUserLoginDTO.NewLogin);
                return new Maybe(true);
            }
            return new Maybe(false, "The user does not exist");
        }

        public Maybe ChangePassword(string updatorLogin, ChangeUserPasswordDTO changeUserPasswordDTO)
        {
            if (DatabaseMock.UsersTable.ContainsKey(changeUserPasswordDTO.UserLogin))
            {
                var user = DatabaseMock.UsersTable[updatorLogin];
                if (user.IsAdmin)
                {
                    DatabaseMock.UsersTable[changeUserPasswordDTO.UserLogin].Password = changeUserPasswordDTO.NewPassword;
                    ModifyUser(updatorLogin, changeUserPasswordDTO.UserLogin);
                    return new Maybe(true);
                }
                if (user.Login != changeUserPasswordDTO.UserLogin)
                    return new Maybe(false, "You can't change passwords for other users");
                if (user.RevokedOn != null)
                    return new Maybe(false, "User deleted");
                if (user.Password != changeUserPasswordDTO.OldPassword)
                    return new Maybe(false, "Passwords don't match");

                DatabaseMock.UsersTable[user.Login].Password = changeUserPasswordDTO.NewPassword;
                ModifyUser(updatorLogin, user.Login);
                return new Maybe(true);
            }
            return new Maybe(false, "The user does not exist");
        }

        public Maybe TryCreate(string creatorLogin, UserCreateDTO userDTO)
        {
            var result = DatabaseMock.UsersTable.TryAdd(userDTO.Login, new User(userDTO, creatorLogin));
            return result ? new Maybe(true) : new Maybe(false, "User with this login already exists");
        }

        public Maybe DeleteUserCompletely(string userLogin)
        {
            if (DatabaseMock.UsersTable.ContainsKey(userLogin))
            {
                DatabaseMock.UsersTable.Remove(userLogin);
                return new Maybe(true);
            }
            return new Maybe(false, "The user does not exist");
        }

        public Maybe DeleteUserSoftly(string updatorLogin, string userLogin)
        {
            if (DatabaseMock.UsersTable.ContainsKey(userLogin))
            {
                var user = DatabaseMock.UsersTable[userLogin];
                user.RevokedBy = updatorLogin;
                user.RevokedOn = DateTime.Now;
                ModifyUser(updatorLogin, userLogin);
                return new Maybe(true);
            }
            return new Maybe(false, "The user does not exist");
        }

        public IEnumerable<User> GetActiveUsers()
        {
            return DatabaseMock.UsersTable
                .Select(user => user.Value)
                .Where(user => user.RevokedOn == null)
                .OrderBy(user => user.CreatedOn);
        }

        public TMaybe<User> GetUserByLogin(string userLogin)
        {
            if (DatabaseMock.UsersTable.ContainsKey(userLogin))
            {
                return new TMaybe<User>(DatabaseMock.UsersTable[userLogin]);
            }
            else
                return new TMaybe<User>("The user does not exist");
        }

        public TMaybe<User> GetUserByLoginAndPassword(string userLogin, string userPassword)
        {
            if (DatabaseMock.UsersTable.ContainsKey(userLogin))
            {
                User user = DatabaseMock.UsersTable[userLogin];
                if (user.RevokedOn == null)
                {
                    if (user.Password == userPassword)
                        return new TMaybe<User>(user);
                    else
                        return new TMaybe<User>("Wrong password");
                }
                else
                    return new TMaybe<User>("User deleted");
            }
            return new TMaybe<User>("The user does not exist");
        }

        public IEnumerable<User> GetUsersOlderThan(int age)
        {
            return DatabaseMock.UsersTable
                .Where(user => user.Value.Birthday != null && IsOlderThan(user.Value, age))
                .Select(user => user.Value);
        }

        private bool IsOlderThan(User user, int age) =>
            DateTime.Now.Year - user.Birthday?.Year > age;

        public Maybe RestoreUser(string userLogin)
        {
            if (DatabaseMock.UsersTable.ContainsKey(userLogin))
            {
                var user = DatabaseMock.UsersTable[userLogin];
                user.RevokedBy = null;
                user.RevokedOn = null;
                return new Maybe(true);
            }
            return new Maybe(false, "The user does not exist");
        }

        public Maybe TryUpdate(string updatorLogin, string userLogin, UserUpdateDTO userDTO)
        {
            if (DatabaseMock.UsersTable.ContainsKey(userLogin))
            {
                var user = DatabaseMock.UsersTable[updatorLogin];
                if (user.IsAdmin)
                {
                    UpdateUser(updatorLogin, userLogin, userDTO);
                    return new Maybe(true);
                }
                if (user.RevokedOn != null)
                    return new Maybe(false, "User deleted");

                UpdateUser(updatorLogin, userLogin, userDTO);
                return new Maybe(true);
            }
            return new Maybe(false, "The user does not exist");

        }

        private void UpdateUser(string updatorLogin, string userLogin, UserUpdateDTO userUpdateDTO)
        {
            var userToUpdate = DatabaseMock.UsersTable[userLogin];
            if (userUpdateDTO.Name != null)
                userToUpdate.Name = userUpdateDTO.Name;
            if (userUpdateDTO.Gender != null)
                userToUpdate.Gender = (int)userUpdateDTO.Gender;
            if (userUpdateDTO.Birthday != null)
                userToUpdate.Birthday = userUpdateDTO.Birthday;
            ModifyUser(updatorLogin, userLogin);

        }

        private void ModifyUser(string updatorLogin, string userLogin)
        {
            var modifiedUser = DatabaseMock.UsersTable[userLogin];
            modifiedUser.ModifiedBy = updatorLogin;
            modifiedUser.ModifiedOn = DateTime.Now;
        }
    }
}
