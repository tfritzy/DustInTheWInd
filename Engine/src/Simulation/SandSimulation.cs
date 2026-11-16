using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

public static class SandSimulation
{
    public static void Step(Sandbox sandbox)
    {
        int width = sandbox.Width;
        int height = sandbox.Height;
        uint[,] pixels = sandbox.Pixels;

        // The value of processed this iteration will flop all pixels to. 
        bool targetProcessed = !Msk.GetProcessed(pixels[0, 0]);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (Msk.GetProcessed(pixels[x, y]) == targetProcessed)
                    continue;

                Mat mat = Msk.GetMat(pixels[x, y]);
                var processed = false;
                switch (mat)
                {
                    case Mat.Air:
                        break;
                    case Mat.Sand:
                        processed = ProcessPowder(sandbox, pixels, x, y);
                        break;
                    case Mat.Stone:
                        break;
                    case Mat.Water:
                        processed = ProcessLiquid(sandbox, pixels, x, y);
                        break;
                }

                if (!processed)
                    pixels[x, y] = Msk.ToggleProcessed(pixels[x, y]);
            }
        }

        Constraints.Assert(sandbox);
    }

    public static bool ProcessPowder(Sandbox sandbox, uint[,] pixels, int x, int y)
    {
        if (TrySwap(sandbox, pixels, x, y, x, y - 1))
            return true;

        if (TrySwap(sandbox, pixels, x, y, x - 1, y - 1))
            return true;

        if (TrySwap(sandbox, pixels, x, y, x + 1, y - 1))
            return true;

        return false;
    }

    public static bool ProcessLiquid(Sandbox sandbox, uint[,] pixels, int x, int y)
    {
        if (TrySwap(sandbox, pixels, x, y, x, y - 1))
            return true;

        if (TrySwap(sandbox, pixels, x, y, x - 1, y - 1))
            return true;

        if (TrySwap(sandbox, pixels, x, y, x + 1, y - 1))
            return true;

        if (TrySwap(sandbox, pixels, x, y, x + 1, y))
            return true;

        if (TrySwap(sandbox, pixels, x, y, x - 1, y))
            return true;

        if (TrySwap(sandbox, pixels, x, y, x + 2, y))
            return true;

        if (TrySwap(sandbox, pixels, x, y, x - 2, y))
            return true;

        return false;
    }

    private static bool TrySwap(Sandbox sandbox, uint[,] pixels, int sx, int sy, int tx, int ty)
    {
        if (ty < 0) return false;
        if (tx < 0) return false;
        if (tx >= sandbox.Width) return false;

        uint target = pixels[tx, ty];
        if (Msk.GetMat(target) == Mat.Air)
        {
            var tProc = !Msk.GetProcessed(pixels[sx, sy]);
            pixels[tx, ty] = Msk.SetProcessed(pixels[sx, sy], tProc);
            pixels[sx, sy] = Msk.SetProcessed(target, tProc);
            return true;
        }

        return false;
    }
}