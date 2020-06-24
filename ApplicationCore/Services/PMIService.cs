using ApplicationCore.Interfaces;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.Exceptions;
using static ApplicationCore.ViewModels.PMIModel;
using System;

namespace ApplicationCore.Services
{
    public class PMIService : IPMIService
    {
        private readonly IAppLogger<PMIService> _logger;


        private readonly IAsyncRepository<PMIHEAD> _PMIRepository;
        private readonly IAsyncRepository<PMIDATA> _PMIDataRepository;
        private readonly IQueryRepository _queryRepository;
        private readonly ICompanyService _companyService;

        private IUnitOfWork _uow;

        public PMIService(
            IUnitOfWork uow,
            ICompanyService companyService,
            IAsyncRepository<PMIHEAD> PMIRepository,
            IAsyncRepository<PMIDATA> PMIDataRepository,
            IQueryRepository queryRepository,
            IAppLogger<PMIService> logger)
        {
            _uow = uow;

            _companyService = companyService;
            _PMIRepository = PMIRepository;
            _PMIDataRepository = PMIDataRepository;
            _queryRepository = queryRepository;
            _logger = logger;
        }




        public async Task SavePMI(PMIModel PMIModel, int userId)
        {
            // if (PMIModel.UserId == 0)
            //     throw new DataNotFoundException("User not found");

            try
            {
                _uow.BeginTransaction();



                //---------- Insert PMI ----------
                PMIModel.PMIHEAD.ModifyUser = userId;

                var head = await _queryRepository.GETPMI(PMIModel.PMIHEAD.Month, PMIModel.PMIHEAD.Year, userId);
                
                
                if (PMIModel.PMIHEAD.Id != 0)
                {
                    var data = await _queryRepository.GETPMIDATA(PMIModel.PMIHEAD.Month, PMIModel.PMIHEAD.Year, userId);

                     PMIModel.PMIHEAD.CreateUser = head.CreateUser;
                     PMIModel.PMIHEAD.CreateDate = head.CreateDate;
                    await _PMIRepository.UpdateAsync(PMIModel.PMIHEAD);
                    foreach (var item in data)
                    {
                        await _PMIDataRepository.DeleteAsync(item);
                    }
                }
                else
                {

                    await _PMIRepository.AddAsync(PMIModel.PMIHEAD);

                    // 2018-12-21 BALL  ย้ายเงื่อนไขมา Update ถ้าเป็นการ Insert PMI ครั้งแรกของเดือน จากเดิม update ทุกครั้งเมื่อกดบันทึก
                    //###################### Insert Data ######################
                    var company = await _companyService.GetCompanyByUserId(userId);
                    company.ContactName = PMIModel.PMIHEAD.InformantFName;
                    company.ContactSurname = PMIModel.PMIHEAD.InformantLName;
                    company.CompanyNameTh = PMIModel.PMIHEAD.InformantCompany;
                    // 2018-12-20 BALL  เพิ่มบันทึก ตำแหน่ง , โทรศัพท์ อีเมล์ ลง Tbl_Company
                    company.ContactPosition = PMIModel.PMIHEAD.InformantPosition;
                    company.ContactEmail = PMIModel.PMIHEAD.Email;
                    company.ContactNo = PMIModel.PMIHEAD.Phone;
                    await _companyService.UpdateAsync(company);

                }

                //---------- Insert PMI DATA ----------
                foreach (PMIDATA item in PMIModel.PMIData)
                {
                    item.ModifyUser = userId;
                    item.PMI_ID = PMIModel.PMIHEAD.Id;
                    await _PMIDataRepository.AddAsync(item);
                }

                _uow.CommitTransaction();
            }
            catch (System.Exception e)
            {
                _uow.RollbackTransaction();
                throw;
            }
        }

          public async Task<PMIModel> GetDataPMI(int month,int year,int userId)
        {
            var head = await _queryRepository.GETPMI(month, year, userId);
            var data = await _queryRepository.GETPMIDATA(month, year, userId);
            return new PMIModel()
            {
                PMIHEAD =  head,
                PMIData =  data
               
            };
       }

        public async Task<IList<PMIIndicator>> GetPMIIndicator()
        {
            var data = await _queryRepository.GetPMIIndicator();
            return data;
        }

        public async Task<IList<PMIMonthYear>> GetPMIMonthYear()
        {
            var data = await _queryRepository.GetPMIMonthYear();
            return data;
        }
        public async Task<IList<PMIListForCal>> GetPMIListForCal(int month, int year , string industrySize)
        {
            var data = await _queryRepository.GetPMIListForCal(month , year , industrySize);
            return data;
        }
        
        public async Task PMIForCalSave(IList<PMIForCalSave> model, int userId, int selectedMonth, int selectedYear , string selectedIndustrySize)
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
                await _queryRepository.PMIForCalSave(XmlData, selectedMonth, selectedYear, selectedIndustrySize, userId);
                _uow.CommitTransaction();
            }
            catch
            {
                _uow.RollbackTransaction();
                throw;
            }
        }

        public async Task<IList<PMIVersionIndicator>> GetPMIVersionIndicator()
        {
            var data = await _queryRepository.GetPMIVersionIndicator();
            return data;
        }


        public async Task PMIVersionUpdateFlag(int version , int userId)
        {
            try
            {
                _uow.BeginTransaction();
                await _queryRepository.PMIVersionUpdateFlag(version , userId);
                _uow.CommitTransaction();
            }
            catch(Exception e)
            {
                _uow.RollbackTransaction();
                throw;
            }
        }
        public async Task PMIIndicatorSetupSave(IList<PMIIndicatorForSave> model, int userId)
        {
            try
            {
                string XmlData = "<ArrayOfSelectedData>";
                foreach (var item in model)
                {
                
                    if (item.Checked)
                        XmlData += $"<Selected><Id>{item.code_ID}</Id><Indicator_wg>{item.Indicator_wg}</Indicator_wg></Selected>";
                }
                XmlData += "</ArrayOfSelectedData>";

                _uow.BeginTransaction();
                await _queryRepository.PMIIndicatorSetupSave(XmlData , userId);
                _uow.CommitTransaction();
            }
            catch
            {
                _uow.RollbackTransaction();
                throw;
            }
        }

        public async Task PMICalculateSave(int month , int year , int version ,  int userId)
        {
            try
            {
                _uow.BeginTransaction();
                await _queryRepository.PMICalculateSave(month , year, version , userId);
                _uow.CommitTransaction();
            }
            catch
            {
                _uow.RollbackTransaction();
                throw;
            }
        }


        
        public async Task<IList<PMIMonthYear>> GetPMICheckLatestMonthYear()
        {
            var data = await _queryRepository.GetPMICheckLatestMonthYear();
            return data;
        }


        public async Task<IList<PMIResult>> GetPMIResultList(int year)
        {
            var data = await _queryRepository.GetPMIResultList(year);
            return data;
        }


        public async Task SavePMIDataUseFlag(int idToUse)
        {
            try
            {
                _uow.BeginTransaction();
                await _queryRepository.SavePMIDataUseFlag(idToUse);
                _uow.CommitTransaction();
            }
            catch
            {
                _uow.RollbackTransaction();
                throw;
            }
        }

        public async Task SavePMIReportIndicatorSummary(PMIReportIndicatorSummaryDataModel summaryData, int userId)
        {
            try
            {
                _uow.BeginTransaction();
                await _queryRepository.SavePMIReportIndicatorSummary(summaryData, userId);
                _uow.CommitTransaction();
            }
            catch
            {
                _uow.RollbackTransaction();
                throw;
            }
        }
    }
}
