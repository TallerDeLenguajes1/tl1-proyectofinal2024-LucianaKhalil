using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text.Json;

PersonajeJson salida= await GetPersonaje();

Console.WriteLine("Personaje " + salida);

static async Task<PersonajeJson> GetPersonaje(){
    var url ="https://www.dnd5eapi.co/api/";
     try
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();//si la solicitud es exitosa se lee la respuesta
        string responseBody = await response.Content.ReadAsStringAsync();
        PersonajeJson personajeJson = JsonSerializer.Deserialize<PersonajeJson>(responseBody);//Se deserializa el contenido JSON en un objeto CoinDesk 
        return personajeJson;
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("Problemas de acceso a la API");
        Console.WriteLine("Message :{0} ", e.Message);
        return null;
    }
}