using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatApplication.Data;
using chatApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chatApplication.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        ApplicationDbContext db;
        public BranchController(ApplicationDbContext context)
        {
            db = context;
        }

        [HttpPost("CreateBranch")]
        public IActionResult CreateBranch(string Name)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Branch branch = new Branch();
                    branch.Name = Name;
                    db.Branches.Add(branch);
                    db.SaveChanges();

                    return Ok();
                }
                ModelState.AddModelError("error", "something happen wrong");
                return NotFound();
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpPut("UpdateBranch")]
        public IActionResult UpdateBranch(Branch branch)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var oldBranch = db.Branches.SingleOrDefault(b => b.Id == branch.Id);

                    oldBranch.Name = branch.Name;
                    db.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception)
            {

                throw;
            }
        }


        [HttpGet("GetAllBranches")]
        public JsonResult GetAllBranches()
        {


            return new JsonResult(db.Branches.ToList());

           
        }
    }
}