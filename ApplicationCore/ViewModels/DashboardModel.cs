using System.Collections.Generic;

namespace ApplicationCore.ViewModels
{
    public class IndexViewModel : IndicatorModel
    {
        public string CompareCountry { get; set; }
        public IndexViewModel()
        {
            SubIndexItems = new List<SubIndexModel>();
        }
        public IList<SubIndexModel> SubIndexItems { get; set; }
    }

    public class SubIndexModel : IndicatorModel
    {
        public SubIndexModel()
        {
            PillarItems = new List<PillarModel>();
        }
        public string GroupType { get; set; }
        public IList<PillarModel> PillarItems { get; set; }
    }

    public class PillarModel : IndicatorModel
    {
        public PillarModel()
        {
            GroupOfIndicatorItems = new List<GroupOfIndicatorModel>();
        }
        public bool Show { get; set; }
        public int Groupid { get; set; }
        public IList<GroupOfIndicatorModel> GroupOfIndicatorItems { get; set; }
    }

    public class GroupOfIndicatorModel : IndicatorModel
    {
        public GroupOfIndicatorModel()
        {
            IndicatorItems = new List<IndicatorModel>();
        }
        public int Groupid { get; set; }
        public IList<IndicatorModel> IndicatorItems { get; set; }
    }

    public class IndicatorModel
    {
        public string Title { get; set; }
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public double Value3 { get; set; }
    }

    public class CountryCompareIndicatorScoreModel
    {
        public string Index_Name { get; set; }
        public int Index_ID { get; set; }
        public int Index_Level { get; set; }
        public int Index_Parent_ID { get; set; }
        public int GroupOfIndicatorId { get; set; }
        public int GroupOfIndicatorParentId { get; set; }
        public double Thai_Base_Score { get; set; }
        public double Compare_Base_Score { get; set; }
    }

    public class PillarScoreByIndustryAndCountry
    {
        public string CountryName { get; set; }
        public string CompetitiveType { get; set; }
        public string IndustryID { get; set; }
        public string IndustryName { get; set; }
        public string IndustryNameThai { get; set; }
        public double BaseScore { get; set; }
    }



    public class Industry40IndicatorScoreViewModel
    {
        public Industry40IndicatorScoreViewModel()
        {
            Items = new List<Industry40IndicatorScoreViewModel>();
        }
        public IList<Industry40IndicatorScoreViewModel> Items { get; set; }
        public string IndexName { get; set; }
        public double PrevScore { get; set; }
        public double CurrentScore { get; set; }
        public double FutureScore { get; set; }
    }

    /// <summary>
    /// FROM DB
    /// </summary>
    public class Industry40IndicatorScoreModel
    {
        public int IndexID { get; set; }
        public string IndexName { get; set; }
        public int IndexLevel { get; set; }
        public int IndexParentID { get; set; }
        public double PrevScore { get; set; }
        public double CurrentScore { get; set; }
        public double FutureScore { get; set; }
    }
}