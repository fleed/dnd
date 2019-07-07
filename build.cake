///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");
var runtime = Argument("runtime", "win-x64");

var sln = File("./Dnd.sln");
var cli = File("./src/CliApp/CliApp.csproj");

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
   // Executed BEFORE the first task.
   Information("Running tasks...");
});

Teardown(ctx =>
{
   // Executed AFTER the last task.
   Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

Task("Clean")
   .Does(() => {
      if (DirectoryExists("./out"))
      {
         Information("Deleting the out directory");
         DeleteDirectory("./out", new DeleteDirectorySettings { Recursive = true });
         return;
      }

      Information("The out directory was not found");
   });
Task("Publish")
   .IsDependentOn("Clean")
   .Does(() => {
      var settings = new DotNetCorePublishSettings
      {
            Framework = "netcoreapp2.2",
            Configuration = configuration,
            OutputDirectory = "./out/",
            Runtime = runtime,
            SelfContained = true
      };
      DotNetCorePublish(cli, settings);
   });

Task("Build")
   .Does(() =>{
      DotNetCoreBuild(sln);
   });

Task("Default")
.IsDependentOn("Publish")
.Does(() => {
   Information("Done!");
});

RunTarget(target);