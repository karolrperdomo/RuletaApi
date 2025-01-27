public class Regla
{
    // La propiedad 'Id' representa un identificador único para la regla.
    // 'get' se usa para obtener el valor de 'Id' y 'set' se usa para asignar un valor a 'Id'.
    public int Id { get; set; }

    // La propiedad 'Activa' indica si la regla está activa o no.
    // 'get' se usa para obtener el valor de 'Activa' y 'set' se usa para cambiar su valor (verdadero o falso).
    public bool Activa { get; set; }

    // La propiedad 'FechaCreacion' guarda la fecha y hora en que se creó la regla.
    // 'get' se usa para obtener el valor de 'FechaCreacion' y 'set' se usa para asignar una nueva fecha.
    public DateTime FechaCreacion { get; set; }
}
