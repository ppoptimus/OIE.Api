namespace ApplicationCore.ViewModels
{
    public class IndustryIndicatorModel
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Version { get; set; }
        public int CountSurvey { get; set; }
        public int CountSecondary { get; set; }
        public double AvgIndex { get; set; }
        public bool UseFlag { get; set; }
    }
}