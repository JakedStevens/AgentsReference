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

			string commandText = "SELECT * FROM Agents WHERE AgentCode = @AgentCode";

			var connString = _configuration.GetConnectionString("default");
			using (SqlConnection connection = new SqlConnection(connString))
			{
				connection.Open();

				SqlCommand command = new SqlCommand(commandText, connection);
				command.Parameters.Add("@AgentCode", SqlDbType.Char);
				command.Parameters["@AgentCode"].Value = id;

				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					var aCode = reader["AgentCode"].ToString();
					var aName = reader["AgentName"].ToString();
					var wArea = reader["WorkingArea"].ToString();
					var commission = Convert.ToDecimal(reader["Commission"]);
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

		public string GetLastId()
		{
			var newAgentCode = "";

			string commandText = "SELECT * FROM Agents WHERE AgentCode=(SELECT max(AgentCode) FROM Agents)";

			var connString = _configuration.GetConnectionString("default");
			using (SqlConnection connection = new SqlConnection(connString))
			{
				connection.Open();

				SqlCommand command = new SqlCommand(commandText, connection);

				var reader = command.ExecuteReader();

				while (reader.Read())
				{
					var agentCode = reader["AgentCode"].ToString();
					var codeNo = agentCode.Split('0');
					string id1 = codeNo[1];
					int id2 = Int32.Parse(id1);
					int increasedId = id2 + 1;
					string newId = $"A0{increasedId}";

					newAgentCode = newId;
				}
			}

			return newAgentCode;

		}

		public void CreateNewAgent(Agent agent)
		{
			string newAgentCode = this.GetLastId();

			string connString = _configuration.GetConnectionString("default");
			using (var connection = new SqlConnection(connString))
			{

				connection.Open();

				var command = new SqlCommand();
				command.CommandType = CommandType.Text;
				command.CommandText =
					$"INSERT INTO Agents (AgentCode, AgentName, WorkingArea, Commission, PhoneNo) " +
					$"VALUES (@agentCode, @agentName, @workingArea, @commission, @phoneNo)";
				command.Parameters.Add(new SqlParameter
				{
					ParameterName = "@agentCode",
					Value = newAgentCode,
					SqlDbType = SqlDbType.Char
				});
				command.Parameters.Add(new SqlParameter
				{
					ParameterName = "@agentName",
					Value = agent.AgentName,
					SqlDbType = SqlDbType.NVarChar
				});
				command.Parameters.Add(new SqlParameter
				{
					ParameterName = "@workingArea",
					Value = agent.WorkingArea,
					SqlDbType = SqlDbType.NVarChar
				});
				command.Parameters.Add(new SqlParameter
				{
					ParameterName = "@commission",
					Value = agent.Commission,
					SqlDbType = SqlDbType.Decimal
				});
				command.Parameters.Add(new SqlParameter
				{
					ParameterName = "@phoneNo",
					Value = agent.PhoneNo,
					SqlDbType = SqlDbType.Char
				});
				command.Connection = connection;

				command.ExecuteNonQuery();
			}
		}

		public void HideAgent(string aCode)
		{
			string connString = _configuration.GetConnectionString("default");
			using (var connection = new SqlConnection(connString))
			{
				connection.Open();

				var command = new SqlCommand();
				command.CommandType = CommandType.Text;
				command.CommandText = "UPDATE Agents SET isHidden = 1 WHERE AgentCode = @agentCode";
				command.Parameters.Add(new SqlParameter
				{
					ParameterName = "@agentCode",
					Value = aCode,
					SqlDbType = SqlDbType.NVarChar
				});
				command.Connection = connection;
				command.ExecuteNonQuery();
			}
		}

		public void UpdateAgent(Agent agent)
		{
			string connString = _configuration.GetConnectionString("default");
			using (var connection = new SqlConnection(connString))
			{
				connection.Open();

				var command = new SqlCommand();
				command.CommandType = CommandType.Text;
				command.CommandText = 
					$"UPDATE Agents SET AgentName = @agentName, " +
					$"WorkingArea = @workingArea, " +
					$"Commission = @commission, " +
					$"PhoneNo = @phoneNo " +
					$"WHERE AgentCode = @agentCode";

				command.Parameters.Add(new SqlParameter
				{
					ParameterName = "@agentCode",
					Value = agent.AgentCode,
					SqlDbType = SqlDbType.Char
				});
				command.Parameters.Add(new SqlParameter
				{
					ParameterName = "@agentName",
					Value = agent.AgentName,
					SqlDbType = SqlDbType.NVarChar
				});
				command.Parameters.Add(new SqlParameter
				{
					ParameterName = "@workingArea",
					Value = agent.WorkingArea,
					SqlDbType = SqlDbType.NVarChar
				});
				command.Parameters.Add(new SqlParameter
				{
					ParameterName = "@commission",
					Value = agent.Commission,
					SqlDbType = SqlDbType.Decimal
				});
				command.Parameters.Add(new SqlParameter
				{
					ParameterName = "@phoneNo",
					Value = agent.PhoneNo,
					SqlDbType = SqlDbType.Char
				});

				command.Connection = connection;
				command.ExecuteNonQuery();
			}
		}

	}
}
