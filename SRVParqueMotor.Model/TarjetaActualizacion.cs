using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRVParqueMotor.Model
{
    public class TarjetaActualizacion
    {
        public string? Placa { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public int? TipoVehiculoId { get; set; }
        public DateTime? FechaSalida { get; set; }
        public bool? AplicaDescuento { get; set; }
    }
}
