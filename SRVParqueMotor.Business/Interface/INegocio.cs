using System;
namespace SRVParqueMotor.Business.Interface
{
	public interface INegocio
	{
        public decimal calculoDescuentoAmbiental(string sCatalogo, decimal dCobro);
        public bool validaFecha(string fInicial, string fFinal);
    }
}