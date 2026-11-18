
namespace test;

[TestClass]
public sealed class MaterialTests
{
    [TestMethod]
    public void Material_HasAllMats()
    {
        foreach (var mat in Enum.GetValues<Mat>())
        {
            CollectionAssert.Contains(MatProps.Props.Keys, mat, $"Props does not contain material '{mat}'");
        }
    }

    [TestMethod]
    public void Material_Densities()
    {
        foreach (var mat in Enum.GetValues<Mat>())
        {
            Assert.AreEqual(MatProps.Props[mat].Density, MatProps.Densities[(int)mat]);
        }
    }
}