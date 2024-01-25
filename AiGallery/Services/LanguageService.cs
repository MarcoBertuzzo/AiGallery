using System.Globalization;
using System.Resources;

namespace AiGallery.Services
{
    public enum UserLanguage
    {
        Italian,
        English
    }
    
    /// <summary>
    /// In base alle lingue accettate dal browser dell'utente imposta la lingua da utilizzare nel sito
    /// Gestisce quindi anche la richiesta di stringhe localizzate
    /// </summary>
    public class LanguageService
    {
        private CultureInfo _currentCulture = new CultureInfo("en-EN");
        private readonly ResourceManager resourceManager;

        public LanguageService(IHttpContextAccessor HttpContextAccessor)
        {
            resourceManager = new ResourceManager("AiGallery.Resources.Resources", typeof(LanguageService).Assembly); //ProjectName.Folder.ResxFileName
            SetCurrentLanguage(HttpContextAccessor);
        }

        public void SetCurrentLanguage(IHttpContextAccessor HttpContextAccessor)
        {
            if (HttpContextAccessor.HttpContext != null && HttpContextAccessor.HttpContext.Request.Headers["Accept-Language"].ToString().ToLower().IndexOf("it-it") >= 0)
                _currentCulture = new CultureInfo("it-IT");
            else
                _currentCulture = new CultureInfo("en-EN");
        }

        public UserLanguage CurrentLanguage
        {
            get
            {
                if (_currentCulture.Name == CultureInfo.GetCultureInfo("it-IT").Name)
                    return UserLanguage.Italian;
                else
                    return UserLanguage.English;
            }
        }

        public string Translate(string? key)
        {
            if (string.IsNullOrEmpty(key))
                return "";

            try
            {
                string? translatedValue = resourceManager.GetString(key, _currentCulture);
                return translatedValue ?? key;
            }
            catch (MissingManifestResourceException)
            {
                return key;
            }
        }

    }
}




