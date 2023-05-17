using System;
using SRVParqueMotor.Model;

namespace SRVParqueMotor.Repository.Interface
{
	public interface IParqueaderoRepository
	{
        public void post(Tarjeta objParqueadero);
        public void put(int id_tarjeta, Tarjeta objTarjeta);
        public Tarjeta getById(int id_tarjeta);
        public IEnumerable<Tarjeta> getAll();
    }
}