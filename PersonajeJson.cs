using System.Text.Json.Serialization;
using System;
using System.Text.Json;
using System.IO;

public class PersonajesJson
{
    public void GuardarPersonajes(List<PersonajeJson> personajes, string nombreArchivo)//metodo que recciba lista psjes y archivo y guarde en json
    {
        try
        {
            var opcionesJson = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(personajes, opcionesJson);
            File.WriteAllText(nombreArchivo, json);
            Console.WriteLine($"personajes guardados en el archivo: {nombreArchivo}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"error al guardar personajes: {e.Message}");
        }
    }
    public List<PersonajeJson> LeerPersonajes(string nombreArchivo)
    {
        try
        {
            if (!File.Exists(nombreArchivo) || new FileInfo(nombreArchivo).Length == 0)
            {
                throw new FileNotFoundException("El archivo no existe o está vacío.");
            }

            string json = File.ReadAllText(nombreArchivo);
            List<PersonajeJson> personajes = JsonSerializer.Deserialize<List<PersonajeJson>>(json);
            return personajes;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error al leer personajes: {e.Message}");
            return new List<PersonajeJson>();
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
