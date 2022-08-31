using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Vue3_TS_Sandbox.Models.Store {
	public class SspTranslationItem {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int ID { get; set; }
		public string Alias { get; set; }
		public string Text { get; set; }

		[Column(TypeName = "varchar(2)")]
		public string LangCode { get; set; }
	}
}
