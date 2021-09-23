using FlowerAPI.FlowerModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlowerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlowerController : ControllerBase
    {
        public readonly dbFlowerStoreContext db;
        public FlowerController(dbFlowerStoreContext db)
        {
            this.db = db;
        }


        //---------------------------------------------Customer Registration-----------------------------
        [HttpPost]
        [Route("RegisterCustomer")]
        public async Task<IActionResult> RegisterCustomer(Customer temp)
        {
            db.Customers.Add(temp);
            await db.SaveChangesAsync();
            return Ok();
        }

        //---------------------------------------------Customer Login------------------------------------
        [HttpGet]
        [Route("CustomerLogin")]
        public async Task<ActionResult<Customer>> CustomerLogin(string tempInput,string tempPass)
        {
            foreach (var temp in await db.Customers.ToListAsync())
            {
                if ((temp.Email.ToLower() ==tempInput.ToLower() || temp.Phone == tempInput) && temp.Password == tempPass)
                {
                    return Ok();
                }
            }

            return NotFound();
            
        }

        //---------------------------------------------Get Customer by ID---------------------------------
        [HttpGet]
        [Route("CustomerbyId")]
        public async Task<ActionResult<Customer>> CustomerbyId(int id)
        {
            Customer temp = await db.Customers.FindAsync(id);
            return Ok(temp);
        }

        //---------------------------------------------Update Customer Details-----------------------------
        [HttpPut]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(Customer temp)
        {
            db.Entry(temp).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return Ok();
        }

        //---------------------------------------------Flower Registration-------------------------------
        [HttpPost]
        [Route("RegisterFlower")]
        public async Task<IActionResult> RegisterFlower(Flower temp)
        {
            db.Flowers.Add(temp);
            await db.SaveChangesAsync();
            return Ok();
        }

        //---------------------------------------------Get Flower by ID---------------------------------
        [HttpGet]
        [Route("FlowerbyId")]
        public async Task<ActionResult<Flower>> FlowerbyId(int id)
        {
            Flower temp = await db.Flowers.FindAsync(id);
            return Ok(temp);
        }

        //---------------------------------------------Get Flower by Name-------------------------------
        [HttpGet]
        [Route("FlowerbyName")]
        public async Task<ActionResult<Flower>> FlowerbyId(string flowername)
        {
            List<Flower> flowerlist = new List<Flower>();
            foreach (var temp in await db.Flowers.ToListAsync())
            {
                if (temp.Name.ToLower() == flowername.ToLower())
                {
                    flowerlist.Add(temp);
                }
            }
            return Ok(flowerlist);
        }

        //---------------------------------------------Delete a Flower by ID----------------------------
        [HttpDelete]
        [Route("DeleteFlowerbyId")]
        public async Task<ActionResult<Flower>> DeleteFlowerbyId(int id)
        {
            Flower temp = await db.Flowers.FindAsync(id);
            db.Flowers.Remove(temp);
            await db.SaveChangesAsync();
            return Ok();
        }

        //----------------------------------Get Cart of Particular Customer by ID------------------------
        [HttpGet]
        [Route("CartbyCustID")]
        public async Task<ActionResult<Flower>> CartbyCustID(int id)
        {
            List<Cart> Cartlist = new List<Cart>();
            foreach (var temp in await db.Carts.ToListAsync())
            {
                if (temp.CustomerId == id)
                {
                    Cartlist.Add(temp);
                }
            }
            return Ok(Cartlist);
        }


    }
}

