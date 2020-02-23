using System;
using HedgehogDevelopment.SitecoreCommon.Data.Items;
using HedgehogDevelopment.SitecoreProject.Tasks.Extensibility;
using System.IO;
using System.Linq;

namespace SaintSkeeta.TDS.BuildExtensions.GitDeltaDeploy.Tasks.Filters
{
    public class CullItemsFromProjectByExistanceInChangedItemsFile : ICanIncludeItem
    {
        private static string[] _changedFiles;

        private static bool _fileRead;

#if DEBUG
        private static bool _changedFilesWritten;
#endif

        public bool IncludeItemInBuild(string parameters, IItem parsedItem, string filePath)
        {
            if (!File.Exists(parameters))
            {
                return true;
            }

            if (!_fileRead)
            {
                Console.WriteLine("Opening file changed items file: " + parameters);
                _changedFiles = File.ReadAllLines(parameters);
                _fileRead = true;
                Console.WriteLine("Finished reading changed items file: " + parameters);
            }

            // ".." is required to consume bundled projects properly
            var convertedFilePath = filePath.Replace(@"\", @"/").Split(new[] { ".." }, StringSplitOptions.None).Last();

#if DEBUG
            Console.WriteLine("File path: " + filePath);

            var folderPath = Path.GetDirectoryName(parameters);
            var sw = new StreamWriter(folderPath + @"\DeltaDeployCompare.txt", true);
            
            if (!_changedFilesWritten)
            {
                foreach (var file in _changedFiles)
                {
                    sw.WriteLine("Git changed item file path is " + file);
                }
                sw.WriteLine("----");
                _changedFilesWritten = true;
            }
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

            var addItem = _changedFiles.Any(x => convertedFilePath.EndsWith(x)) || _changedFiles.Any(x => x.EndsWith(convertedFilePath));

#if DEBUG
            if (!addItem)
            {
                sw.WriteLine("-- Item not added: " + convertedFilePath);
            }
            else
            {
                sw.WriteLine("++ Item added: " + convertedFilePath);
            }
            sw.Close();
#endif
            return addItem;
        }
    }
}