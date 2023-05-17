using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SRVParqueMotor.Model;
public class Vehiculo
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Placa { get; set; }
    public TipoVehiculo Tipo { get; set; }

    [JsonIgnore]
    public Catalogo? Catalogo { get; set; }
}
