using System;
namespace SRVParqueMotor.Business.Interface
{
	public interface INegocio
	{
        public decimal calculoDescuentoAmbiental(string dCobro);
        public decimal calculoCostoParqueo(DateTime fIngreso, DateTime fSalida, int sTipo);
        public bool validaFecha(DateTime fIngreso, DateTime fSalida);
    }
}