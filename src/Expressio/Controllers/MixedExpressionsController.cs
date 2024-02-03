using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Expressio.Models;
using System.Collections.Generic;
using AutoMapper;

namespace Expressio.Controllers
{
    [Route("api/[controller]/{lang}")]
    [ApiController]
    public class MixedExpressionsController : ControllerBase
    {
        private readonly ExpressioContext _context;
        private readonly IMapper _mapper;
        private readonly Dictionary<string, Mixer> _mixers = new Dictionary<string, Mixer>();

        public MixedExpressionsController(ExpressioContext context, IMapper mapper)
        {
            context.Database.EnsureCreated();
            _context = context;
            _mapper = mapper;
            foreach(Language l in _context.Languages.Include(l => l.Expressions)) {
                _mixers[l.Code] = new Mixer(l.Expressions.ToList());
            }
        }

        [HttpGet("random")]
        public async Task<ActionResult<MixedExpressionDTO>> GetRandomMixedExpression(string lang)
        {
            var mixed_expression = await Task.Run(() => _mixers[lang].Generate());
            return _mapper.Map<MixedExpression, MixedExpressionDTO>(mixed_expression);
        }

        [HttpGet("seeded/{seed}")]
        public async Task<ActionResult<MixedExpressionDTO>> GetSeededMixedExpression(int seed, string lang)
        {
            var mixed_expression = await Task.Run(() => _mixers[lang].Generate(seed));
            return _mapper.Map<MixedExpression, MixedExpressionDTO>(mixed_expression);
        }
    }
}