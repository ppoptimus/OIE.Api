using ApplicationCore.Entities;
using ApplicationCore.ViewModels;
using System;

namespace ApplicationCore.Specifications
{
    public class PrimaryDataHeadFilterSpecification : BaseSpecification<PrimaryDataHead>
    {
        public PrimaryDataHeadFilterSpecification(int userId)
            : base(
                  p => p.CreateUser == userId
            )
        {
        }

        public PrimaryDataHeadFilterSpecification(int userId, int year)
            : base(
                  p => p.Year == year
                    && p.CreateUser == userId
            )
        {
        }

    }
}
