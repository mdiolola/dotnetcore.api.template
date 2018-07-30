using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcore.api.template.Settings
{
    public class Authenticate
    {
        #region "Properties"

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string Username { get; set; }

        public string DbRole { get; set; }

        public Claim[] Claims
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.Username) ?
                    null :
                    new[] { new Claim(ClaimTypes.Name, this.Username), new Claim(ClaimTypes.Role, this.DbRole.ToString())};
            }
        }

        public int ExpireMinutes { get; set; }

        public DateTime Expires
        {
            get { return DateTime.Now.AddMinutes(ExpireMinutes); }
        }

        public SigningCredentials Credentials
        {
            get
            {
                return this.Key == null ?
                    null :
                    new SigningCredentials(this.Key, SecurityAlgorithms.HmacSha256);
            }
        }

        public string SecurityKey { get; set; }

        public SymmetricSecurityKey Key
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.SecurityKey) ?
                    null :
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.SecurityKey));
            }
        }

        #endregion
    }
}
