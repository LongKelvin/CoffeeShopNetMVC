using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace PIMTool.Language
{
    public class LanguageManagerment
    {
        public LanguageManagerment()
        {
        }

        public static List<Languages> AvailableLanguages = new List<Languages> {
            new Languages {
                LanguageFullName = "English", LanguageCultureName = "en"
            },
            new Languages {
                LanguageFullName = "VietNam", LanguageCultureName = "vi"
            },
            //new Languages {
            //    LanguageFullName = "japanese", LanguageCultureName = "Ja"
            //},
            //new Languages {
            //    LanguageFullName = "arabic", LanguageCultureName = "Ar"
            //},
        };

        public static bool IsLanguageAvailable(string lang)
        {
            return AvailableLanguages.FirstOrDefault(a => a.LanguageCultureName.Equals(lang)) != null;
        }

        public static string GetDefaultLanguage()
        {
            return AvailableLanguages.First().LanguageCultureName;
        }

        public void SetLanguage(string lang)
        {
            try
            {
                if (!IsLanguageAvailable(lang)) lang = GetDefaultLanguage();
                var cultureInfo = new CultureInfo(lang);
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
                HttpCookie langCookie = new HttpCookie("culture", lang)
                {
                    Expires = DateTime.Now.AddYears(1)
                };
                HttpContext.Current.Response.Cookies.Add(langCookie);
            }
            catch (Exception) { }
        }
    }

    public class Languages
    {
        public string LanguageFullName
        {
            get;
            set;
        }

        public string LanguageCultureName
        {
            get;
            set;
        }
    }
}