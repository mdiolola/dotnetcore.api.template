using System;

namespace dotnetcore.api.model.template.Common
{
    public sealed class CustomException : Exception
    {
        public CustomException(string errorMessage) : base(errorMessage) { }
    }
}
