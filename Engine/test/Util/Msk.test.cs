namespace test;

[TestClass]
public sealed class MskTests
{
    [TestMethod]
    public void Material_BackAndForth()
    {
        Assert.AreEqual(Material.Air, Msk.GetMaterial(Msk.SetMaterial(0, Material.Air)));
        Assert.AreEqual(Material.Sand, Msk.GetMaterial(Msk.SetMaterial(0, Material.Sand)));
        Assert.AreEqual(Material.Stone, Msk.GetMaterial(Msk.SetMaterial(0, Material.Stone)));
    }

    [TestMethod]
    public void Material_OnlySetsMatBits()
    {
        uint src = 0b_10101010_11001100_11001100_11001100;
        Assert.AreEqual<uint>(0b_00000001_11001100_11001100_11001100, Msk.SetMaterial(src, Material.Sand));
    }
}
