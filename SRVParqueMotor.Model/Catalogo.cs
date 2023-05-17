using System;
using System.Text.Json.Serialization;

namespace SRVParqueMotor.Model
{
    public class Catalogo
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public string? Descripcion { get; set; }
    }
}