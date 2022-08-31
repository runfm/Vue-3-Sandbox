using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vue3_TS_Sandbox.DI.Singleton;

namespace Vue3_TS_Sandbox.DI.Scoped {
	public class SspContext {
		public SspApp App { get; set; }
		public SspStore Store { get; set; }

		public SspUser User { get; set; }

		public SspContext(SspApp app, SspStore store, SspUser user) {
			App = app;
			Store = store;
			User = user;
		}
	}
}
