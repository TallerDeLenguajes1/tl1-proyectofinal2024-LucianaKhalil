using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Proyecto
{
    public class HistorialJson
    {
        public void GuardarGanador(Personaje ganador, string nombreArchivo)
        {
            try
            {
                List<EntradaHistorial> historial;
                if (File.Exists(nombreArchivo))
                {
                    string jsonExistente = File.ReadAllText(nombreArchivo);
                    historial = JsonSerializer.Deserialize<List<EntradaHistorial>>(jsonExistente) ?? new List<EntradaHistorial>();
                }
                else
                {
                    historial = new List<EntradaHistorial>();
                }

                var ganadorConFecha = new EntradaHistorial
                {
                    Ganador = new Ganador
                    {
                        Datos = ganador.Datos,
                        Caracteristicas = ganador.Caracteristicas
                    },
                    FechaYHora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                };

                historial.Add(ganadorConFecha);

                var opcionesJson = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(historial, opcionesJson);
                File.WriteAllText(nombreArchivo, json);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al guardar el historial: {e.Message}");
            }
        }

        public List<EntradaHistorial> LeerGanadores(string nombreArchivo)
        {
            try
            {
                if (!File.Exists(nombreArchivo) || new FileInfo(nombreArchivo).Length == 0)
                {
                    Console.WriteLine("El archivo no existe o está vacío.");
                    return new List<EntradaHistorial>();
                }

                string json = File.ReadAllText(nombreArchivo);
                List<EntradaHistorial> historial = JsonSerializer.Deserialize<List<EntradaHistorial>>(json);
                return historial ?? new List<EntradaHistorial>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error al leer el historial: {e.Message}");
                return new List<EntradaHistorial>();
            }
        }
    }
}
