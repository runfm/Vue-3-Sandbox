using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Vue3_TS_Sandbox.Extension;

namespace Vue3_TS_Sandbox.Models.Store {
	public class SspFontFamily {
		public int ID { get; set; }
		public string Name { get; set; }

		public SspFontFamily(DataRow row) {
			ID = row.GetInt();
			Name = row.GetString("Name");
		}
	}
}
