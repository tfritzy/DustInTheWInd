using System.Text;

public class Sandbox
{
    public Chunk[,] Chunks;
    public int ChunkSize;
    public List<Rigidbody> Rigidbodies;
    public int Width { get; private set; }
    public int Height { get; private set; }

    public Sandbox(int width, int height, int chunkSize = 64)
    {
        Rigidbodies = new(128);
        Width = width;
        Height = height;
        ChunkSize = chunkSize;

        int cWidth = width / chunkSize + (width % chunkSize > 0 ? 1 : 0);
        int cHeight = height / chunkSize + (height % chunkSize > 0 ? 1 : 0);
        Chunks = new Chunk[cWidth, cHeight];
        for (int x = 0; x < cWidth; x++)
        {
            for (int y = 0; y < cHeight; y++)
            {
                Chunks[x, y] = new(x, y, chunkSize, chunkSize);
            }
        }
    }

    public Chunk GetChunkForPixel(int pixelX, int pixelY)
    {
        return Chunks[pixelX / ChunkSize, pixelY / ChunkSize];
    }

    public void SetPixel(int x, int y, uint value)
    {
        var chunk = Chunks[x / ChunkSize, y / ChunkSize];
        chunk.Pixels.Data[x - chunk.WorldX, y - chunk.WorldY] = value;
    }

    public void SetPixelMat(int x, int y, Mat mat)
    {
        SetPixel(x, y, Msk.SetMaterial(0, mat));
    }

    public uint GetPixel(int x, int y)
    {
        var chunk = Chunks[x / ChunkSize, y / ChunkSize];
        return chunk.Pixels.Data[x - chunk.WorldX, y - chunk.WorldY];
    }

    public Mat GetPixelMat(int x, int y)
    {
        return Msk.GetMat(GetPixel(x, y));
    }
}