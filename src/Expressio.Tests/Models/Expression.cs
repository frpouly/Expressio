using Xunit;
using Expressio.Models;
using System.Drawing;

namespace Expressio.UnitTests.Models
{
    public class Expression_Words
    {
        [Theory]
        [InlineData("Prendre de l'âge", 1)]
        [InlineData("Raining cats and dogs", 2)]
        [InlineData(null, 0)]
        public void ItContainsTheCorrectNumberOfWords(string expressionContent, int expectedSize)
        {
            var expression = new Expression
            {
                Content = expressionContent
            };
            int size = expression.Words().Count();

            Assert.Equal(size, expectedSize);
        }

        [Theory]
        [InlineData("Prendre de l'âge", new string[] { "de" })]
        [InlineData("Raining cats and dogs", new string[] {"cats", "and"})]
        public void ItHasTheCorrectWords(string expressionContent, IEnumerable<string> expectedWords)
        {
            var expression = new Expression
            {
                Content = expressionContent
            };

            Assert.True(expression.Words().Count() == expectedWords.Count() 
                && expression.Words().Intersect(expectedWords).Count() == expectedWords.Count());
        }
    }

     public class Expression_SplitFirst
    {
        [Theory]
        [InlineData("prendre de l'âge", "de", "prendre de")]
        [InlineData("with the same same word", "same", "with the same")]
        public void ItSplitsTheSentenceCorrectly(string expressionContent, string word, string expectedResult)
        {
            var expression = new Expression
            {
                Content = expressionContent
            };

            Assert.Equal(expectedResult, expression.SplitFirst(word));
        }
    }

    public class Expression_SplitLast
    {
        [Theory]
        [InlineData("prendre de l'âge", "de", "l'âge")]
        [InlineData("with the same same word", "same", "same word")]
        [InlineData("une hirondelle fait le printemps", "le", "printemps")]
        public void ItSplitsTheSentenceCorrectly(string expressionContent, string word, string expectedResult)
        {

            var expression = new Expression
            {
                Content = expressionContent
            };

            Assert.Equal(expectedResult, expression.SplitLast(word));
        }
    }
}