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
    public void SandSimulation_WaterSpreads()
    {
        Sandbox box = new(3, 3);
        box.SetPixel(0, 0, Mat.Sand);
        box.SetPixel(1, 0, Mat.Sand);
        box.SetPixel(0, 1, Mat.Water);

        SandSimulation.Step(box);
        Assert.AreEqual(Mat.Water, box.GetPixelMat(1, 1), box.ToString());
        SandSimulation.Step(box);
        Assert.AreEqual(Mat.Water, box.GetPixelMat(2, 0), box.ToString());
    }

    [TestMethod]
    public void SandSimulation_SandDisplacesWater()
    {
        Sandbox box = new(2, 3);
        box.SetPixel(0, 0, Mat.Stone);
        box.SetPixel(1, 0, Mat.Stone);
        box.SetPixel(0, 1, Mat.Water);
        box.SetPixel(1, 1, Mat.Water);
        box.SetPixel(0, 2, Mat.Sand);

        Assert.AreEqual(Mat.Water, box.GetPixelMat(0, 1), box.ToString());
        SandSimulation.Step(box);
        Assert.AreEqual(Mat.Sand, box.GetPixelMat(0, 1), box.ToString());
        SandSimulation.Step(box);
        Assert.AreEqual(Mat.Sand, box.GetPixelMat(0, 1), box.ToString());
    }
}
