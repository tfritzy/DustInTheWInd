public static class Constraints
{
    static Constraints()
    {
        string testAssemblyName = "Microsoft.VisualStudio.TestPlatform.TestFramework";
        Constraints.IsInUnitTest = AppDomain.CurrentDomain.GetAssemblies()
            .Any(a => a.FullName.StartsWith(testAssemblyName));
    }

    public static bool IsInUnitTest { get; private set; }

    public static void Assert(Sandbox sandbox)
    {
        if (!IsInUnitTest) return;
        var targetProcessed = Msk.GetProcessed(sandbox.Pixels[0, 0]);

        for (int x = 0; x < sandbox.Width; x++)
        {
            for (int y = 0; y < sandbox.Height; y++)
            {
                if (targetProcessed != Msk.GetProcessed(sandbox.Pixels[x, y]))
                {
                    throw new Exception($"Did not flip processed correctly at {x}, {y}. \n{sandbox.GetProcessedString()}\n{sandbox.ToString()}");
                }
            }
        }
    }
}