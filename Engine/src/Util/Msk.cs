public static class Msk
{
    public const int MATERIAL_OFFSET = 24;
    public const int MATERIAL_BITS = 8;
    public const uint MAT_MASK = 0b_00000000_11111111_11111111_11111111;

    public static Material GetMaterial(uint val) => (Material)(val >> MATERIAL_OFFSET);
    public static uint SetMaterial(uint val, Material material)
    {
        val &= MAT_MASK;
        uint source = (uint)material << MATERIAL_OFFSET;
        return val | source;
    }
}