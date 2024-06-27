using Proyecto;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace claseAPI{
    public class Referencia
{
    [JsonPropertyName("index")]
    public string index { get; set; }

    [JsonPropertyName("name")]
    public string name { get; set; }

    [JsonPropertyName("url")]
    public  string url { get; set; }
}
}
