using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMRestApi2023.Models;

namespace LMRestApi2023.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
       // private readonly northwindContext db = new northwindContext();

        // Dependency Injection tyyli
        private readonly Context db;

        public CustomersController(Context dbparam)
        {
            db = dbparam;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var customers = db.Customers;
            return Ok(customers);
          
        }

        // Get 1 customer by id
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetOneCustomer(string id)
        {
            try
            {
                var asiakas = db.Customers.Find(id);

                if (asiakas == null) {
                    return NotFound("Asiakasta id:llä " + id + " ei löytynyt.");
                  }

                return Ok(asiakas);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

       
        [HttpDelete]
        [Route("{id}")]
        public ActionResult Remove(string id)
        {
            var customer = db.Customers.Find(id);
            if (customer == null)
            {
                return NotFound("Asiakasta id:llä " + id + " ei löytynyt");
            }
            else
            {
                try
                {
                    db.Customers.Remove(customer);
                    db.SaveChanges();

                    return Ok("Removed customer " + customer.CompanyName);
                }
                catch (Exception e)
                {
                    return BadRequest("Poisto ei onnistunut. Ongelma saattaa johtua siitä, jos asiakkaalla on tilauksia?");
                }
            }  
        }

        // Uuden lisääminen
        [HttpPost]
        public ActionResult PostCreateNew([FromBody] Customer asiakas)
        {
            try
            {
                db.Customers.Add(asiakas);
                db.SaveChanges();
                //return Created(".../api/customers", asiakas); <-- yksi tapa tämäkin
                return Ok($"Luotiin {asiakas.CompanyName}");

            }
            catch (Exception e)
            {
                return BadRequest("Asiakkaan lisääminen ei onnistunut. Tässä lisätietoa: " + e);
            }
        }

        //Muokkaus
        [HttpPut]
        [Route("{id}")]
        public ActionResult PutEdit(string id, [FromBody] Customer asiakas)
        {

            if (asiakas == null)
            {
                return BadRequest("Asiakas puuttuu pyynnön bodysta");
            }

            try
            {
                var customer = db.Customers.Find(id);

                if (customer != null)
                {
                    customer.CompanyName = asiakas.CompanyName;
                    customer.VauvaIka = asiakas.VauvaIka;
                    customer.SynnytysViikko = asiakas.SynnytysViikko;
                    customer.SynnytysResume = asiakas.SynnytysResume;
                    customer.SynnytysHaaste = asiakas.SynnytysHaaste;
                    customer.ContactName = asiakas.ContactName;
                    customer.ContactTitle = asiakas.ContactTitle;
                    customer.Country = asiakas.Country;
                    customer.Address = asiakas.Address;
                    customer.City = asiakas.City;
                    customer.PostalCode = asiakas.PostalCode;
                    customer.Phone = asiakas.Phone;
                    customer.Fax = asiakas.Fax;

                    db.SaveChanges();
                    return Ok("Muokattu asiakasta: " + customer.CompanyName);
                }
                else
                {
                    return NotFound("Päivitettävää asiakasta ei löytynyt!");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Jokin meni pieleen asiakasta päivitettäessä. Alla lisätietoa" + e);
            }
           
        }     

    }
}
