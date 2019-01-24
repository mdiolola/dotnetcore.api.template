using System;

namespace dotnetcore.api.model.Common
{
    public sealed class CustomException : Exception
    {
        public CustomException(string errorMessage) : base(errorMessage) { }
    }
}
