using HomeWork.Context;
using HomeWork.User;
using Microsoft.EntityFrameworkCore;

namespace HomeWork.Controller
{
    public class DbUserController
    {
        private readonly DBAppContext _Dbcontext;
        private static DBUser AUTORIZED_USER;
        public DBUser autorizedUser => AUTORIZED_USER;

        public DbUserController(DBAppContext dbContext)
        {
            _Dbcontext = dbContext;
        }
        #region === Authorizzation ===
        public bool IsAuthorized(DBUser user)
        {
            try
            {
                _Dbcontext.Database.OpenConnection();

                var found = _Dbcontext.users.FirstOrDefault(x => x.Login == user.Login && x.Password == user.Password);

                if (found == null)
                    return false;

                AUTORIZED_USER = found;
            }
            finally
            {
                _Dbcontext.Database.CloseConnection();
            }
            return true;
        }
        #endregion

        #region === test Administrator BD === 
        public void Add(DBUser user)
        {
            _Dbcontext.Database.OpenConnection();

            _Dbcontext.users.Add(user);
            _Dbcontext.SaveChanges();

            _Dbcontext.Database.CloseConnection();
        }

        public void Update(DBUser user)
        {
            _Dbcontext.Database.OpenConnection();

            var found = _Dbcontext.users.FirstOrDefault(u => u.id == user.id);
            if (found == null)
            {
                throw new Exception($"DbUserController.Update: User not found with ID: {user.id}");
            }
            found.Login = user.Login;
            found.Password = user.Password;

            _Dbcontext.SaveChanges();

            _Dbcontext.Database.CloseConnection();
        }

        public void Delete(DBUser user)
        {
            _Dbcontext.Database.OpenConnection();

            var found = _Dbcontext.users.FirstOrDefault(u => u.id == user.id);
            if (found == null)
            {
                throw new Exception($"DbUserController.Update: User not found with ID: {user.id}");
            }

            _Dbcontext.users.Remove(found);
            _Dbcontext.SaveChanges();

            _Dbcontext.Database.CloseConnection();
        }

        public void ConsoleVeiw_Test(DBUser user)
        {
            _Dbcontext.Database.OpenConnection();





            _Dbcontext.Database.CloseConnection();
        }
        #endregion
    }
}
