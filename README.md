# TDS.BuildExtensions.GitDeltaDeploy #
A custom delta deploy step for Sitecore TDS deployments using Git.
Note: This is a third party extension, so it isn’t affiliated with Hedgehog or Sitecore, and their support teams *should not* be contacted for help with this.
Instead I encourage developers to follow the debugging steps provided below, and to discuss their projects on the #tds channel in the [Sitecore Community Slack](https://sitecore.chat/).

[![Build status](https://ci.appveyor.com/api/projects/status/biqveu6ugx859i2f?svg=true)](https://ci.appveyor.com/project/SeanHolmesby/tds-buildextensions-gitdeltadeploy/branch/master)
[![NuGet](https://img.shields.io/nuget/v/Hedgehog.TDS.BuildExtensions.GitDeltaDeploy.svg?maxAge=2592000)](https://www.nuget.org/packages/Hedgehog.TDS.BuildExtensions.GitDeltaDeploy)
[![Licence](https://img.shields.io/github/license/mashape/apistatus.svg?maxAge=2592000)](https://github.com/SaintSkeeta/TDS.BuildExtensions.GitDeltaDeploy/blob/master/LICENSE)

NuGet package available: [Hedgehog.TDS.BuildExtensions.GitDeltaDeploy](https://www.nuget.org/packages/Hedgehog.TDS.BuildExtensions.GitDeltaDeploy)

Detailed Blog Post
 - [http://www.seanholmesby.com/true-delta-deploys-with-tds-classic/](http://www.seanholmesby.com/true-delta-deploys-with-tds-classic/)

Debugging GitDeltaDeploy
 - [https://www.seanholmesby.com/debugging-gitdeltadeploy-with-a-sitecore-tds-project/](https://www.seanholmesby.com/debugging-gitdeltadeploy-with-a-sitecore-tds-project/)

Enhancements from Geekhive's John Rappel
 - [https://www.geekhive.com/buzz/post/2017/09/enhancements-sitecore-true-delta-deploy-tds-git/](https://www.geekhive.com/buzz/post/2017/09/enhancements-sitecore-true-delta-deploy-tds-git/)
 - [https://sitecorerap.wordpress.com/2017/09/15/fully-automating-git-delta-deploys-in-sitecore/](https://sitecorerap.wordpress.com/2017/09/15/fully-automating-git-delta-deploys-in-sitecore/)

## Usage ##
 - Ensure you have git.exe in your PATH environment variable.
 - Install the NuGet package into your TDS projects.
   - if you wish to use GitDeltaDeploy from VisualStudio, OR if your TDS projects DON'T use the HedgehogDevelopment.TDS NuGet package, you need to copy the packages/SaintSkeeta.TDS.BuildExtensions.GitDeltaDeploy.x.x.x/build/SaintSkeeta.TDS.BuildExtensions.GitDeltaDeploy.dll file to your Hedgehog MSBuild directory (C:\Program Files (x86)\MSBuild\HedgehogDevelopment\SitecoreProject\v9.0).
   - if you DO use the HedgehogDevelopment.TDS NuGet package on your build server, the build will copy the SaintSkeeta.TDS.BuildExtensions.GitDeltaDeploy.dll file to the HedgehogDevelopment.TDS NuGet package folder, alongside the HedgehogDevelopment.SitecoreProject.Tasks.dll so it can be used.
 - Activate GitDeltaDeploy
   - by setting `<CustomGitDeltaDeploy>True</CustomGitDeltaDeploy>` in your config (either in scproj, scproj.user, TdsGlobal.config or TdsGlobal.config.user).
 - Deploy using either
   -  the project itself (right click project -> Deploy)
   -  deploy the solution (right click solution -> Deploy Solution)
   -  trigger a 'build' on MSBuild from the command line.
 - View the /packages/SaintSkeeta.TDS.BuildExtensions.GitDeltaDeploy.x.x.x/build/readme.txt file for additional settings and options.

## How it works ##
The initial deployment will do a regular deployment, and save a LastDeploymentGitCommitId.txt file in your DeltaArtifactsFolder (which is {project}/Report by default).
Subsequent deployments will use this file to check if there's difference between the last deployment commit id, and what the local repository is currently at.
It finds the difference in .item files, and saves a list of changed .item files in {DeltaArtifactsFolder}/ChangedItemFiles.txt.
Then Sitecore TDS will use a custom 'CullItemsFromProjectClass' that checks the ChangedItemFiles.txt file for each item. If the file is found in the list, it is 'deployable'. If not, TDS skips it.
The LastDeploymentGitCommitId.txt file is then resaved with the newest commit id, ready for the next deployment.
