using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRVParqueMotor.Model
{
    public class TarjetaVehiculo
    {
        public int TarjetaId { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime? FechaSalida { get; set; }
        public int VehiculoId { get; set; }
        public string Placa { get; set; }
    }
}
