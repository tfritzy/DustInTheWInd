public static class SandSimulation
{
    public static void Step(Sandbox sandbox)
    {
        // The value processed will get flipped to this iteration. 
        bool targetProcessed = !Msk.GetProcessed(sandbox.Chunks[0, 0].Pixels.Data[0, 0]);

        for (int cy = 0; cy < sandbox.Chunks.GetLength(1); cy++)
        {
            for (int cx = 0; cx < sandbox.Chunks.GetLength(0); cx++)
            {
                var chunk = sandbox.Chunks[cx, cy];
                var pixels = chunk.Pixels.Data;
                for (int x = 0; x < chunk.Width; x++)
                {
                    for (int y = 0; y < chunk.Height; y++)
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
                                processed = ProcessPowder(sandbox, chunk, x + chunk.WorldX, y + chunk.WorldY);
                                break;
                            case Mat.Stone:
                                break;
                            case Mat.Water:
                                processed = ProcessLiquid(sandbox, chunk, x + chunk.WorldX, y + chunk.WorldY);
                                break;
                        }

                        if (!processed)
                            pixels[x, y] = Msk.ToggleProcessed(pixels[x, y]);
                    }
                }
            }
        }
    }

    public static bool ProcessPowder(Sandbox sandbox, Chunk chunk, int x, int y)
    {
        if (TrySwap(sandbox, chunk, x, y, x, y - 1))
            return true;

        if (TrySwap(sandbox, chunk, x, y, x - 1, y - 1))
            return true;

        if (TrySwap(sandbox, chunk, x, y, x + 1, y - 1))
            return true;

        return false;
    }

    public static bool ProcessLiquid(Sandbox sandbox, Chunk chunk, int x, int y)
    {
        if (TrySwap(sandbox, chunk, x, y, x, y - 1))
            return true;

        if (TrySwap(sandbox, chunk, x, y, x - 1, y - 1))
            return true;

        if (TrySwap(sandbox, chunk, x, y, x + 1, y - 1))
            return true;

        if (TrySwap(sandbox, chunk, x, y, x + 1, y))
            return true;

        if (TrySwap(sandbox, chunk, x, y, x - 1, y))
            return true;

        if (TrySwap(sandbox, chunk, x, y, x + 2, y))
            return true;

        if (TrySwap(sandbox, chunk, x, y, x - 2, y))
            return true;

        return false;
    }

    private static bool TrySwap(Sandbox sandbox, Chunk chunk, int sx, int sy, int tx, int ty)
    {
        if (ty < 0) return false;
        if (tx < 0) return false;
        if (tx >= sandbox.Width) return false;

        var sChunk = chunk;
        var tChunk = chunk;
        if (tx < chunk.WorldX || tx >= chunk.WorldX + chunk.Width)
            tChunk = sandbox.GetChunkForPixel(tx, ty);
        else if (ty < chunk.WorldY)
            tChunk = sandbox.GetChunkForPixel(tx, ty);

        var ltx = tx - tChunk.WorldX;
        var lty = ty - tChunk.WorldY;
        var lsx = sx - sChunk.WorldX;
        var lsy = sy - sChunk.WorldY;

        uint source = sChunk.Pixels.Data[lsx, lsy];
        uint target = tChunk.Pixels.Data[ltx, lty];
        int tMat = (int)Msk.GetMat(target);
        int sMat = (int)Msk.GetMat(source);
        if (MatProps.Densities[sMat] > MatProps.Densities[tMat])
        {
            var tProc = !Msk.GetProcessed(source);
            tChunk.Pixels.Data[ltx, lty] = Msk.SetProcessed(source, tProc);
            sChunk.Pixels.Data[lsx, lsy] = Msk.SetProcessed(target, tProc);
            return true;
        }

        return false;
    }
}