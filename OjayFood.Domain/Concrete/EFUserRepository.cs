using OjayFood.Domain.Abstract;
using OjayFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjayFood.Domain.Concrete
{
    public class EFUserRepository : IUserRepository
    {
        private readonly EFDbContext _dbcontext;
        public EFUserRepository()
        {
            _dbcontext = new EFDbContext();
        }

        public IQueryable<User> Users => _dbcontext.Users;

        public void SaveUser(User user)
        {
            if (_dbcontext.Users.Find(user.Id) == null)
            {
                _dbcontext.Users.Add(user);
            }
            else
            {
                var dbEntry = _dbcontext.Users.Find(user.Id);
                if (dbEntry != null)
                {

                    dbEntry.Id = user.Id;
                    dbEntry.LastName = user.LastName;
                    dbEntry.FirstName = user.FirstName;
                    dbEntry.Phone = user.Phone;
                    dbEntry.Address = user.Address;
                    dbEntry.Status = user.Status;
                    dbEntry.Date_Of_Birth = user.Date_Of_Birth;
                    dbEntry.State = user.State;
                    dbEntry.Province = user.Province;
                    dbEntry.Username = user.Username;
                    dbEntry.Password = user.Password;
                }

            }
            _dbcontext.SaveChanges();
        }
        public User DeleteUser(int id)
        {
            var dbEntry = _dbcontext.Users.Find(id);
            if (dbEntry != null)
            {
                _dbcontext.Users.Remove(dbEntry);
                _dbcontext.SaveChanges();
            }
            return dbEntry;
        }
        public User GetUser(int id)
        {
            return _dbcontext.Users.Find(id);
        }
        public IEnumerable<User> GetUser(string username)
        {
            var model = from c in _dbcontext.Users where c.Username == username select c;
            return model.ToList();
        }
        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}
