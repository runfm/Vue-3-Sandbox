using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vue3_TS_Sandbox.DI.Singleton.App;

namespace Vue3_TS_Sandbox.DI.Singleton {
	public class SspAppSettings {
		public SspAppStoreSettings Store { get; set; }
		public SspAppDBSettings DB { get; set; }
		public SspAppOmnitrackerApiSettings OmnitrackerApi { get; set; }
		public SspAppUserSettings User { get; set; }
	}
}
