using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MonoGameWepAPI.Models;

namespace MonoGameWepAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/GamePlayer")]
    public class PlayersController : ApiController
    {
        private GameContext db = new GameContext();

        // GET: api/Players
        public IQueryable<DbPlayer> GetPlayers()
        {
            return db.Players;
        }

        // GET: api/Players/5
        [ResponseType(typeof(DbPlayer))]
        public async Task<IHttpActionResult> GetDbPlayer(string id)
        {
            DbPlayer dbPlayer = await db.Players.FindAsync(id);
            if (dbPlayer == null)
            {
                return NotFound();
            }

            return Ok(dbPlayer);
        }

        // PUT: api/Players/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDbPlayer(string id, DbPlayer dbPlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dbPlayer.Id)
            {
                return BadRequest();
            }

            db.Entry(dbPlayer).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DbPlayerExists(id))
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

        // POST: api/Players
        [ResponseType(typeof(DbPlayer))]
        public async Task<IHttpActionResult> PostDbPlayer(DbPlayer dbPlayer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Players.Add(dbPlayer);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DbPlayerExists(dbPlayer.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = dbPlayer.Id }, dbPlayer);
        }

        // DELETE: api/Players/5
        [ResponseType(typeof(DbPlayer))]
        public async Task<IHttpActionResult> DeleteDbPlayer(string id)
        {
            DbPlayer dbPlayer = await db.Players.FindAsync(id);
            if (dbPlayer == null)
            {
                return NotFound();
            }

            db.Players.Remove(dbPlayer);
            await db.SaveChangesAsync();

            return Ok(dbPlayer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DbPlayerExists(string id)
        {
            return db.Players.Count(e => e.Id == id) > 0;
        }
    }
}