using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Vue3_TS_Sandbox.DI.Scoped;

namespace Vue3_TS_Sandbox.DI.Singleton.SessionsStore {
	public class SspSession: SspExpirable {
		public SspUser User { get; set; }

		[JsonIgnore]
		public WebSocket Ws { get; set; }

		public SspSession(SspUser user, int live):base(live) {
			User = user;
		}

		public override void Update() {
			UpdateStrict();
		}

		public override void UpdateStrict() {
			Changed = DateTime.Now;
		}
	}
}
