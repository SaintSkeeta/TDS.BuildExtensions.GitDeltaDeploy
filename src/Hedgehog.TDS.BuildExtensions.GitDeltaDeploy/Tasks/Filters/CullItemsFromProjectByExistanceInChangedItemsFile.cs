using HedgehogDevelopment.SitecoreCommon.Data.Items;
using HedgehogDevelopment.SitecoreProject.Tasks.Extensibility;
using System.IO;
using System.Linq;

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

            var changedFiles = File.ReadAllLines(parameters);

            var convertedFilePath = filePath.Replace(@"\", @"/");

#if DEBUG
            var folderPath = Path.GetDirectoryName(parameters);
            var sw = new StreamWriter(folderPath + @"\DeltaDeployCompare.txt", true);
            sw.WriteLine("TDS Item filePath is " + convertedFilePath);

            foreach (var file in changedFiles)
            {
                sw.WriteLine("Git changed item file path is " + file);
            }

            sw.WriteLine("----");
            sw.Close();
#endif
            // filePaths formats
            //   Update package creation: absolute path
            //     e.g. C:/Projects/TDS.Project/sitecore/content/myitem.item
            //
            //   Deployment: relative path 
            //     e.g. sitecore/content/myitem.item

            // parameters formats (ChangedItemFiless.txt via git diff)
            //   relative path from repo root, 
            //     e.g. TDSProjects/TDS.Master/sitecore/content/myitem.item

            return changedFiles.Any(x => convertedFilePath.EndsWith(x)) || changedFiles.Any(x => x.EndsWith(convertedFilePath));
        }
    }
}