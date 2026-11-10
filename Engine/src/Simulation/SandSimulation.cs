public static class SandSimulation
{
    public static void Step(Sandbox sandbox)
    {
        int width = sandbox.Width;
        int height = sandbox.Height;
        uint[,] pixels = sandbox.Pixels;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Mat mat = Msk.GetMat(pixels[x, y]);

                switch (mat)
                {
                    case Mat.Air:
                        continue;
                    case Mat.Sand:
                        ProcessPowder(sandbox, pixels, x, y);
                        break;
                    case Mat.Stone:
                        break;
                }
            }
        }
    }

    public static void ProcessPowder(Sandbox sandbox, uint[,] pixels, int x, int y)
    {
        int index = sandbox.Index(x, y);

        if (TrySwap(sandbox, pixels, x, y, x, y - 1))
            return;

        if (TrySwap(sandbox, pixels, x, y, x - 1, y - 1))
            return;

        if (TrySwap(sandbox, pixels, x, y, x + 1, y - 1))
            return;
    }

    private static bool TrySwap(Sandbox sandbox, uint[,] pixels, int sx, int sy, int tx, int ty)
    {
        if (ty < 0) return false;
        if (tx < 0) return false;
        if (tx >= sandbox.Width) return false;

        uint target = pixels[tx, ty];
        if (Msk.GetMat(target) == Mat.Air)
        {
            pixels[tx, ty] = pixels[sx, sy];
            pixels[sx, sy] = target;
            return true;
        }

        return false;
    }
}