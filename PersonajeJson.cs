namespace Proyecto{
using System.Text.Json.Serialization;
using System;
using System.Text.Json;
using System.IO;

public class PersonajesJson
{
    public void GuardarPersonajes(List<Personaje> personajes, string nombreArchivo)//metodo que reciba lista psjes y archivo y guarde en json
    {
        try
        {
            var opcionesJson = new JsonSerializerOptions { WriteIndented = true };//writeIntended da formato al json
            string json = JsonSerializer.Serialize(personajes, opcionesJson);
            File.WriteAllText(nombreArchivo, json);
            Console.WriteLine($"personajes guardados en el archivo: {nombreArchivo}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"error al guardar personajes: {e.Message}");
        }
    }
    public List<Personaje> LeerPersonajes(string nombreArchivo)
    {
        try
        {
            if (!File.Exists(nombreArchivo) || new FileInfo(nombreArchivo).Length == 0)
            {
                throw new FileNotFoundException("El archivo no existe o esta vacío.");
            }

            string json = File.ReadAllText(nombreArchivo);
            List<Personaje> personajes = JsonSerializer.Deserialize<List<Personaje>>(json);
            return personajes;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al leer personajes: {e.Message}");
            return new List<Personaje>();
        }
    }

    public bool Existe(string nombreArchivo)
    {
        try
        {
            return File.Exists(nombreArchivo) && new FileInfo(nombreArchivo).Length > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al verificar existencia del archivo: {e.Message}");
            return false;
        }
    }
}
}