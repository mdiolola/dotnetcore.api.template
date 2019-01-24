using System.ComponentModel.DataAnnotations;

namespace dotnetcore.api.model.Users
{
    public class User : model.Base
    {
        #region "Properties"

        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        #endregion

        #region "Constructors"

        public User() { }
    
        public User(long id) : base(id) { }

        #endregion
    }

    public sealed class UserLogin : model.Base
    {
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = Constants.ErrorMessages.FieldRequired)]
        public string Password { get; set; }
    }

    public sealed class UserAuth
    {
        #region "Properties"

        public long Id { get; set; }

        public string Token { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name { get; set; }

        #endregion
    }
}
