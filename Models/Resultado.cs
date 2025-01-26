public class Resultado
{
    public int Id { get; set; }
    public int ReglaId { get; set; }
    public int NumeroGanador { get; set; }
    public required string ColorGanador { get; set; }
    public decimal MontoGanado { get; set; }
}
