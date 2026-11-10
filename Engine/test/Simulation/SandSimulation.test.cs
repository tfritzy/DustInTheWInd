namespace test;

[TestClass]
public sealed class SandSimulationTests
{
    [TestMethod]
    public void SandSimulation_SandMovesDown()
    {
        Sandbox box = new(3, 3);
        box.SetPixel(1, 2, Mat.Sand);

        Assert.AreEqual(Mat.Sand, box.GetPixelMat(1, 2));
        Assert.AreEqual(Mat.Air, box.GetPixelMat(1, 1));
        Assert.AreEqual(Mat.Air, box.GetPixelMat(1, 0));

        SandSimulation.Step(box);

        Assert.AreEqual(Mat.Air, box.GetPixelMat(1, 2));
        Assert.AreEqual(Mat.Sand, box.GetPixelMat(1, 1));
        Assert.AreEqual(Mat.Air, box.GetPixelMat(1, 0));

        SandSimulation.Step(box);

        Assert.AreEqual(Mat.Air, box.GetPixelMat(1, 2));
        Assert.AreEqual(Mat.Air, box.GetPixelMat(1, 1));
        Assert.AreEqual(Mat.Sand, box.GetPixelMat(1, 0));

        SandSimulation.Step(box);

        Assert.AreEqual(Mat.Air, box.GetPixelMat(1, 2));
        Assert.AreEqual(Mat.Air, box.GetPixelMat(1, 1));
        Assert.AreEqual(Mat.Sand, box.GetPixelMat(1, 0));
    }

    [TestMethod]
    public void SandSimulation_Perf()
    {
        Sandbox box = new(1000, 1000);
        for (int x = 0; x < 1000; x++)
        {
            for (int y = 0; y < 1000; y++)
            {
                box.Pixels[x, y] = Msk.SetMaterial(0, Mat.Sand);
            }
        }

        for (int i = 0; i < 200; i++)
        {
            SandSimulation.Step(box);
        }
    }
}
