using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Agents.Models
{
    public class AgentsData
    {
        private readonly IConfiguration _configuration;

        public AgentsData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Agent> AllAgentData()
        {
            var agents = new List<Agent>();

            var connString = _configuration.GetConnectionString("default");
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Agents";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var aCode = reader["AgentCode"].ToString();
                    var aName = reader["AgentName"].ToString();
                    var wArea = reader["WorkingArea"].ToString();
                    var commission = Convert.ToInt32(reader["Commission"]);
                    var phone = reader["PhoneNo"].ToString();

                    agents.Add(new Agent
                    {
                        AgentCode = aCode,
                        AgentName = aName,
                        WorkingArea = wArea,
                        Commission = commission,
                        PhoneNo = phone
                    });
                }
            }

            return agents;
        }

    }
}
