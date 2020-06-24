using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.ViewModels
{
    public class SmartElectronicDataModel
    {
        public string PerProduction { get; set; }
        public string SampleProduct { get; set; }
        public string RemainCapac { get; set; }
        public string RemainCapacReason { get; set; }
        //public string VCTelecom { get; set; }
        //public string VCAero { get; set; }
        //public string VCAgro { get; set; }
        //public string VCAI { get; set; }
        //public string VCIndustrail { get; set; }
        //public string VCSSD { get; set; }
        //public string VCMed { get; set; }
        //public string VCBD { get; set; }
        //public string VCSmartHome { get; set; }
        //public string VCAutomotive { get; set; }
        public string VC { get; set; }
        public string VCOther { get; set; }
        public string ProdComponent { get; set; }
        public string ProdDevice { get; set; }
        public string ProdApp { get; set; }
        public string OEMYearPrev2 { get; set; }
        public string OEMYearPrev1 { get; set; }
        public string ODMYearPrev2 { get; set; }
        public string ODMYearPrev1 { get; set; }
        public string OBMYearPrev2 { get; set; }
        public string OBMYearPrev1 { get; set; }
        public string GRYear { get; set; }
        public string GRYearReason { get; set; }
        public string GRYearNext1 { get; set; }
        public string GRYearNext1Reason { get; set; }
        public string ExportPerRev { get; set; }
        public string ImportFlag { get; set; }
    }
}
