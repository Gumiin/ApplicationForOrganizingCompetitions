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
    public class ResultsController(ICompetitionsData competitions) : ControllerBase
    {
        [HttpGet("by-stage/{stageId:int}", Name = "Results_GetByStage")]
        [Authorize]

        public async Task<IActionResult> GetByStage(int stageId)
        {
            var result = await competitions.GetResultsByStageAsync(stageId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id:int}", Name = "Results_GetById")]
        [Authorize]

        public async Task<IActionResult> GetById(int id)
        {
            var result = await competitions.GetResultByIdAsync(id);
            return this.ToGetResult(result);
        }

        [HttpPost(Name = "Results_Create")]
        [Authorize]

        public async Task<IActionResult> Create([FromBody] ResultCreateDto dto)
        {
            if (dto is null) return BadRequest("Model is empty");

            var entity = new Result
            {
                EventStageId = dto.EventStageId,
                AthleteId = dto.AthleteId,
                Name = dto.Name,
                Score = dto.Score,
                Unit = dto.Unit,
                Rank = dto.Rank,
                Note = dto.Note,
                CreatedBy = dto.CreatedBy
            };

            var result = await competitions.CreateResultAsync(entity);
            return this.ToCreateResult(result, "Results_GetById", new { id = (result.Data as Result)?.Id ?? 0 });
        }

        [HttpPut("{id:int}", Name = "Results_Update")]
        [Authorize]

        public async Task<IActionResult> Update(int id, [FromBody] ResultUpdateDto dto)
        {
            if (dto is null) return BadRequest("Model is empty");
            if (dto.Id != id) return BadRequest("Route id mismatch");

            var entity = new Result
            {
                Id = dto.Id,
                EventStageId = dto.EventStageId,
                AthleteId = dto.AthleteId,
                Name = dto.Name,
                Score = dto.Score,
                Unit = dto.Unit,
                Rank = dto.Rank,
                Note = dto.Note,
                ModifiedBy = dto.ModifiedBy,
                ModifiedAt = DateTime.UtcNow
            };

            var result = await competitions.UpdateResultAsync(entity);
            return this.ToUpdateResult(result);
        }

        [HttpDelete("{id:int}", Name = "Results_Delete")]
        [Authorize]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await competitions.DeleteResultAsync(id);
            return this.ToDeleteResult(result);
        }
    }
}