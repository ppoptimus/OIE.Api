namespace ApplicationCore.ViewModels
{
    public class IndustryIndicatorDataForSetupSecondary
    {   
        public int ID { get; set; }
        public int Year { get; set; }
        public int Indicator_ID { get; set; }
        public string Indicator_Name { get; set; }
        public double? Score { get; set; }
        public int Cnt { get; set; }
        public bool Checked { get; set; }
    }
}