using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Vue3_TS_Sandbox.Extension {
	public static class OtcDataRowExtension {
		public static int GetInt(this DataRow row, string key = "ID") {
			var value = row[key];
			if (value == null || value is DBNull || value == DBNull.Value)
				return 0;
			return (int)value;
		}

		public static string GetString(this DataRow row, string key = "ID") {
			return row[key].ToString().Trim();
		}
	}
}
