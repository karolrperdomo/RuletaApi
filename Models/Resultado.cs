public class Resultado
{
    // El 'Id' es el identificador único de cada resultado.
    public int Id { get; set; }

    // 'ReglaId' hace referencia a la regla asociada con este resultado.
    public int ReglaId { get; set; }

    // 'NumeroGanador' es el número que salió ganador en la apuesta.
    public int NumeroGanador { get; set; }

    // 'ColorGanador' es el color asociado al número ganador (Rojo o Negro).
    // 'required' indica que este valor debe ser proporcionado obligatoriamente.
    public required string ColorGanador { get; set; }

    // 'MontoGanado' es el total que se ha ganado por las apuestas en este resultado.
    public decimal MontoGanado { get; set; }
}
