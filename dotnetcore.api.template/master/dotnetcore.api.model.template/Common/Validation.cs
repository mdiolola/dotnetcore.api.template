using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace dotnetcore.api.model.template.Common
{
    public sealed class CustomValidator
    {
        public static bool IsModelValid<T>(T obj) where T : model.template.Base
        {
            var context = new ValidationContext(instance: obj, serviceProvider: null, items: null);
            var result = new List<ValidationResult>();
            string objName = obj.GetType().Name;

            obj.ValidationResult = new Dictionary<string, string>();

            if (!Validator.TryValidateObject(
                instance: obj,
                validationContext: context,
                validationResults: result,
                validateAllProperties: true))
            {
                foreach (ValidationResult entry in result)
                {
                    var key = $"{objName}{entry.MemberNames.ElementAt(0)}";

                    if (obj.ValidationResult.ContainsKey(key))
                        continue;

                    obj.ValidationResult.Add(key, entry.ErrorMessage);
                }

                return false;
            }

            return true;
        }
    }
}
