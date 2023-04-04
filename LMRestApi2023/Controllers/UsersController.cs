using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMRestApi2023.Models;

namespace LMRestApi2023.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // private readonly northwindContext db = new northwindContext();

        // Dependency Injection tyyli
        private readonly Context db;

        public UsersController(Context dbparam)
        {
            db = dbparam;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            var users = db.Users;


            foreach (var user in users)
            {
                user.Password = null;
            }
            return Ok(users);

        }

        // Uuden lisääminen
        [HttpPost]
        public ActionResult PostCreateNew([FromBody] User u)
        {
            try
            {

                db.Users.Add(u);
                db.SaveChanges();
                return Ok("Lisättiin käyttäjä " + u.Username);
            }
            catch (Exception e)
            {
                return BadRequest("Lisääminen ei onnistunut. Tässä lisätietoa: " + e);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Remove(int id)
        {
            var user = db.Users.Find(id);
            if (user == null)
            {
                return NotFound("Asiakasta id:llä " + id + " ei löytynyt");
            }
            else
            {
                try
                {
                    db.Users.Remove(user);
                    db.SaveChanges();

                    return Ok("Removed user " + user.Username);
                }
                catch (Exception e)
                {
                    return BadRequest("Poisto ei onnistunut. Ongelma saattaa johtua siitä, jos asiakkaalla on tilauksia?");
                }
            }
        }

        //Muokkaus
        [HttpPut]
        [Route("{id}")]
        public ActionResult PutEdit(int id, [FromBody] User kayttaja)
        {

            if (kayttaja == null)
            {
                return BadRequest("käyttajä puuttuu pyynnön bodysta");
            }

            try
            {
                var user = db.Users.Find(id);

                if (user != null)
                {
                    user.UserId = kayttaja.UserId;
                    user.Firstname = kayttaja.Firstname;
                    user.Lastname = kayttaja.Lastname;
                    user.Email =   kayttaja.Email;
                    user.Username = kayttaja.Username;
                    user.Password = kayttaja.Password;
                    user.AccesslevelId = kayttaja.AccesslevelId;
                    
                    db.SaveChanges();
                    return Ok("Muokattu asiakasta: " + user.Username);
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



