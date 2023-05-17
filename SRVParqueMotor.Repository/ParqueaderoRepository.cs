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

    public IEnumerable<TarjetaVehiculo> getAll()
    {
        using (var connection = new NpgsqlConnection(this._IobjConfiguration.GetSection("PostgreSQLConnection").Value))
        {
            connection.Open();

            string query = @"
            SELECT t.id AS tarjeta_id, t.fecha_ingreso, t.fecha_salida, v.id AS vehiculo_id, v.placa
            FROM tarjeta t
            INNER JOIN vehiculo v ON t.vehiculo_id = v.id";

            return connection.Query<TarjetaVehiculo>(query).ToList();
        }
    }

    public void post(Tarjeta objTarjeta)
    {
        if (this._IObjNegocio.validaFecha(objTarjeta.FechaIngreso, objTarjeta.FechaSalida))
        {
            if (!validarPlazas(objTarjeta.objVehiculo.Tipo.Id))
            {
                objTarjeta.Costo = this._IObjNegocio.calculoCostoParqueo(objTarjeta.FechaIngreso, objTarjeta.FechaSalida, objTarjeta.objVehiculo.Tipo.Id);
                Catalogo objCatalogo = new Catalogo();
                objTarjeta.objVehiculo.Catalogo = objCatalogo;
                if (objTarjeta.aplicaDescuento)
                {
                    objTarjeta.Costo = this._IObjNegocio.calculoDescuentoAmbiental(objTarjeta.Costo.ToString());
                    objTarjeta.objVehiculo.Catalogo.Id = 1;
                }else
                {
                    objTarjeta.objVehiculo.Catalogo.Id = 2;
                }

                using (var connection = new NpgsqlConnection(this._IobjConfiguration.GetSection("PostgreSQLConnection").Value))
                {
                    string query = @"
                        WITH vehiculo_insert AS (
                            INSERT INTO vehiculo (placa, tipo_vehiculo_id, catalogo_id)
                            VALUES (@placa, @tipo_vehiculo_id, @catalogo_id)
                            RETURNING id
                        )
                        INSERT INTO tarjeta (fecha_ingreso, fecha_salida, vehiculo_id, costo, plaza, estado, aplicadescuento)
                        SELECT @fecha_ingreso, @fecha_salida, id, @costo, @plaza, @estado, @aplicadescuento
                        FROM vehiculo_insert;
                         ";

                    var parameters = new
                    {
                        placa = objTarjeta.objVehiculo.Placa,
                        tipo_vehiculo_id = objTarjeta.objVehiculo.Tipo.Id,
                        catalogo_id = objTarjeta.objVehiculo.Catalogo.Id,
                        fecha_ingreso = objTarjeta.FechaIngreso,
                        fecha_salida = objTarjeta.FechaSalida,
                        costo = objTarjeta.Costo,
                        plaza = objTarjeta.Plaza,
                        estado = true,
                        aplicadescuento = objTarjeta.aplicaDescuento
                    };

                    connection.Execute(query, parameters);
                }
            }else
            {
                throw new Exception("No hay mas plazas disponibles para el tipo de vehiculo " + objTarjeta.objVehiculo.Tipo.Id);
            }
        } else {
            throw new Exception("Revise la fecha, pueden ser iguales pero la hora de ingreso debe ser menor a la de salida");
        }
    }

    public void delete(int tarjetaId)
    {
        using (var connection = new NpgsqlConnection(this._IobjConfiguration.GetSection("PostgreSQLConnection").Value))
        {
            connection.Open();

            string deleteQuery = @"
            DELETE FROM tarjeta
            WHERE id = @tarjetaId";

            var deleteParameters = new { tarjetaId };

            connection.Execute(deleteQuery, deleteParameters);

         }
    }

    public void put(TarjetaActualizacion objTarjetaActualizacion)
    {
        string placa = objTarjetaActualizacion.Placa;
        DateTime? fechaIngreso = objTarjetaActualizacion.FechaIngreso;
        int? tipoVehiculoId = objTarjetaActualizacion.TipoVehiculoId;
        DateTime? fechaSalida = objTarjetaActualizacion.FechaSalida;
        bool? aplicaDescuento = objTarjetaActualizacion.AplicaDescuento;

        int tarjetaId = ObtenerIdTarjeta(placa, fechaIngreso);

        using (var connection = new NpgsqlConnection(this._IobjConfiguration.GetSection("PostgreSQLConnection").Value))
        {
            connection.Open();

            string updateQuery = @"
            UPDATE tarjeta
            SET fecha_ingreso = @fechaIngreso,
                tipo_vehiculo_id = @tipoVehiculoId,
                fecha_salida = @fechaSalida,
                aplica_descuento = @aplicaDescuento
            WHERE id = @tarjetaId";

            var parameters = new
            {
                tarjetaId,
                fechaIngreso,
                tipoVehiculoId,
                fechaSalida,
                aplicaDescuento
            };

            connection.Execute(updateQuery, parameters);
        }
    }

    public int ObtenerIdTarjeta(string placa, DateTime? fechaIngreso)
    {
        using (var connection = new NpgsqlConnection(this._IobjConfiguration.GetSection("PostgreSQLConnection").Value))
        {
            connection.Open();

            string query = "SELECT id FROM tarjeta WHERE placa = @placa AND fecha_ingreso = @fechaIngreso";
            var parameters = new { placa, fecha_ingreso = fechaIngreso };

            int tarjetaId = connection.ExecuteScalar<int>(query, parameters);

            return tarjetaId;
        }
    }

    public bool validarPlazas(int tipo)
    {
        using (var connection = new NpgsqlConnection(this._IobjConfiguration.GetSection("PostgreSQLConnection").Value))
        {
            connection.Open();

            string query;
            int totalPlazas;

            if (tipo == 1)
            {
                query = @"
                        SELECT COUNT(*) AS total_plazas
                        FROM vehiculo v
                        JOIN tipo_vehiculo tv ON v.tipo_vehiculo_id = tv.id
                        WHERE tv.descripcion = 'Motocicleta';";
                totalPlazas = connection.ExecuteScalar<int>(query);
                return totalPlazas > 6;
            }

            query = @"
                        SELECT COUNT(*) AS total_plazas
                        FROM vehiculo v
                        JOIN tipo_vehiculo tv ON v.tipo_vehiculo_id = tv.id
                        WHERE tv.descripcion = 'Vehiculo Ligero';";
            totalPlazas = connection.ExecuteScalar<int>(query);
            return totalPlazas > 5;
        }
    }

    public decimal cerrarParqueadero()
    {
        decimal totalCostos;
        using (var connection = new NpgsqlConnection(this._IobjConfiguration.GetSection("PostgreSQLConnection").Value))
        {
            string query;
            connection.Open();

            query = @"
                SELECT t.id, t.aplicadescuento, t.fecha_ingreso, tp.id AS tipo_vehiculo
                FROM tarjeta AS t
                INNER JOIN vehiculo AS v ON t.vehiculo_id = v.id
                INNER JOIN tipo_vehiculo AS tp ON v.tipo_vehiculo_id = tp.id
                WHERE t.estado = true";

            var results = connection.Query(query);

            foreach (var result in results)
            {
                DateTime fechaIngreso = result.fecha_ingreso;
                DateTime fechaMomentoCierre = DateTime.Now;
                int tipoVehiculo = result.tipo_vehiculo;

                decimal nuevoCosto = this._IObjNegocio.calculoCostoParqueo(fechaIngreso, fechaMomentoCierre, tipoVehiculo);
                if (result.aplicadescuento)
                {
                    nuevoCosto = this._IObjNegocio.calculoDescuentoAmbiental(nuevoCosto.ToString());

                }
                int tarjetaId = result.id;

                string updateQuery = @"
                UPDATE tarjeta
                SET estado = false,
                    costo = @nuevoCosto
                WHERE estado = true
                    AND id = @tarjetaId";

                var parameters = new { nuevoCosto, tarjetaId };

                connection.Execute(updateQuery, parameters);
            }

            query = "SELECT SUM(costo) FROM tarjeta";
            totalCostos = connection.ExecuteScalar<decimal>(query);
        }
        return totalCostos;
    }
}