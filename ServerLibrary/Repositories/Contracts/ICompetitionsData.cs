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
        Task<GeneralResponse> GetCompetitions();
        Task<GeneralResponse> GetCompetition(int id);
        Task<GeneralResponse> CreateCompetition(Competition competition);
        Task<GeneralResponse> UpdateCompetition(int id, Competition competition);
        Task<GeneralResponse> DeleteCompetition(int id);
        Task<GeneralResponse> AddStatus(Status status);
        Task<GeneralResponse> UpdateCompetitionStatus(int id, int statusId);
        Task<GeneralResponse> AddRound(int competitionId, Round round);
        Task<GeneralResponse> AddParticipant(int competitionId, Participant participant);
        Task<GeneralResponse> AddPoints(int roundId, Points points);
        Task<GeneralResponse> UpdateSumPointsAndPlace(int id, double sumPoints, int place);
    }
}
