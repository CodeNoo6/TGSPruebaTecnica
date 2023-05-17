using System.Globalization;
using SRVParqueMotor.Business.Interface;

namespace SRVParqueMotor.Business;

public class Negocio : INegocio
{
    public decimal calculoDescuentoAmbiental(string sCatalogo, decimal dCobro)
    {
        if (sCatalogo.Equals("Electrico") || sCatalogo.Equals("Hibrido"))
        {
            return (dCobro * (1 + 0.25M));
        }
        return dCobro;
    }

    public bool validaFecha(string fInicial, string fFinal)
    {
        DateTime fechaInicio;
        DateTime fechaFin;

        if (DateTime.TryParseExact(fInicial, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaInicio) &&
            DateTime.TryParseExact(fFinal, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaFin))
        {
            if (fechaInicio <= fechaFin && fechaInicio.TimeOfDay < fechaFin.TimeOfDay)
            {
                return true;
            }
        }
        return false;
    }
}