using System.Globalization;
using SRVParqueMotor.Business.Interface;

namespace SRVParqueMotor.Business;

public class Negocio : INegocio
{
    public decimal calculoDescuentoAmbiental(string dCobro)
    {
        decimal dCalculo;
        Decimal.TryParse(dCobro, out dCalculo);
        return (dCalculo * (1 - 0.25M));
    }

    public decimal calculoCostoParqueo(DateTime fIngreso, DateTime fSalida, int sTipo)
    {
        double horas;

        if (fSalida.Date > fIngreso.Date || (fSalida.Date == fIngreso.Date && fSalida.Hour >= fIngreso.Hour))
        {
            horas = (fSalida - fIngreso).TotalHours;
        }
        else
        {
            horas = (24 - fIngreso.Hour + fSalida.Hour) % 24;
        }

        decimal costoPorHora = 0;

        if (sTipo == 1)
        {
            costoPorHora = 62;
        }
        else if (sTipo == 2)
        {
            costoPorHora = 120;
        }
        return costoPorHora * (decimal)horas;
    }

    public bool validaFecha(DateTime fIngreso, DateTime fSalida)
    {
        return fIngreso.Date <= fSalida.Date && fIngreso.TimeOfDay < fSalida.TimeOfDay;
    }
}