using System.Globalization;
using System.Text;

namespace AiGallery.Utilities
{
    public static class StringUtility
    {
        /// <summary>
        /// Converte una qualsiasi stringa (ad esempio un titolo) in un nome di pagina web, escludendo tutti i caratteri diversi da caratteri alfanumerici e da underscore
        /// Converte gli spazi in underscore
        /// Sostituisce le lettere accentate con le relative lettere non accentate
        /// </summary>
        /// <param name="inputString">Stringa di partenza</param>
        /// <returns>La stringa normalizzata per poter essere utilizzata con nome pagina web</returns>
        public static string ConvertToPageName(string? inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return "";
            //convert 'à' to 'a', 'è' to 'e', etc
            string normalizedString = inputString.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }
            normalizedString = stringBuilder.ToString().Replace(" ", "_");

            //only letters, digits and underscore
            normalizedString = System.Text.RegularExpressions.Regex.Replace(normalizedString, @"[^0-9a-zA-Z\\_]", "");

            return normalizedString;
        }
    }
}
