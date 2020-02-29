

/*******************************/

SaintSkeeta TDS.Extensions.GitDeltaDeploy

/*******************************/

Thank you for downloading TDS.Extensions.GitDeltaDeploy, and wanting to property speed up your deployment times.
Please Note: This is a third party extension, so it isn’t affiliated with Hedgehog or Sitecore, and their support teams *should not* be contacted for help with this.
Developers should instead follow the debugging steps provided [here](https://www.seanholmesby.com/debugging-gitdeltadeploy-with-a-sitecore-tds-project/), and to discuss their projects on the #tds channel in the [Sitecore Community Slack](https://sitecore.chat/).

//TODO: CHECK THE FOLLOWING!!!

 * If you're building with GitDeltaDeploy turned on, on a machine with Sitecore TDS installed (developer machine or OnPrem build server) ensure you copy 
   the packages/SaintSkeeta.TDS.BuildExtensions.GitDeltaDeploy.X.X.X/build/SaintSkeeta.TDS.BuildExtensions.GitDeltaDeploy.dll to 
   your MSBuild Hedgehog directory (C:\Program Files (x86)\MSBuild\HedgehogDevelopment\SitecoreProject\v9.0).
 * Read through the considerations for your build/deployment to ensure your setup is configured well. https://www.seanholmesby.com/gitdeltadeploy-installation-and-usage-considerations/
 

Settings:-
Add this setting to your TdsGlobal.config and/or TdsGlobal.config.user file for the appropriate Configurations, to enable and manage the GitDeltaDeploy.

    <CustomGitDeltaDeploy>True</CustomGitDeltaDeploy>

    Here are some other optional settings you can also use, with example values given. Note that these property names can also be passed
    in as command line parameters when calling MSBuild from the command line.
    
    <!-- LastDeploymentGitCommitID: Normally calculated from the previous deployment output, but can be overridden with the property here. -->
    <LastDeploymentGitCommitID>50e42cceced5661dcb8360d010189b8163558e24</LastDeploymentGitCommitID>
    
    <!-- LastDeploymentGitTagName: Normally calculated from the previous tagged deployment output, but can be overridden with the property here. -->
    <LastDeploymentGitTagName>ProductionRelease</LastDeploymentGitTagName>
    
    <!-- CullProjectFiles: Option to cull the files in the deployment as well, not just the items. -->
    <CullProjectFiles>True</CullProjectFiles>

    <!-- Default Delta Deploy - this is an example of passing the date to the default delta deploy feature. (This is only for the default delta deploy feature. It is not used for the custom git delta deploy. -->
    <IncludeItemsChangedAfter>2016-03-21</IncludeItemsChangedAfter>
    
    <!-- Default Delta Deploy - example of passing in a relative date (1 week ago) to the default delta deploy feature. -->
    <IncludeItemsChangedAfter>$([System.DateTime]::Now.AddDays(-‌​7))</IncludeItemsCha‌​ngedAfter>

For more information on how this project works, please refer to the blog post here. http://www.seanholmesby.com/true-delta-deploys-with-tds-classic/ 

Source Code can be found here. https://github.com/SaintSkeeta/TDS.BuildExtensions.GitDeltaDeploy

NuGet project page is here. https://www.nuget.org/packages/SaintSkeeta.TDS.BuildExtensions.GitDeltaDeploy

Debugging issues blog post can be found here: https://www.seanholmesby.com/debugging-gitdeltadeploy-with-a-sitecore-tds-project/

Big thanks to John Rappel for his work on GitDeltaDeploy, and for his extensive testing of the solution.

