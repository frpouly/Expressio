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
    [Route("api/[controller]")]
    [ApiController]
    public class ExpressionsController : ControllerBase
    {
        private readonly ExpressionContext _context;

        public ExpressionsController(ExpressionContext context)
        {
            _context = context;
        }

        // GET: api/Expressions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expression>>> GetExpressions()
        {
            return await _context.Expressions.ToListAsync();
        }

        // GET: api/Expressions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Expression>> GetExpression(long id)
        {
            var Expression = await _context.Expressions.FindAsync(id);

            if (Expression == null)
            {
                return NotFound();
            }

            return Expression;
        }

        private bool ExpressionExists(long id)
        {
            return _context.Expressions.Any(e => e.Id == id);
        }
    }
}
