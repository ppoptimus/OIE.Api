using ApplicationCore.Entities;
using System;
using System.Collections.Generic;

namespace ApplicationCore.ViewModels
{
    public class SurveyViewModel
    {
        public Company company { get; set; }
        public PrimaryDataHead primaryDataHead { get; set; }
        public IList<PrimaryData> primaryData { get; set; }
        public IList<IndustryIndicator40CoreDimensionData> industry40CoreDimension { get; set; }
        public SmartElectronicDataModel smartElectronicData { get; set; }
    }
}