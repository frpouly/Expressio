using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Expressio.Models;

namespace Expressio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MixedExpressionsController : ControllerBase
    {
        private readonly ExpressioContext _context;
        private readonly Mixer _mixer;

        public MixedExpressionsController(ExpressioContext context)
        {
            context.Database.EnsureCreated();
            _context = context;
            _mixer = new Mixer(_context.Languages.First().Expressions.ToList());
        }

        [HttpGet("random")]
        public async Task<ActionResult<MixedExpression>> GetRandomMixedExpression()
        {
            return await Task.Run(() => _mixer.Generate());
        }

        [HttpGet("seeded/{seed}")]
        public async Task<ActionResult<MixedExpression>> GetSeededMixedExpression(int seed)
        {
            return await Task.Run(() => _mixer.Generate(seed));
        }
    }
}