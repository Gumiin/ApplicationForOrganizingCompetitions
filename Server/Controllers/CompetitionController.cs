using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionController(ICompetitionsData competitionsInterface) : ControllerBase
    {
        [HttpGet("get-competitions")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCompetitions()
        {
            var result = await competitionsInterface.GetCompetitions();
            return Ok(result);
        }

        [HttpGet("get-competition")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCompetition(int id)
        {
            if (!(id > 0)) return BadRequest("Model is empty");
            var result = await competitionsInterface.GetCompetition(id);
            return Ok(result);
        }

        [HttpPost("create-competition")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateCompetition(Competition competition)
        {
            if (competition == null) return BadRequest("Model is empty");
            var result = await competitionsInterface.CreateCompetition(competition);
            return Ok(result);
        }

        [HttpPut("update-competition")]
        [Authorize]
        public async Task<IActionResult> UpdateCompetition(int id, Competition competition)
        {
            var users = await competitionsInterface.UpdateCompetition(id, competition);
            if (users == null) return NotFound();
            return Ok(users);
        }

        [HttpDelete("delete-competition/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCompetition(int id)
        {
            var result = await competitionsInterface.DeleteCompetition(id);
            return Ok(result);
        }

        [HttpPost("add-status")]
        [Authorize]
        public async Task<IActionResult> AddStatus(Status status)
        {
            var users = await competitionsInterface.AddStatus(status);
            if (users == null) return NotFound();
            return Ok(users);
        }

        [HttpPut("update-competition-status/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCompetitionStatus(int id, int statusId)
        {
            var result = await competitionsInterface.UpdateCompetitionStatus(id, statusId);
            return Ok(result);
        }

        [HttpPost("add-round")]
        [Authorize]
        public async Task<IActionResult> AddRound(int competitionId, Round round)
        {
            var result = await competitionsInterface.AddRound(competitionId, round);
            return Ok(result);
        }

        [HttpPost("add-participant")]
        [Authorize]
        public async Task<IActionResult> AddParticipant(int competitionId, Participant participant)
        {
            var result = await competitionsInterface.AddParticipant(competitionId, participant);
            return Ok(result);
        }

        [HttpPost("add-points")]
        [Authorize]
        public async Task<IActionResult> AddPoints(int roundId, Points points)
        {
            var result = await competitionsInterface.AddPoints( roundId,  points);
            return Ok(result);
        }

        [HttpPut("update-sum-points-and-place")]
        [Authorize]
        public async Task<IActionResult> UpdateSumPointsAndPlace(int id, double sumPoints, int place)
        {
            var result = await competitionsInterface.UpdateSumPointsAndPlace(id, sumPoints, place);
            return Ok(result);
        }
    }
    //{
    //    // GET: api/Competitions
    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<Competition>>> GetCompetitions()
    //    {
    //        return await _context.Competitions.Include(c => c.ApplicationUser).Include(c => c.Status).ToListAsync();
    //    }

    //    // GET: api/Competitions/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<Competition>> GetCompetition(int id)
    //    {
    //        var competition = await _context.Competitions.Include(c => c.ApplicationUser).Include(c => c.Status).FirstOrDefaultAsync(c => c.Id == id);

    //        if (competition == null)
    //        {
    //            return NotFound();
    //        }

    //        return competition;
    //    }

    //    // POST: api/Competitions
    //    [HttpPost]
    //    public async Task<ActionResult<Competition>> CreateCompetition(Competition competition)
    //    {
    //        _context.Competitions.Add(competition);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction(nameof(GetCompetition), new { id = competition.Id }, competition);
    //    }

    //    // PUT: api/Competitions/5
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> UpdateCompetition(int id, Competition competition)
    //    {
    //        if (id != competition.Id)
    //        {
    //            return BadRequest();
    //        }

    //        _context.Entry(competition).State = EntityState.Modified;

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!CompetitionExists(id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }

    //        return NoContent();
    //    }

    //    // DELETE: api/Competitions/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteCompetition(int id)
    //    {
    //        var competition = await _context.Competitions.FindAsync(id);
    //        if (competition == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.Competitions.Remove(competition);
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }

    //    // POST: api/Statuses
    //    [HttpPost("statuses")]
    //    public async Task<ActionResult<Status>> AddStatus(Status status)
    //    {
    //        _context.Statuses.Add(status);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction(nameof(AddStatus), new { id = status.Id }, status);
    //    }

    //    // PUT: api/Competitions/5/status
    //    [HttpPut("{id}/status")]
    //    public async Task<IActionResult> UpdateCompetitionStatus(int id, int statusId)
    //    {
    //        var competition = await _context.Competitions.FindAsync(id);
    //        if (competition == null) return NotFound();

    //        competition.StatusId = statusId;
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }

    //    // POST: api/Competitions/5/rounds
    //    [HttpPost("{competitionId}/rounds")]
    //    public async Task<ActionResult<Round>> AddRound(int competitionId, Round round)
    //    {
    //        round.CompetitionId = competitionId;
    //        _context.Rounds.Add(round);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction(nameof(AddRound), new { id = round.Id }, round);
    //    }

    //    // POST: api/Competitions/5/participants
    //    [HttpPost("{competitionId}/participants")]
    //    public async Task<ActionResult<Participant>> AddParticipant(int competitionId, Participant participant)
    //    {
    //        _context.Participants.Add(participant);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction(nameof(AddParticipant), new { id = participant.Id }, participant);
    //    }

    //    // POST: api/Rounds/5/points
    //    [HttpPost("rounds/{roundId}/points")]
    //    public async Task<ActionResult<Points>> AddPoints(int roundId, Points points)
    //    {
    //        points.RoundId = roundId;
    //        _context.Points.Add(points);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction(nameof(AddPoints), new { id = points.Id }, points);
    //    }

    //    // PUT: api/Participants/5/sumpoints
    //    [HttpPut("participants/{id}/sumpoints")]
    //    public async Task<IActionResult> UpdateSumPointsAndPlace(int id, double sumPoints, int place)
    //    {
    //        var participant = await _context.Participants.FindAsync(id);
    //        if (participant == null) return NotFound();

    //        participant.SumPoints = sumPoints;
    //        participant.Place = place;
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }

    //    private bool CompetitionExists(int id)
    //    {
    //        return _context.Competitions.Any(e => e.Id == id);
    //    }
    //}
}
