using System.Drawing;

public static class MatProps
{
    public static readonly Dictionary<Mat, MatPropVals> Props = new()
    {
        {Mat.Air, new MatPropVals(name:"Air", density:0, color: FromHexString("#00000000"))},
        {Mat.Water, new MatPropVals(name:"Water", density:.5f, color: FromHexString("#5555FF"))},
        {Mat.Sand, new MatPropVals(name:"Sand", density:1f, color: FromHexString("#AA9988"))},
        {Mat.Stone, new MatPropVals(name:"Stone", density:1f, color: FromHexString("#777777"))},
    };

    private static uint FromHexString(string hex)
    {
        hex = hex[1..];
        var alpha = hex.Length > 6 ? hex.Substring(6, 2) : "FF";
        var r = hex.Substring(0, 2);
        var g = hex.Substring(2, 2);
        var b = hex.Substring(4, 2);
        var color = hex[0..6];
        return Convert.ToUInt32(alpha + b + g + r, 16);
    }

    public static float[] Densities = [.. Props.ToArray().OrderBy(p => p.Key).Select(p => p.Value.Density)];
    public static uint[] Colors = [.. Props.ToArray().OrderBy(p => p.Key).Select(p => p.Value.Color)];
}