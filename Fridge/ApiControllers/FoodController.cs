using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Fridge.Models;
using Fridge.Data;

namespace Fridge.ApiControllers
{
    public class FoodController : ApiController
    {
        private FridgeContext db = new FridgeContext();

        // GET api/Food
        public IQueryable<Food> GetFoods()
        {
            return db.Foods;
        }

        // GET api/Food/5
        [ResponseType(typeof(Food))]
        public async Task<IHttpActionResult> GetFood(int id)
        {
            Food food = await db.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            return Ok(food);
        }

        // PUT api/Food/5
        public async Task<IHttpActionResult> PutFood(int id, Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != food.Id)
            {
                return BadRequest();
            }

            db.Entry(food).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Food
        [ResponseType(typeof(Food))]
        public async Task<IHttpActionResult> PostFood(Food food)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Foods.Add(food);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = food.Id }, food);
        }

        // DELETE api/Food/5
        [ResponseType(typeof(Food))]
        public async Task<IHttpActionResult> DeleteFood(int id)
        {
            Food food = await db.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            db.Foods.Remove(food);
            await db.SaveChangesAsync();

            return Ok(food);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FoodExists(int id)
        {
            return db.Foods.Count(e => e.Id == id) > 0;
        }
    }
}