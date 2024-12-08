using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReenbitTestTask24.DB;
using ReenbitTestTask24.DTO;
using ReenbitTestTask24.Entities;
using ReenbitTestTask24.Interfaces;

namespace ReenbitTestTask24.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService service)
        {
           messageService= service;
        }

        [HttpGet("messages")]
        public async Task<ActionResult<IEnumerable<MessageDTO>>> GetMessages()
        {
            return await messageService.GetMessages();
        }
    }
}
