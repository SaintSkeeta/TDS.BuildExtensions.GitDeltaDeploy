img src="http://www.hhogdev.com/Images/newsletter/logo_hedgehog.jpg" alt="Hedgehog Development" width="203" height="65" border="0">

================================

# Hedgehog TDS.BuildExtensions.GitDeltaDeploy #
A custom delta deploy step for TDS deployments using Git.

NuGet package available: Hedgehog.TDS.BuildExtensions.GitDeltaDeploy

## Usage ##
Ensure you have git.exe in your PATH environment variable.
Install the NuGet package into your TDS projects.
 - if your TDS projects DON'T use the HedgehogDevelopment.TDS NuGet package, you need to copy the packages/Hedgehog.TDS.BuildExtensions.GitDeltaDeploy.1.0.0.0/build/Hedgehog.TDS.BuildExtensions.GitDeltaDeploy.dll file to your Hedgehog MSBuild directory (C:\Program Files (x86)\MSBuild\HedgehogDevelopment\SitecoreProject\v9.0).
 - if you DO use the HedgehogDevelopment.TDS NuGet package, the build will copy the Hedgehog.TDS.BuildExtensions.GitDeltaDeploy.dll file to the HedgehogDevelopment.TDS NuGet package folder, alongside the HedgehogDevelopment.SitecoreProject.Tasks.dll. Just make sure your scproj has a reference to the Hedgehog.TDS.BuildExtensions.GitDeltaDeploy.targets file AFTER the HedgehogDevelopment.TDS.targets file.
Activate GitDeltaDeploy
 - by setting <CustomGitDeltaDeploy>True</CustomGitDeltaDeploy> in your config (either in scproj, scproj.user, TdsGlobal.config or TdsGlobal.config.user).
Deploy using either
 -  the project itself (right click project -> Deploy)
 -  deploy the solution (right click solution -> Deploy Solution)
 -  trigger a 'build' on MSBuild from the command line.

## How it works ##
The initial deployment will do a regular deployment, and save a LastDeploymentGitCommitId.txt file in your Report/ folder.
Subsequent deployments will use this file to check if there's difference between the last deployment commit id, and what the local repository is currently at.
It finds the difference in .item files, and saves a list of changed .item files in Report/ChangedItemFiles.txt.
Then we've customized TDS to use a custom 'CullItemsFromProjectClass' that checks the ChangedItemFiles.txt file for each item. If the file is found in the list, it is 'deployable'. If not, TDS skips it.
The LastDeploymentGitCommitId.txt file is then resaved with the newest commit id, ready for the next deployment.
