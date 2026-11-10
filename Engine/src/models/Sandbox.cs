public class Sandbox
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Length => Width * Height;
    public uint[,] Pixels { get; private set; }

    public Sandbox(int width, int height)
    {
        Width = width;
        Height = height;
        Pixels = new uint[width, height];
    }

    public int Index(int x, int y)
    {
        return (Height - 1 - y) * Height + x;
    }

    public void SetPixel(int x, int y, Mat material)
    {
        Pixels[x, y] = Msk.SetMaterial(0, material);
    }

    public uint GetPixel(int x, int y)
    {
        return Pixels[x, y];
    }

    public Mat GetPixelMat(int x, int y)
    {
        return Msk.GetMat(Pixels[x, y]);
    }
}