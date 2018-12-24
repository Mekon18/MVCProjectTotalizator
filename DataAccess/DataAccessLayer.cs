using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class DataAccessLayer : IDataAccessLayer
    {
        private string _connectingString;
        public DataAccessLayer()
        {
            _connectingString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public Team GetTeam(int id)
        {
            Team team = null;

            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = $"SELECT * FROM Teams WHERE Id = {id}";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    string name = reader.GetFieldValue<string>(1);
                    string countryName = reader.GetString(2);
                    team = new Team() { Id = id, CountryName = countryName, Name = name };
                }
                reader.Close();
            }
            return team;
        }
        public void AddTeam(Team team)
        {
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = $"INSERT INTO Teams (Name, CountryName) VALUES (N'{team.Name}', N'{team.CountryName}')",
                    Connection = connection
                };
                command.ExecuteNonQuery();
            }
        }

        public void AddSportEvent(SportEvent sportEvent)
        {
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand
                {
                    CommandText = $"INSERT INTO SportEvents (Date, KindOfSport,FirstTeam,SecondTeam) VALUES (N'{sportEvent.DateTime}', N'{sportEvent.KindOfSport}',N'{sportEvent.FirstTeam}',N'{sportEvent.SecondTeam}')",
                    Connection = connection
                };

                command.ExecuteNonQuery();

            }
        }

        public SportEvent GetSportEvent(int id)
        {
            SportEvent sportEvent = null;
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = $"select * from SportEvents where Id = {id}";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())
                    {
                        DateTime date = (DateTime)reader["Date"];
                        int firstTeamId = (int)reader["FirstTeamId"];
                        int secondTeamId = (int)reader["SecondTeamId"];
                        string kindOfSport = (string)reader["KindOfSport"];
                        double coef1 = (double)reader["FirstCoeficient"];
                        double coef2 = (double)reader["SecondCoeficient"];

                        Team firstTeam = GetTeam(firstTeamId);
                        Team secondTeam = GetTeam(secondTeamId);

                        sportEvent = new SportEvent()
                        {
                            Id = id,
                            DateTime = date,
                            FirstTeam = firstTeam,
                            SecondTeam = secondTeam,
                            KindOfSport = kindOfSport,
                            FirstCoeficient = coef1,
                            SecondCoeficient = coef2
                        };
                    }
                }
                reader.Close();
            }
            return sportEvent;
        }

        public List<SportEvent> GetNearSportEvents()
        {
            List<SportEvent> sportEvents = new List<SportEvent>();
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = $"select * from SportEvents where Date between GETDATE()-32 and GETDATE()";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        DateTime date = reader.GetDateTime(1);
                        int firstTeamId = reader.GetInt32(2);
                        int secondTeamId = reader.GetInt32(3);
                        string kindOfSport = reader.GetString(4);

                        Team firstTeam = GetTeam(firstTeamId);
                        Team secondTeam = GetTeam(secondTeamId);

                        sportEvents.Add(new SportEvent() { Id = id, DateTime = date, FirstTeam = firstTeam, SecondTeam = secondTeam, KindOfSport = kindOfSport });
                    }
                }
                reader.Close();
            }
            return sportEvents;
        }

        public int GetUsersMoney(string id)
        {
            int money = 0;
            using (SqlConnection connection = new SqlConnection(_connectingString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = $"SELECT Money FROM AspNetUsers WHERE Id = N'{id}'";
                command.Connection = connection;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) // если есть данные
                {
                    reader.Read();
                    money = reader.GetInt32(0);
                }
                reader.Close();
            }
            return money;
        }
    }
}
