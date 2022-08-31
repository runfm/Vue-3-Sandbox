using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Vue3_TS_Sandbox.DI.Scoped;
using Vue3_TS_Sandbox.Models.Store;

namespace Vue3_TS_Sandbox.DI.Singleton {
	public class SspStore : SspExpirable {
		OtcDbReader DBReader { get; set; }
		public List<string> TestCollection = new List<string> { "a", "b", "c" };



		public ConcurrentDictionary<string, SspComponentType> ComponentTypes { get; set; }

		public ConcurrentDictionary<string, SspButtonType> ButtonTypes { get; set; }

		public ConcurrentDictionary<string, SspFontFamily> FontFamilies { get; set; }

		public ConcurrentDictionary<string, SspSize> Sizes { get; set; }

		public SspStore(SspApp app, OtcDbReader db__) : base(app.Settings.Store.Live * 60) {
			DBReader = db__;
			UpdateStrict();

		}

		public override void Update() {
			if (IsExpired && !IsUpdating) {
				IsUpdating = true;
				Task.Run(() => UpdateStrict());
			}
		}

		public override void UpdateStrict() {
			TestCollection = new List<string>() { "aaz", "ddf", "asd" };
			var connection = DBReader.CreateConnection();
			using (connection) {
				connection.Open();
				var componentTypes__ = DBReader.GetRows("select * from ComponentTypes", connection)
					.Select(r => new SspComponentType(r))
					.ToDictionary(k => k.Name);
				ComponentTypes = new ConcurrentDictionary<string, SspComponentType>(componentTypes__);

				var buttonTypes__ = DBReader.GetRows("select * from ButtonTypes", connection)
					.Select(r => new SspButtonType(r))
					.ToDictionary(k => k.Name);
				ButtonTypes = new ConcurrentDictionary<string, SspButtonType>(buttonTypes__);

				var fontFamilies__ = DBReader.GetRows("select * from FontFamilies", connection)
					.Select(r => new SspFontFamily(r))
					.ToDictionary(k => k.Name);
				FontFamilies = new ConcurrentDictionary<string, SspFontFamily>(fontFamilies__);


				var sizes__ = DBReader.GetRows("select * from Sizes", connection)
					.Select(r => new SspSize(r))
					.ToDictionary(k => k.Name);
				Sizes = new ConcurrentDictionary<string, SspSize>(sizes__);

			}



			Changed = DateTime.Now;
			IsUpdating = false;
		}
	}
}
