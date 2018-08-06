using dotnetcore.api.model.template.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcore.api.template.Services.Interface
{
   public interface IUserService 
    {
        void Insert(User obj);
        void Update(User obj);
        User Select();
        List<User> Select(long Id);
        void Delete(long Id);
    }
}
