namespace dotnetcore.api.template.Services.Interface
{
    public interface IService<T> where T : class
    {
        #region "Public Methods"

        bool IsValid(T obj);

        #endregion
    }
}
