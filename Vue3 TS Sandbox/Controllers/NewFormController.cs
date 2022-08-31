using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vue3_TS_Sandbox.DI.Scoped;
using Vue3_TS_Sandbox.DI.Singleton;
using Vue3_TS_Sandbox.DI.Singleton.SessionsStore;

namespace Vue3_TS_Sandbox.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class NewFormController : ControllerBase {
		SspContext Context { get; set; }
		SspSessionsStore SessionsStore { get; set; }

		ApplicationContext EF { get; set; }

		public NewFormController(SspContext context, SspSessionsStore sessionsStore, ApplicationContext db) {
			Context = context;
			SessionsStore = sessionsStore;
			EF = db;
		}

		[HttpGet("app/info")]
		public IActionResult GetAppInfo() {
			return Ok(JsonConvert.SerializeObject(Context));
		}



		[HttpGet("app/data")]
		public IActionResult GetServerData() {
			SspSession session = null;
			session = SessionsStore.GetSession(Context);
			if (session == null)
				session = SessionsStore.CreateSession(Context);

			var rnd = new Random();
			var max = rnd.Next(3, 9);
			var randomContextMenuItems = Enumerable.Range(1, max)
				.Select(num => {
					var ID = Guid.NewGuid().ToString();
					return new {
						ID = ID,
						Name = $"ContextMenuItem{num} {ID.Substring(0, 5)}",
						Alias = Guid.NewGuid().ToString()
					};
				})
				.ToList();

			var contextMenu = new {
				Items = randomContextMenuItems,
				Split = false
			};

			var result = new {
				ContextMenu = contextMenu,
				Sizes = Context.Store.Sizes.Values.Select(v => v.Name).ToList(),
				ComponentTypes = Context.Store.ComponentTypes.Values.ToList(),
				ButtonTypes = Context.Store.ButtonTypes.Values.Select(v => v.Name).ToList(),
				FontFamilies = Context.Store.FontFamilies.Values.Select(v => v.Name).ToList(),
				Session = session
			};

			return Ok(JsonConvert.SerializeObject(result));
		}

		[Authorize]
		[HttpGet("ef_test")]
		public async Task<IActionResult> Index() {
			//var users = await EF.Users.ToListAsync();

			EF.CreateTables();

			return Ok();
		}

		[HttpGet("user/{login}/login")]
		public async Task<IActionResult> Login(string login) {
			var claims = new List<Claim>{
				new Claim(ClaimsIdentity.DefaultNameClaimType, login) 
			};

			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			// установка аутентификационных куки
			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));

			return Ok();
		}

		[HttpGet("ef_add/{template}")]
		public async Task<IActionResult> Create(string template) {

			var user = new SspUser();
			user.Created = DateTime.Now;

			var namePart = Guid.NewGuid().ToString().Substring(0, 5);
			if(template == "admin") {
				user.Name = "admin";
				user.IsAdmin = true;
			}
				
			else
				user.Name = $"{template} {namePart}";

			//EF.CreateTables();
			EF.Users.Add(user);
			await EF.SaveChangesAsync();
			return Ok();
		}

		[HttpGet("ws_test")]
		public async Task GetWs() {
			if (HttpContext.WebSockets.IsWebSocketRequest) {
				var session = SessionsStore.CreateOrUpdateSession(Context);
				using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
				
				session.Ws = webSocket;
				await Echo(webSocket);
			}
		}

		[HttpGet("ws_close")]
		public async void CloseWs() {
			var session = SessionsStore.GetSession(Context);
			await session.Ws.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
		}

		private async Task Echo(WebSocket webSocket) {
			var buffer = new byte[1024 * 4];
			var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
			while (!result.CloseStatus.HasValue) {
				var str = Encoding.UTF8.GetString(buffer);
				var serverMsg = Encoding.UTF8.GetBytes($"Server: Hello. You said: {str}");
				await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);
				result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
			}
			await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
		}
	}
}
