
using System.Text;

public class Pixels
{
    public int Width => Data.GetLength(0);
    public int Height => Data.GetLength(1);
    public int Length => Width * Height;
    public uint[,] Data { get; private set; }

    public Pixels(int width, int height)
    {
        Data = new uint[width, height];
    }

    public void SetPixel(int x, int y, Mat material)
    {
        Data[x, y] = Msk.SetMaterial(0, material);
    }

    public Mat GetPixelMat(int x, int y)
    {
        return Msk.GetMat(Data[x, y]);
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
                sb.Append(Msk.GetProcessed(Data[x, y]) ? "1" : "0");
                sb.Append(' ');
            }
            sb.Append('\n');
        }

        return sb.ToString();
    }
}