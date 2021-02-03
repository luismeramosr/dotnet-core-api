using Microsoft.AspNetCore.Mvc;
using dotnet_core_api.Models;
using System.Linq;

namespace dotnet_core_api.Controllers {

    [ApiController]
    [Route("/propietario")]
    public class PropietariosController : ControllerBase {

        private BDTRANSITO2020_EC03Context db = new BDTRANSITO2020_EC03Context();

        [HttpGet]
        [Route("list")]
        public ActionResult<Propietario[]> findAllByInitial(char initial) {
            return Ok(this.db.Propietarios.ToList().Where(p => p.Nompro.StartsWith(initial)));
        }

        [HttpGet]
        [Route("query")]
        public ActionResult<Papeleta[]> findNotPaidByDni(string dni) {
            
            var query = from prop in this.db.Propietarios
                        join v in this.db.Vehiculos
                        on prop.Dnipro equals v.Dnipro
                        join pa in this.db.Papeletas
                        on v.Nropla equals pa.Nropla
                        join inf in this.db.Infracciones
                        on pa.Codinf equals inf.Codinf
                        select new { Propietario = prop, Vehiculo = v, Papeleta = pa, Infraccione = inf };          
                        
            return Ok(query.Where(e => e.Propietario.Dnipro == dni && e.Vehiculo.Dnipro == dni));
        }

    }

}