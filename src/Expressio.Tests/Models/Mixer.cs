using Expressio.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Expressio.UnitTests.Models
{
    public class Mixer_Generate
    {
        [Fact]
        public void ItGeneratesMixedSentence()
        {
            var mixer = CreateMixer();
            var regEx = @"frotter le fromage|entre la poire et le lard ensemble";
            var value = mixer.Generate();
            Assert.Matches(regEx, value.Content);
        }

        [Fact]
        public void ItGeneratesTheSameExpressionWithSeed()
        {
            var mixer = CreateMixer();
            int seed = 78833297;
            var val = mixer.Generate(seed);
            Assert.Equal(mixer.Generate(seed).Content, mixer.Generate(seed).Content);
        }

        public Mixer CreateMixer()
        {
            var data = new List<Expression>
            {
                new Expression { Id = 1, Content = "frotter le lard ensemble", Definitions = []},
                new Expression { Id = 2, Content = "entre la poire et le fromage", Definitions = []}
            };

            return new Mixer(data);
        }
    }
}