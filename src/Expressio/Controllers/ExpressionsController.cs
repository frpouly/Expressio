using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Expressio.Models;
using AutoMapper;

namespace Expressio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpressionsController : ControllerBase
    {
        private readonly ExpressioContext _context;
        private readonly IMapper _mapper;

        public ExpressionsController(ExpressioContext context, IMapper mapper)
        {
            context.Database.EnsureCreated();
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Expressions/:lang
        [HttpGet("{lang}")]
        public async Task<ActionResult<IEnumerable<ExpressionDTO>>> GetExpressions(string lang)
        {
            return await _context
                .Expressions
                .Include(e => e.Language)
                .Where(e => e.Language.Code == lang)
                .Select(e =>
                    _mapper.Map<Expression, ExpressionDTO>(e)
                )
                .ToListAsync();
        }

        // GET: api/Expressions/5
        [HttpGet("item/{id}")]
        public async Task<ActionResult<ExpressionDTO>> GetExpression(long id)
        {
            var expression = await _context.Expressions.FindAsync(id);

            if (expression == null)
            {
                return NotFound();
            }

            return _mapper.Map<Expression, ExpressionDTO>(expression);
        }
    }
}
