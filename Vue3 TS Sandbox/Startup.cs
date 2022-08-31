using GraphiQl;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;


using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vue3_TS_Sandbox.DI.Scoped;
using Vue3_TS_Sandbox.DI.Singleton;
using Vue3_TS_Sandbox.DI.Singleton.SessionsStore;
using Vue3_TS_Sandbox.Models.GraphQL;
using Vue3_TS_Sandbox.Models.GraphQL_Test;
using VueCliMiddleware;

namespace Vue3_TS_Sandbox {
	public class Startup {
		public Startup(IConfiguration configuration) {
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {

			string connectionString = Configuration.GetValue<string>("App:DB:ConnectionString");

			services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options => { //CookieAuthenticationOptions
				options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
			});

			services.AddSwaggerGen();

			services.AddControllersWithViews();

			services.AddSingleton<SspApp>();
			services.AddSingleton<OtcDbReader>();
			services.AddSingleton<SspStore>();
			services.AddSingleton<SspSessionsStore>();

			services.AddScoped<SspUser>();
			services.AddScoped<SspContext>();

			services.AddScoped<IDependencyResolver>(_ => new FuncDependencyResolver(_.GetRequiredService));
			services.AddScoped<IDocumentExecuter, DocumentExecuter>();
			services.AddScoped<IDocumentWriter, DocumentWriter>();
			services.AddScoped<SspUserService>();
			services.AddScoped<SspUserQuery>();
			services.AddScoped<SspUserMutation>();
			services.AddScoped<SspUserType>();
			services.AddScoped<SspUserInputType>();
			services.AddScoped<ISchema, GraphQLDemoSchema>();

			services.AddSpaStaticFiles(configuration => {
				configuration.RootPath = "sandb";
			});
			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {

			app.ApplicationServices.GetService<SspApp>();
			app.ApplicationServices.GetService<OtcDbReader>();
			app.ApplicationServices.GetService<SspStore>();
			app.ApplicationServices.GetService<SspSessionsStore>();

			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}
			app.UseGraphiQl("/graphql_ui", "/graphql");

			app.UseSwagger();
			app.UseSwaggerUI();
			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();    // аутентификация
			app.UseAuthorization();     // авторизация

			var webSocketOptions = new WebSocketOptions {
				KeepAliveInterval = TimeSpan.FromMinutes(2)
			};

			app.UseWebSockets(webSocketOptions);

			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});

			app.UseSpaStaticFiles();
			app.UseSpa(spa => {
				if (env.IsDevelopment())
					spa.Options.SourcePath = "sandb";
				else
					spa.Options.SourcePath = "sandb";

				if (env.IsDevelopment()) {
					spa.UseVueCli(npmScript: "serve");
				}

			});
		}
	}
}
