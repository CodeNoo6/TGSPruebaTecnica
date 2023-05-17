using System.Data;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SRVParqueMotor.Business.Interface;
using SRVParqueMotor.Model;
using SRVParqueMotor.Repository.Interface;

namespace SRVParqueMotor.Repository;
public class ParqueaderoRepository : IParqueaderoRepository
{
    private readonly IConfiguration _IobjConfiguration;
    private readonly INegocio _IObjNegocio;

    public ParqueaderoRepository(IConfiguration IobjConfiguration, INegocio IobjNegocio)
    {
        this._IobjConfiguration = IobjConfiguration;
        this._IObjNegocio = IobjNegocio;
    }

    public IEnumerable<Tarjeta> getAll()
    {
        using (var connection = new NpgsqlConnection(this._IobjConfiguration.GetSection("PostgreSQLConnection").Value))
        {
            connection.Open();

            string query = "SELECT id, fecha_ingreso, fecha_salida, vehiculo_id FROM tarjeta";
            var tarjetas = connection.Query<Tarjeta>(query);

            return tarjetas.ToList();
        }
    }

    public Tarjeta getById(int id_tarjeta)
    {
        throw new NotImplementedException();
    }

    public void post(Tarjeta objTarjeta)
    {
        // 1. We will create a connection
        using (var connection = new NpgsqlConnection(this._IobjConfiguration.GetSection("PostgreSQLConnection").Value))
        {
            string commandText = $"INSERT INTO vehiculo (placa,tipo_id,catalogo_id) VALUES (@placa, @tipo_id, @catalogo_id)";

            var queryArguments = new
            {
                placa = objTarjeta.Id,
                tipo_id = objTarjeta.objVehiculo.Tipo.Id,
                catalogo_id = objTarjeta.objVehiculo.Catalogo.Id
            };

            connection.Execute(commandText, queryArguments);
        }


        //using (var connection = new NpgsqlConnection(this._IobjConfiguration.GetSection("PostgreSQLConnection").Value))
        //{
        //    connection.Open();

        //    Vehiculo objVehiculo = new Vehiculo()
        //    {
        //        Placa = "Prueba"
        //    };

        //    string vehiculoQuery = "INSERT INTO vehiculo (placa,tipo_id,catalogo_id) VALUES ('d', 1, 1)";

        //    connection.Execute(vehiculoQuery);




        //    //int vehiculoId = connection.ExecuteScalar<int>(vehiculoQuery, tarjeta.Vehiculo);
        //    //tarjeta.Vehiculo.Id = vehiculoId;

        //    // Insertar la tarjeta con el ID del vehiculo correspondiente
        //    //string tarjetaQuery = "INSERT INTO tarjeta (fecha_ingreso, fecha_salida, vehiculo_id) VALUES (@FechaIngreso, @FechaSalida, @VehiculoId)";
        //    //connection.Execute(tarjetaQuery, tarjeta);
        //}
    }

    public void put(int id_tarjeta, Tarjeta objTarjeta)
    {
        throw new NotImplementedException();
    }

    public bool validarPlazas()
    {
        return true;
    }
}