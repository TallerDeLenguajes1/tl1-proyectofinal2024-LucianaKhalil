using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace Proyecto
{
    public class HistorialJson
    {
        public void GuardarGanador(Personaje ganador, List<Personaje> enemigos, string nombreArchivo)
        {
            try
            {
                var historial = new
                {
                    Ganador = ganador,
                    Enemigos = enemigos
                };

                var opcionesJson = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(historial, opcionesJson);
                File.WriteAllText(nombreArchivo, json);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al guardar el historial: {e.Message}");
            }
        }

        public dynamic LeerGanadores(string nombreArchivo)
        {
            try
            {
                if (!File.Exists(nombreArchivo) || new FileInfo(nombreArchivo).Length == 0)
                {
                    Console.WriteLine("El archivo no existe o está vacío.");
                    return null;
                }

                string json = File.ReadAllText(nombreArchivo);
                var historial = JsonSerializer.Deserialize<dynamic>(json);
                return historial;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al leer el historial: {e.Message}");
                return null;
            }
        }
    }
}
