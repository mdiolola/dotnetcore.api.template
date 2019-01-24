using dotnetcore.api.model.Users;

namespace dotnetcore.api.template.Services.Interface 
{
    public interface IAuthenticateService : IService<UserLogin>
    {
        #region "Public Methods"

        bool TryRequestToken(string username, string password, out UserAuth userAuth);

        string Decode(string text);

        void UpdateSessionEntity(string token, User obj);

        #endregion
    }
}
