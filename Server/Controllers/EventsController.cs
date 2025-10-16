using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Infrastructure;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventsController(ICompetitionsData competitions) : ControllerBase
    {
        [HttpGet("by-competition/{competitionId:int}", Name = "Events_GetByCompetition")]
        [Authorize]
        public async Task<IActionResult> GetByCompetition(int competitionId)
        {
            var result = await competitions.GetEventsByCompetitionAsync(competitionId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id:int}", Name = "Events_GetById")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await competitions.GetEventByIdAsync(id);
            return this.ToGetResult(result);
        }

        [HttpPost(Name = "Events_Create")]
        [Authorize]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] EventCreateDto dto)
        {
            if (dto is null) return BadRequest("Model is empty");

            var entity = new Event
            {
                CompetitionId = dto.CompetitionId,
                Name = dto.Name,
                Category = dto.Category,
                Description = dto.Description
            };

            var result = await competitions.CreateEventAsync(entity);
            return this.ToCreateResult(result, "Events_GetById", new { id = (result.Data as Event)?.Id ?? 0 });
        }

        [HttpPut("{id:int}", Name = "Events_Update")]
        [Authorize]

        public async Task<IActionResult> Update(int id, [FromBody] EventUpdateDto dto)
        {
            if (dto is null) return BadRequest("Model is empty");
            if (dto.Id != id) return BadRequest("Route id mismatch");

            var entity = new Event
            {
                Id = dto.Id,
                CompetitionId = dto.CompetitionId,
                Name = dto.Name,
                Category = dto.Category,
                Description = dto.Description
            };

            var result = await competitions.UpdateEventAsync(entity);
            return this.ToUpdateResult(result);
        }

        [HttpDelete("{id:int}", Name = "Events_Delete")]
        [Authorize]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await competitions.DeleteEventAsync(id);
            return this.ToDeleteResult(result);
        }
    }
}