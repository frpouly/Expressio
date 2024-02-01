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
            var val = mixer.Generate().Content;
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
            }.AsQueryable();
            
            var mockSet = new Mock<DbSet<Expression>>();
            mockSet.As<IQueryable<Expression>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Expression>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Expression>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Expression>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            var mockContext = new Mock<ExpressionContext>();
            mockContext.Setup(c => c.Expressions).Returns(mockSet.Object);

            return new Mixer(mockContext.Object);
        }
    }
}