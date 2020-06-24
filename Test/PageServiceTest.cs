//using ApplicationCore.Entities;
//using ApplicationCore.Interfaces;
//using ApplicationCore.Services;
//using Infrastructure.Data;
//using Infrastructure.Logging;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Threading.Tasks;

//namespace Test
//{
//    [TestClass]
//    public class UnitTest1
//    {
//        [TestMethod]
//        public async Task TestMethod1()
//        {
//            using (var context = new TestDbContextFactory().CreateDbContext(null))
//            {
//                using (var uow = new UnitOfWork(context))
//                {

//                    IAsyncRepository<Page> pageRepository = new EfRepository<Page>(context);
//                    //IAppLogger<PageService> logger = new LoggerAdapter<PageService>();
//                    PageService pS = new PageService(uow, pageRepository, null);

//                    await pS.CreateAsync(new Page()
//                    {
//                        PageTitle = "TEST1"
//                    });
//                } 
//            }


//            using (var context = new TestDbContextFactory().CreateDbContext(null))
//            {
//                using (var uow = new UnitOfWork(context))
//                {

//                    IAsyncRepository<Page> pageRepository = new EfRepository<Page>(context);
//                    //IAppLogger<PageService> logger = new LoggerAdapter<PageService>();
//                    PageService pS = new PageService(uow, pageRepository, null);

//                    await pS.CreateAsync(new Page()
//                    {
//                        PageTitle = "TEST2"
//                    });
//                }
//            }


//        }
//    }
//}
