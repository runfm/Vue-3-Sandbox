using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Vue3_TS_Sandbox.DI.Singleton;
using Vue3_TS_Sandbox.Models.Store;

namespace Vue3_TS_Sandbox.DI.Scoped {
	public class SspUser {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int ID { get; set; }
		public string Name { get; set; }
		public bool IsAdmin { get; set; }

		
		public DateTime Created { get; set; }


		public SspUser() { }

		public SspUser(SspApp app) {
			Name = "Anonimous";
		}
	}

	public class ApplicationContext : DbContext {
		public DbSet<SspUser> Users { get; set; }
		public DbSet<SspTranslationItem> TranslationItems { get; set; }
		public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options) {

			//Database.EnsureCreated();   // создаем базу данных при первом обращении
		}

		public void CreateTables() {
			var databaseCreator = (Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator);
			databaseCreator.CreateTables();
		}
	}
}
