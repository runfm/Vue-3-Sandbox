using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vue3_TS_Sandbox.DI.Scoped;

namespace Vue3_TS_Sandbox.DI.Singleton.SessionsStore {
	public class SspSessionsStore {
		ConcurrentDictionary <string, SspSession > sessions { get; set; }
		SspApp app { get; set; }
		SspStore store { get; set; }

		public SspSessionsStore(SspApp app, SspStore store) {
			this.app = app;
			this.store = store;
			sessions = new ConcurrentDictionary<string, SspSession>();
		}

		public SspSession CreateSession(SspContext context) {
			var session = new SspSession(context.User, app.Settings.User.Live * 60);
			sessions.TryAdd(context.User.Name, session);
			return session;
		}

		public bool DeleteSession(SspContext context) {
			SspSession s = null;
			return sessions.TryRemove(context.User.Name, out s);
		}

		public SspSession GetSession(SspContext context) {
			SspSession session = null;
			sessions.TryGetValue(context.User.Name, out session);
			return session;
		}

		public bool HasSession(SspContext context) {
			SspSession session = null;
			return sessions.TryGetValue(context.User.Name, out session);
		}

		public SspSession CreateOrUpdateSession(SspContext context) {
			SspSession session = null;
			bool has = sessions.TryGetValue(context.User.Name, out session);
			if (!has) {
				session = new SspSession(context.User, app.Settings.User.Live);
				sessions.TryAdd(context.User.Name, session);
			}
			session.Update();
			return session;
		}
	}
}
