using ShoppingCart.Data.Abstract;
using ShoppingCart.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace ShoppingCart.Service
{
    public class LanguageService :ILanguageService
    {
        private readonly IContext _context;
        private readonly HttpSessionState _session;
        private readonly string _languageSessionKey = "Language";

        public LanguageService(IContext context, HttpSessionState session = null)
        {
            _context = context;
            _session = session ?? HttpContext.Current.Session;
        }

        public Language GetLanguage()
        {
            Language language = _context.Languages.Find(l => l.LanguageCode == _session[_languageSessionKey].ToString());
            if(language == null)
            {
                language = _context.Languages.Find(l => l.Default == true);
            }
            return language;
        }
        public void SetLanguage(string languageCode="tr")
        {
            _session[_languageSessionKey] = languageCode;
        }

    }
}
