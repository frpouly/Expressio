using Xunit;
using Expressio.Models;
using System.Drawing;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Expressio.UnitTests.Models
{
    public class FileLoader_Load
    {
        [Fact]
        public void ItLoadsTheJSON()
        {
            string path = "Resources/expressions_fr.json";
            FileLoader loader = new FileLoader(path);

            var exception = Record.Exception(() => loader.Load());
            Assert.Null(exception);
        }

        [Fact]
        public void ItHasTheCorrectNumberOfExpressions()
        {
            string path = "Resources/expressions_fr.json";
            FileLoader loader = new FileLoader(path);
            var results = loader.Load();

            Assert.Equal(results.Last().Id + 1, results.Count());
        }

        [Fact]
        public void ItHasTheCorrectFirstAndLastExpressions()
        {
            string path = "Resources/expressions_fr.json";
            FileLoader loader = new FileLoader(path);
            var results = loader.Load();

            Assert.Equal("parler dans sa barbe", results.Last().Content);
            Assert.Equal("les doigts dans le nez", results.First().Content);
        }
    }
}