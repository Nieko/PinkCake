using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PinkCake.Settings
{
    public class UserAppHomeStore : ISettingsStore
    {
        public SourceSettings Load()
        {
            var filePath = new FileInfo(GetFilePath());

            if (!filePath.Exists)
            {
                return new SourceSettings();
            }

            try
            {
                var settings = JsonSerializer.Deserialize<SourceSettings>(File.ReadAllText(filePath.FullName)) ?? new SourceSettings();
            
                if(settings.Sources == null)
                {
                    settings.Sources = new SourceFolder[] { };
                }

                return settings;
            }
            catch 
            {
                return new SourceSettings();
            }
        }

        public void Save(SourceSettings settings)
        {
            var filePath = GetFilePath();
            if(! Directory.Exists(GetSettingsFolder()))
            {
                Directory.CreateDirectory(GetSettingsFolder());
            }

            File.WriteAllText(filePath, JsonSerializer.Serialize(settings));
        }

        private string GetFilePath()
        {
            return GetSettingsFolder() + "\\settings.json";
        }

        private string GetSettingsFolder()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\pinkcake";
        }
    }
}
