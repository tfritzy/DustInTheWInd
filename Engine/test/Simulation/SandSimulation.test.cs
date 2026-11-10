namespace test;

[TestClass]
public sealed class SandSimulationTests
{
    [TestMethod]
    public void SandSimulation_SandMovesDown()
    {
        Sandbox box = new(3, 3);
        box.SetPixel(1, 2, Material.Sand);

        Assert.AreEqual(Material.Sand, box.GetPixelMat(1, 2));
        Assert.AreEqual(Material.Air, box.GetPixelMat(1, 1));
        Assert.AreEqual(Material.Air, box.GetPixelMat(1, 0));

        Assert.AreEqual(Material.Air, box.GetPixelMat(1, 2));
        Assert.AreEqual(Material.Sand, box.GetPixelMat(1, 1));
        Assert.AreEqual(Material.Air, box.GetPixelMat(1, 0));

        Assert.AreEqual(Material.Air, box.GetPixelMat(1, 2));
        Assert.AreEqual(Material.Air, box.GetPixelMat(1, 1));
        Assert.AreEqual(Material.Sand, box.GetPixelMat(1, 0));

        Assert.AreEqual(Material.Air, box.GetPixelMat(1, 2));
        Assert.AreEqual(Material.Air, box.GetPixelMat(1, 1));
        Assert.AreEqual(Material.Sand, box.GetPixelMat(1, 0));
    }
}
