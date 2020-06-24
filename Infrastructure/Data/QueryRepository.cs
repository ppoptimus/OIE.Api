using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ViewModels;
using System.Collections.Generic;
using Dapper;
using System.Data.SqlClient;
using ApplicationCore;
using ApplicationCore.Dtos;
using System;
using Microsoft.EntityFrameworkCore;
using static ApplicationCore.ViewModels.PMIModel;

namespace Infrastructure.Data
{
    public class QueryRepository : EfRepository<Document>, IQueryRepository
    {
        int CommandTimeout = 300;
        public QueryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }


        //         public async Task<ChartDataModel> GetSelfAssessmentReport(int userId, int year)
        //         {
        //             //  using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
        //             // {
        //             //     return Dapper.SqlMapper.Query<TestSPModel>(dbConnection, "stp_assessment_report @username @year", new { parm_test = 1 }, commandTimeout: this.CommandTimeout).ToList();
        //             // }

        //             var thailand = new DataModel()
        //             {
        //                 Data = new double[] { 7.48, 5.15, 4.94, 0.22, 3.74, 3.07, 6.67, 0.42, 8.40, 0.94, 2.43, 9.93, 9.50, 0.96, 9.37, 1 },
        //                 Label = "ค่าเฉลี่ย",
        //             };
        //             var indonesia = new DataModel()
        //             {
        //                 Data = new double[] { 9.81, 5.44, 4.08, 6.66, 4.03, 0.89, 0.86, 0.04, 5.33, 2.97, 7.38, 4.16, 0.97, 1.47, 9.10, 1 },
        //                 Label = "ผู้ประกอบการประเมินตนเอง",
        //             };

        //             return new ChartDataModel()
        //             {
        //                 Data = new DataModel[] { thailand, indonesia },
        //                 Labels = new string[]
        //                 {
        // "สภาพแวดล้อมทางสังคมและเศรษฐกิจมหภาค (ในและต่างประเทศ)",
        // "ประสิทธิภาพและการดำเนินนโยบายภาครัฐ",
        // "ระบบขนส่งและโครงสร้างพื้นฐาน",
        // "ระบบการศึกษา",
        // "ปัจจัยแรงงาน: ปริมาณและคุณภาพ",
        // "ศักยภาพของเครื่องจักร",
        // "อำนาจต่อรองกับผู้ขาย",
        // "การลงทุนด้านวิจัยและพัฒนา",
        // "กลยุทธ์ในภาพรวม",
        // "ผลิตภาพการผลิต",
        // "การผลิตที่เป็นมิตรต่อสิ่งแวดล้อม",
        // "การบริหารจัดการองค์กร",
        // "ปริมาณการจำหน่าย (ในและต่างประเทศ)",
        // "การทำกำไร",
        // "อำนาจต่อรองกับลูกค้า ",
        // "แนวโน้มในอนาคต"
        //                 }
        //             };
        //         }
        //public Page GetByIdWithItems(int id)
        //{
        //    return null;
        //    //return _dbContext.Pages
        //    //    //.Include(o => o.OrderItems)
        //    //    //.Include("OrderItems.ItemOrdered")
        //    //    .FirstOrDefault();
        //}

        public async Task<IList<AssessmentScoreModel>> GetSelfAssessmentReport(int userId, int year)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<AssessmentScoreModel>(dbConnection, "stp_assessment_report @userId, @year",
                    new { userId = userId, year = year }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<AssessmentScoreModel>> GetSelfAssessmentReport4(int userId, int year)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<AssessmentScoreModel>(dbConnection, "stp_assessment_report_4 @userId, @year",
                    new { userId = userId, year = year }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<PMIScoreModel>> GetPMIReport(int year, int month, string resultType)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<PMIScoreModel>(dbConnection, "stp_pmi_report @year, @month, @resultType",
                    new { year = year, month = month, resultType = resultType }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<IndexScoreModel>> GetRadarReportAsync(int year, int industry, int country)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndexScoreModel>(dbConnection, "get_pillar_index_by_industry_and_country @year, @industry, @country",
                    new { year = year, industry = industry, country = country }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<CountryCompareIndicatorScoreModel>> GetCountryCompareIndicatorScore(int year, int industry, int country)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<CountryCompareIndicatorScoreModel>(dbConnection, "get_country_compare_indicator_score @year, @industry, @country",
                    new { year = year, industry = industry, country = country }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<IndexScoreModel>> GetFactorsDataAsync(int year, int industry, int country)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndexScoreModel>(dbConnection, "get_factors_data @year, @industry, @country",
                    new { year = year, industry = industry, country = country }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<IndustryReviewModel>> GetListIndustryReview()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndustryReviewModel>(dbConnection, "get_industry_review_list", commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IndustryReviewModel> GetIndustryReview(int year, int industryId, int countryId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndustryReviewModel>(dbConnection, "get_industry_review @year, @industryId, @countryId",
                    new { year = year, industryId = industryId, countryId = countryId }).FirstOrDefault();
            }
        }
        //Export Master
        public async Task<IList<V_M_Industry>> Get_V_M_Industry()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_M_Industry>(dbConnection, "stp_get_exportdata @VIEWNAME", new { viewname = "V_M_Industry" }, commandTimeout: this.CommandTimeout).ToList();

            }
        }

        public async Task<IList<V_M_Country>> Get_V_M_Country()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_M_Country>(dbConnection, "stp_get_exportdata @VIEWNAME", new { viewname = "V_M_Country" }, commandTimeout: this.CommandTimeout).ToList();

            }
        }

        public async Task<IList<V_M_Company>> Get_V_M_Company()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_M_Company>(dbConnection, "stp_get_exportdata @VIEWNAME", new { viewname = "V_M_Company" }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<V_M_Index>> Get_V_M_Index()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_M_Index>(dbConnection, "stp_get_exportdata @VIEWNAME", new { viewname = "V_M_Index" }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<V_M_Indicator>> Get_V_M_Indicator()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_M_Indicator>(dbConnection, "stp_get_exportdata @VIEWNAME", new { viewname = "V_M_Indicator" }, commandTimeout: this.CommandTimeout).ToList();
            }
        }
        public async Task<IList<V_M_COMPETITIVENESS_INDEX>> Get_V_M_COMPETITIVENESS_INDEX()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_M_COMPETITIVENESS_INDEX>(dbConnection, "stp_get_exportdata @VIEWNAME", new { viewname = "V_M_COMPETITIVENESS_INDEX" }, commandTimeout: this.CommandTimeout).ToList();

            }
        }

        public async Task<IList<V_M_Dimension_Indicator>> Get_V_M_Dimension_Indicator()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_M_Dimension_Indicator>(dbConnection, "stp_get_exportdata @VIEWNAME", new { viewname = "V_M_Dimension_Indicator" }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<V_M_Pmi_Indicator>> Get_V_M_Pmi_Indicator()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_M_Pmi_Indicator>(dbConnection, "stp_get_exportdata @VIEWNAME", new { viewname = "V_M_Pmi_Indicator" }, commandTimeout: this.CommandTimeout).ToList();
            }
        }
        public async Task<IList<V_M_Professional_Weight_4>> Get_V_M_Professional_Weight_4()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_M_Professional_Weight_4>(dbConnection, "stp_get_exportdata @VIEWNAME", new { viewname = "V_M_Professional_Weight_4" }, commandTimeout: this.CommandTimeout).ToList();
            }
        }
        public async Task<IList<V_M_MasterWeight_4>> Get_V_M_MasterWeight_4()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_M_MasterWeight_4>(dbConnection, "stp_get_exportdata @VIEWNAME", new { viewname = "V_M_MasterWeight_4" }, commandTimeout: this.CommandTimeout).ToList();
            }
        }
        public async Task<IList<V_M_Pmi_Industry>> Get_V_M_Pmi_Industry()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_M_Pmi_Industry>(dbConnection, "stp_get_exportdata @VIEWNAME", new { viewname = "V_M_Pmi_Industry" }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<PillarScoreByIndustryAndCountry>> GetPillarScoreGroupByIndustryAndCountry(int year, int pillar)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<PillarScoreByIndustryAndCountry>(dbConnection, "stp_get_pillar_score_group_by_industry_country @year, @pillar",
                    new { year = year, pillar = pillar }, commandTimeout: this.CommandTimeout).ToList();
            }
        }
        //Export Data
        public async Task<IList<V_D_SURVEY>> Get_V_D_SURVEY(int year)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_D_SURVEY>(dbConnection, "stp_get_exportdata @VIEWNAME, @YEAR", new { viewname = "V_D_SURVEY", year = year }, commandTimeout: this.CommandTimeout).ToList();
            }
        }
        public async Task<IList<V_D_PRIMARY>> Get_V_D_PRIMARY(int year)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_D_PRIMARY>(dbConnection, "stp_get_exportdata @VIEWNAME, @YEAR", new { viewname = "V_D_PRIMARY", year = year }, commandTimeout: this.CommandTimeout).ToList();
            }
        }
        public async Task<IList<V_D_SECONDARY>> Get_V_D_SECONDARY(int year)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_D_SECONDARY>(dbConnection, "stp_get_exportdata @VIEWNAME, @YEAR", new { viewname = "V_D_SECONDARY", year = year }, commandTimeout: this.CommandTimeout).ToList();
            }
        }
        public async Task<IList<V_D_ASSESSEMENT>> Get_V_D_ASSESSEMENT(int year)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_D_ASSESSEMENT>(dbConnection, "stp_get_exportdata @VIEWNAME, @YEAR", new { viewname = "V_D_ASSESSEMENT", year = year }, commandTimeout: this.CommandTimeout).ToList();
            }
        }
        public async Task<IList<V_D_SURVEY_RECOMEND>> Get_V_D_SURVEY_RECOMEND(int year)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_D_SURVEY_RECOMEND>(dbConnection, "stp_get_exportdata @VIEWNAME, @YEAR", new { viewname = "V_D_SURVEY_RECOMEND", year = year }, commandTimeout: this.CommandTimeout).ToList();
            }
        }
        public async Task<IList<V_D_SECONDARY_THONLY>> Get_V_D_SECONDARY_THONLY(int year)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_D_SECONDARY_THONLY>(dbConnection, "stp_get_exportdata @VIEWNAME , @YEAR", new { viewname = "V_D_SECONDARY_THONLY" , year = year }, commandTimeout: this.CommandTimeout).ToList();

            }
        }
        public async Task<IList<V_D_SECONDARY_THONLY>> Get_V_D_SECONDARY_THONLY_N(int year)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_D_SECONDARY_THONLY>(dbConnection, "stp_get_exportdata @VIEWNAME , @YEAR", new { viewname = "V_D_SECONDARY_THONLY_N", year = year }, commandTimeout: this.CommandTimeout).ToList();

            }
        }
        public async Task<IList<V_D_PMI_PART1>> Get_V_D_PMI_PART1(int year,int month)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_D_PMI_PART1>(dbConnection, "stp_get_exportdata @VIEWNAME, @YEAR, NULL, @MONTH", new { viewname = "V_D_PMI_PART1", year = year ,  month = month }, commandTimeout: this.CommandTimeout).ToList();
            }
        }
        public async Task<IList<V_D_PMI_PART2>> Get_V_D_PMI_PART2(int year, int month)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_D_PMI_PART2>(dbConnection, "stp_get_exportdata @VIEWNAME, @YEAR ,NULL,  @MONTH", new { viewname = "V_D_PMI_PART2", year = year , month = month }, commandTimeout: this.CommandTimeout).ToList();
            }
        }
        public async Task<IList<V_D_PRIMARY_DATA_4>> Get_V_D_PRIMARY_DATA_4(int year)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_D_PRIMARY_DATA_4>(dbConnection, "stp_get_exportdata @VIEWNAME, @YEAR", new { viewname = "V_D_PRIMARY_DATA_4", year = year }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        //Export  Result
        public async Task<IList<V_R_INDUSTRY_INDICATOR>> Get_V_R_INDUSTRY_INDICATOR(int year, int version)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_R_INDUSTRY_INDICATOR>(dbConnection, "stp_get_exportdata @VIEWNAME, @YEAR, @VERSION", new { viewname = "V_R_INDUSTRY_INDICATOR", year = year, version = version }, commandTimeout: this.CommandTimeout).ToList();
            }
        }
        public async Task<IList<V_R_ASSESSMENT_BASE>> Get_V_R_ASSESSMENT_BASE(int year, int version)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_R_ASSESSMENT_BASE>(dbConnection, "stp_get_exportdata @VIEWNAME, @YEAR, @VERSION", new { viewname = "V_R_ASSESSMENT_BASE", year = year, version = version }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<V_R_TH_5SW>> Get_V_R_TH_5SW(int year, int version)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_R_TH_5SW>(dbConnection, "stp_get_exportdata @VIEWNAME, @YEAR, @VERSION", new { viewname = "V_R_TH_5SW", year = year, version = version }, commandTimeout: this.CommandTimeout).ToList();
            }
        }
        public async Task<IList<V_R_COMPETITIVENESS_INDEX>> Get_V_R_COMPETITIVENESS_INDEX(int year, int version)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_R_COMPETITIVENESS_INDEX>(dbConnection, "stp_get_exportdata @VIEWNAME, @YEAR, @VERSION", new { viewname = "V_R_COMPETITIVENESS_INDEX", year = year, version = version }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<V_R_RESULT_4>> Get_V_R_RESULT_4(int year, int version)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_R_RESULT_4>(dbConnection, "stp_get_exportdata @VIEWNAME, @YEAR, @VERSION", new { viewname = "V_R_RESULT_4", year = year, version = version }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<V_R_PMI_RESULT>> Get_V_R_PMI_RESULT(int year, int month)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<V_R_PMI_RESULT>(dbConnection, "stp_get_exportdata @VIEWNAME, @YEAR ,NULL,  @MONTH", new { viewname = "V_R_PMI_RESULT", year = year, month = month }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<TransIndiGraphModel>> GetTransIndicatorGraph(int industry)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<TransIndiGraphModel>(dbConnection, "stp_get_trans_indi_graph @industry ",
                    new { industry = industry }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<ListViewModel>> GetPillarIndex()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<ListViewModel>(dbConnection, "select Index_ID ID, Index_Name Name, Index_Name NameThai  from Tbl_Index (nolock) where Index_Level = 3 order by Hierarchy_Code", commandTimeout: this.CommandTimeout).ToList();
            }
        }


        public async Task<IList<ListViewModel>> GetIndustries()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<ListViewModel>(dbConnection, "select ID, Industry_Name Name, Industry_Name_Thai NameThai from Tbl_Industry (nolock) order by case when ID = 99 then 0 else ID end", commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<ListViewModel>> GetPmiIndustries()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<ListViewModel>(dbConnection, "select ID, Industry_Name Name, Industry_Name_Thai NameThai from Tbl_PMI_Industry (nolock) order by case when ID = 99 then 0 else ID end", commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<ListViewModel>> GetCountries()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<ListViewModel>(dbConnection, "select ID, Country_Name Name, Country_Name_Thai NameThai from Tbl_Country (nolock)", commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<IndustryIndicatorModel>> GetIndustryIndicatorList()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndustryIndicatorModel>(dbConnection, @"select distinct Year, Version from Tbl_Inds_Indicator (nolock) order by Year, Version", commandTimeout: this.CommandTimeout).ToList();
            }
        }


        public async Task<IList<IndustryCountryListViewModel>> GetIndustryCountryList()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndustryCountryListViewModel>(dbConnection, @"select Industry_ID, CompetitiveType, Industry_Name , Country_ID, Country_Name, Country_Name_Thai from Tbl_Industry_Country (nolock) ic 
inner join Tbl_Country c on c.ID = ic.Country_ID
inner join Tbl_Industry i on i.ID = ic.Industry_ID
", commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<AssessmentBase>> GetAssessmentBase(int industry, int year)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<AssessmentBase>(dbConnection, "stp_get_assessment_base @industry, @year ",
                    new { industry = industry, year = year }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<IndustryIndicatorModel>> GetIndustryIndicatorModelList(int year)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndustryIndicatorModel>(dbConnection, "stp_get_industry_indicator @year", new { year = year }, commandTimeout: this.CommandTimeout).ToList();
            }
        }
        public async Task<IList<IndustryIndicatorModel>> GetIndustryIndicatorModelList4(int year)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndustryIndicatorModel>(dbConnection, "stp_get_industry_indicator_4 @year", new { year = year }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IndustryIndicatorDataCountModel> GetIndustryIndicatorDataCount(int year)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndustryIndicatorDataCountModel>(dbConnection, "stp_industry_indicator_get_for_newversion @year",
                    new { year = year }, commandTimeout: this.CommandTimeout).FirstOrDefault();
            }
        }

        public async Task<IndustryIndicatorDataCountModel> GetIndustryIndicatorDataCount4(int year)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndustryIndicatorDataCountModel>(dbConnection, "stp_industry_indicator_get_for_newversion_4 @year",
                    new { year = year }, commandTimeout: this.CommandTimeout).FirstOrDefault();
            }
        }


        public async Task<IndustryIndicatorModel> GetIndustryIndicatorCalculate(int year, int userId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndustryIndicatorModel>(dbConnection, "BuildWeightTable @year, @UserId",
                    new { year = year, userid = userId }, commandTimeout: this.CommandTimeout).FirstOrDefault();
            }
        }


        public async Task<IndustryIndicatorModel> GetIndustryIndicatorCalculate4(int year, int userId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndustryIndicatorModel>(dbConnection, "stp_industry_indicator_calculate_save_4 @year, @UserId",
                    new { year = year, userid = userId }, commandTimeout: this.CommandTimeout).FirstOrDefault();
            }
        }
        public async Task<IList<YearVersionViewModel>> GetYearVersion()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<YearVersionViewModel>(dbConnection, "stp_get_year_version", commandTimeout: this.CommandTimeout).ToList();
            }
        }


        public async Task<IndustryIndicatorModel> IndustryIndicatorUseSave(int year, int industry_Id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndustryIndicatorModel>(dbConnection, "stp_IndustryIndicatorUseSave @year, @industry_Id",
                    new { year = year, industry_Id = industry_Id }, commandTimeout: this.CommandTimeout).FirstOrDefault();
            }
        }

        public async Task<IndustryIndicatorModel> IndustryIndicatorUseSave4(int year, int iiid)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndustryIndicatorModel>(dbConnection, "stp_IndustryIndicatorUseSave4 @year, @iiid",
                    new { year = year, iiid = iiid }, commandTimeout: this.CommandTimeout).FirstOrDefault();
            }
        }

        public async Task SaveXMLPrimaryData(string XMLPrimaryData)
        {
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@XMLPrimaryData", XMLPrimaryData);

            _dbContext.Database.ExecuteSqlCommand("stp_save_primary_data_xml @XMLPrimaryData", arParams);
        }

        public async Task DeletePrimaryData(int PrimaryH_ID)
        {
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@PrimaryH_ID", PrimaryH_ID);

            _dbContext.Database.ExecuteSqlCommand("stp_delete_primary_data @PrimaryH_ID", arParams);
        }

        public async Task SaveSelfAssessment(string XMLAssessmentData, int userId, int oldSelfAssessmentId)
        {
            SqlParameter[] arParams = new SqlParameter[3];
            arParams[0] = new SqlParameter("@xml", XMLAssessmentData);
            arParams[1] = new SqlParameter("@userId", userId);
            arParams[2] = new SqlParameter("@oldSelfAssessmentId", oldSelfAssessmentId);
            

            _dbContext.Database.ExecuteSqlCommand("stp_self_assessment_save @xml, @userId, @oldSelfAssessmentId", arParams);
        }

        //public async Task DeleteAssessmentData(int AssessmentId)
        //{
        //    SqlParameter[] arParams = new SqlParameter[1];
        //    arParams[0] = new SqlParameter("@AssessmentId", AssessmentId);

        //    _dbContext.Database.ExecuteSqlCommand("stp_delete_assessment_data @AssessmentId", arParams);
        //}

        public async Task IndustryIndicatorDelete(int year, int iiid)
        {
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@year", year);
            arParams[1] = new SqlParameter("@iiid", iiid);

            _dbContext.Database.ExecuteSqlCommand("stp_industry_indicator_delete @year, @iiid", arParams);
        }
        public async Task IndustryIndicatorDelete4(int year, int iiid)
        {
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@year", year);
            arParams[1] = new SqlParameter("@iiid", iiid);

            _dbContext.Database.ExecuteSqlCommand("stp_industry_indicator_delete_4 @year, @iiid", arParams);
        }
        public async Task IndustryIndicatorSetupPrimarySave(string xml, int selectYear , int industry, string employeeNo, int userId)
        {
            SqlParameter[] arParams = new SqlParameter[5];
            arParams[0] = new SqlParameter("@userId", userId);
            arParams[1] = new SqlParameter("@selectYear", selectYear);
            arParams[2] = new SqlParameter("@industry", industry);
            arParams[3] = new SqlParameter("@employeeNo", employeeNo);
            arParams[4] = new SqlParameter("@data", xml);

            _dbContext.Database.ExecuteSqlCommand("stp_industry_indicator_setup_primary_save @userId, @selectYear, @industry, @employeeNo, @data", arParams);
        }
        public async Task IndustryIndicatorSetupPrimarySave4(string xml, int selectYear, int industry, string employeeNo, int userId)
        {
            SqlParameter[] arParams = new SqlParameter[5];
            arParams[0] = new SqlParameter("@userId", userId);
            arParams[1] = new SqlParameter("@selectYear", selectYear);
            arParams[2] = new SqlParameter("@industry", industry);
            arParams[3] = new SqlParameter("@employeeNo", employeeNo);
            arParams[4] = new SqlParameter("@data", xml);

            _dbContext.Database.ExecuteSqlCommand("stp_industry_indicator_setup_primary_save_4 @userId, @selectYear, @industry, @employeeNo, @data", arParams);
        }
        public async Task IndustryIndicatorSetupSecondarySave(string xml, int selectYear, int industry, int country, int userId)
        {
            SqlParameter[] arParams = new SqlParameter[5];
            arParams[0] = new SqlParameter("@userId", userId);
            arParams[1] = new SqlParameter("@selectYear", selectYear);
            arParams[2] = new SqlParameter("@industry", industry);
            arParams[3] = new SqlParameter("@country", country);
            arParams[4] = new SqlParameter("@data", xml);

            _dbContext.Database.ExecuteSqlCommand("stp_industry_indicator_setup_secondary_save @userId, @selectYear, @industry, @country, @data", arParams);
        }

        public async Task<IList<IndustryIndicatorDataForSetupPrimary>> GetIndustryIndicatorSetupPrimary(int year , int industryId , string employeeNo)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndustryIndicatorDataForSetupPrimary>(dbConnection, "stp_get_industry_indicator_setup_for_primary @year,@industryId,@employeeNo", new { year = year , industryId = industryId , employeeNo = employeeNo }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<IndustryIndicatorDataForSetupPrimary>> GetIndustryIndicatorSetupPrimary4(int year, int industryId, string employeeNo)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndustryIndicatorDataForSetupPrimary>(dbConnection, "stp_get_industry_indicator_setup_for_primary_4 @year,@industryId,@employeeNo", new { year = year, industryId = industryId, employeeNo = employeeNo }, commandTimeout: this.CommandTimeout).ToList();
            }
        }
        public async Task<IList<IndustryIndicatorDataForSetupSecondary>> GetIndustryIndicatorSetupSecondary(int year , int industryId , int countryId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndustryIndicatorDataForSetupSecondary>(dbConnection, "stp_get_industry_indicator_setup_for_secondary @year, @industryId , @countryId", new { year = year, industryId = industryId ,countryId = countryId }, commandTimeout: this.CommandTimeout).ToList();
            }

        }
        public async Task<IList<int>> GetSelfAssessmentYear(int user)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<int>(dbConnection, "SELECT  [Year] FROM [OIE_DB].[dbo].[Tbl_Assessment] (nolock) WHERE [Create_User] = @Create_User  ORDER BY Year", new { Create_User = user }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<int>> GetSelfAssessmentYear4(int user)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<int>(dbConnection, "SELECT DISTINCT [Year] FROM [OIE_DB].[dbo].[Tbl_PrimaryData_4] (nolock) WHERE [Create_User] = @Create_User  ORDER BY Year", new { Create_User = user }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<PMIHEAD> GETPMI(int month, int year, int userId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<PMIHEAD>(dbConnection, "stp_get_pmi @MONTH, @YEAR, @USERID",
                  new
                  {
                      MONTH = month,
                      YEAR = year,
                      USERID = userId,
                  }, commandTimeout: this.CommandTimeout).FirstOrDefault();
            }
        }
        public async Task<IList<PMIDATA>> GETPMIDATA(int month, int year, int userId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<PMIDATA>(dbConnection, "stp_get_pmidata @MONTH, @YEAR, @USERID",
                new
                {
                    MONTH = month,
                    YEAR = year,
                    USERID = userId,
                }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<string>> SaveCounter(string url, string ipaddress, string sessionid , string date)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<string>(dbConnection, "stp_web_counter_save @url , @ipaddress , @sessionid , @date", new { url = url , ipaddress = ipaddress , sessionid = sessionid , date = date}, commandTimeout: this.CommandTimeout).ToList();
            }
        }
        public async Task<IList<IndustryCountryViewModel>> GetIndustryCountry()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndustryCountryViewModel>(dbConnection, "stp_get_industry_country", commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<PMIIndicator>> GetPMIIndicator()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<PMIIndicator>(dbConnection, "stp_get_pmi_indicator", commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<PMIMonthYear>> GetPMIMonthYear()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<PMIMonthYear>(dbConnection, "stp_get_pmi_month_year", commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<PMIListForCal>> GetPMIListForCal(int month, int year , string industrySize)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<PMIListForCal>(dbConnection, "stp_get_pmi_list_for_cal @Month, @Year, @IndustrySize", new { Month = month , Year = year , IndustrySize = industrySize } , commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task PMIForCalSave(string xml, int selectedMonth, int selectedYear, string selectedIndustrySize, int userId)
        {
            SqlParameter[] arParams = new SqlParameter[5];
            arParams[0] = new SqlParameter("@userId", userId);
            arParams[1] = new SqlParameter("@selectedMonth", selectedMonth);
            arParams[2] = new SqlParameter("@selectedYear", selectedYear);
            arParams[3] = new SqlParameter("@selectedIndustrySize", selectedIndustrySize);
            arParams[4] = new SqlParameter("@data", xml);

            _dbContext.Database.ExecuteSqlCommand("stp_pmi_for_cal_save @userId, @selectedMonth, @selectedYear, @selectedIndustrySize, @data", arParams);
        }


        public async Task<IList<PMIVersionIndicator>> GetPMIVersionIndicator()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<PMIVersionIndicator>(dbConnection, "stp_get_pmi_version_indicator",  commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task PMIVersionUpdateFlag(int version , int userId)
        {
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@userId", userId);
            arParams[1] = new SqlParameter("@version", version);

            _dbContext.Database.ExecuteSqlCommand("stp_pmi_version_update_flag @userId, @version", arParams);
        }

        public async Task PMIIndicatorSetupSave(string xml, int userId)
        {
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@userId", userId);
            arParams[1] = new SqlParameter("@data", xml);

            _dbContext.Database.ExecuteSqlCommand("stp_pmi_indicator_setup_save @userId, @data", arParams);
        }
        public async Task PMICalculateSave(int month , int year, int version ,  int userId)
        {
            SqlParameter[] arParams = new SqlParameter[4];
            arParams[0] = new SqlParameter("@userId", userId);
            arParams[1] = new SqlParameter("@month", month);
            arParams[2] = new SqlParameter("@year", year);
            arParams[3] = new SqlParameter("@version", version);

            _dbContext.Database.ExecuteSqlCommand("stp_pmi_calculate_save @userId, @month, @year , @version", arParams);
        }
        public async Task<IList<PMIMonthYear>> GetPMICheckLatestMonthYear()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<PMIMonthYear>(dbConnection, "stp_pmi_check_latest_month_year", commandTimeout: this.CommandTimeout).ToList();
            }
        }
        public async Task<IList<IndustryIndicator40CoreDimensionData>> GetIndustry40Data(int year , int industryId , int companyId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndustryIndicator40CoreDimensionData>(dbConnection, "stp_get_dimension_index_data @year, @industryId, @companyId", new { year = year , industryId = industryId , companyId = companyId}, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<IndustryIndicator40DimensionData>> GetDimensionData(int year, int industryId, int companyId)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<IndustryIndicator40DimensionData>(dbConnection, "stp_get_dimension_data @year, @industryId, @companyId", new { year = year, industryId = industryId, companyId = companyId }, commandTimeout: this.CommandTimeout).ToList();
            }
        }


        public async Task SaveXMLIndustry40CoreDimension(string xmlIndustry40CoreDimension)
        {
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@xmlIndustry40CoreDimension", xmlIndustry40CoreDimension);

            _dbContext.Database.ExecuteSqlCommand("stp_save_industry_40_core_dimension_xml @xmlIndustry40CoreDimension", arParams);
        }


        public async Task SaveXMLIndustry40Dimension(string xmlIndustry40Dimension , string xmlIndustry40CoreDimension)
        {
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@xmlIndustry40Dimension", xmlIndustry40Dimension);
            arParams[1] = new SqlParameter("@xmlIndustry40CoreDimension", xmlIndustry40CoreDimension);

            _dbContext.Database.ExecuteSqlCommand("stp_save_industry_40_dimension_xml @xmlIndustry40Dimension , @xmlIndustry40CoreDimension", arParams);
        }

        
        public async Task DeleteIndustry40Data(int primaryHID)
        {
            SqlParameter[] arParams = new SqlParameter[1];
            //arParams[0] = new SqlParameter("@year", year);
            //arParams[1] = new SqlParameter("@industryId", industryId);
            //arParams[2] = new SqlParameter("@companyId", companyId);
            arParams[0] = new SqlParameter("@primaryHID", primaryHID);

            _dbContext.Database.ExecuteSqlCommand("stp_delete_industry_40_data @primaryHID", arParams);
        }


        public async Task<IList<PMIResult>> GetPMIResultList(int year)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<PMIResult>(dbConnection, "stp_get_pmi_result @Year", new { year = year }, commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task SavePMIDataUseFlag(int idToUse)
        {
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@idToUse", idToUse);

            _dbContext.Database.ExecuteSqlCommand("stp_pmi_update_data_use_flag @idToUse", arParams);
        }

        public async Task SavePMIReportIndicatorSummary(PMIReportIndicatorSummaryDataModel summaryData, int userId)
        {
            SqlParameter[] arParams = new SqlParameter[6];
            arParams[0] = new SqlParameter("@IndicatorId", summaryData.IndicatorId);
            arParams[1] = new SqlParameter("@Year", summaryData.Year);
            arParams[2] = new SqlParameter("@Month", summaryData.Month);
            arParams[3] = new SqlParameter("@ResultType", summaryData.ResultType);
            arParams[4] = new SqlParameter("@Summary", summaryData.Summary);
            arParams[5] = new SqlParameter("@UserId", userId);

            _dbContext.Database.ExecuteSqlCommand("stp_pmi_update_report_indicator_summary @IndicatorId, @Year, @Month, @ResultType, @Summary, @UserId", arParams);
        }


        public async Task<IList<DimensionIndicator40ScoreModel>> GetRadar40ReportAsync(int year, int industry)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<DimensionIndicator40ScoreModel>(dbConnection, "get_40_score @year, @industry",
                    new { year = year, industry = industry}, commandTimeout: this.CommandTimeout).ToList();
            }
        }


        public async Task<IList<ListViewModel>> GetDimensionIndicator40Index()
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<ListViewModel>(dbConnection, "select ID, Index_Name Name, Index_Name NameThai  from [Tbl_Dimension_Indicator] (nolock) where Index_Level = 2 order by ID", commandTimeout: this.CommandTimeout).ToList();
            }
        }

        public async Task<IList<Industry40IndicatorScoreModel>> GetIndustry40IndicatorScore(int year, int industry)
        {
            using (IDbConnection dbConnection = new SqlConnection(ServerUtils.ConfigInfo.ConnectionString))
            {
                return Dapper.SqlMapper.Query<Industry40IndicatorScoreModel>(dbConnection, "get_industry40_indicator_score @year, @industry",
                    new { year = year, industry = industry}, commandTimeout: this.CommandTimeout).ToList();
            }
        }
    }
}
