using ApplicationCore.Entities;
using System;
using System.Collections.Generic;

namespace ApplicationCore.ViewModels
{
    public class SelfAssessmentModel
    {
        public Company company { get; set; }
        public Assessment assessment { get; set; }
        public IList<AssessmentData> assessmentData { get; set; }
    }
}