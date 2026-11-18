public static class MatProps
{
    public static readonly Dictionary<Mat, MatPropVals> Props = new()
    {
        {Mat.Air, new MatPropVals(name:"Air", density:0)},
        {Mat.Water, new MatPropVals(name:"Water", density:.5f)},
        {Mat.Sand, new MatPropVals(name:"Sand", density:1f)},
        {Mat.Stone, new MatPropVals(name:"Stone", density:1f)},
    };

    public static float[] Densities = [.. Props.ToArray().Select(p => p.Value.Density)];
}