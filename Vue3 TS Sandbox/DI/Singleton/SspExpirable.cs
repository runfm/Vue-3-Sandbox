using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vue3_TS_Sandbox.DI.Singleton {
	public interface ISspExpirable {
		public void Update();
	}

	public abstract class SspExpirable {
		public DateTime Created { get; set; }
		public DateTime Changed { get; set; }
		public bool IsUpdating {
			get {
				return updating;
			}
			set {
				updating = value;

			}
		}


		int live { get; set; }
		bool updating;

		public bool IsExpired {
			get {
				return (DateTime.Now - Changed).TotalSeconds >= live;
			}
		}

		public SspExpirable(int live) : this() {
			this.live = live;
		}

		SspExpirable() {
			Created = DateTime.Now;
			Changed = DateTime.Now;
		}

		public abstract void Update();
		public abstract void UpdateStrict();
	}
}
