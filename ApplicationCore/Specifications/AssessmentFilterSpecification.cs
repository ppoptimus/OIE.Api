using ApplicationCore.Entities;
using ApplicationCore.ViewModels;
using System;

namespace ApplicationCore.Specifications
{
    public class AssessmentFilterSpecification : BaseSpecification<Assessment>
    {
        public AssessmentFilterSpecification(int year, int userId)
            : base(
                  p => 
                  (p.Year == year)
                  && (p.CreateUser == userId)
            )
        {
        }

    }
}
