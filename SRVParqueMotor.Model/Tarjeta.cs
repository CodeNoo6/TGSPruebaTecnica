using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SRVParqueMotor.Model
{
    public class Tarjeta
    {
        public int Id { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaSalida { get; set; }
        public int VehiculoId { get; set; }
        public Vehiculo objVehiculo { get; set; }
    }
}