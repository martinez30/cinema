using System.ComponentModel;

namespace Domain
{
    public enum MovieClassification
    {
        [Description("Livre")]
        Free = 0,
        [Description("10+")]
        Ten = 10,
        [Description("12+")]
        Twelve = 12,
        [Description("14+")]
        Fourteen = 14,
        [Description("16+")]
        Sixteen = 16,
        [Description("18+")]
        Eighteen = 18
    }
}
