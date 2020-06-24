namespace ApplicationCore.ViewModels
{
    public class ListViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string NameThai { get; set; }
    }
    public class IndustryCountryListViewModel
    {
        public int IndustryID { get; set; }
        public string CompetitiveType { get; set; }
        public string IndustryName { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string CountryNameThai { get; set; }
    }

    public class IndustryCountryViewModel
    {
        public int IndustryID { get; set; }
        public string IndustryName { get; set; }
        public string IndustryNameThai { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public string CountryNameThai { get; set; }
    }
    public class YearVersionViewModel {
        public int Year { get; set; }
        public int Version { get; set; }
        public string Type { get; set; }
    }
}