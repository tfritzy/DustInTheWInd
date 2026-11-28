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

        var targetProcessed = Msk.GetProcessed(sandbox.Chunks[0, 0].Pixels.Data[0, 0]);
        for (int cx = 0; cx < sandbox.Chunks.GetLength(0); cx++)
        {
            for (int cy = 0; cy < sandbox.Chunks.GetLength(1); cy++)
            {
                var chunk = sandbox.Chunks[cx, cy];
                var pixels = chunk.Pixels.Data;
                for (int x = 0; x < pixels.GetLength(0); x++)
                {
                    for (int y = 0; y < pixels.GetLength(1); y++)
                    {
                        if (targetProcessed != Msk.GetProcessed(pixels[x, y]))
                        {
                            throw new Exception($"Did not flip processed correctly at {x}, {y}");
                        }
                    }
                }
            }
        }
    }
}