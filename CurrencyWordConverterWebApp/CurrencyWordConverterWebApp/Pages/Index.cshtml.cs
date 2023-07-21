using CurrencyWordConverter.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CurrencyWordConverterWebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConverterService _converterService;

        public string ConverterResult = string.Empty;
        public string InputValue = string.Empty;
        public string CurrencyValue = string.Empty;


        public IndexModel(ILogger<IndexModel> logger, IConverterService converterService)
        {
            _logger = logger;
            _converterService = converterService;
        }


        public void OnPost(string decimalValue, string selectedCurrency)
        {
            var split = decimalValue.Split('.');
            if (split.Length > 1 && split[1].Length > 2)
            {
                var secondPart = split[1].Substring(0, 2);
                decimalValue = split[0] + "." + secondPart;
            }
            InputValue = decimalValue;

            var result = _converterService.GetTranslationOfDecimalToPhysical(decimalValue, selectedCurrency);
            ConverterResult = result;
        }

        public void OnGet()
        {

        }



    }
}