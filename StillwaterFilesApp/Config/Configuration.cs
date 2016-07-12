using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillwaterFilesApp.Config
{
    /// <summary>
    /// Basic in-memory configuration class
    /// </summary>
    public class Configuration : IConfiguration
    {
        #region Fields
        private Dictionary<string, object> _properties;
        #endregion

        #region Constructors
        public Configuration()
        {
            _properties = new Dictionary<string, object>();
        }
        #endregion

        #region Public Methods
        public string Get(string property)
        {
            return _properties[property].ToString();
        }
        public T Get<T>(string property)
        {
            return (T)_properties[property];
        }
        public void Add(string property, object value)
        {
            _properties.Add(property, value);
        }
        #endregion
    }
}
