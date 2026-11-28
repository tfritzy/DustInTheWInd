public class Chunk(int x, int y, int w, int h)
{
    public int X = x;
    public int Y = y;
    public int WorldX = x * w;
    public int WorldY = y * h;
    public int Width = w;
    public int Height = h;
    public Pixels Pixels = new(w, h);
}