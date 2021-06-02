using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agents.Models
{
	public class Agent
	{
		public string AgentCode { get; set; }
		public string AgentName { get; set; }
		public string WorkingArea { get; set; }
		public decimal Commission { get; set; }
		public string PhoneNo { get; set; }
		public byte isHidden { get; set; }
	}
}
