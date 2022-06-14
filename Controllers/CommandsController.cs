using AutoMapper;
using Commander.API.Data;
using Commander.API.Dtos;
using Commander.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.API.Controllers
{
    // Base route
    [Route("api/commands")]
    [ApiController] // <<- Makes life alot easier by providing up with defualt controller abilities
    public class CommandsController : Controller
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        //private readonly MockCommanderRepo _repository = new MockCommanderRepo();
        //GET api/commands
        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commandItem = _repository.GetAppComands();

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItem));
        }

        //Get api/commands/{id}
        [HttpGet("{id}", Name ="GetCommandById")]
        public ActionResult <CommandReadDto> GetCommandById(int id)
        {
             var commandItem = _repository.GetCommandById(id);

            if(commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }

            return NotFound();
        }

        //POST api/commands
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);


            return CreatedAtRoute(nameof(GetCommandById), new { Id = commandReadDto.Id }, commandReadDto);

            //return Ok(commandReadDto);


        }
        //PUt api/commands/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandCreateDto commandCreateDto)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(commandCreateDto, commandModelFromRepo);


            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();
            return NoContent();
        }

        //PATCH api/commnands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandCreateDto> patchDoc)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            
            // Creating a new Cerate command Dto with the contents from
            // Our repository and putting it in the patch update
            var commandModel = _mapper.Map<CommandCreateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandModel, ModelState);
            if (!TryValidateModel(commandModel))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandModel, commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();

        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}
