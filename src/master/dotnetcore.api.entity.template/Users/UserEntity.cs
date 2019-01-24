using System;
using System.ComponentModel.DataAnnotations.Schema;
using dotnetcore.api.model.Users;

namespace dotnetcore.api.entity.Users
{
    [Table("Users", Schema = "Account")]
    public class UserEntity : BaseEntity<Int64, model.Users.User>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        #region "Constructors"

        public UserEntity() { }

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
