
using System.Text.Json.Serialization;

namespace SRVParqueMotor.Model
{
    public class TipoVehiculo
    {
        public int Id { get; set; }

        [JsonIgnore]
        public string? Descripcion { get; set; }
    }
}

