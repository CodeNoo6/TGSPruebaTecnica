using System;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SRVParqueMotor.Model
{
    public class Tarjeta
    {
        [JsonIgnore]
        public int Id { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaSalida { get; set; }
        [JsonIgnore]
        public int VehiculoId { get; set; }
        [JsonIgnore]
        public decimal Costo { get; set; }
        public int Plaza { get; set; }
        public Vehiculo objVehiculo { get; set; }
        public bool aplicaDescuento { get; set; }
    }
}