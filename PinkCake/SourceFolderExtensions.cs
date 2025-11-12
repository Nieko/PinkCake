using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkCake.Settings
{
    public static class SourceFolderExtensions
    {
        public static string AsDisplay(this SourceFolder sourceFolder)
        {
            if (string.IsNullOrEmpty(sourceFolder.Filter))
            {
                return sourceFolder.RootFolder;
            }

            return sourceFolder.RootFolder + " (" + sourceFolder.Filter + ")";
        }

        public static SourceFolder ToSourceFolder(this string displayText)
        {
            var lastBraket = displayText.LastIndexOf(" (");
            
            if(lastBraket < 0)
            {
                return new SourceFolder
                {
                    RootFolder = displayText
                };
            }

            var rootFolder = displayText.Substring(0, lastBraket -1);
            var filter = displayText.Substring(lastBraket - 1);

            return new SourceFolder
            {
                Filter = filter,
                RootFolder = rootFolder
            };
        }
    }
}
