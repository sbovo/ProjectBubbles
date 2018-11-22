using System;
using Microsoft.AspNetCore.Mvc;

using ProjectBubbles.Models;

namespace ProjectBubbles.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {

        private readonly IItemRepository ItemRepository;

        public ItemsController(IItemRepository itemRepository)
        {
            ItemRepository = itemRepository;
        }

        [HttpGet]
        public IActionResult List()
        {
            return Ok(ItemRepository.GetAll());
        }

        [HttpGet("{meetingName}")]
        public IActionResult ListMeetings(string meetingName)
        {
            return Ok(ItemRepository.GetAllForADate(meetingName));
        }
    }
}
