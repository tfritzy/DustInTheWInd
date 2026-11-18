public static class Msk
{
    // 0b_TTTTTTTT_XXXXXXYY_YYYYLLLL_RRRRRRBP

    public const int MATERIAL_OFFSET = 24;
    public const int MATERIAL_BITS = 8;
    public const uint MAT_MASK = 0b_00000000_11111111_11111111_11111111;
    public const uint PROCESSED_MASK = 0b_11111111_11111111_11111111_11111110;
    public const uint PROCESSED_MASK_OP = ~PROCESSED_MASK;

    public static Mat GetMat(uint val) => (Mat)(val >> MATERIAL_OFFSET);
    public static uint SetMaterial(uint val, Mat material)
    {
        val &= MAT_MASK;
        uint source = (uint)material << MATERIAL_OFFSET;
        return val | source;
    }

    public static bool GetProcessed(uint val) => (val & PROCESSED_MASK_OP) > 0;
    public static uint SetProcessed(uint val, bool processed) => processed ? val | 1u : val & PROCESSED_MASK;
    public static uint ToggleProcessed(uint val) => val ^ 1u;
}