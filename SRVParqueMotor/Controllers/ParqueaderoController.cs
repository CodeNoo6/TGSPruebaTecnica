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

		[HttpPost]
		[Route("api/IngresarVehiculo")]
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
    }
}