using GraphQL;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vue3_TS_Sandbox.DI.Scoped;

namespace Vue3_TS_Sandbox.Models.GraphQL {
	public class SspUserType : ObjectGraphType<SspUser> {
		public SspUserType() {
			Name = "SspUser";
			Field(t => t.Name).Description("User Name");
			Field(t => t.ID).Description("User ID").Name("id");
			Field(t => t.Created).Description("User Creation Date");
		}
	}

	public class SspUserInputType : InputObjectGraphType {
		public SspUserInputType() {
			Name = "userInput";
			Field<NonNullGraphType<StringGraphType>>("name");
		}
	}

	public class SspUserQuery : ObjectGraphType {
		public SspUserQuery(SspUserService userService) {
			int id = 0;
			Field<ListGraphType<SspUserType>>(
			name: "users", resolve: context => {
				return userService.GetAll();
			});
			Field<SspUserType>(
				name: "user",
				arguments: new QueryArguments(new	QueryArgument<IntGraphType> { Name = "id" }),
				resolve: context => {
					id = context.GetArgument<int>("id");
					return userService.Get(id);
				}
			);
		}
	}

	public class SspUserMutation : ObjectGraphType {
		public SspUserMutation(SspUserService userService) {
			Field<SspUserType>(
				"createUser",
				arguments: new QueryArguments(new QueryArgument<NonNullGraphType<SspUserInputType>> { Name = "user" }),
				resolve: context =>
				{
					var owner = context.GetArgument<SspUser>("user");
					return userService.Add(owner);
				}
			);
		}
	}


	public class GraphQLDemoSchema : Schema, ISchema {
		public GraphQLDemoSchema(IDependencyResolver resolver) : base(resolver) {
			Query = resolver.Resolve<SspUserQuery>();
			Mutation = resolver.Resolve<SspUserMutation>();
		}
	}

	public class SspUserService {
		private ApplicationContext ef { get; }
		public SspUserService(ApplicationContext db) {
			ef = db;
		}

		public List<SspUser> GetAll() {
			return ef.Users.ToList();
		}

		public SspUser Get(int id) {
			return ef.Users.FirstOrDefault(u => u.ID == id);
		}

		public SspUser Add(SspUser user) {
			ef.Users.Add(user);
			ef.SaveChangesAsync();
			return user;
		}
	}
}
