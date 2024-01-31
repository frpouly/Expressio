using Xunit;
using Expressio.Models;
using System.Drawing;

namespace Expressio.UnitTests.Models
{
    public class Expression_Words
    {
        [Theory]
        [InlineData("Prendre de l'âge", 2)]
        [InlineData("Raining cats and dogs", 3)]
        [InlineData(null, 0)]
        public void ItContainsTheCorrectNumberOfWords(string expressionContent, int expectedSize)
        {
            var expression = new Expression();
            expression.Content = expressionContent;
            int size = expression.Words().Count();

            Assert.Equal(size, expectedSize);
        }

        [Theory]
        [InlineData("Prendre de l'âge", new string[] { "de", "l'âge" })]
        [InlineData("Raining cats and dogs", new string[] {"cats", "and", "dogs"})]
        public void ItHasTheCorrectWords(string expressionContent, IEnumerable<string> expectedWords)
        {
            var expression = new Expression();
            expression.Content = expressionContent;

            Assert.True(expression.Words().Count() == expectedWords.Count() 
                && expression.Words().Intersect(expectedWords).Count() == expectedWords.Count());
        }
        
    }
}