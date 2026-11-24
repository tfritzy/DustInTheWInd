using System.Drawing;

public static class MatProps
{
    public static readonly Dictionary<Mat, MatPropVals> Props = new()
    {
        {Mat.Air, new MatPropVals(name:"Air", density:0, color: Convert.ToUInt32("00000000", 16))},
        {Mat.Water, new MatPropVals(name:"Water", density:.5f, color: Convert.ToUInt32("5555FF", 16))},
        {Mat.Sand, new MatPropVals(name:"Sand", density:1f, color: Convert.ToUInt32("AA9988", 16))},
        {Mat.Stone, new MatPropVals(name:"Stone", density:1f, Convert.ToUInt32("777777", 16))},
    };

    public static float[] Densities = [.. Props.ToArray().OrderBy(p => p.Key).Select(p => p.Value.Density)];
}