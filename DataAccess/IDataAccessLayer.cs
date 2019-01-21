using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface IDataAccessLayer
    {
        Team GetTeam(int id);
        void AddTeam(Team team);
        void AddSportEvent(SportEvent sportEvent);
        List<SportEvent> GetNearSportEvents();
        int GetUsersMoney(string id);
        void SetUsersMoney(string id, int money);
        SportEvent GetSportEvent(int id);
        void AddBets(List<Bet> bets, int RateId);
        int AddRate(Rate rate);
        List<Rate> GetUsersRates(string userId);
        List<Bet> GetBets(int rateId);
        List<Team> GetAllTeams();
        List<SportEvent> GetAllSportEvents();
        void EditEvent(SportEvent sportEvent);
        void DeleteEvent(int id);
        void EditTeam(Team team);
        void DeleteTeam(int id);
        List<User> GetAllUsers();
        void EditUser(User user);
        void DeleteUser(string id);
        User GetUser(string id);
        string GetUsersRole(string id);
        void SetUserRole(string id, string roleId);
        KindOfSport GetKindOfSport(int id);
        List<KindOfSport> GetAllKindsOfSport();
        List<SportEvent> GetNearSportEventsByKindOfSport(int kindId);
        List<SportEvent> SearchSportEvents(string status, DateTime date, int kindId);
        void EditBet(Bet bet);
        void DeleteBet(int betId);
        void DeleteRate(int rateId);
    }
}
