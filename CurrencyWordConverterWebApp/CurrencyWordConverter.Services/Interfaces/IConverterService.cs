namespace CurrencyWordConverter.Services.Interfaces
{
    public interface IConverterService
    {
        public string GetTranslationOfDecimalToPhysical(string value, string currency);

    }
}
