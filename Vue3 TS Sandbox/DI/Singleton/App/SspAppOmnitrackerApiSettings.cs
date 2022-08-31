using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vue3_TS_Sandbox.DI.Singleton {
	public class SspAppOmnitrackerApiSettings {
		public int ConnectionTimeout { get; set; }
		public string ServiceUser { get; set; }
		public string ServicePassword { get; set; }
		public string Host { get; set; }
		public int Port { get; set; }
	}
}
