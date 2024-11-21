using System;
using System.Collections.Generic;

namespace api.Models;

public partial class Pago
{
    public int Id { get; set; }

    public int IdInscripcion { get; set; }

    public decimal Monto { get; set; }

    public DateTime? FechaPago { get; set; }

    public string MetodoPago { get; set; } = null!;

    public string? Estado { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Inscripcione IdInscripcionNavigation { get; set; } = null!;
}
