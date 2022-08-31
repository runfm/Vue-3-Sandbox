using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vue3_TS_Sandbox.Extension {
	public static class OtcIEnumerableExtension {
		public static string Join<T>(this List<T> list, string delimiter = ",") {
			return string.Join(delimiter, list);
		}
		public static string Join<T>(this IEnumerable<T> list, string delimiter = ",") {
			return string.Join(delimiter, list);
		}
		public static string Join<T, O>(this IEnumerable<T> list, Func<T, O> selector, string delimiter = ",") {
			return string.Join(delimiter, list.Select(selector));
		}
	}
}
