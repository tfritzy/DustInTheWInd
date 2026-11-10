public static class Msk
{
    public const int MATERIAL_OFFSET = 24;
    public const int MATERIAL_BITS = 8;
    public const uint MAT_MASK = 0b_00000000_11111111_11111111_11111111;

    public static Mat GetMat(uint val) => (Mat)(val >> MATERIAL_OFFSET);
    public static uint SetMaterial(uint val, Mat material)
    {
        val &= MAT_MASK;
        uint source = (uint)material << MATERIAL_OFFSET;
        return val | source;
    }
}