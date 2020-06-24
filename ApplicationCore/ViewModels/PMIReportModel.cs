using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.ViewModels
{
    public class PMIReportModel
    {
        public PMIOverallChartDataModel Overall { get; set; }
        public IList<PMIChartDataModel> ByIndicator { get; set; }

    }
}
