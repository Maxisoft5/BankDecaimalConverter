using CurrencyWordConverter.Services.Services;
using Newtonsoft.Json.Linq;
using Shouldly;

namespace CurrencyWordConverter.Tests
{
    public class ConverterTests
    {
        [Theory]
        [InlineData("1357256.32", "one million, three hundred and fifty seven thousand, two hundred and fifty six DOLLARS AND thirty two CENTS")]
        [InlineData("1523516300", "one bilion, five hundred and twenty three million, five hundred sixteen thousand, three hundred DOLLARS")]
        [InlineData("523516312", "five hundred and twenty three million, five hundred sixteen thousand, three hundred twelve DOLLARS")]
        [InlineData("23516322", "twenty three million, five hundred sixteen thousand, three hundred and twenty two DOLLARS")]
        [InlineData("2000000", "two million DOLLARS")]
        [InlineData("2000000.00", "two million DOLLARS")]
        [InlineData("20000000", "twenty million DOLLARS")]
        [InlineData("200000000", "two hundred million DOLLARS")]
        [InlineData("200000", "two hundred thousand DOLLARS")]
        [InlineData("20000", "twenty thousand DOLLARS")]
        [InlineData("2000", "two thousand DOLLARS")]
        [InlineData("200", "two hundred DOLLARS")]
        [InlineData("20", "twenty DOLLARS")]
        [InlineData("2", "two DOLLARS")]
        [InlineData("2.55", "two DOLLARS AND fifty five CENTS")]
        [InlineData("2.05", "two DOLLARS AND five CENTS")]
        [InlineData("2.50", "two DOLLARS AND fifty CENTS")]
        public void ConvertValidDecimalStringTests(string value, string excpectedRes)
        {
            // arrange
            var converter = new ConverterService();

            // act
            var actual = converter.GetTranslationOfDecimalToPhysical(value, "USD");

            // assert
            actual.ShouldBe(excpectedRes);
        }
    }
}