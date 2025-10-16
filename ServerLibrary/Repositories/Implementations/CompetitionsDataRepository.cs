using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class CompetitionsDataRepository(AppDbContext appDbContext) : ICompetitionsData
    {
        // Helpers (wzorem UserAccountRepository)
        private async Task<T> AddToDatabase<T>(T model)
        {
            var result = appDbContext.Add(model!);
            await appDbContext.SaveChangesAsync();
            return (T)result.Entity;
        }

        
        // --- COMPETITIONS ---

        public async Task<GeneralResponse> GetAllCompetitionsAsync()
        {
            var items = await appDbContext.Competitions
                .AsNoTracking()
                .ToListAsync();

            return GeneralResponse.Ok("OK", items);
        }

        public async Task<GeneralResponse> GetCompetitionByIdAsync(int id)
        {
            var item = await appDbContext.Competitions
                .AsNoTracking()
                .FirstOrDefaultAsync(_ => _.Id == id);

            return item is null ? GeneralResponse.NotFound("Competition") : GeneralResponse.Ok("OK", item);
        }

        public async Task<GeneralResponse> CreateCompetitionAsync(Competition competition)
        {
            if (competition is null) return GeneralResponse.Invalid("Model is empty");
            var created = await AddToDatabase(competition);
            return GeneralResponse.Ok("Competition created", created);
        }

        public async Task<GeneralResponse> UpdateCompetitionAsync(Competition competition)
        {
            if (competition is null) return GeneralResponse.Invalid("Model is empty");

            var existing = await appDbContext.Competitions.FirstOrDefaultAsync(_ => _.Id == competition.Id);
            if (existing is null) return GeneralResponse.NotFound("Competition");

            appDbContext.Entry(existing).CurrentValues.SetValues(competition);
            await appDbContext.SaveChangesAsync();

            return GeneralResponse.Ok("Competition updated", existing);
        }

        public async Task<GeneralResponse> DeleteCompetitionAsync(int id)
        {
            var existing = await appDbContext.Competitions.FirstOrDefaultAsync(_ => _.Id == id);
            if (existing is null) return GeneralResponse.NotFound("Competition");

            appDbContext.Competitions.Remove(existing);
            await appDbContext.SaveChangesAsync();

            return GeneralResponse.Ok("Competition deleted");
        }

        public async Task<GeneralResponse> PublishCompetitionAsync(int id, bool publish)
        {
            var existing = await appDbContext.Competitions.FirstOrDefaultAsync(_ => _.Id == id);
            if (existing is null) return GeneralResponse.NotFound("Competition");

            existing.IsPublished = publish;
            await appDbContext.SaveChangesAsync();

            return GeneralResponse.Ok(publish ? "Competition published" : "Competition unpublished", existing);
        }

        // --- EVENTS (konkurencje) ---

        public async Task<GeneralResponse> GetEventsByCompetitionAsync(int competitionId)
        {
            var items = await appDbContext.Events
                .AsNoTracking()
                .Where(_ => _.CompetitionId == competitionId)
                .ToListAsync();

            return GeneralResponse.Ok("OK", items);
        }

        public async Task<GeneralResponse> GetEventByIdAsync(int id)
        {
            var item = await appDbContext.Events
                .AsNoTracking()
                .FirstOrDefaultAsync(_ => _.Id == id);

            return item is null ? GeneralResponse.NotFound("Event") : GeneralResponse.Ok("OK", item);
        }

        public async Task<GeneralResponse> CreateEventAsync(Event @event)
        {
            if (@event is null) return GeneralResponse.Invalid("Model is empty");

            var compExists = await appDbContext.Competitions.AsNoTracking()
                .AnyAsync(_ => _.Id == @event.CompetitionId);
            if (!compExists) return GeneralResponse.NotFound("Competition");

            var created = await AddToDatabase(@event);
            return GeneralResponse.Ok("Event created", created);
        }

        public async Task<GeneralResponse> UpdateEventAsync(Event @event)
        {
            if (@event is null) return GeneralResponse.Invalid("Model is empty");

            var existing = await appDbContext.Events.FirstOrDefaultAsync(_ => _.Id == @event.Id);
            if (existing is null) return GeneralResponse.NotFound("Event");

            appDbContext.Entry(existing).CurrentValues.SetValues(@event);
            await appDbContext.SaveChangesAsync();

            return GeneralResponse.Ok("Event updated", existing);
        }

        public async Task<GeneralResponse> DeleteEventAsync(int id)
        {
            var existing = await appDbContext.Events.FirstOrDefaultAsync(_ => _.Id == id);
            if (existing is null) return GeneralResponse.NotFound("Event");

            appDbContext.Events.Remove(existing);
            await appDbContext.SaveChangesAsync();

            return GeneralResponse.Ok("Event deleted");
        }

        // --- STAGES (etapy) ---

        public async Task<GeneralResponse> GetStagesByEventAsync(int eventId)
        {
            var items = await appDbContext.EventStages
                .AsNoTracking()
                .Where(_ => _.EventId == eventId)
                .ToListAsync();

            return GeneralResponse.Ok("OK", items);
        }

        public async Task<GeneralResponse> CreateStageAsync(EventStage stage)
        {
            if (stage is null) return GeneralResponse.Invalid("Model is empty");

            var evExists = await appDbContext.Events.AsNoTracking()
                .AnyAsync(_ => _.Id == stage.EventId);
            if (!evExists) return GeneralResponse.NotFound("Event");

            var created = await AddToDatabase(stage);
            return GeneralResponse.Ok("Stage created", created);
        }

        public async Task<GeneralResponse> UpdateStageAsync(EventStage stage)
        {
            if (stage is null) return GeneralResponse.Invalid("Model is empty");

            var existing = await appDbContext.EventStages.FirstOrDefaultAsync(_ => _.Id == stage.Id);
            if (existing is null) return GeneralResponse.NotFound("Stage");

            appDbContext.Entry(existing).CurrentValues.SetValues(stage);
            await appDbContext.SaveChangesAsync();

            return GeneralResponse.Ok("Stage updated", existing);
        }

        public async Task<GeneralResponse> DeleteStageAsync(int id)
        {
            var existing = await appDbContext.EventStages.FirstOrDefaultAsync(_ => _.Id == id);
            if (existing is null) return GeneralResponse.NotFound("Stage");

            appDbContext.EventStages.Remove(existing);
            await appDbContext.SaveChangesAsync();

            return GeneralResponse.Ok("Stage deleted");
        }

        // --- JUDGES (sędziowie) ---
        // Model encji Judge wskazuje na możliwość przypięcia sędziego albo do Competition, albo do Event poprzez CompetitionId/EventId.

        public async Task<GeneralResponse> GetJudgesByCompetitionAsync(int competitionId)
        {
            var judges = await appDbContext.Judges
                .AsNoTracking()
                .Where(_ => _.CompetitionId == competitionId)
                .ToListAsync();

            return GeneralResponse.Ok("OK", judges);
        }

        public async Task<GeneralResponse> GetJudgesByEventAsync(int eventId)
        {
            var judges = await appDbContext.Judges
                .AsNoTracking()
                .Where(_ => _.EventId == eventId)
                .ToListAsync();

            return GeneralResponse.Ok("OK", judges);
        }

        public async Task<GeneralResponse> AssignJudgeToCompetitionAsync(int competitionId, int judgeId)
        {
            var compExists = await appDbContext.Competitions.AsNoTracking().AnyAsync(_ => _.Id == competitionId);
            if (!compExists) return GeneralResponse.NotFound("Competition");

            var judge = await appDbContext.Judges.FirstOrDefaultAsync(_ => _.Id == judgeId);
            if (judge is null) return GeneralResponse.NotFound("Judge");

            judge.CompetitionId = competitionId;
            judge.EventId = null; // upewniamy się, że nie jest równolegle przypięty do eventu
            await appDbContext.SaveChangesAsync();

            return GeneralResponse.Ok("Judge assigned to competition", judge);
        }

        public async Task<GeneralResponse> AssignJudgeToEventAsync(int eventId, int judgeId)
        {
            var evExists = await appDbContext.Events.AsNoTracking().AnyAsync(_ => _.Id == eventId);
            if (!evExists) return GeneralResponse.NotFound("Event");

            var judge = await appDbContext.Judges.FirstOrDefaultAsync(_ => _.Id == judgeId);
            if (judge is null) return GeneralResponse.NotFound("Judge");

            judge.EventId = eventId;
            judge.CompetitionId = null; // odpinamy od competition jeśli był
            await appDbContext.SaveChangesAsync();

            return GeneralResponse.Ok("Judge assigned to event", judge);
        }

        public async Task<GeneralResponse> RemoveJudgeAsync(int judgeId)
        {
            var judge = await appDbContext.Judges.FirstOrDefaultAsync(_ => _.Id == judgeId);
            if (judge is null) return GeneralResponse.NotFound("Judge");

            appDbContext.Judges.Remove(judge);
            await appDbContext.SaveChangesAsync();

            return GeneralResponse.Ok("Judge removed");
        }

        // --- RESULTS (wyniki) ---

        public async Task<GeneralResponse> GetResultsByStageAsync(int stageId)
        {
            var items = await appDbContext.Results
                .AsNoTracking()
                .Where(_ => _.EventStageId == stageId)
                .ToListAsync();

            return GeneralResponse.Ok("OK", items);
        }

        public async Task<GeneralResponse> GetResultByIdAsync(int id)
        {
            var item = await appDbContext.Results
                .AsNoTracking()
                .FirstOrDefaultAsync(_ => _.Id == id);

            return item is null ? GeneralResponse.NotFound("Result") : GeneralResponse.Ok("OK", item);
        }

        public async Task<GeneralResponse> CreateResultAsync(Result result)
        {
            if (result is null) return GeneralResponse.Invalid("Model is empty");

            var stageExists = await appDbContext.EventStages.AsNoTracking().AnyAsync(_ => _.Id == result.EventStageId);
            if (!stageExists) return GeneralResponse.NotFound("Stage");

            var athleteExists = await appDbContext.Athletes.AsNoTracking().AnyAsync(_ => _.Id == result.AthleteId);
            if (!athleteExists) return GeneralResponse.NotFound("Athlete");

            var created = await AddToDatabase(result);
            return GeneralResponse.Ok("Result created", created);
        }

        public async Task<GeneralResponse> UpdateResultAsync(Result result)
        {
            if (result is null) return GeneralResponse.Invalid("Model is empty");

            var existing = await appDbContext.Results.FirstOrDefaultAsync(_ => _.Id == result.Id);
            if (existing is null) return GeneralResponse.NotFound("Result");

            // Audyt można obsłużyć domenowo w innym miejscu; tutaj prosty update.
            appDbContext.Entry(existing).CurrentValues.SetValues(result);
            existing.ModifiedAt = System.DateTime.UtcNow;
            await appDbContext.SaveChangesAsync();

            return GeneralResponse.Ok("Result updated", existing);
        }

        public async Task<GeneralResponse> DeleteResultAsync(int id)
        {
            var existing = await appDbContext.Results.FirstOrDefaultAsync(_ => _.Id == id);
            if (existing is null) return GeneralResponse.NotFound("Result");

            appDbContext.Results.Remove(existing);
            await appDbContext.SaveChangesAsync();

            return GeneralResponse.Ok("Result deleted");
        }

        public async Task<GeneralResponse> PublishResultsAsync(int stageId)
        {
            var exists = await appDbContext.EventStages.AsNoTracking().AnyAsync(_ => _.Id == stageId);
            if (!exists) return GeneralResponse.NotFound("Stage");

            return GeneralResponse.Ok("Results publish action completed");
        }
    }
}