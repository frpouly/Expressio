using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Expressio.Models;
using AutoMapper;
using Asp.Versioning;

namespace Expressio.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class ExpressionsController : ControllerBase
    {
        private readonly ExpressioContext _context;
        private readonly IMapper _mapper;

        public ExpressionsController(ExpressioContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/v1/Expressions/:lang
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

        // GET: api/v1/Expressions/5
        [HttpGet("item/{id}")]
        public async Task<ActionResult<ExpressionDTO>> GetExpression(long id)
        {
            var expression = await _context.Expressions.FindAsync(id);

            if (expression == null)
            {
                return NotFound($"No expression with the following id: {id}");
            }

            return _mapper.Map<Expression, ExpressionDTO>(expression);
        }
    }
}
