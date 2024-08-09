using Proyecto;
public class Ganador
{
    public DatosPersonaje Datos { get; set; }
    public CaracteristicasPersonaje Caracteristicas { get; set; }
}

public class EntradaHistorial
{
    public Ganador Ganador { get; set; }
    public string FechaYHora { get; set; }
}
