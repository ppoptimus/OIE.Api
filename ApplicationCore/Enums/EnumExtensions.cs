using ApplicationCore.Exceptions;
using ApplicationCore.Entities;
using ApplicationCore.Enums;

namespace ApplicationCore.Enums
{
    public enum COUNTRY
    {
        Thailand = 14
    }
    //public static class EnumExtensions
    //{
    //    public static ContentLanguage ToContentLanguage(this string language)
    //    {
    //        if (language.ToLower() == "en")
    //            return ContentLanguage.English;
    //        return ContentLanguage.Thai;
    //    }

    //    public static ContentModule ToContentModule(this string module)
    //    {
    //        module = module.ToLower().Replace("-", "");
    //        if (module == ContentModule.ThaiEquity.ToString().ToLower())
    //            return ContentModule.ThaiEquity;
    //        else if (module == ContentModule.Derivatives.ToString().ToLower())
    //            return ContentModule.Derivatives;
    //        else if (module == ContentModule.Intermarkets.ToString().ToLower())
    //            return ContentModule.Intermarkets;

    //        else if (module == ContentModule.OtherProducts.ToString().ToLower())
    //            return ContentModule.OtherProducts;

    //        else if (module == ContentModule.Services.ToString().ToLower())
    //            return ContentModule.Services;

    //        else if (module == ContentModule.Research.ToString().ToLower())
    //            return ContentModule.Research;

    //        else if (module == ContentModule.Contactus.ToString().ToLower())
    //            return ContentModule.Contactus;
    //        return ContentModule.FrontPage;
    //    }
    //}
}