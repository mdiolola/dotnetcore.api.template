using System;
using dotnetcore.api.model.template.Users;

namespace dotnetcore.api.entity.template.Users
{
    public class UserEntity : BaseEntity<Int64, model.template.Users.User>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        #region "Constructors"
        public UserEntity(User obj){
            this.Id = obj.Id;
            this.Username = obj.Username;
            this.Password = obj.Password;
            this.FirstName = obj.FirstName;
            this.LastName = obj.LastName;
            this.Address = obj.Address;
        }

        #endregion
        #region "Public Methods"    
        public override User ToModel()
        {
            return new User(this.Id)
            {
                Username = this.Username,
                Password = this.Password,
                FirstName = this.FirstName,
                LastName = this.LastName,
                Address = this.Address
            };
        }
        #endregion
    }
}
