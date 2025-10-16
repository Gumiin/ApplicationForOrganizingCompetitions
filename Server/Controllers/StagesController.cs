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
    public class StagesController(ICompetitionsData competitions) : ControllerBase
    {
        [HttpGet("by-event/{eventId:int}", Name = "Stages_GetByEvent")]
        [Authorize]

        public async Task<IActionResult> GetByEvent(int eventId)
        {
            var result = await competitions.GetStagesByEventAsync(eventId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost(Name = "Stages_Create")]
        [Authorize]

        public async Task<IActionResult> Create([FromBody] StageCreateDto dto)
        {
            if (dto is null) return BadRequest("Model is empty");

            var entity = new EventStage
            {
                EventId = dto.EventId,
                Name = dto.Name,
                Order = dto.Order,
                ScheduledStart = dto.ScheduledStart
            };

            var result = await competitions.CreateStageAsync(entity);
            return this.ToCreateResult(result, "Stages_GetByEvent", new { eventId = entity.EventId });
        }

        [HttpPut("{id:int}", Name = "Stages_Update")]
        [Authorize]

        public async Task<IActionResult> Update(int id, [FromBody] StageUpdateDto dto)
        {
            if (dto is null) return BadRequest("Model is empty");
            if (dto.Id != id) return BadRequest("Route id mismatch");

            var entity = new EventStage
            {
                Id = dto.Id,
                EventId = dto.EventId,
                Name = dto.Name,
                Order = dto.Order,
                ScheduledStart = dto.ScheduledStart
            };

            var result = await competitions.UpdateStageAsync(entity);
            return this.ToUpdateResult(result);
        }

        [HttpDelete("{id:int}", Name = "Stages_Delete")]
         
        public async Task<IActionResult> Delete(int id)
        {
            var result = await competitions.DeleteStageAsync(id);
            return this.ToDeleteResult(result);
        }

        [HttpPost("{stageId:int}/publish-results", Name = "Stages_PublishResults")]
        [Authorize]

        public async Task<IActionResult> PublishResults(int stageId)
        {
            var result = await competitions.PublishResultsAsync(stageId);
            // traktujemy to jak update stanu – 204 przy sukcesie
            return this.ToUpdateResult(result);
        }
    }
}