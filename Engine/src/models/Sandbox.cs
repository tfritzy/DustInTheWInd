public class Sandbox
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Length => Width * Height;
    public uint[] Pixels { get; private set; }

    public Sandbox(int width, int height)
    {
        Pixels = new uint[width * height];
    }

    public int Index(int x, int y)
    {
        return (Height - y) * Height + x;
    }

    public void SetPixel(int x, int y, Material material)
    {
        Pixels[Index(x, y)] = Msk.SetMaterial(0, material);
    }

    public uint GetPixel(int x, int y)
    {
        return Pixels[Index(x, y)];
    }

    public Material GetPixelMat(int x, int y)
    {
        return Msk.GetMaterial(Pixels[Index(x, y)]);
    }
}