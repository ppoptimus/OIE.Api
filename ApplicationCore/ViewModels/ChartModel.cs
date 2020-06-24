using ApplicationCore.Dtos;
using ApplicationCore.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    public class DataModel : Entity
    {
        public IList<double> Data { get; set; }
        public string Label { get; set; }
    }

    public class ChartDataModel
    {
        public IList<DataModel> Data { get; set; }
        public IList<string> Labels { get; set; }
        public IList<int> Year { get; set; }
        public string Title { get; set; }

        public ChartDataModel()
        {
            Data = new List<DataModel>();
        }

        internal void AddScore(DataModel score)
        {
            this.Data.Add(score);
        }
    }

    public class PMIOverallChartDataModel : ChartDataModel
    {
        public IList<PMIIndicatorSummaryDataModel> IndicatorSummary { get; set; }
        public string Summary { get; set; }
        public PMIOverallChartDataModel() : base()
        {
            IndicatorSummary = new List<PMIIndicatorSummaryDataModel>();
        }

        internal void AddIndicatorSummary(PMIIndicatorSummaryDataModel indicatorSummary)
        {
            this.IndicatorSummary.Add(indicatorSummary);
        }
    }

    public class PMIReportIndicatorSummaryDataModel
    {
        public int IndicatorId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string ResultType { get; set; }
        public string Summary { get; set; }
    }

    public class PMIIndicatorSummaryDataModel 
    {
        public string IndicatorName { get; set; }
        public double CurrentMonthValue { get; set; }
        public double PreviousMonthValue { get; set; }
        public double ChangedValue
        {
            get
            {
                return Math.Round( CurrentMonthValue - PreviousMonthValue, 1);
            }
        }
        public string State
        {
            get
            {
                if (ChangedValue > 0)
                    return "เพิ่มขึ้น";

                if (ChangedValue < 0)
                    return "ลดลง";

                return "เท่าเดิม";
            }
        }
    }

    public class PMIChartDataModel : ChartDataModel
    {
        public DataModel PieData { get; set; }
        public IList<string> PieLabels { get; set; }
        public string Summary { get; set; }
        public PMIChartDataModel() : base()
        {
        }
    }

    public class AppSettingViewModel
    {
        public List<int> Year { get; set; }
        public IList<ListViewModel> Pillar { get; set; }
        public IList<ListViewModel> Country { get; set; }
        public IList<ListViewModel> Industry { get; set; }
        public IList<IndustryIndicatorModel> IndustryIndicator { get; set; }
        public IList<IndustryCountryListViewModel> IndustryCountry { get; set; }
        public IList<ListViewModel> PmiIndustry { get; set; }
        public IList<IndustryCountryViewModel> CountryByIndustry { get; set; }
        public IList<ListViewModel> DimensionIndicator40 { get; set; }
        public IList<YearVersionViewModel> YearVersion { get; set; }
    }


    public class Entity
    {
        public int Id { get; set; }
    }

    public class IndexScoreModel
    {
        public double Base_Score { get; set; }
        public string Index_Name { get; set; }

        public string DisplayFlag { get; set; }


    }

    public class IndustryReviewModel
    {
        public int Id { get; set; }

        public int Year { get; set; }

        public string IndustryName { get; set; }
        public string IndustryNameThai { get; set; }

        public string CountryName { get; set; }
        public string CountryNameThai { get; set; }

        public string HtmlInformation { get; set; }
    }

    public class AssessmentScoreModel
    {
        // public double ID { get; set; }
        // public string Year { get; set; }
        // public string Industry_ID  { get; set; }
        // public string Ass_No  { get; set; }
        public double BaseScore { get; set; }
        public double MyScore { get; set; }
        public string IndexName { get; set; }
        public string CompanyNameTH { get; set; }
    }

    public class PMIScoreModel
    {
        public string IndustryName { get; set; }
        public string IndustryNameThai { get; set; }
        public string IndicatorName { get; set; }
        public string IndicatorNameThai { get; set; }
        public string PMIIndicatorID { get; set; }
        public string PMIIndustryID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public double Score { get; set; }
        public string MONTH_YEAR_NAME { get; set; }
        public DateTime MONTH_DATE { get; set; }
        public double IncreaseCount { get; set; }
        public double DecreaseCount { get; set; }
        public double EqualCount { get; set; }
        public string Summary { get; internal set; }
    }


    public class IndustryGroupModel
    {
        public int Groupid { get; set; }
        public string Title { get; set; }
        public double Value2 { get; set; }
        public IList<GroupOfCountryModel> GroupOfCountryItems { get; set; }

    }
    public class GroupOfCountryModel
    {
        public int Groupid { get; set; }
        public string Title { get; set; }
        public double Value2 { get; set; }
        public IList<CountryIndicatorModel> CountryIndicatorItems { get; set; }
    }
    public class CountryIndicatorModel
    {
        public string title { get; set; }
        public double value2 { get; set; }
    }

    public class TransIndiGraphModel
    {
        public string Indicator_Name { get; set; }
        public int GraphPos { get; set; }
        public int Year { get; set; }
        public double Score { get; set; }
    }


    public class DimensionIndicator40ScoreModel
    {
        public string Index_Name { get; set; }

        public double PrevScore { get; set; }
        public double CurrentScore { get; set; }
        public double FutureScore { get; set; }

    }
}