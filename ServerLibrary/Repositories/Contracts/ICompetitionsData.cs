using BaseLibrary.DTOs;
using BaseLibrary.Entities;
using BaseLibrary.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Contracts
{
    public interface ICompetitionsData
    {
        // --- COMPETITIONS ---

        Task<GeneralResponse> GetAllCompetitionsAsync();
        Task<GeneralResponse> GetCompetitionByIdAsync(int id);
        Task<GeneralResponse> CreateCompetitionAsync(Competition competition);
        Task<GeneralResponse> UpdateCompetitionAsync(Competition competition);
        Task<GeneralResponse> DeleteCompetitionAsync(int id);
        Task<GeneralResponse> PublishCompetitionAsync(int id, bool publish);


        // --- EVENTS (konkurencje) ---

        Task<GeneralResponse> GetEventsByCompetitionAsync(int competitionId);
        Task<GeneralResponse> GetEventByIdAsync(int id);
        Task<GeneralResponse> CreateEventAsync(Event @event);
        Task<GeneralResponse> UpdateEventAsync(Event @event);
        Task<GeneralResponse> DeleteEventAsync(int id);


        // --- STAGES (etapy) ---

        Task<GeneralResponse> GetStagesByEventAsync(int eventId);
        Task<GeneralResponse> CreateStageAsync(EventStage stage);
        Task<GeneralResponse> UpdateStageAsync(EventStage stage);
        Task<GeneralResponse> DeleteStageAsync(int id);


        // --- JUDGES (sędziowie) ---

        Task<GeneralResponse> GetJudgesByCompetitionAsync(int competitionId);
        Task<GeneralResponse> GetJudgesByEventAsync(int eventId);
        Task<GeneralResponse> AssignJudgeToCompetitionAsync(int competitionId, int judgeId);
        Task<GeneralResponse> AssignJudgeToEventAsync(int eventId, int judgeId);
        Task<GeneralResponse> RemoveJudgeAsync(int judgeId);


        // --- RESULTS (wyniki) ---

        Task<GeneralResponse> GetResultsByStageAsync(int stageId);
        Task<GeneralResponse> GetResultByIdAsync(int id);
        Task<GeneralResponse> CreateResultAsync(Result result);
        Task<GeneralResponse> UpdateResultAsync(Result result);
        Task<GeneralResponse> DeleteResultAsync(int id);
        Task<GeneralResponse> PublishResultsAsync(int stageId);


        // --- AUDIT TRAIL ---

        Task<GeneralResponse> GetAuditTrailByResultAsync(int resultId);
    }
}