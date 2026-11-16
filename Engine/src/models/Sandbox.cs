using System.Security.Principal;
using System.Text;

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

    public void SetPixel(int x, int y, Mat material)
    {
        Pixels[x, y] = Msk.SetMaterial(0, material);
    }

    public Mat GetPixelMat(int x, int y)
    {
        return Msk.GetMat(Pixels[x, y]);
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append('\n');
        for (int y = Height - 1; y >= 0; y--)
        {
            for (int x = 0; x < Width; x++)
            {
                sb.Append(GetPixelMat(x, y).ToString()[0]);
                sb.Append(' ');
            }
            sb.Append('\n');
        }

        return sb.ToString();
    }

    public string GetProcessedString()
    {
        StringBuilder sb = new();
        sb.Append('\n');
        for (int y = Height - 1; y >= 0; y--)
        {
            for (int x = 0; x < Width; x++)
            {
                sb.Append(Msk.GetProcessed(Pixels[x, y]) ? "1" : "0");
                sb.Append(' ');
            }
            sb.Append('\n');
        }

        return sb.ToString();
    }
}