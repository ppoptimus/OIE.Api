using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.ViewModels;

namespace ApplicationCore.Services
{
    public class IndustryIndicatorService : IIndustryIndicatorService
    {
        private readonly IQueryRepository _queryRepository;
        private readonly IUnitOfWork _uow;
        public IndustryIndicatorService(IQueryRepository queryRepository, IUnitOfWork uow)
        {
            _uow = uow;
            _queryRepository = queryRepository;
        }
        public async Task<IList<IndustryIndicatorModel>> GetIndustryIndicator(int year)
        {
            var industryIndicators = (await _queryRepository.GetIndustryIndicatorModelList(year));
            foreach (var ii in industryIndicators)
            {
                var resurt = new List<IndustryIndicatorModel>()
                {
                    new IndustryIndicatorModel(){
                        Id = ii.Id,
                        Year = ii.Year,
                        Version = ii.Version,
                        CountSurvey = ii.CountSurvey,
                        CountSecondary = ii.CountSecondary,
                        AvgIndex = ii.AvgIndex,
                        UseFlag = ii.UseFlag
                    }
                };
            }
            return industryIndicators;
        }

        public async Task<IList<IndustryIndicatorModel>> GetIndustryIndicator4(int year)
        {
            var industryIndicators = (await _queryRepository.GetIndustryIndicatorModelList4(year));
            foreach (var ii in industryIndicators)
            {
                var resurt = new List<IndustryIndicatorModel>()
                {
                    new IndustryIndicatorModel(){
                        Id = ii.Id,
                        Year = ii.Year,
                        Version = ii.Version,
                        CountSurvey = ii.CountSurvey,
                        AvgIndex = ii.AvgIndex,
                        UseFlag = ii.UseFlag
                    }
                };
            }
            return industryIndicators;
        }

        public async Task<IndustryIndicatorDataCountModel> GetIndustryIndicatorDataCount(int year)
        {
            var data = await _queryRepository.GetIndustryIndicatorDataCount(year);
            return new IndustryIndicatorDataCountModel()
            {
                Year = year,
                NextVersion = data.NextVersion,
                CntPri = data.CntSec,
                CntSec = data.CntPri,
            };
        }
        public async Task<IndustryIndicatorDataCountModel> GetIndustryIndicatorDataCount4(int year)
        {
            var data = await _queryRepository.GetIndustryIndicatorDataCount4(year);
            return new IndustryIndicatorDataCountModel()
            {
                Year = year,
                NextVersion = data.NextVersion,
                CntPri = data.CntSec,
                CntSec = data.CntPri,
            };
        }
        public async Task<IList<IndustryIndicatorDataForSetupPrimary>> GetIndustryIndicatorForPrimary(int year , int industryId , string employeeNo)
        {
            var data = (await _queryRepository.GetIndustryIndicatorSetupPrimary(year , industryId , employeeNo));
            foreach (var ii in data)
            {
                var resurt = new List<IndustryIndicatorDataForSetupPrimary>()
                {
                    new IndustryIndicatorDataForSetupPrimary(){
                        ID = ii.ID,
                        Year=ii.Year,
                        Industry_Name = ii.Industry_Name,
                        Industry_Name_Thai=ii.Industry_Name_Thai,
                        Company_Name=ii.Company_Name,
                        Company_Name_TH=ii.Company_Name_TH,
                        Checked=ii.Checked
                    }
                };
            }
            return data;
        }

        public async Task<IList<IndustryIndicatorDataForSetupPrimary>> GetIndustryIndicatorForPrimary4(int year, int industryId, string employeeNo)
        {
            var data = (await _queryRepository.GetIndustryIndicatorSetupPrimary4(year, industryId, employeeNo));
            foreach (var ii in data)
            {
                var resurt = new List<IndustryIndicatorDataForSetupPrimary>()
                {
                    new IndustryIndicatorDataForSetupPrimary(){
                        ID = ii.ID,
                        Year=ii.Year,
                        Industry_Name = ii.Industry_Name,
                        Industry_Name_Thai=ii.Industry_Name_Thai,
                        Company_Name=ii.Company_Name,
                        Company_Name_TH=ii.Company_Name_TH,
                        Checked=ii.Checked
                    }
                };
            }
            return data;
        }
        public async Task<IList<IndustryIndicatorDataForSetupSecondary>> GetIndustryIndicatorForSecondary(int year , int industryId , int countryId)
        {
            var data = (await _queryRepository.GetIndustryIndicatorSetupSecondary(year, industryId, countryId));
            foreach (var ii in data)
            {
                var resurt = new List<IndustryIndicatorDataForSetupSecondary>()
                {
                    new IndustryIndicatorDataForSetupSecondary(){
                        ID = ii.ID,
                        Year=ii.Year,
                        Indicator_ID=ii.Indicator_ID ,
                        Indicator_Name=ii.Indicator_Name ,
                        Score = ii.Score,
                        Cnt=ii.Cnt,
                        Checked=ii.Checked
                    }
                };
            }
            return data;
        }

        public async Task IndustryIndicatorCalculate(int year, int userId)
        {
            try
            {
                _uow.BeginTransaction();
                await _queryRepository.GetIndustryIndicatorCalculate(year, userId);
                _uow.CommitTransaction();
            }
            catch
            {
                _uow.RollbackTransaction();
                throw;
            }
        }

        public async Task IndustryIndicatorCalculate4(int year, int userId)
        {
            try
            {
                _uow.BeginTransaction();
                await _queryRepository.GetIndustryIndicatorCalculate4(year, userId);
                _uow.CommitTransaction();
            }
            catch
            {
                _uow.RollbackTransaction();
                throw;
            }
        }

        public async Task IndustryIndicatorDelete(int year, int iiid)
        {
            try
            {
                _uow.BeginTransaction();
                await _queryRepository.IndustryIndicatorDelete(year, iiid);
                _uow.CommitTransaction();
            }
            catch
            {
                _uow.RollbackTransaction();
                throw;
            }
        }
        public async Task IndustryIndicatorDelete4(int year, int iiid)
        {
            try
            {
                _uow.BeginTransaction();
                await _queryRepository.IndustryIndicatorDelete4(year, iiid);
                _uow.CommitTransaction();
            }
            catch
            {
                _uow.RollbackTransaction();
                throw;
            }
        }
        public async Task IndustryIndicatorForPrimarySave(IList<IndustryIndicatorDataForSetupPrimary> model, int userId , int selectYear , int industry , string employeeNo)
        {
            try
            {
                int year = 0;
                string XmlData = "<ArrayOfSelectedData>";
                foreach (var item in model)
                {
                    year = item.Year;
                    if (item.Checked)
                        XmlData += $"<Selected><Id>{item.ID}</Id></Selected>";
                }
                XmlData += "</ArrayOfSelectedData>";

                _uow.BeginTransaction();
                await _queryRepository.IndustryIndicatorSetupPrimarySave(XmlData , selectYear , industry , employeeNo, userId);
                _uow.CommitTransaction();
            }
            catch
            {
                _uow.RollbackTransaction();
                throw;
            }
        }
        public async Task IndustryIndicatorForPrimarySave4(IList<IndustryIndicatorDataForSetupPrimary> model, int userId, int selectYear, int industry, string employeeNo)
        {
            try
            {
                int year = 0;
                string XmlData = "<ArrayOfSelectedData>";
                foreach (var item in model)
                {
                    year = item.Year;
                    if (item.Checked)
                        XmlData += $"<Selected><Id>{item.ID}</Id></Selected>";
                }
                XmlData += "</ArrayOfSelectedData>";

                _uow.BeginTransaction();
                await _queryRepository.IndustryIndicatorSetupPrimarySave4(XmlData, selectYear, industry, employeeNo, userId);
                _uow.CommitTransaction();
            }
            catch
            {
                _uow.RollbackTransaction();
                throw;
            }
        }
        public async Task IndustryIndicatorForSecondarySave(IList<IndustryIndicatorDataForSetupSecondary> model, int userId, int selectYear, int industry , int country)
        {
            try
            {
                string XmlData = "<ArrayOfSelectedData>";
                foreach (var item in model)
                {
                    if (item.Checked)
                        XmlData += $"<Selected><Id>{item.ID}</Id></Selected>";
                }
                XmlData += "</ArrayOfSelectedData>";

                _uow.BeginTransaction();
                await _queryRepository.IndustryIndicatorSetupSecondarySave(XmlData, selectYear , industry, country  , userId);
                _uow.CommitTransaction();
            }
            catch
            {
                _uow.RollbackTransaction();
                throw;
            }
        }

        public async Task IndustryIndicatorUseSave(int year, int iiid)
        {
            try
            {
                _uow.BeginTransaction();
                await _queryRepository.IndustryIndicatorUseSave(year, iiid);
                _uow.CommitTransaction();
            }
            catch
            {
                _uow.RollbackTransaction();
                throw;
            }
        }
        public async Task IndustryIndicatorUseSave4(int year, int iiid)
        {
            try
            {
                _uow.BeginTransaction();
                await _queryRepository.IndustryIndicatorUseSave4(year, iiid);
                _uow.CommitTransaction();
            }
            catch
            {
                _uow.RollbackTransaction();
                throw;
            }
        }
        public async Task<IList<IndustryIndicator40CoreDimensionData>> GetIndustry40Data(int year, int industryId, int companyId)
        {
            var coreDimension = await _queryRepository.GetIndustry40Data(year, industryId, companyId);
            var dimension = await _queryRepository.GetDimensionData(year, industryId, companyId);
            List<IndustryIndicator40DimensionData> xdata = new List<IndustryIndicator40DimensionData>();

            var result = new List<IndustryIndicator40CoreDimensionData>();
            foreach (var ii in coreDimension)
            {

                        var sub = (from a in dimension where a.Index_ID == ii.ID select a).ToList();
                        result.Add(new IndustryIndicator40CoreDimensionData()
                        {
                            ID = ii.ID,
                            Index_Name = ii.Index_Name,
                            //YEAR = ii.YEAR,
                            //Weight_ID = ii.Weight_ID,
                            //PrimaryH_ID = ii.PrimaryH_ID,
                            //Company_ID = ii.Company_ID,
                            //Industry_ID = ii.Industry_ID,
                            DM_Weight = ii.DM_Weight,
                            //DropFlag = ii.DropFlag,
                            Dimension = sub
                        });
            }


            return result;
        }


        public async Task<IList<IndustryIndicator40DimensionData>> GetDimensionData(int year, int industryId, int companyId)
        {
            var data = await _queryRepository.GetDimensionData(year, industryId, companyId);
            foreach (var ii in data)
            {
                var result = new List<IndustryIndicator40DimensionData>()
                {
                    new IndustryIndicator40DimensionData(){
                        ID = ii.ID,
                        Dimension_Name = ii.Dimension_Name ,
                        Index_ID = ii.Index_ID ,
                        //Data_ID = ii.Data_ID ,
                        //PrimaryH_ID = ii.PrimaryH_ID ,
                        //Industry_ID  = ii.Industry_ID ,
                        //Company_ID = ii.Company_ID ,
                        Score1 = ii.Score1 ,
                        Score2 = ii.Score2 ,
                        Score3 = ii.Score3 
                        //Weight_Cal = ii.Weight_Cal
                    }
                };
            }
            return data;
        }
    }
}