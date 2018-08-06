using dotnetcore.api.entity.template.Users;
using dotnetcore.api.model.template.Common;
using dotnetcore.api.model.template.Users;
using dotnetcore.api.template.Data;
using dotnetcore.api.template.Services.Interface;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcore.api.template.Services
{
    public class UserService : IUserService
    {
        #region "Private Members"

        private readonly ApiContext _context = null;
        private readonly Security _security = null;


        #endregion

        #region "Contructors"

        public UserService(ApiContext context, IOptions<Security> security)
        {
            this._context = context;
            this._security = security.Value;
        }

        #endregion

        public void Delete(long Id)
        {
            throw new NotImplementedException();
        }

        public void Insert(User obj)
        {
            if (obj == null)
                return;

            obj.Password = obj.Password.Encrypt(_security.PassPhrase);

            var userEntity = new UserEntity(obj);
            this._context.Users.Add(userEntity);
            this._context.SaveChanges();

            obj.Id = userEntity.Id;
        }

        public User Select()
        {
            throw new NotImplementedException();
        }

        public List<User> Select(long Id)
        {
            throw new NotImplementedException();
        }

        public void Update(User obj)
        {
            throw new NotImplementedException();
        }
    }
}
