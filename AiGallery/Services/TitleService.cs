using AiGallery.Data;
using Microsoft.JSInterop;

namespace AiGallery.Services
{
    /// <summary>
    /// Manages the site's 'title' meta-tag.
    /// Used to manage both the Blazor <PageTitle> component and to manually force the <title> meta tag via javascript as the user gradually browses the image gallery/// 
    /// </summary>
    public class TitleService
    {
        private readonly string MainPageTitleDefault;
        private readonly MyDbContext _dbContext;
        private readonly IJSRuntime _jsRuntime;
        private readonly LanguageService _languageService;

        public TitleService(MyDbContext dbContext, IJSRuntime jsRuntime, LanguageService languageService)
        {
            _dbContext = dbContext;
            _jsRuntime = jsRuntime;
            _languageService = languageService;

            MainPageTitleDefault = languageService.Translate("MainTitle");
        }

        public string GetTitle(int? ImageStripId = null)
        {
            string title = MainPageTitleDefault;
            if (ImageStripId > 0)
            {
                var item = _dbContext.Strips.FirstOrDefault(s => s.Id == ImageStripId);

                if (item != null)
                {
                    if (_languageService.CurrentLanguage == UserLanguage.Italian)
                        title = item.Title_Ita;
                    else
                        title = item.Title_Eng;
                }
            }
            return title;
        }

        public async Task SetTitleJS(int? ImageStripId = null)
        {
            if (ImageStripId == null || ImageStripId <= 0)
            {
                await _jsRuntime.InvokeVoidAsync("changeDocumentTitle", MainPageTitleDefault);
            }
            else
            {
                var item = _dbContext.Strips.FirstOrDefault(s => s.Id == ImageStripId);

                if (item != null)
                {
                    string title;
                    if (_languageService.CurrentLanguage == UserLanguage.Italian)
                        title = item.Title_Ita;
                    else
                        title = item.Title_Eng;

                    // Runs JavaScript to set the title
                    await _jsRuntime.InvokeVoidAsync("changeDocumentTitle", title);
                }
            }
        }
    }
}

