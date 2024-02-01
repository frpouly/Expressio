using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Expressio.Models;

namespace Expressio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MixedExpressionsController : ControllerBase
    {
        private readonly ExpressionContext _context;
        private readonly Mixer _mixer;

        public MixedExpressionsController(ExpressionContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
            _mixer = new Mixer(_context);
        }

        // GET: api/Expressions
        [HttpGet("random")]
        public async Task<ActionResult<MixedExpression>> GetMixedRandomExpression()
        {
            return await Task.Run(() => _mixer.Generate());
        }
    }
}