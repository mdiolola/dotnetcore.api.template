using System;
using System.Collections.Generic;
using System.Text;

namespace dotnetcore.api.model.template.Common
{
    public sealed class HttpResponse<T>
    {
        #region "Properties"

        public T Raw { get; }

        public string ErrorMessage { get; }

        public bool IsSuccess { get; }

        #endregion

        #region "Constructors"

        public HttpResponse()
        {
            this.IsSuccess = true;
        }

        public HttpResponse(T raw = default(T), string errorMessage = null)
        {
            this.Raw = raw;
            this.ErrorMessage = errorMessage;
            this.IsSuccess = (errorMessage == null);
        }

        #endregion
    }
}
