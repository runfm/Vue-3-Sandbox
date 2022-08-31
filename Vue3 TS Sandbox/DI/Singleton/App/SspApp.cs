using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vue3_TS_Sandbox.DI.Singleton {
	public class SspApp {
		IConfiguration Configuration { get; set; }
		public string Target { get; set; }
		public SspAppSettings Settings { get; set; }
		public SspApp(IConfiguration configuration, IWebHostEnvironment env) {
			Configuration = configuration;
			Target = Environment.MachineName;
			Settings = configuration.GetSection("App").Get<SspAppSettings>();
		}
	}
}
