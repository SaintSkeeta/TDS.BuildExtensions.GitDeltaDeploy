using HedgehogDevelopment.SitecoreCommon.Data.Items;
using HedgehogDevelopment.SitecoreProject.Tasks.Extensibility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hedgehog.TDS.BuildExtensions.GitDeltaDeploy.Tasks.Filters
{
    public class CullItemsFromProjectByExistanceInChangedItemsFile : ICanIncludeItem
    {
        public bool IncludeItemInBuild(string parameters, IItem parsedItem, string filePath)
        {
            if (!File.Exists(parameters))
            {
                return true;
            }

            string[] changedFiles = File.ReadAllLines(parameters);

#if DEBUG
            var folderPath = Path.GetDirectoryName(parameters);
            StreamWriter sw = new StreamWriter(folderPath + @"\DeltaDeployCompare.txt", true);
            sw.WriteLine("TDS Item filePath is " + filePath.Replace(@"\", @"/"));

            foreach (var file in changedFiles)
            {
                sw.WriteLine("Git changed item file path is " + file);
            }

            sw.WriteLine("----");
            sw.Close();
#endif
            return changedFiles.Any(x => x.EndsWith(filePath.Replace(@"\", @"/")));
        }
    }
}
