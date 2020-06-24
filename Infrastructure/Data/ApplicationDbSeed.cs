using Infrastructure.Services;
using ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbSeed : IApplicationDbSeed
    {
        public async Task SeedAsync(ApplicationDbContext applicationDbContext,
            ILoggerFactory loggerFactory)
        {
            try
            {
                // TODO: Only run this if using a real database
                // context.Database.Migrate();
                //if (!applicationDbContext.Carousels.Any())
                //{
                //    await applicationDbContext.Carousels.AddAsync(new Carousel() { Caption = "Test" });
                //    await applicationDbContext.SaveChangesAsync();
                //}
                //if (!applicationDbContext.Contents.Any())
                //{
                //    await applicationDbContext.Contents.AddAsync(new Content() { Title = "Test" });
                //    await applicationDbContext.SaveChangesAsync();
                //}




                //if (!applicationDbContext.Menus.Any())
                //{
                //    string filePash = "../OIE.Api/wwwroot/static-contents/thaiequity/th/";
                //    foreach (var file in Directory.GetFiles(filePash,"*",SearchOption.AllDirectories))
                //    {
                //        var slug = file.Split('/')[file.Split('/').Length - 1].Replace(".html", "");
                //        StreamReader sr = new StreamReader(file, System.Text.Encoding.UTF8);
                //        string html = sr.ReadToEnd();
                //        sr.Close();
                //        var page = new Page()
                //        {
                //            Language = ApplicationCore.Enums.ContentLanguage.Thai,
                //            Module = ApplicationCore.Enums.ContentModule.ThaiEquity,
                //            Slug = slug,
                //            Html = html
                //        };

                //        var menu = new Menu()
                //        {
                //            Language = ApplicationCore.Enums.ContentLanguage.Thai,
                //            Module = ApplicationCore.Enums.ContentModule.ThaiEquity,
                //            Slug = slug, Title = slug
                //        };

                //        await applicationDbContext.Pages.AddAsync(page);
                //        await applicationDbContext.Menus.AddAsync(menu);
                //    }
                //    /*
                //     * file in folder
                //     * new Page(){ html = file
                //     */
                //    //applicationDbContext.Pages.AddAsync()
                //    //applicationDbContext.Pages.AddRange(GetPages());
                //    //applicationDbContext.News.AddRange(GetNews());
                //    //applicationDbContext.Menus.AddRange(GetMenus());
                //    applicationDbContext.Carousels.AddRange(GetCarousel());
                //    await applicationDbContext.SaveChangesAsync();
                //}

            }
            catch (Exception ex)
            {
                var log = loggerFactory.CreateLogger<ApplicationDbSeed>();
                log.LogError(ex.Message);
            }
        }
        //private IEnumerable<Carousel> GetCarousel()
        //{
        //    return new List<Carousel>() { };
        //    //{
        //    //    new Carousel(){ Module = ApplicationCore.Enums.ContentModule.ThaiEquity,Language=ApplicationCore.Enums.ContentLanguage.Thai,Picture="Link",Content="<H1>Hello this is Carousel content th</H1>"},
        //    //    new Carousel(){ Module = ApplicationCore.Enums.ContentModule.FrontPage,Language=ApplicationCore.Enums.ContentLanguage.English,Picture="Link",Content="<H1>Hello this is Carousel content en</H1>"},
        //    //};
        //}
        //private IEnumerable<Content> GetContent()
        //{
        //    return new List<Content>() { };
        //    //{
        //    //    new Carousel(){ Module = ApplicationCore.Enums.ContentModule.ThaiEquity,Language=ApplicationCore.Enums.ContentLanguage.Thai,Picture="Link",Content="<H1>Hello this is Carousel content th</H1>"},
        //    //    new Carousel(){ Module = ApplicationCore.Enums.ContentModule.FrontPage,Language=ApplicationCore.Enums.ContentLanguage.English,Picture="Link",Content="<H1>Hello this is Carousel content en</H1>"},
        //    //};
        //}
    }
}
