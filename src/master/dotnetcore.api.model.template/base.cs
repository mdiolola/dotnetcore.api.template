using System.Collections.Generic;
using System.Xml.Serialization;

namespace dotnetcore.api.model
{
    public abstract class Base
    {
        #region "Properties"

        public long Id { get; set; }

        [XmlIgnore]
        public IDictionary<string, string> ValidationResult { get; set; } = new Dictionary<string, string>();

        #endregion

        #region "Constructors"

        protected Base() { }

        protected Base(long id)
        {
            this.Id = id;
        }

        #endregion
    }
}
