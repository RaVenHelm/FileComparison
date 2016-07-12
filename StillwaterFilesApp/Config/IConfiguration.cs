using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillwaterFilesApp.Config
{
    public interface IConfiguration
    {
        string Get(string property);
        T Get<T>(string property);
        void Add(string property, object value);
    }
}
