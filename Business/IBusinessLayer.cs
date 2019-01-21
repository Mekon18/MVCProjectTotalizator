using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IBusinessLayer
    {
        void AddTeam(Team team);
        Team GetTeam(int id);
        List<SportEvent> GetNearSportEvents();
        int GetUsersMoney(string id);
        void TakeUsersMoney(string userId, int money);
        SportEvent GetSportEvent(int id);
        void SetBets(Rate rate, List<Bet> bets);
        List<Rate> GetUsersRates(string userId);
        void GiveUserMoney(string id, int money);
        List<Team> GetAllTeams();
        void AddSportEvent(SportEvent sportEvent);
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
        void SetUserRole(string id, string role);
        KindOfSport GetKindOfSport(int id);
        List<KindOfSport> GetAllKindsOfSport();
        List<SportEvent> GetNearSportEventsByKindOfSport(int kindId);
        List<SportEvent> SearchSportEvents(string status, DateTime date, int kindId);
        string GetAdvertisement();
        List<Bet> GetBets(int rateId);
        void EditBets(List<Bet> bets);
        void DeleteBet(int betId);
        void DeleteRate(int rateId);
        void AddBets(List<Bet> bets, int rateId);
    }
}
