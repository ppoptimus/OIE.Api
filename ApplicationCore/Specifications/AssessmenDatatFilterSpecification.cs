using ApplicationCore.Entities;
using ApplicationCore.ViewModels;
using System;

namespace ApplicationCore.Specifications
{
    public class AssessmentDataFilterSpecification : BaseSpecification<AssessmentData>
    {
        public AssessmentDataFilterSpecification(int assessmentId)
            : base(
                  p =>  
                  (p.AssessmentId == assessmentId)
            )
        {
        }

    }
}
