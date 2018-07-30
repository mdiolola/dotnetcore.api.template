using dotnetcore.api.model.template.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnetcore.api.entity.template
{
   public abstract class BaseEntity<T, TModel>
    {
        #region "Properties"

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public T Id { get; set; }

        public DateTime? DateUpdated { get; set; }

        public DateTime DateCreated { get; set; } = DateTimeHelper.GetDateTime();

        #endregion

        #region "Public Methods"

        public abstract TModel ToModel();

        #endregion
    }
}
