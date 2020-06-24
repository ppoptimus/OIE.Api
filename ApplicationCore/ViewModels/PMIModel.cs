using ApplicationCore.Entities;
using System;
using System.Collections.Generic;

namespace ApplicationCore.ViewModels
{
    public class PMIModel
    {
        public PMIHEAD PMIHEAD { get; set; }
        public IList<PMIDATA> PMIData { get; set; }

        public class PMIMonthYear
        {
            public int Month { get; set; }
            public int Year { get; set; }
            public int Count_Pmi_For_Cal { get; set; }
            public int Count_Pmi_Result { get; set; }
        }



        public class PMIListForCal
        {
            public int Id { get; set; }
            public string FormNo { get; set; }
            public string InformantFName { get; set; }
            public string InformantLName { get; set; }
            public string InformantPosition { get; set; }
            public string InformantCompany { get; set; }
            public string IndustrySize { get; set; }
            public int PMI_Industry_ID { get; set; }
            public int Year { get; set; }
            public int Month { get; set; }
            public bool Checked { get; set; }

        }

        public class PMIForCalSave
        {
            public int ID { get; set; }
            public int Year { get; set; }
            public int Month { get; set; }
            public bool Checked { get; set; }

        }
        public class PMIIndicatorForSave
        {
            public int code_ID { get; set; }
            public decimal Indicator_wg { get; set; }
            public bool Checked { get; set; }

        }

        public class PMIVersionIndicator
        {

            public int Version { get; set; }
            public string Indicator { get; set; }
            public string Use_Flag { get; set; }
            public int Count_Indicator { get; set; }
            // V.ID , V.Version , V.PMI_Indicator_ID , I.Indicator_Name , I.Indicator_Name_Thai , V.Use_Flag

        }
    
        public class PMIResult {
            public int ID { get; set; }
            public int Year { get; set; }
            public int Month { get; set; }
            public int PMI_Industry_ID { get; set; }
            public int PMI_Indicator_ID { get; set; }
            public decimal Score { get; set; }
            public string Result_Type { get; set; }
            public bool Use_Flag { get; set; }

        }

    }
}