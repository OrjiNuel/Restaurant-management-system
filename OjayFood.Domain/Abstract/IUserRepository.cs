﻿using OjayFood.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OjayFood.Domain.Abstract
{
    public interface IUserRepository
    {
        IQueryable<User> Users { get; }
        void SaveUser(User user);
        User DeleteUser(int id);
        User GetUser(int id);
        IEnumerable<User> GetUser(string username);
        void Dispose();
    }
}
