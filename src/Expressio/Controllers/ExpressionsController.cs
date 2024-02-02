using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Expressio.Models;

namespace Expressio.Controllers
{
    [Route("api/{lang}/[controller]")]
    [ApiController]
    public class ExpressionsController : ControllerBase
    {
        private readonly ExpressioContext _context;

        public ExpressionsController(ExpressioContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
        }

        // GET: api/Expressions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpressionDTO>>> GetExpressions(string lang)
        {
            return await _context
                .Expressions
                .Include(e => e.Language)
                .Select(e =>
                    new ExpressionDTO() {
                        Id = e.Id,
                        Content = e.Content,
                        Definitions = e.Definitions
                    }
                )
                .ToListAsync();
        }

        // GET: api/Expressions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expression>> GetExpression(long id)
        {
            var Expression = _context.Languages.First().Expressions.ToList().Find(e => e.Id == id);

            if (Expression == null)
            {
                return NotFound();
            }

            return Expression;
        }
    }
}
