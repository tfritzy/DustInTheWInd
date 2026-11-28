using System.Diagnostics;

namespace test;

[TestClass]
public sealed class SandSimulationTests
{
    [TestMethod]
    public void SandSimulation_SandMovesDown()
    {
        Sandbox box = new(3, 3);
        box.SetPixelMat(1, 2, Mat.Sand);

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
        box.SetPixelMat(0, 0, Mat.Sand);
        box.SetPixelMat(1, 0, Mat.Sand);
        box.SetPixelMat(0, 1, Mat.Water);

        SandSimulation.Step(box);
        Assert.AreEqual(Mat.Water, box.GetPixelMat(1, 1), box.ToString());
        SandSimulation.Step(box);
        Assert.AreEqual(Mat.Water, box.GetPixelMat(2, 0), box.ToString());
    }

    [TestMethod]
    public void SandSimulation_SandDisplacesWater()
    {
        Sandbox box = new(2, 3);
        box.SetPixelMat(0, 0, Mat.Stone);
        box.SetPixelMat(1, 0, Mat.Stone);
        box.SetPixelMat(0, 1, Mat.Water);
        box.SetPixelMat(1, 1, Mat.Water);
        box.SetPixelMat(0, 2, Mat.Sand);

        Assert.AreEqual(Mat.Water, box.GetPixelMat(0, 1), box.ToString());
        SandSimulation.Step(box);
        Assert.AreEqual(Mat.Sand, box.GetPixelMat(0, 1), box.ToString());
        SandSimulation.Step(box);
        Assert.AreEqual(Mat.Sand, box.GetPixelMat(0, 1), box.ToString());
    }

    [TestMethod]
    public void SandSimulation_FPSMeasuer()
    {
        // Sandbox box = new(1920 / 4, 1080 / 4);
        // for (int x = 0; x < 1920 / 4; x++)
        // {
        //     for (int y = 0; y < 1080 / 4; y++)
        //     {
        //         box.SetPixelMat(x, y, Mat.Sand);
        //     }
        // }

        // Stopwatch watch = new();
        // watch.Start();
        // for (int i = 0; i < 1000; i++)
        // {
        //     SandSimulation.Step(box);
        // }
        // watch.Stop();

        // Console.WriteLine($"Simulation processed at {1000f / watch.Elapsed.Seconds} FPS");
        // Assert.Fail();
    }
}
