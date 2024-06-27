using System.Text.Json.Serialization;
using System;
using System.Net.Http;
using System.Text.Json;

namespace caracteristicasBonosyRazas{
    class Bonos{
        public (int Fuerza, int Destreza, int Velocidad) ObtenerBonosPorRaza(string raza)
        {
            switch (raza)
            {
                case "Dragonborn":
                    return (7, 8, 9);
                case "Dwarf":
                    return (4, 5, 6);
                case "Elf":
                    return (3, 7, 5);
                case "Gnome":
                    return (2, 6, 4);
                case "Half-Elf":
                    return (5, 8, 3);
                case "Half-Orc":
                    return (6, 4, 7);
                case "Halfling":
                    return (2, 7, 3);
                case "Human":
                    return (5, 5, 5);
                case "Tiefling":
                    return (3, 4, 5);
                default:
                    return (0, 0, 0);
            }
        }

        public (int Fuerza, int Destreza, int Velocidad) ObtenerBonosPorClase(string clase)
        {
            switch (clase)
            {
                case "Barbarian":
                    return (9, 6, 8);
                case "Bard":
                    return (4, 9, 7);
                case "Cleric":
                    return (3, 5, 6);
                case "Druid":
                    return (2, 4, 3);
                case "Fighter":
                    return (8, 6, 7);
                case "Monk":
                    return (5, 8, 6);
                case "Paladin":
                    return (7, 5, 8);
                case "Ranger":
                    return (4, 9, 7);
                case "Rogue":
                    return (3, 9, 5);
                case "Sorcerer":
                    return (2, 4, 3);
                case "Warlock":
                    return (5, 3, 6);
                case "Wizard":
                    return (2, 6, 4);
                default:
                    return (0, 0, 0);
            }
        }
    }
  
    }
    
