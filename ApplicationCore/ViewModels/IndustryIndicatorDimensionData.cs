using System.Collections.Generic;

namespace ApplicationCore.ViewModels
{


    public class IndustryIndicator40CoreDimensionData
    {
        // Dimension Index (Core Dimension)
        public int ID { get; set; }
        public string Index_Name { get; set; }
        //public int Weight_ID { get; set; }
        //public int PrimaryH_ID { get; set; }
        //public int YEAR { get; set; }
        //public int Industry_ID { get; set; }
        //public int Company_ID { get; set; }
        public double? DM_Weight { get; set; }
        //public string DropFlag { get; set; }
        public IList<IndustryIndicator40DimensionData> Dimension { get; set; }
    }

    public class IndustryIndicator40DimensionData {
        // Dimension (ตัวย่อย)
        public int ID { get; set; }
        public string Dimension_Name { get; set; }
        //public int Data_ID { get; set; }
        //public int PrimaryH_ID { get; set; }
        //public int Industry_ID { get; set; }
        //public int Company_ID { get; set; }
        public double Score1 { get; set; }
        public double Score2 { get; set; }
        public double Score3 { get; set; }
        public double DM_Weight { get; set; }
        public int Index_ID { get; set; }
        //public double? Weight_Cal { get; set; }
    }
}