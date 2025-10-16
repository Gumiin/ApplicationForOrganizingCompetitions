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
    public class CompetitionsController(ICompetitionsData competitions) : ControllerBase
    {
        [HttpGet(Name = "Competitions_GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var result = await competitions.GetAllCompetitionsAsync();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id:int}", Name = "Competitions_GetById")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await competitions.GetCompetitionByIdAsync(id);
            return this.ToGetResult(result);
        }

        [HttpPost(Name = "Competitions_Create")]
        [Authorize]

        public async Task<IActionResult> Create([FromBody] CompetitionCreateDto dto)
        {
            if (dto is null) return BadRequest("Model is empty");

            var entity = new Competition
            {
                Name = dto.Name,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Location = dto.Location,
                IsPublished = false
            };

            var result = await competitions.CreateCompetitionAsync(entity);
            return this.ToCreateResult(result, "Competitions_GetById", new { id = (result.Data as Competition)?.Id ?? 0 });
        }

        [HttpPut("{id:int}", Name = "Competitions_Update")]
        [Authorize]

        public async Task<IActionResult> Update(int id, [FromBody] CompetitionUpdateDto dto)
        {
            if (dto is null) return BadRequest("Model is empty");
            if (dto.Id != id) return BadRequest("Route id mismatch");

            var entity = new Competition
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Location = dto.Location,
                IsPublished = dto.IsPublished
            };

            var result = await competitions.UpdateCompetitionAsync(entity);
            return this.ToUpdateResult(result);
        }

        [HttpDelete("{id:int}", Name = "Competitions_Delete")]
        [Authorize]

        public async Task<IActionResult> Delete(int id)
        {
            var result = await competitions.DeleteCompetitionAsync(id);
            return this.ToDeleteResult(result);
        }

        [HttpPut("{id:int}/publish", Name = "Competitions_Publish")]
        [Authorize]

        public async Task<IActionResult> Publish(int id, [FromBody] PublishCompetitionDto body)
        {
            if (body is null) return BadRequest("Model is empty");
            var result = await competitions.PublishCompetitionAsync(id, body.Publish);
            return this.ToUpdateResult(result);
        }
    }
}