using Common;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class BusinessLayer : IBusinessLayer
    {
        private IDataAccessLayer _dataAccessLayer;

        public BusinessLayer(IDataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }
        #region Teams
        public void AddTeam(Team team)
        {
            _dataAccessLayer.AddTeam(team);
        }
        public List<Team> GetAllTeams()
        {
            return _dataAccessLayer.GetAllTeams();
        }

        public Team GetTeam(int id)
        {
            return _dataAccessLayer.GetTeam(id);
        }

        public void EditTeam(Team team)
        {
            _dataAccessLayer.EditTeam(team);
        }
        public void DeleteTeam(int id)
        {
            _dataAccessLayer.DeleteTeam(id);
        }
        #endregion

        #region SportEvents
        public List<SportEvent> GetNearSportEvents()
        {
            return _dataAccessLayer.GetNearSportEvents();
        }

        public SportEvent GetSportEvent(int id)
        {
            return _dataAccessLayer.GetSportEvent(id);
        }

        public void AddSportEvent(SportEvent sportEvent)
        {
            _dataAccessLayer.AddSportEvent(sportEvent);
        }

        public List<SportEvent> GetAllSportEvents()
        {
            return _dataAccessLayer.GetAllSportEvents();
        }

        public void EditEvent(SportEvent sportEvent)
        {
            _dataAccessLayer.EditEvent(sportEvent);
        }
        public void DeleteEvent(int id)
        {
            _dataAccessLayer.DeleteEvent(id);
        }
        #endregion

        #region Bets       
        public void SetBets(Rate rate, List<Bet> bets)
        {
            int rateId = _dataAccessLayer.AddRate(rate);
            _dataAccessLayer.AddBets(bets, rateId);
        }
        #endregion

        #region Rates    
        public List<Rate> GetUsersRates(string userId)
        {
            var rates = _dataAccessLayer.GetUsersRates(userId);
            foreach (var rate in rates)
            {
                var bets = _dataAccessLayer.GetBets(rate.Id);
                rate.Bets = bets;
            }
            return rates;
        }
        #endregion

        #region Users
        public void GiveUserMoney(string id, int money)
        {
            _dataAccessLayer.SetUsersMoney(id, money);
        }

        public int GetUsersMoney(string id)
        {

            return _dataAccessLayer.GetUsersMoney(id);
        }

        public void TakeUsersMoney(string userId, int money)
        {
            var userMoney = _dataAccessLayer.GetUsersMoney(userId);
            userMoney -= money;
            _dataAccessLayer.SetUsersMoney(userId, userMoney);
        }



        #endregion

    }
}
