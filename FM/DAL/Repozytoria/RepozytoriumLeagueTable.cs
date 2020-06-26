﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM.DAL.Repozytoria
{
    using System.Data.SQLite;
    using ENCJE;
    class RepozytoriumLeagueTable
    {
        public static List<LeagueTable> GetBundesligaTable()
        {
            List<LeagueTable> clubs = new List<LeagueTable>();
            using (var connection = DBConnection.Instance.connection)
            {
                SQLiteCommand command = new SQLiteCommand("select name, points, played, scored_goals, lost_goals, wins, lost, draws from club where league = \"Bundesliga\" order by points desc, scored_goals desc, lost_goals asc", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.HasRows)
                {
                    clubs.Add(
                        new LeagueTable(
                            Convert.ToInt32(reader["id"].ToString()),
                            reader["name"].ToString(),
                            Convert.ToInt32(reader["points"].ToString()),
                            Convert.ToInt32(reader["played"].ToString()),
                            Convert.ToInt32(reader["scored_goals"].ToString()),
                            Convert.ToInt32(reader["lost_goals"].ToString()),
                            Convert.ToInt32(reader["wins"].ToString()),
                            Convert.ToInt32(reader["lost"].ToString()),
                            Convert.ToInt32(reader["draws"].ToString())
                            ));
                }
                connection.Close();
            }

            return clubs;
        }

        public static List<LeagueTable> GetPremierLeagueTable()
        {
            List<LeagueTable> clubs = new List<LeagueTable>();
            using (var connection = DBConnection.Instance.connection)
            {
                SQLiteCommand command = new SQLiteCommand("select name, points, played, scored_goals, lost_goals, wins, lost, draws from club where league = \"Premier League\" order by points desc, scored_goals desc, lost_goals asc", connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.HasRows)
                {
                    clubs.Add(
                        new LeagueTable(
                            Convert.ToInt32(reader["id"].ToString()),
                            reader["name"].ToString(),
                            Convert.ToInt32(reader["points"].ToString()),
                            Convert.ToInt32(reader["played"].ToString()),
                            Convert.ToInt32(reader["scored_goals"].ToString()),
                            Convert.ToInt32(reader["lost_goals"].ToString()),
                            Convert.ToInt32(reader["wins"].ToString()),
                            Convert.ToInt32(reader["lost"].ToString()),
                            Convert.ToInt32(reader["draws"].ToString())
                            ));
                }
                connection.Close();
            }

            return clubs;
        }

        public void ClubWins(int id, int scoredGoals, int lostGoals)
        {
            string update = $"UPDATE club set played = played + 1, points = points + 3, scored_goals = scored_goals + {scoredGoals}, lost_goals = lost_goals + {lostGoals}, wins = wins + 1 where id = {id}";
            using (var connection = DBConnection.Instance.connection)
            {
                SQLiteCommand command = new SQLiteCommand(update, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void ClubLoses(int id, int scoredGoals, int lostGoals)
        {
            string update = $"UPDATE club set played = played + 1, scored_goals = scored_goals + {scoredGoals}, lost_goals = lost_goals + {lostGoals}, lost = lost + 1 where id = {id}";
            using (var connection = DBConnection.Instance.connection)
            {
                SQLiteCommand command = new SQLiteCommand(update, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void ClubsDraws(int hostId, int visitorId, int scoredGoals, int lostGoals)
        {
            string update = $"UPDATE club set played = played + 1, points = points + 1, scored_goals = scored_goals + {scoredGoals}, lost_goals = lost_goals + {lostGoals}, draws = draws + 1 where id = {hostId} or id = {visitorId}";
            using (var connection = DBConnection.Instance.connection)
            {
                SQLiteCommand command = new SQLiteCommand(update, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}