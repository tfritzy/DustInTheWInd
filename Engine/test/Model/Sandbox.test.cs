
namespace test;

[TestClass]
public sealed class SandboxTests
{
    [TestMethod]
    public void Sandbox_HasCorrectChunkCounts()
    {
        Assert.AreEqual(0, new Sandbox(0, 0, 64).Chunks.GetLength(0));
        Assert.AreEqual(0, new Sandbox(0, 0, 64).Chunks.GetLength(1));

        Assert.AreEqual(1, new Sandbox(1, 0, 64).Chunks.GetLength(0));
        Assert.AreEqual(0, new Sandbox(1, 0, 64).Chunks.GetLength(1));

        Assert.AreEqual(4, new Sandbox(256, 257, 64).Chunks.GetLength(0));
        Assert.AreEqual(5, new Sandbox(256, 257, 64).Chunks.GetLength(1));
    }
}