using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;

namespace StillwaterFilesApp.Config
{
    public class JsonConfig : IConfiguration
    {
        #region Fields
        private JObject _json;
        private DateTime _cachedTimeStamp;
        private bool _hasRead;
        #endregion

        #region Properties
        public string FilePath { get; set; }
        #endregion

        #region Constructors
        public JsonConfig()
        {
            FilePath = Path.Combine(Environment.CurrentDirectory, "config.json");
        }

        public JsonConfig(string path)
        {
            FilePath = path;
        }
        #endregion

        #region Public Methods
        public void Add(string property, object value)
        {
            if (VerifyTimestamp() && _hasRead)
            {

            }
        }

        public string Get(string property)
        {
            if (_hasRead)
            {
                return _json[property].ToString();
            }

            ReadFromFile();

            return _json[property].ToString();
        }

        public T Get<T>(string property)
        {
            if (_hasRead)
            {
                return _json[property].ToObject<T>();
            }
            ReadFromFile();
            return _json[property].ToObject<T>();
        }
        #endregion

        #region Private Methods
        private void ReadFromFile()
        {
            if (VerifyTimestamp()) return;

            var jsonString = string.Join("", File.ReadAllLines(FilePath));
            _cachedTimeStamp = File.GetLastWriteTime(FilePath);
            _hasRead = true;
            _json = JObject.Parse(jsonString);
        }

        private async void ReadFromFileAsync()
        {
            if (VerifyTimestamp()) return;

            var jsonString = string.Empty;
            using (var file = new FileStream(FilePath, FileMode.Open))
            {
                byte[] buffer = new byte[file.Length];
                int bytesRead = 0;
                while ((bytesRead = await file.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    jsonString += Convert.ToString(buffer);
                }
            }
            _cachedTimeStamp = File.GetLastWriteTime(FilePath);
            _hasRead = true;
            _json = JObject.Parse(jsonString);
        }

        private bool VerifyTimestamp()
        {
            if (_cachedTimeStamp == null)
            {
                _hasRead = false;
                return false;
            }
            else
            {
                var timestamp = File.GetLastWriteTime(FilePath);
                if (timestamp == _cachedTimeStamp)
                {
                    return true;
                }
                _hasRead = false;
                return false;
            }
        }
        #endregion
    }
}
