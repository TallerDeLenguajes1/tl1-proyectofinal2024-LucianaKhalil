using Proyecto;
public class Ganador//clase para guardar y luego leer el ganador en el json del historial
{
    public DatosPersonaje Datos { get; set; }
    public CaracteristicasPersonaje Caracteristicas { get; set; }
}

public class EntradaHistorial
{
    public Ganador Ganador { get; set; }
    public string FechaYHora { get; set; }
}
