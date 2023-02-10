using Microsoft.AspNetCore.Mvc;

namespace Robust.WebApi.Controllers
{
    public class OwnerController : Controller
    {
        private ILoggerManager _logger;
        private IRepoService _repoService;

        public OwnerController(ILoggerManager logger, IRepoService repoService)
        {
            _logger = logger;
            _repoService = repoService;
        }

        [HttpGet]
        public IActionResult GetAllOwners()
        {
        }

        [HttpGet("{id}", Name = "OwnerById")]
        public IActionResult GetOwnerById(Guid id)
        {
        }

        [HttpGet("{id}/account")]
        public IActionResult GetOwnerWithDetails(Guid id)
        {
        }

        [HttpPost]
        public IActionResult CreateOwner([FromBody] OwnerForCreationDto owner)
        {
            try
            {
                if (owner == null)
                {
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                //additional code

                return CreatedAtRoute("OwnerById", new { id = createdOwner.Id }, createdOwner);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the CreateOwner action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOwner(Guid id, [FromBody] OwnerForUpdateDto owner)
        {
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOwner(Guid id)
        {
        }
    }
}
