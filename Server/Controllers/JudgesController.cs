using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Infrastructure;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JudgesController(ICompetitionsData competitions) : ControllerBase
    {
        [HttpGet("by-competition/{competitionId:int}", Name = "Judges_GetByCompetition")]
        [Authorize]

        public async Task<IActionResult> GetByCompetition(int competitionId)
        {
            var result = await competitions.GetJudgesByCompetitionAsync(competitionId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("by-event/{eventId:int}", Name = "Judges_GetByEvent")]
        [Authorize]

        public async Task<IActionResult> GetByEvent(int eventId)
        {
            var result = await competitions.GetJudgesByEventAsync(eventId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("assign-competition", Name = "Judges_AssignToCompetition")]

        [Authorize]
        public async Task<IActionResult> AssignToCompetition([FromBody] AssignJudgeToCompetitionDto dto)
        {
            if (dto is null) return BadRequest("Model is empty");
            var result = await competitions.AssignJudgeToCompetitionAsync(dto.CompetitionId, dto.JudgeId);
            return this.ToUpdateResult(result);
        }

        [HttpPost("assign-event", Name = "Judges_AssignToEvent")]

        [Authorize]
        public async Task<IActionResult> AssignToEvent([FromBody] AssignJudgeToEventDto dto)
        {
            if (dto is null) return BadRequest("Model is empty");
            var result = await competitions.AssignJudgeToEventAsync(dto.EventId, dto.JudgeId);
            return this.ToUpdateResult(result);
        }

        [HttpDelete("{judgeId:int}", Name = "Judges_Remove")]

        [Authorize]
        public async Task<IActionResult> Remove(int judgeId)
        {
            var result = await competitions.RemoveJudgeAsync(judgeId);
            return this.ToDeleteResult(result);
        }
    }
}