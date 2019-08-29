var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

var paths = new
{
    Artifacts = Directory("./artifacts"),
    ArtifactsDeploy = Directory("./artifacts/wwwroot"),
    ArtifactsPackage = "./artifacts/wwwroot.zip",
    Solution = "./Demo.sln",
};

Task("Restore")
    .Does(() => NuGetRestore(paths.Solution));

Task("Compile")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        DotNetCoreBuild(paths.Solution, new DotNetCoreBuildSettings
        {
            Configuration = configuration,
            MSBuildSettings = new DotNetCoreMSBuildSettings
            {
                MaxCpuCount = -1,
            },
        });
    });

Task("PackagePrep")
    .Does(() =>
    {
        EnsureDirectoryExists(paths.Artifacts);
        CleanDirectories(paths.Artifacts);
        EnsureDirectoryExists(paths.ArtifactsDeploy);
    });

Task("PackageWebsite")
    .IsDependentOn("PackagePrep")
    .Does(() =>
    {
        var msBuildsettings = new DotNetCoreMSBuildSettings
        {
            MaxCpuCount = -1,
            NoLogo = true,
        };

        msBuildsettings.WithProperty("PublishUrl", MakeAbsolute(paths.ArtifactsDeploy).FullPath)
                       .WithProperty("DeployOnBuild", "true")
                       .WithProperty("Configuration", configuration)
                       .WithProperty("WebPublishMethod", "FileSystem")
                       .WithProperty("DeployTarget", "WebPublish")
                       .WithProperty("AutoParameterizationWebConfigConnectionStrings", "false")
                       .WithProperty("SolutionDir", ".");

        DotNetCoreMSBuild(paths.Solution, msBuildsettings);
    });

Task("Package")
    .IsDependentOn("PackageWebsite")
    .Does(() =>
    {
        Zip(paths.ArtifactsDeploy, paths.ArtifactsPackage);
    });

Task("Default")
    .IsDependentOn("Compile")
    .IsDependentOn("Package");

RunTarget(target);
