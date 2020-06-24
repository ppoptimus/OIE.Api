using ApplicationCore.Entities;
using ApplicationCore.ViewModels;
using System;

namespace ApplicationCore.Specifications
{
    public class CompanyFilterSpecification : BaseSpecification<Company>
    {
        public CompanyFilterSpecification(Company filter)
            : base(
                  p => p.CreateUser == filter.CreateUser
            )
        {
        }

        public CompanyFilterSpecification(int contentId)
            : base(
                  p => p.Id == contentId
            )
        {
        }

    }
}
