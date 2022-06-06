using System.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNet.NuGet.Push;
using Cake.Common.Tools.DotNet.Test;
using Cake.Common.Tools.DotNetCore.Pack;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Frosting;

public static class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .UseContext<BuildContext>()
            .Run(args);
    }
}

public class BuildContext : FrostingContext
{
    public string SolutionPath { get; }
    public string NugetApiKey { get; }
    public string Version { get; }
    public string AssemblyVersion { get; }

    public BuildContext(ICakeContext context)
        : base(context)
    {
        SolutionPath = System.IO.Path.GetFullPath(context.Arguments.GetArgument("SolutionPath"));
        NugetApiKey = context.Arguments.GetArgument("NugetApiKey");
        Version = context.Arguments.GetArgument("BuildVersion");
        AssemblyVersion = context.Arguments.GetArgument("AssemblyVersion");
    }
}

[TaskName("Clean")]
public sealed class CleanTask : FrostingTask<BuildContext>
{
  public override void Run(BuildContext context)
  {
    var artifacts = System.IO.Path.Combine(context.SolutionPath, "artifacts/test-results");
    if (Directory.Exists(artifacts))
    {
      Directory.Delete(artifacts);
    }
  }
}

[TaskName("Restore")]
[IsDependentOn(typeof(CleanTask))]
public sealed class RestoreTask : FrostingTask<BuildContext>
{
  public override void Run(BuildContext context)
  {
    context.Log.Information($"Restoring solution: {context.SolutionPath}");
    context.DotNetRestore(context.SolutionPath);
  }
}

[TaskName("Build")]
[IsDependentOn(typeof(RestoreTask))]
public sealed class BuildTask : FrostingTask<BuildContext>
{
  public override void Run(BuildContext context)
  {
    context.DotNetBuild(context.SolutionPath, new DotNetBuildSettings
    {
      Configuration = "Release",
      NoRestore = true,
      ArgumentCustomization = args => args
        .Append($"/p:Version={context.Version}")
        .Append($"/p:AssemblyVersion={context.AssemblyVersion}")
    });
  }
}

[TaskName("Test")]
[IsDependentOn(typeof(BuildTask))]
public sealed class TestTask : FrostingTask<BuildContext>
{
  public override void Run(BuildContext context)
  {
    context.DotNetTest(context.SolutionPath, new DotNetTestSettings
    {
      Configuration = "Release",
      NoRestore = true,
      NoBuild = true,
      ResultsDirectory = System.IO.Path.Combine(context.SolutionPath, "artifacts/test-results"),
      Loggers = {"trx"}
    });
  }
}

[TaskName("Pack")]
[IsDependentOn(typeof(TestTask))]
public sealed class PackTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        context.DotNetPack(context.SolutionPath, new DotNetCorePackSettings
        {
          Configuration = "Release",
          NoRestore = true, 
          NoBuild = true,
          OutputDirectory = System.IO.Path.Combine(context.SolutionPath, "artifacts/packages"),
          ArgumentCustomization = args => args
            .Append($"/p:Version={context.Version}")
        });
    }
}

[TaskName("Publish")]
[IsDependentOn(typeof(PackTask))]
public sealed class PublishTask : FrostingTask<BuildContext>
{
    // Tasks can be asynchronous
    public void RunAsync(BuildContext context)
    {
        context.DotNetNuGetPush(
            System.IO.Path.Combine(context.SolutionPath, "artifacts/packages"), 
            new DotNetNuGetPushSettings
            {
              ApiKey = context.NugetApiKey,
            });
    }
}

[TaskName("Default")]
[IsDependentOn(typeof(PublishTask))]
public class DefaultTask : FrostingTask
{
}