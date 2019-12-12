<img src="https://avatars0.githubusercontent.com/u/1766826?s=400&u=1ebe7c83124f453defd5467a6cae5cb127c81840&v=4" alt="Hedgehog Development" border="0"> 


# TDS.BuildExtensions.GitDeltaDeploy #
A custom delta deploy step for Sitecore TDS deployments using Git.

[![Build status](https://ci.appveyor.com/api/projects/status/biqveu6ugx859i2f?svg=true)](https://ci.appveyor.com/project/SeanHolmesby/tds-buildextensions-gitdeltadeploy/branch/master)
[![NuGet](https://img.shields.io/nuget/v/Hedgehog.TDS.BuildExtensions.GitDeltaDeploy.svg?maxAge=2592000)](https://www.nuget.org/packages/Hedgehog.TDS.BuildExtensions.GitDeltaDeploy)
[![Licence](https://img.shields.io/github/license/mashape/apistatus.svg?maxAge=2592000)](https://github.com/SaintSkeeta/TDS.BuildExtensions.GitDeltaDeploy/blob/master/LICENSE)

NuGet package available: [Hedgehog.TDS.BuildExtensions.GitDeltaDeploy](https://www.nuget.org/packages/Hedgehog.TDS.BuildExtensions.GitDeltaDeploy)

Detailed Blog Post
 - [http://www.seanholmesby.com/true-delta-deploys-with-tds-classic/](http://www.seanholmesby.com/true-delta-deploys-with-tds-classic/)

Enhancements from Geekhive's John Rappel
 - [https://www.geekhive.com/buzz/post/2017/09/enhancements-sitecore-true-delta-deploy-tds-git/](https://www.geekhive.com/buzz/post/2017/09/enhancements-sitecore-true-delta-deploy-tds-git/)
 - [https://sitecorerap.wordpress.com/2017/09/15/fully-automating-git-delta-deploys-in-sitecore/](https://sitecorerap.wordpress.com/2017/09/15/fully-automating-git-delta-deploys-in-sitecore/)

## Usage ##
 - Ensure you have git.exe in your PATH environment variable.
 - Install the NuGet package into your TDS projects.
   - if your TDS projects DON'T use the HedgehogDevelopment.TDS NuGet package, you need to copy the packages/Hedgehog.TDS.BuildExtensions.GitDeltaDeploy.x.x.x/build/Hedgehog.TDS.BuildExtensions.GitDeltaDeploy.dll file to your Hedgehog MSBuild directory (C:\Program Files (x86)\MSBuild\HedgehogDevelopment\SitecoreProject\v9.0).
   - if you DO use the HedgehogDevelopment.TDS NuGet package, the build will copy the Hedgehog.TDS.BuildExtensions.GitDeltaDeploy.dll file to the HedgehogDevelopment.TDS NuGet package folder, alongside the HedgehogDevelopment.SitecoreProject.Tasks.dll. Just make sure your scproj has a reference to the Hedgehog.TDS.BuildExtensions.GitDeltaDeploy.targets file AFTER the HedgehogDevelopment.TDS.targets file.
 - Activate GitDeltaDeploy
   - by setting `<CustomGitDeltaDeploy>True</CustomGitDeltaDeploy>` in your config (either in scproj, scproj.user, TdsGlobal.config or TdsGlobal.config.user).
 - Deploy using either
   -  the project itself (right click project -> Deploy)
   -  deploy the solution (right click solution -> Deploy Solution)
   -  trigger a 'build' on MSBuild from the command line.
 - View the /packages/Hedgehog.TDS.BuildExtensions.GitDeltaDeploy.x.x.x/build/readme.txt file for additional settings and options.

## How it works ##
The initial deployment will do a regular deployment, and save a LastDeploymentGitCommitId.txt file in your Report/ folder.
Subsequent deployments will use this file to check if there's difference between the last deployment commit id, and what the local repository is currently at.
It finds the difference in .item files, and saves a list of changed .item files in Report/ChangedItemFiles.txt.
Then we've customized TDS to use a custom 'CullItemsFromProjectClass' that checks the ChangedItemFiles.txt file for each item. If the file is found in the list, it is 'deployable'. If not, TDS skips it.
The LastDeploymentGitCommitId.txt file is then resaved with the newest commit id, ready for the next deployment.
