using System.ComponentModel;

namespace Domain
{
    public enum ProductCategory
    {
        [Description("Bomboniere")]
        Food = 1, 
        [Description("Ingresso")]
        Ticket=2
    }
}
