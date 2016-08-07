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
using ReallyEstate.Models;

namespace ReallyEstate.Controllers
{
    public class ListingsController : ApiController
    {
        private ReallyEstateContext db = new ReallyEstateContext();

        // GET: api/Listings
        public IQueryable<Listing> GetListings()
        {
            return db.Listings;
        }

        // GET: api/Listings/5
        [ResponseType(typeof(Listing))]
        public async Task<IHttpActionResult> GetListing(int id)
        {
            Listing listing = await db.Listings.FindAsync(id);
            if (listing == null)
            {
                return NotFound();
            }

            return Ok(listing);
        }

        // PUT: api/Listings/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutListing(int id, Listing listing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != listing.Id)
            {
                return BadRequest();
            }

            db.Entry(listing).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingExists(id))
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

        // POST: api/Listings
        [ResponseType(typeof(Listing))]
        public async Task<IHttpActionResult> PostListing(Listing listing)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Listings.Add(listing);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = listing.Id }, listing);
        }

        // DELETE: api/Listings/5
        [ResponseType(typeof(Listing))]
        public async Task<IHttpActionResult> DeleteListing(int id)
        {
            Listing listing = await db.Listings.FindAsync(id);
            if (listing == null)
            {
                return NotFound();
            }

            db.Listings.Remove(listing);
            await db.SaveChangesAsync();

            return Ok(listing);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ListingExists(int id)
        {
            return db.Listings.Count(e => e.Id == id) > 0;
        }
    }
}