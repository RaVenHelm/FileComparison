using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StillwaterFilesApp.Config
{
    public enum ConfigStore
    {
        FILE,
        MEMORY,
        DATABASE
    }

    public interface IConfigStore
    {
        ConfigStore StoreType { get; }
        void AddProperty(string property, object value);
    }
}
