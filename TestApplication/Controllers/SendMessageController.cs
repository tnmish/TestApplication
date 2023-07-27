using Microsoft.AspNetCore.Mvc;
using TestApplication.Database;
using TestApplication.Models;
using TestApplication.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMessageController : ControllerBase
    {
        private readonly IRepository<User> userset;
        private readonly IRepository<WebcabMessage> messageset;       
        private readonly IEnumerable<ISendMessage> messageServices;

        public SendMessageController(IRepository <User> Userset, IRepository<WebcabMessage> Messageset, IEnumerable<ISendMessage> msgServices)
        {
            userset = Userset;
            messageServices= msgServices;
            messageset = Messageset;
        }

        // POST api/<SendMessageController>/5

        [HttpPost("{id}")]
        public async Task<ActionResult> Post(int id, [FromBody] Message message)
        {
            User? user = await userset.FindByIdAsync(id); 
            if (user == null)
                return NotFound();

            foreach (var sendMsg in messageServices )
            {
                await sendMsg.SendMessage(user, message);
            }             
            return Ok();
        }

        //GET api/<SendMessageController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> Get(int id)
        {
            WebcabMessage? wmessage = await messageset.FindByIdAsync(id);
            if (wmessage == null)
                return NotFound();
            
            return new ObjectResult(wmessage);
        }

        // GET api/<SendMessageController>
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<WebcabMessage>>> Get()
        {
            IEnumerable<WebcabMessage> wmessage = await messageset.GetAsync();
            return new ActionResult<IEnumerable<WebcabMessage>>(wmessage);
           
        }
    }
}
