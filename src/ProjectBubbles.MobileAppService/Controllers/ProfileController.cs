using System;
using Microsoft.AspNetCore.Mvc;

using ProjectBubbles.Models;

namespace ProjectBubbles.Controllers
{
    [Route("api/[controller]")]
    public class ProfileController : Controller
    {

        private readonly IProfileRepository ProfileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            ProfileRepository = profileRepository;
        }

        [HttpGet("{id}")]
        public Item GetItem(string id)
        {
            Item item = ProfileRepository.Get(id);
            return item;
        }

        [HttpPost]
        public IActionResult Create([FromBody]Item item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }

                ProfileRepository.Add(item);

            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return Ok(item);
        }

        [HttpPut]
        public IActionResult Edit([FromBody] Item item)
        {
            try
            {
                if (item == null || !ModelState.IsValid)
                {
                    return BadRequest("Invalid State");
                }
                ProfileRepository.Update(item);
            }
            catch (Exception)
            {
                return BadRequest("Error while creating");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            ProfileRepository.Remove(id);
        }
    }
}
