using System;
using System.Threading.Tasks;
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

        [HttpGet("{UserName}")]
        public async Task<Profile> GetItem(string UserName)
        {
            Profile item = await ProfileRepository.Get(UserName);
            return item;
        }

        [HttpPost]
        public IActionResult Create([FromBody]Profile item)
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
        public IActionResult Edit([FromBody] Profile item)
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
