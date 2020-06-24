using ApplicationCore.Entities;
using ApplicationCore.ViewModels;
using System;

namespace ApplicationCore.Specifications
{
    public class PrimaryDataFilterSpecification : BaseSpecification<PrimaryData>
    {
        public PrimaryDataFilterSpecification(int year, int userId)
            : base(
                  p => p.Year == year
                    && p.CreateUser == userId
            )
        {
        }

    }
}
