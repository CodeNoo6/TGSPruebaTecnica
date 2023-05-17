using System;
using SRVParqueMotor.Model;

namespace SRVParqueMotor.Repository.Interface
{
	public interface IParqueaderoRepository
	{
        public void post(Tarjeta objParqueadero);
        public void put(TarjetaActualizacion objTarjetaActualizacion);
        public IEnumerable<TarjetaVehiculo> getAll();

        public void delete(int tarjetaId);

        public decimal cerrarParqueadero();
    }
}