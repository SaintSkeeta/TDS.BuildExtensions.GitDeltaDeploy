using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace SaintSkeeta.TDS.BuildExtensions.GitDeltaDeploy.Tasks.Filters
{
    public class CullFilesFromProject : Task
    {
        public override bool Execute()
        {
            if (string.IsNullOrWhiteSpace(UnchangedFiles))
            {
                Log.LogCommandLine("WARNING: UnchangedFiles must be set.");
                return true;
            }

            if (string.IsNullOrWhiteSpace(OutputPath))
            {
                Log.LogCommandLine("WARNING: OutputPath must be set.");
                return true;
            }

            Log.LogCommandLine("! CullFilesFromProject Task !");
            Log.LogCommandLine("OutputPath: " + OutputPath);
            Log.LogCommandLine("UnchangedFiles: " + UnchangedFiles);
            
            var normalizedFiles = File.ReadAllLines(UnchangedFiles).Select(i => i.Replace("/", @"\")).ToArray();
            
            var normalizedOutputDir = (OutputPath?.Replace("\\\\", "\\")).TrimEnd('\\');
            
            foreach (var file in Directory.EnumerateFiles(normalizedOutputDir, "*.*", SearchOption.AllDirectories))
            {
                // do not delete config files or xml files as they may be transformed
                if (file.EndsWith(".config") || file.EndsWith(".xml"))
                    continue;

                // need to search /bin for *.dll files
                if (file.EndsWith(".dll"))
                {
                    var dllOnly = file.Split('\\').Last();

                    if (!normalizedFiles.Any(i => i.EndsWith(dllOnly)))
                        continue;

                    File.Delete(file);
                    Log.LogCommandLine("CullFilesFromProject :: Deleted file: " + file);
                    continue;
                }
                
                var relativeFilePath = file.Replace(normalizedOutputDir, "");
                
                if (!normalizedFiles.Any(i => i.EndsWith(relativeFilePath)))
                    continue;

                File.Delete(file);
                Log.LogCommandLine("CullFilesFromProject :: Deleted file: " + file);
            }

            foreach (var directory in Directory.EnumerateDirectories(normalizedOutputDir, "*", SearchOption.AllDirectories).OrderByDescending(i => i.Length))
            {
                if (Directory.GetFiles(directory).Length != 0 || Directory.GetDirectories(directory).Length != 0)
                    continue;

                Directory.Delete(directory, false);
                Log.LogCommandLine("CullFilesFromProject :: Deleted empty directory: " + directory);
            }

            return true;
        }
        
        public string OutputPath { get; set; }
        
        public string UnchangedFiles { get; set; }
        
        [Output]
        public string DebugMessage { get; set; }
    }
}