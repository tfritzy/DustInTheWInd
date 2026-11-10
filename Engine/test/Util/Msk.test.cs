namespace test;

[TestClass]
public sealed class MskTests
{
    [TestMethod]
    public void Material_BackAndForth()
    {
        Assert.AreEqual(Mat.Air, Msk.GetMat(Msk.SetMaterial(0, Mat.Air)));
        Assert.AreEqual(Mat.Sand, Msk.GetMat(Msk.SetMaterial(0, Mat.Sand)));
        Assert.AreEqual(Mat.Stone, Msk.GetMat(Msk.SetMaterial(0, Mat.Stone)));
    }

    [TestMethod]
    public void Material_OnlySetsMatBits()
    {
        uint src = 0b_10101010_11001100_11001100_11001100;
        Assert.AreEqual<uint>(0b_00000001_11001100_11001100_11001100, Msk.SetMaterial(src, Mat.Sand));
    }
}
