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
using System.Data;
using System.Data.SqlClient;
using System;

namespace ApplicationCore.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly IAppLogger<SurveyService> _logger;

        private readonly IAsyncRepository<Company> _companyRepository;
        private readonly IAsyncRepository<PrimaryData> _primaryDataRepository;
        private readonly IAsyncRepository<PrimaryDataHead> _primaryDataHeadRepository;
        private readonly IQueryRepository _queryRepository;

        private IUnitOfWork _uow;

        public SurveyService(
            IUnitOfWork uow,
            IAsyncRepository<Company> companyRepository,
            IAsyncRepository<PrimaryData> primaryDataRepository,
            IAsyncRepository<PrimaryDataHead> primaryDataHeadRepository,
            IQueryRepository queryRepository,
            IAppLogger<SurveyService> logger)
        {
            _uow = uow;
            _companyRepository = companyRepository;
            _primaryDataRepository = primaryDataRepository;
            _primaryDataHeadRepository = primaryDataHeadRepository;
            _queryRepository = queryRepository;
            _logger = logger;
        }

        public async Task<SurveyViewModel> GetSurveyByUserIdAndYear(int userId, int Year)
        {
            Year = Year == 0 ? DateTime.Now.Year : Year;

            // - - - - - Survey - - - - -
            PrimaryDataHead primaryDataHead = (await _primaryDataHeadRepository.ListAsync(new PrimaryDataHeadFilterSpecification(userId, Year))).FirstOrDefault();
           
            // - - - - - Company - - - - -
            Company company = (await _companyRepository.ListAsync(new CompanyFilterSpecification(new Company() { CreateUser = userId }))).FirstOrDefault();
            SmartElectronicDataModel smartElectronicData = new SmartElectronicDataModel();

            if(primaryDataHead != null) { 
                smartElectronicData.PerProduction = primaryDataHead.PerProduction;
                smartElectronicData.SampleProduct = primaryDataHead.SampleProduct;
                smartElectronicData.RemainCapac = primaryDataHead.RemainCapac;
                smartElectronicData.RemainCapacReason = primaryDataHead.RemainCapacreason;
                smartElectronicData.ProdApp = primaryDataHead.ProdApp;
                smartElectronicData.ProdComponent = primaryDataHead.ProdComponent;
                smartElectronicData.ProdDevice = primaryDataHead.ProdDevice;
                string vc = "";
                vc += primaryDataHead.VCAero == null ? "": "VCAero";
                vc += primaryDataHead.VCAgro == null ? "" : "VCAgro";
                vc += primaryDataHead.VCAI == null ? "" : "VCAI";
                vc += primaryDataHead.VCAutomotive == null ? "" : "VCAutomotive";
                vc += primaryDataHead.VCBD == null ? "" : "VCBD";
                vc += primaryDataHead.VCIndustrail == null ? "" : "VCIndustrail";
                vc += primaryDataHead.VCMed == null ? "" : "VCMed";
                vc += primaryDataHead.VCSmartHome == null ? "" : "VCSmartHome";
                vc += primaryDataHead.VCSSD == null ? "" : "VCSSD";
                vc += primaryDataHead.VCTelecom == null ? "" : "VCTelecom";
                vc += primaryDataHead.VCOther == null ? "" : "VCOther";
                smartElectronicData.VC = vc;
                smartElectronicData.VCOther = primaryDataHead.VCOther;
                smartElectronicData.OBMYearPrev1 = primaryDataHead.OBMYearPrev1;
                smartElectronicData.OBMYearPrev2 = primaryDataHead.OBMYearPrev2;
                smartElectronicData.ODMYearPrev1 = primaryDataHead.ODMYearPrev1;
                smartElectronicData.ODMYearPrev2 = primaryDataHead.ODMYearPrev2;
                smartElectronicData.OEMYearPrev1 = primaryDataHead.OEMYearPrev1;
                smartElectronicData.OEMYearPrev2 = primaryDataHead.OEMYearPrev2;
                smartElectronicData.GRYear = primaryDataHead.GRYear;
                smartElectronicData.GRYearReason = primaryDataHead.GRYearReason;
                smartElectronicData.GRYearNext1 = primaryDataHead.GRYearNext1;
                smartElectronicData.GRYearNext1Reason = primaryDataHead.GRYearNext1Reason;
                smartElectronicData.ExportPerRev = primaryDataHead.ExportPerRev;
            }
            else { 
                return new SurveyViewModel()
                {
                    company = company,
                    primaryDataHead = new PrimaryDataHead() { ImportFlag = "I" },
                    primaryData = new List<PrimaryData>(),
                    smartElectronicData = smartElectronicData
                };
            }

            // - - - - - Assessment Data - - - - -
            IList<PrimaryData> primaryData = await _primaryDataRepository.ListAsync(new PrimaryDataFilterSpecification(Year, userId));

            return new SurveyViewModel()
            {
                company = company,
                primaryDataHead = primaryDataHead,
                primaryData = primaryData,
                smartElectronicData = smartElectronicData
            };

        }

        public async Task<List<int>> GetYearList(int userId)
        {
            List<int> result = new List<int>();
            IList<PrimaryDataHead> primaryDataHeadList = await _primaryDataHeadRepository.ListAsync(new PrimaryDataHeadFilterSpecification(userId));
            //ไม่เอาปีปัจจุบัน เพื่อป้องกัน ปีซ้ำ
            IList<PrimaryDataHead> filter = primaryDataHeadList.Where(o => o.Year != DateTime.Now.Year).ToList();
            result.Add(DateTime.Now.Year);
            foreach (PrimaryDataHead item in filter)
            {
                result.Add(item.Year);
            }
            result.Sort();
            return result;
        }

        public async Task SaveAsync(SurveyViewModel surveyModel, int UserId)
        {
            if(UserId == 0)
            throw new DataNotFoundException("User not found");

            try
            {
                _uow.BeginTransaction();

                //###################### Delete Data ######################
                //---------- Search PrimaryDataHead ----------                
                IList<PrimaryDataHead> primaryDataHeadList = await _primaryDataHeadRepository.ListAsync(new PrimaryDataHeadFilterSpecification(UserId, surveyModel.primaryDataHead.Year));
                PrimaryDataHead primaryDataHead = primaryDataHeadList.FirstOrDefault();
                if (primaryDataHead != null)
                {
                    await _queryRepository.DeleteIndustry40Data(primaryDataHead.Id);

                    //---------- Delete PrimaryData ----------
                    await _queryRepository.DeletePrimaryData(primaryDataHead.Id);

                    //---------- Delete PrimaryDataHead ----------
                    await _primaryDataHeadRepository.DeleteAsync(primaryDataHead);

                }



                //###################### Insert Data ######################

                //---------- Insert Compnay ----------
                surveyModel.company.ModifyUser = UserId;
                if (surveyModel.company.Id == 0)
                {
                    // 2018-07-27 [MoVarin] ค้นหา Company ของ User ที่ส่งเข้ามา เพื่อ Update
                    Company result = (await _companyRepository.ListAsync(new CompanyFilterSpecification(new Company() { CreateUser = UserId }))).FirstOrDefault();
                    if(result != null)
                    {
                        // Update
                        surveyModel.company.Id = result.Id;
                        await _companyRepository.UpdateAsync(surveyModel.company);
                    }
                    else
                    {
                        // Insert
                        await _companyRepository.AddAsync(surveyModel.company);
                    }
                }
                else
                {
                    // Update
                    await _companyRepository.UpdateAsync(surveyModel.company);
                }

                //---------- Insert PrimaryDataHead ----------
                surveyModel.primaryDataHead.Id = 0;
                surveyModel.primaryDataHead.CompanyId = surveyModel.company.Id;
                surveyModel.primaryDataHead.IndustryId = surveyModel.company.IndustryId;
                surveyModel.primaryDataHead.ModifyUser = UserId;
                surveyModel.primaryDataHead.CreateUser = UserId;

                if(surveyModel.company.IndustryId == 2) { 
                    surveyModel.primaryDataHead.PerProduction = surveyModel.smartElectronicData.PerProduction;
                    surveyModel.primaryDataHead.SampleProduct = surveyModel.smartElectronicData.SampleProduct;
                    surveyModel.primaryDataHead.RemainCapac = surveyModel.smartElectronicData.RemainCapac;
                    surveyModel.primaryDataHead.RemainCapacreason = surveyModel.smartElectronicData.RemainCapacReason;
                    surveyModel.primaryDataHead.VCAero = surveyModel.smartElectronicData.VC == "VCAero" ? "1" : null;
                    surveyModel.primaryDataHead.VCAgro = surveyModel.smartElectronicData.VC == "VCAgro" ? "1" : null;
                    surveyModel.primaryDataHead.VCAI = surveyModel.smartElectronicData.VC == "VCAI" ? "1" : null;
                    surveyModel.primaryDataHead.VCAutomotive = surveyModel.smartElectronicData.VC == "VCAutomotive" ? "1" : null;
                    surveyModel.primaryDataHead.VCBD = surveyModel.smartElectronicData.VC == "VCBD" ? "1" : null;
                    surveyModel.primaryDataHead.VCIndustrail = surveyModel.smartElectronicData.VC == "VCIndustrail" ? "1" : null;
                    surveyModel.primaryDataHead.VCMed = surveyModel.smartElectronicData.VC == "VCMed" ? "1" : null;
                    surveyModel.primaryDataHead.VCSmartHome = surveyModel.smartElectronicData.VC == "VCSmartHome" ? "1" : null;
                    surveyModel.primaryDataHead.VCSSD = surveyModel.smartElectronicData.VC == "VCSSD" ? "1" : null; 
                    surveyModel.primaryDataHead.VCTelecom = surveyModel.smartElectronicData.VC == "VCTelecom" ? "1" : null;
                    surveyModel.primaryDataHead.VCOther = surveyModel.smartElectronicData.VC == "VCOther" ? surveyModel.smartElectronicData.VCOther : null;
                    surveyModel.primaryDataHead.ProdComponent = surveyModel.smartElectronicData.ProdComponent == "true" || surveyModel.smartElectronicData.ProdComponent == "1" ? "1" : null;
                    surveyModel.primaryDataHead.ProdDevice = surveyModel.smartElectronicData.ProdDevice == "true" || surveyModel.smartElectronicData.ProdDevice == "1" ? "1" : null;
                    surveyModel.primaryDataHead.ProdApp = surveyModel.smartElectronicData.ProdApp == "true" || surveyModel.smartElectronicData.ProdApp == "1" ? "1" : null;
                    surveyModel.primaryDataHead.OEMYearPrev1 = surveyModel.smartElectronicData.OEMYearPrev1;
                    surveyModel.primaryDataHead.OEMYearPrev2 = surveyModel.smartElectronicData.OEMYearPrev2;
                    surveyModel.primaryDataHead.ODMYearPrev1 = surveyModel.smartElectronicData.ODMYearPrev1;
                    surveyModel.primaryDataHead.ODMYearPrev2 = surveyModel.smartElectronicData.ODMYearPrev2;
                    surveyModel.primaryDataHead.OBMYearPrev1 = surveyModel.smartElectronicData.OBMYearPrev1;
                    surveyModel.primaryDataHead.OBMYearPrev2 = surveyModel.smartElectronicData.OBMYearPrev2;
                    surveyModel.primaryDataHead.GRYear = surveyModel.smartElectronicData.GRYear;
                    surveyModel.primaryDataHead.GRYearReason = surveyModel.smartElectronicData.GRYearReason;
                    surveyModel.primaryDataHead.GRYearNext1 = surveyModel.smartElectronicData.GRYearNext1;
                    surveyModel.primaryDataHead.GRYearNext1Reason = surveyModel.smartElectronicData.GRYearNext1Reason;
                    surveyModel.primaryDataHead.ExportPerRev = surveyModel.smartElectronicData.ExportPerRev;
                }
                else
                {
                    surveyModel.primaryDataHead.PerProduction = null;
                    surveyModel.primaryDataHead.SampleProduct = null;
                    surveyModel.primaryDataHead.RemainCapac = null;
                    surveyModel.primaryDataHead.RemainCapacreason = null;
                    surveyModel.primaryDataHead.VCAero = null;
                    surveyModel.primaryDataHead.VCAgro = null;
                    surveyModel.primaryDataHead.VCAI = null;
                    surveyModel.primaryDataHead.VCAutomotive = null;
                    surveyModel.primaryDataHead.VCBD = null;
                    surveyModel.primaryDataHead.VCIndustrail = null;
                    surveyModel.primaryDataHead.VCMed = null;
                    surveyModel.primaryDataHead.VCSmartHome = null;
                    surveyModel.primaryDataHead.VCSSD = null;
                    surveyModel.primaryDataHead.VCTelecom = null;
                    surveyModel.primaryDataHead.VCOther = null;
                    surveyModel.primaryDataHead.ProdComponent = null;
                    surveyModel.primaryDataHead.ProdDevice = null;
                    surveyModel.primaryDataHead.ProdApp = null;
                    surveyModel.primaryDataHead.OEMYearPrev1 = null;
                    surveyModel.primaryDataHead.OEMYearPrev2 = null;
                    surveyModel.primaryDataHead.ODMYearPrev1 = null;
                    surveyModel.primaryDataHead.ODMYearPrev2 = null;
                    surveyModel.primaryDataHead.OBMYearPrev1 = null;
                    surveyModel.primaryDataHead.OBMYearPrev2 = null;
                    surveyModel.primaryDataHead.GRYear = null;
                    surveyModel.primaryDataHead.GRYearReason = null;
                    surveyModel.primaryDataHead.GRYearNext1 = null;
                    surveyModel.primaryDataHead.GRYearNext1Reason = null;
                    surveyModel.primaryDataHead.ExportPerRev = null;

                }
                await _primaryDataHeadRepository.AddAsync(surveyModel.primaryDataHead);

                //---------- Insert PrimaryDataHead Data ----------
                string xmlPrimaryData = "<ArrayOfPrimaryData>";
                foreach (PrimaryData item in surveyModel.primaryData)
                {
                    if (item.IndicatorId != 0)
                    {
                        xmlPrimaryData += "<PrimaryData>";

                        xmlPrimaryData += "<PrimaryHId>" + surveyModel.primaryDataHead.Id + "</PrimaryHId>";
                        xmlPrimaryData += "<Year>" + item.Year + "</Year>";
                        xmlPrimaryData += "<IndustryId>" + item.IndustryId + "</IndustryId>";
                        xmlPrimaryData += "<CountryId>" + item.CountryId + "</CountryId>";
                        xmlPrimaryData += "<CompanyId>" + surveyModel.company.Id + "</CompanyId>";
                        xmlPrimaryData += "<IndicatorId>" + item.IndicatorId + "</IndicatorId>";
                        xmlPrimaryData += "<Score>" + item.Score + "</Score>";
                        xmlPrimaryData += "<Remark>" + item.Remark + "</Remark>";
                        xmlPrimaryData += "<ImportFlag>" + surveyModel.primaryDataHead.ImportFlag + "</ImportFlag>";
                        xmlPrimaryData += "<DropFlag>0</DropFlag>";
                        xmlPrimaryData += "<UserId>" + UserId + "</UserId>";

                        xmlPrimaryData += "</PrimaryData>";
                    }
                }
                xmlPrimaryData += "</ArrayOfPrimaryData>";
                await _queryRepository.SaveXMLPrimaryData(xmlPrimaryData);


                //---------- Insert Industry40 Core Data ----------
                string xmlIndustry40CoreDimension = "<ArrayOfIndustry40CoreDimension>";
                string xmlIndustry40Dimension = "<ArrayOfIndustry40Dimension>";

                foreach (IndustryIndicator40CoreDimensionData item in surveyModel.industry40CoreDimension)
                {
                    // Core Dimension
                    xmlIndustry40CoreDimension += "<Industry40CoreDimensionData>";
                    xmlIndustry40CoreDimension += "<PrimaryH_ID>" + surveyModel.primaryDataHead.Id + "</PrimaryH_ID>";
                    xmlIndustry40CoreDimension += "<Year>" + surveyModel.primaryDataHead.Year + "</Year>";
                    xmlIndustry40CoreDimension += "<Industry_ID>" + surveyModel.primaryDataHead.IndustryId + "</Industry_ID>";
                    xmlIndustry40CoreDimension += "<Company_ID>" + surveyModel.primaryDataHead.CompanyId + "</Company_ID>";
                    xmlIndustry40CoreDimension += "<DM_Indi_ID>" + item.ID + "</DM_Indi_ID>";
                    xmlIndustry40CoreDimension += "<DM_Weight>" + item.DM_Weight + "</DM_Weight>";
                    xmlIndustry40CoreDimension += "<DropFlag>0</DropFlag>";
                    xmlIndustry40CoreDimension += "<UserId>" + UserId + "</UserId>";
                    xmlIndustry40CoreDimension += "</Industry40CoreDimensionData>";

                    // Dimension
                    foreach (IndustryIndicator40DimensionData dm in item.Dimension) {
                        xmlIndustry40Dimension += "<Industry40DimensionData>";
                        xmlIndustry40Dimension += "<PrimaryH_ID>" + surveyModel.primaryDataHead.Id + "</PrimaryH_ID>";
                        xmlIndustry40Dimension += "<Year>" + surveyModel.primaryDataHead.Year + "</Year>";
                        xmlIndustry40Dimension += "<Industry_ID>" + surveyModel.primaryDataHead.IndustryId + "</Industry_ID>";
                        xmlIndustry40Dimension += "<Company_ID>" + surveyModel.primaryDataHead.CompanyId + "</Company_ID>";
                        xmlIndustry40Dimension += "<DM_Indi_ID>" + dm.ID + "</DM_Indi_ID>";
                        xmlIndustry40Dimension += "<Score1>" + dm.Score1 + "</Score1>";
                        xmlIndustry40Dimension += "<Score2>" + dm.Score2 + "</Score2>";
                        xmlIndustry40Dimension += "<Score3>" + dm.Score3 + "</Score3>";
                        xmlIndustry40Dimension += "<DropFlag>0</DropFlag>";
                        xmlIndustry40Dimension += "<UserId>" + UserId + "</UserId>";
                        xmlIndustry40Dimension += "</Industry40DimensionData>";
                    }
                }


                 xmlIndustry40CoreDimension += "</ArrayOfIndustry40CoreDimension>";
                //await _queryRepository.SaveXMLIndustry40CoreDimension(xmlIndustry40CoreDimension);
                xmlIndustry40Dimension += "</ArrayOfIndustry40Dimension>";
                await _queryRepository.SaveXMLIndustry40Dimension(xmlIndustry40Dimension , xmlIndustry40CoreDimension);



                _uow.CommitTransaction();

            }
            catch (System.Exception e)
            {
                _uow.RollbackTransaction();
                throw;
            }
        }

    }
}
