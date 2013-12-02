using ShoppingCart.Data.Entity;

namespace ShoppingCart.Service
{
    public interface ILanguageService
    {
        Language GetLanguage();
        void SetLanguage(string languageCode = "tr");
    }
}
