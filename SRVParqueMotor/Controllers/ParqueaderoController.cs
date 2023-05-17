using System;
using Microsoft.AspNetCore.Mvc;
using SRVParqueMotor.Business.Interface;
using SRVParqueMotor.Model;
using SRVParqueMotor.Repository.Interface;

namespace SRVParqueMotor.Controllers
{
	[ApiController]
	public class ParqueaderoController : ControllerBase
	{
        private readonly IParqueaderoRepository _IObjParqueaderoRepository;
        

        public ParqueaderoController(IParqueaderoRepository objParqueaderoRepository)
        {
            this._IObjParqueaderoRepository = objParqueaderoRepository;
        }

        /// <summary>
        /// Lista las tarjetas entregadas ha vehiculos que ingresaron al parqueadero.
        /// </summary>
        [HttpGet]
        [Route("api/ListarTarjetas")]
        public async Task<IActionResult> listarVehiculos()
        {
            try
            {
                return Ok(this._IObjParqueaderoRepository.getAll());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Vehiculo Registrado Exitosamente");
        }

        /// <summary>
        /// Registra una nueva tarjeta en la base de datos.
        /// </summary>
        /// <remarks>
        /// JSON de ejemplo, el valor de tipo de vehiculo 1->Motocicleta || 2->Vehiculo Ligero:
        ///
        ///     POST /api/IngresarTarjeta
        ///     {
        ///        "fechaIngreso": "2023-05-16T09:30:00",
        ///        "fechaSalida": "2023-05-16T10:30:00",
        ///        "plaza": 1,
        ///        "objVehiculo": {
        ///             "placa": "HTE524",
        ///              "tipo": {
        ///                 "id": 2
        ///               }
        ///        },
        ///        "aplicaDescuento": true
        ///     }
        ///
        /// </remarks>
        [HttpPost]
		[Route("api/IngresarTarjeta")]
        public async Task<IActionResult> ingresoVehiculo(Tarjeta objTarjeta)
        {
            try
            {
                this._IObjParqueaderoRepository.post(objTarjeta);
                return Ok(); 
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Vehiculo Registrado Exitosamente");
        }

        /// <summary>
        /// Actualiza la tarjeta por si el sistema sufre algun fallo.
        /// </summary>
        /// <remarks>
        /// JSON de ejemplo para una actualización:
        ///
        ///     PUT /api/ActualizarTarjeta
        ///     {
        ///        "placa": "string",
        ///        "fechaIngreso": "2023-05-16T09:30:00",
        ///        "tipoVehiculoId": 0,
        ///        "fechaSalida": "2023-05-16T10:30:00",
        ///        "aplicaDescuento": true
        ///     }
        ///
        /// </remarks>
        [HttpPut]
        [Route("api/ActualizarTarjeta")]
        public async Task<IActionResult> actualizar(TarjetaActualizacion objTarjetaActualizacion)
        {
            try
            {
                this._IObjParqueaderoRepository.put(objTarjetaActualizacion);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok("Vehiculo Registrado Exitosamente");
        }

        /// <summary>
        /// Elimina las tarjetas o positivos que genere el sistema mediante un id
        /// </summary>
        [HttpDelete]
        [Route("api/EliminarTarjeta")]
        public async Task<IActionResult> Eliminar(int idTarjeta)
        {
            try
            {
                this._IObjParqueaderoRepository.delete(idTarjeta);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return BadRequest("Error en procedimiento de servicio no se han encontrado datos");
        }

        /// <summary>
        /// Cierra el parqueadero realizando una busqueda de los ehiculos que se encuentran aun alli, los selecciona y les va generando un cóbro.
        /// </summary>
        [HttpGet]
        [Route("api/CerrarParqueadero")]
        public async Task<IActionResult> cerrarParqueadero()
        {
            try
            {
                return Ok("El valor total hasta la fecha de cierre es de: " + this._IObjParqueaderoRepository.cerrarParqueadero());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return BadRequest("Error en procedimiento de servicio no se han encontrado datos");
        }
    }
}