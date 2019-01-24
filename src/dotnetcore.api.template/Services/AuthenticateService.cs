using dotnetcore.api.entity.Users;
using dotnetcore.api.model.Common;
using dotnetcore.api.model.Constants;
using dotnetcore.api.model.Users;
using dotnetcore.api.template.Data;
using dotnetcore.api.template.Services.Interface;
using dotnetcore.api.template.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace dotnetcore.api.template.Services
{
    public class AuthenticateService : IAuthenticateService
    {

        #region "Private Members"

        private readonly ApiContext _context = null;
        private readonly Authenticate _autheticate = null;
        private readonly Security _security = null;

        //private readonly IUserService _clientUserService = null;
        #endregion

        #region "Constructors"

        public AuthenticateService(ApiContext context, IOptions<Authenticate> authenticate, IOptions<Security> security)
        {
            this._context = context;
            this._autheticate = authenticate.Value;
            this._security = security.Value;
            //this._clientUserService = clientUserService;
        }

        #endregion


        public string Decode(string text)
        {
            return text.Decrypt(this._security.PassPhrase);
        }

        public bool IsValid(UserLogin obj)
        {
            var isValid = CustomValidator.IsModelValid(obj);

            if (!isValid)
                throw new Exception(ErrorMessages.HttpInvalidParameters);
            return true;
        }

        public bool TryRequestToken(string username, string password, out UserAuth userAuth)
        {

            userAuth = null;

            UserEntity userEntity = this._context.Users.AsNoTracking().FirstOrDefault(i => i.Username.ToLower() == username.ToLower());

            if (userEntity != null)
            {
                if (string.Equals(userEntity.Password.Decrypt(this._security.PassPhrase), password))
                {
                    this._autheticate.Username = username;
                    this._autheticate.DbRole = userEntity.FirstName;
                    userAuth = new UserAuth
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(
                            issuer: this._autheticate.Issuer,
                            audience: this._autheticate.Audience,
                            claims: this._autheticate.Claims,
                            expires: this._autheticate.Expires,
                            signingCredentials: this._autheticate.Credentials)),
                        Username = username.Encrypt(this._security.PassPhrase),
                        Password = password.Encrypt(this._security.PassPhrase)
                    };
                    return true;
                }
            }

            return false;
        }

        public void UpdateSessionEntity(string token, User obj)
        {
            throw new System.NotImplementedException();
        }
    }
}
