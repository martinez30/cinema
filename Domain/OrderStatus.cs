using System.ComponentModel;

namespace Domain
{
    public enum OrderStatus
    {
        [Description("Em aberto")]
        Open = 1,
        [Description("Finalizado")]
        Closed = 2,
        [Description("Cancelado")]
        Canceled = 3


    }
}
