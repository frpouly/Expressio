using AutoMapper;
using Expressio.Controllers;
using Expressio.Models;
using Microsoft.Identity.Client;

namespace Expressio.UnitTests.Controller
{
    public class ExpressionsController_GetAll
    {
        IMapper _mapper;
        ExpressioContext _context = Utils.CreateFakeContext();
        
        public ExpressionsController_GetAll()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ExpressioProfile());
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Theory]
        [InlineData("fr", 3209)]
        [InlineData("en", 2817)]
        [InlineData("de", 0)]
        public async void ItContainsTheCorrectNumber(string lang, int nbExpressions)
        {
            var controller = new ExpressionsController(_context, _mapper);
            var result = await controller.GetExpressions(lang);

            Assert.Equal(nbExpressions, result.Value.Count());
        }
    }
}