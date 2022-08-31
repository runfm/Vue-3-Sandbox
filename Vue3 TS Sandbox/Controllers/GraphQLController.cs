using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vue3_TS_Sandbox.Models.GraphQL_Test;

namespace Vue3_TS_Sandbox.Controllers {
	[Route("graphql")]
	[ApiController]
	public class GraphQLController : ControllerBase {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _executer;
        public GraphQLController(ISchema schema, IDocumentExecuter executer) {
            _schema = schema;
            _executer = executer;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]  GraphQLQueryDTO query) {
            var result = await _executer.ExecuteAsync(_ => {
                _.Schema = _schema;
                _.Query = query.Query;
                _.Inputs = query.Variables?.ToString().ToInputs();

            });
            if (result.Errors?.Count > 0) {
                return BadRequest();
            }
            return Ok(result.Data);
        }
    }
}
