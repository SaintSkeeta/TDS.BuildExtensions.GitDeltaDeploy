This project is setup to use GitDeltaDeploy and properly test out it's features.

Features
Delta Package creation of only IMetaData and INavigable templates (and some ancester items) from Bundled.Master and FeatureThree from TDS.Master.

Features tested for the delta:-
 - Delta is working across bundled projects (when only the bunding project has GitDelta package installed).
 - Delta is working with both YML format and the older ITEM format.
 - NuGet package install order doesn't matter. The TDS build components NuGet package can have it's targets file before or after the GitDeltaDeploy targets file.

 Still to be proved out with this test project:-
 - Scenario: Delta includes only new items, but RecursiveDeployAction shouldn't delete items not in the package but still in the project.
 - Scenario: Delta includes only new items, but RecursiveDeployAction should delete items not in the project where deployment settings are set to do so.


In each zip
-----------
The solution folder contains the local git data for commits (so we can test standalone - while not connected to the online remote repository).
A tag and a commit id have been setup in this local git data to represent a fake previous release/commit.

(Tests are set in the *Tds.Global.config* file. Remove these if you wish to test it being picked up from the LastDeploymentGitCommitID.txt file.)
Tag: ProductionRelease 
Commit ID: dc1106940d6097e534391fd244fc552165509b46 (in the ITEM package)
Commit ID: 0842ab552af85facbe0ef1cd693a946e1bf52e21 (in the YAML package)

Solution
TdsGlobal.config has the options set for CustomGitDeltaDeploy (to turn on the feature) and the LastDeploymentGitCommitID for the commit ID.
LastDeploymentGitTagName is also in there, commented out but available for testing if needed.

TDS.Master builds the package, while it bundles in Bundled.Master.
Only TDS.Master has the GitDeltaDeploy package installed - but Bundled.Master should have (only) it's changed files in the package too)

Each zip has the 'full' packages already built in them (i.e full project...not the delta). This is for both the Update Package (in bin/Package_Release) and the WebDeployPackage (in bin/WebDeploy_Release).
When building with just the delta, you can compare the size and contents of the built packages with these 'full' packages to see the reduced size.

TDS.Master builds produce the following files in the Report folder for debugging:-
 - ChangedItemFiles.txt - a list of the items that changed according to git.
 - DeltaDeployCompare.txt - when using the Debug DLL, this gives additional information on the processing for GitDelta.

See [here](https://www.seanholmesby.com/debugging-gitdeltadeploy-with-a-sitecore-tds-project/) for debugging.
