public class Apuesta
{
    public int Id { get; set; } // Identificador único de la apuesta
    public int UsuarioId { get; set; } // Relación con el usuario que realiza la apuesta
    public int ReglaId { get; set; } // Relación con la regla activa
    public required string TipoApuesta { get; set; } // Tipo de apuesta (Ej: "Número", "Color", etc.)
    public required string ValorApuesta { get; set; } // Detalle de la apuesta (Ej: "17", "Rojo")
    public decimal Monto { get; set; } // Monto apostado
    public DateTime FechaApuesta { get; set; } // Fecha y hora de la apuesta
}