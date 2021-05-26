using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Agents.Models
{
    public class SingleAgent
    {

        private readonly IConfiguration _configuration;

        public SingleAgent(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Agent GetSingleAgentData(string id)
        {
            var agent = new Agent();

            //string commandText = "UPDATE Sales.Store SET Demographics = @demographics WHERE CustomerID = @ID;";
            string commandText = "SELECT * FROM Agents WHERE AgentCode = @AgentCode";

            var connString = _configuration.GetConnectionString("default");
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();


                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.Add("@AgentCode", SqlDbType.Char);
                command.Parameters["@AgentCode"].Value = id;


                //var cmd = new SqlCommand();
                //cmd.Connection = conn;
                //cmd.CommandType = System.Data.CommandType.Text;
                //cmd.CommandText = "SELECT * FROM Agents";




                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var aCode = reader["AgentCode"].ToString();
                    var aName = reader["AgentName"].ToString();
                    var wArea = reader["WorkingArea"].ToString();
                    var commission = Convert.ToInt32(reader["Commission"]);
                    var phone = reader["PhoneNo"].ToString();

                    agent.AgentCode = aCode;
                    agent.AgentName = aName;
                    agent.WorkingArea = wArea;
                    agent.Commission = commission;
                    agent.PhoneNo = phone;
                }
            }

            return agent;
        }

    }
}
