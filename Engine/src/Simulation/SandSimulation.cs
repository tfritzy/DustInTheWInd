public static class SandSimulation
{
    public static void Step(Sandbox sandbox)
    {
        int width = sandbox.Width;
        int height = sandbox.Height;
        uint[] pixels = sandbox.Pixels;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int index = sandbox.Index(x, y);
                Material mat = Msk.GetMaterial(pixels[index]);

                switch (mat)
                {
                    case Material.Air:
                        continue;
                    case Material.Sand:
                        ProcessPowder(sandbox, pixels, x, y, index);
                        break;
                    case Material.Stone:
                        break;
                }
            }
        }
    }

    public static void ProcessPowder(Sandbox sandbox, uint[] pixels, int x, int y, int index)
    {
        int downI = sandbox.Index(x, y + 1);
        uint down = pixels[downI];
        if (Msk.GetMaterial(down) == Material.Air)
        {
            pixels[downI] = pixels[index];
            pixels[index] = Msk.SetMaterial(0u, Material.Air);
        }
    }
}