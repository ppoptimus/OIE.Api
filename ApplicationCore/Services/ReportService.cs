using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using System.Linq;
using Ardalis.GuardClauses;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Dtos;
using ApplicationCore.ViewModels;
using System;

namespace ApplicationCore.Services
{
    public class ReportService : IReportService
    {
        private readonly IAppLogger<ReportService> _logger;

        private readonly IQueryRepository _queryRepository;
        private readonly IAsyncRepository<Country> _countryRepository;


        public ReportService(
            IQueryRepository queryRepository,
            IAsyncRepository<Country> countryRepository,
            IAppLogger<ReportService> logger
            )
        {
            _countryRepository = countryRepository;
            _queryRepository = queryRepository;
            _logger = logger;
        }

        public async Task<AppSettingViewModel> GetAppSettingAsync()
        {
            var dimensionIndicator40Index = (await _queryRepository.GetDimensionIndicator40Index());
            var pillarIndex = (await _queryRepository.GetPillarIndex());
            var countries = (await _queryRepository.GetCountries());
            var industries = (await _queryRepository.GetIndustries());
            var industryCountry = (await _queryRepository.GetIndustryCountryList());
            var industryIndicators = (await _queryRepository.GetIndustryIndicatorList());
            var pmiindustries = (await _queryRepository.GetPmiIndustries());
            var countryByIndustry = (await _queryRepository.GetIndustryCountry());
            var yearVersion = (await _queryRepository.GetYearVersion());
            List<int> year = new List<int>();
            for (int i = 2018; i <= DateTime.Now.Year; i++)
            {
                year.Add(i);
            }
            return new AppSettingViewModel()
            {
                Year = year,
                Pillar = (from pillar in pillarIndex select new ListViewModel() { ID = pillar.ID, Name = pillar.Name, NameThai = pillar.NameThai }).ToList(),
                Country = (from country in countries select new ListViewModel() { ID = country.ID, Name = country.Name, NameThai = country.NameThai }).ToList(),
                Industry = (from industry in industries select new ListViewModel() { ID = industry.ID, Name = industry.Name, NameThai = industry.NameThai }).ToList(),
                IndustryCountry = industryCountry,
                IndustryIndicator = industryIndicators,
                PmiIndustry = (from pmiindustry in pmiindustries select new ListViewModel() { ID = pmiindustry.ID, Name = pmiindustry.Name, NameThai = pmiindustry.NameThai }).ToList(),
                CountryByIndustry = countryByIndustry,
                DimensionIndicator40 = (from index in dimensionIndicator40Index select new ListViewModel() { ID = index.ID, Name = index.Name, NameThai = index.NameThai }).ToList(),
                YearVersion = yearVersion
            };
        }

        public async Task<ChartDataModel> GetWeaknesses(int year, int industry, int country)
        {
            var negativeData = await _queryRepository.GetFactorsDataAsync(year, industry, country);

            var negative = new DataModel()
            {
                Data = (from n in negativeData where n.DisplayFlag == "W" select n.Base_Score).ToList<double>(),
                Label = "Weaknesses"
            };

            return new ChartDataModel()
            {
                Data = new DataModel[] { negative },
                Labels = (from n in negativeData where n.DisplayFlag == "W" select n.Index_Name).ToArray()
            };
        }

        public async Task<ChartDataModel> GetStrengths(int year, int industry, int country)
        {
            var positiveData = await _queryRepository.GetFactorsDataAsync(year, industry, country);

            var positive = new DataModel()
            {
                Data = (from n in positiveData where n.DisplayFlag == "S" select n.Base_Score).ToList<double>(),
                Label = "Strengths"
            };

            return new ChartDataModel()
            {
                Data = new DataModel[] { positive },
                Labels = (from n in positiveData where n.DisplayFlag == "S" select n.Index_Name).ToArray()
            };
        }

        public async Task<ChartDataModel> GetRadarAsync(int year, int industry, int country)
        {
            var thailandData = await _queryRepository.GetRadarReportAsync(year, industry, (int)Enums.COUNTRY.Thailand);

            var thailand = new DataModel()
            {
                Data = (from t in thailandData select t.Base_Score).ToList<double>(),
                Label = (await _countryRepository.GetByIdAsync((int)Enums.COUNTRY.Thailand))?.CountryNameThai
            };

            var compareCountryData = await _queryRepository.GetRadarReportAsync(year, industry, country);

            var compareCountry = new DataModel()
            {
                Data = (from t in compareCountryData select t.Base_Score).ToList<double>(),
                Label = (await _countryRepository.GetByIdAsync(country))?.CountryNameThai
            };

            return new ChartDataModel()
            {
                Data = new DataModel[] { thailand, compareCountry },
                Labels = (from t in compareCountryData select t.Index_Name).ToArray()
            };
        }

        public async Task<ChartDataModel> GetSelfAssessmentReport(int userId, int year)
        {
            var tbData = await _queryRepository.GetSelfAssessmentReport(userId, year);

            Guard.Against.Null(tbData.Count, $"ไม่พบข้อมูลแบบประเมินของ {userId} ปี {year}");

            var thailandBase = new DataModel()
            {
                Data = (from t in tbData select t.BaseScore).ToList<double>(),
                Label = "ค่าเฉลี่ยอุตสาหกรรม"
            };
            var myAssessment = new DataModel()
            {
                Data = (from t in tbData select t.MyScore).ToList<double>(),
                Label = tbData[0].CompanyNameTH

            };
            var yearReport = await _queryRepository.GetSelfAssessmentYear(userId);

            return new ChartDataModel()
            {
                Data = new DataModel[] { thailandBase, myAssessment },
                Labels = (from t in tbData select t.IndexName).ToList<string>(),
                Year = yearReport
            };

            // return await _queryRepository.GetSelfAssessmentReport(userId, year);
            //            var thailand = new DataModel()
            //            {
            //                Data = new double[] {7.48,5.15,4.94,0.22,3.74,3.07,6.67,0.42,8.40,0.94,2.43,9.93,9.50,0.96,9.37, 1 },
            //                Label = "ค่าเฉลี่ย",
            //            };
            //            var indonesia = new DataModel()
            //            {
            //                Data = new double[] { 9.81, 5.44, 4.08, 6.66, 4.03, 0.89, 0.86, 0.04, 5.33, 2.97, 7.38, 4.16, 0.97, 1.47, 9.10, 1 },
            //                Label = "ผู้ประกอบการประเมินตนเอง",
            //            };

            //            return new ChartDataModel()
            //            {
            //                Data = new DataModel[] { thailand, indonesia },
            //                Labels = new string[]
            //                {
            //"สภาพแวดล้อมทางสังคมและเศรษฐกิจมหภาค (ในและต่างประเทศ)",
            //"ประสิทธิภาพและการดำเนินนโยบายภาครัฐ",
            //"ระบบขนส่งและโครงสร้างพื้นฐาน",
            //"ระบบการศึกษา",
            //"ปัจจัยแรงงาน: ปริมาณและคุณภาพ",
            //"ศักยภาพของเครื่องจักร",
            //"อำนาจต่อรองกับผู้ขาย",
            //"การลงทุนด้านวิจัยและพัฒนา",
            //"กลยุทธ์ในภาพรวม",
            //"ผลิตภาพการผลิต",
            //"การผลิตที่เป็นมิตรต่อสิ่งแวดล้อม",
            //"การบริหารจัดการองค์กร",
            //"ปริมาณการจำหน่าย (ในและต่างประเทศ)",
            //"การทำกำไร",
            //"อำนาจต่อรองกับลูกค้า ",
            //"แนวโน้มในอนาคต"
            //                }
            //            };
        }

        public async Task<ChartDataModel> GetSelfAssessmentReport4(int userId, int year)
        {
            var tbData = await _queryRepository.GetSelfAssessmentReport4(userId, year);

            Guard.Against.Null(tbData.Count, $"ไม่พบข้อมูลแบบประเมินของ {userId} ปี {year}");

            var thailandBase = new DataModel()
            {
                Data = (from t in tbData select t.BaseScore).ToList<double>(),
                Label = "ค่าเฉลี่ยอุตสาหกรรม"
            };
            var myAssessment = new DataModel()
            {
                Data = (from t in tbData select t.MyScore).ToList<double>(),
                Label = tbData[0].CompanyNameTH

            };
            var yearReport = await _queryRepository.GetSelfAssessmentYear4(userId);

            return new ChartDataModel()
            {
                Data = new DataModel[] { thailandBase, myAssessment },
                Labels = (from t in tbData select t.IndexName).ToList<string>(),
                Year = yearReport
            };

        }

        public async Task<IndexViewModel> GetCountryCompareIndicatorScore(int year, int industry, int country)
        {
            var data = await _queryRepository.GetCountryCompareIndicatorScore(year, industry, country);
            var index = data.Where(i => i.Index_Level == 1).FirstOrDefault();

            var indexModel = new IndexViewModel()
            {
                Title = index?.Index_Name,
                Value1 = 10.2,//index.Thai_Base_Score,
                Value2 = 45.44,//index.Compare_Base_Score,
                CompareCountry = (await _countryRepository.GetByIdAsync(country))?.CountryNameThai
            };

            int id = 1;
            var siItems = data.Where(i => i.Index_Parent_ID == index?.Index_ID && i.Index_Level == 2).ToList();
            foreach (var si in siItems)
            {
                var siModel = new SubIndexModel()
                {
                    Title = si.Index_Name,
                    Value1 = si.Thai_Base_Score,
                    Value2 = si.Compare_Base_Score,
                    GroupType = si.Index_Name
                };
                indexModel.SubIndexItems.Add(siModel);

                var pillarItems = data.Where(i => i.Index_Parent_ID == si.Index_ID && i.Index_Level == 3).ToList();
                foreach (var pillar in pillarItems)
                {
                    var piModel = new PillarModel()
                    {
                        Title = pillar.Index_Name,
                        Value1 = pillar.Thai_Base_Score,
                        Value2 = pillar.Compare_Base_Score,
                        Groupid = pillar.Index_ID
                    };
                    siModel.PillarItems.Add(piModel);

                    var goiItems = data.Where(i => i.Index_Parent_ID == pillar.Index_ID && i.Index_Level == 4).ToList();
                    foreach (var goi in goiItems)
                    {
                        var goiModel = new GroupOfIndicatorModel()
                        {
                            Title = goi.Index_Name,
                            Value1 = goi.Thai_Base_Score,
                            Value2 = goi.Compare_Base_Score,
                            Groupid = id++
                        };
                        piModel.GroupOfIndicatorItems.Add(goiModel);

                        var iItems = data.Where(i => i.GroupOfIndicatorParentId == goi.GroupOfIndicatorId && i.Index_Level == 5).ToList();
                        foreach (var indicator in iItems)
                        {
                            var inModel = new IndicatorModel()
                            {
                                Title = indicator.Index_Name,
                                Value1 = indicator.Thai_Base_Score,
                                Value2 = indicator.Compare_Base_Score

                            };
                            goiModel.IndicatorItems.Add(inModel);
                        }

                    }
                }
            }

            return indexModel;
        }

        public async Task<IList<IndustryGroupModel>> GetPillarScoreGroupByIndustryAndCountry(int year, int pillar)
        {
            var data = await _queryRepository.GetPillarScoreGroupByIndustryAndCountry(year, pillar);
            var industryItems = data.Where(i => i.CompetitiveType == null).ToList();

            var industryModels = new List<IndustryGroupModel>();
            int id = 1;

            foreach (var industryItem in industryItems)
            {
                var industryGroupModel = new IndustryGroupModel()
                {
                    Groupid = id++,
                    Title = industryItem.IndustryName + " (" + industryItem.CountryName + ")",
                    Value2 = industryItem.BaseScore,
                    GroupOfCountryItems = new List<GroupOfCountryModel>()
                };

                var groupOfCountryModelA = new GroupOfCountryModel()
                {
                    Groupid = id++,
                    Title = "ผู้นำ",
                    CountryIndicatorItems = new List<CountryIndicatorModel>()
                };
                var aItems = data.Where(i => i.IndustryID == industryItem.IndustryID && i.CompetitiveType == groupOfCountryModelA.Title).ToList();
                foreach (var item in aItems)
                {
                    groupOfCountryModelA.CountryIndicatorItems.Add(
                        new CountryIndicatorModel()
                        {
                            title = item.CountryName,
                            value2 = item.BaseScore
                        });
                }
                industryGroupModel.GroupOfCountryItems.Add(groupOfCountryModelA);

                var groupOfCountryModelB = new GroupOfCountryModel()
                {
                    Groupid = id++,
                    Title = "คู่แข่งที่สำคัญ",
                    CountryIndicatorItems = new List<CountryIndicatorModel>()
                };
                var bItems = data.Where(i => i.IndustryID == industryItem.IndustryID && i.CompetitiveType == groupOfCountryModelB.Title).ToList();
                foreach (var item in bItems)
                {
                    groupOfCountryModelB.CountryIndicatorItems.Add(
                        new CountryIndicatorModel()
                        {
                            title = item.CountryName,
                            value2 = item.BaseScore
                        });
                }
                industryGroupModel.GroupOfCountryItems.Add(groupOfCountryModelB);

                industryModels.Add(industryGroupModel);
            }

            return industryModels;

            //return new List<IndustryGroupModel>()
            //{
            //    new IndustryGroupModel()
            //    {
            //        Groupid = 1,
            //        Title = "อุตสาหกรรมยานยนต์สมัยใหม่",
            //        Value2 = 2.5,
            //        GroupOfCountryItems = new List<GroupOfCountryModel>()
            //        {
            //            new GroupOfCountryModel()
            //            {
            //                Groupid=11,
            //                Title = "ประเทศคู่ค้า",
            //                Value2 = 2.5,
            //                CountryIndicatorItems = new List<CountryIndicatorModel>()
            //                {
            //                    new CountryIndicatorModel()
            //                    {
            //                        title="ญี่ปุ่น",
            //                        value2=5.8
            //                    },
            //                    new CountryIndicatorModel()
            //                    {
            //                        title="ออสเตรเรีย",
            //                        value2=5.8
            //                    },
            //                    new CountryIndicatorModel()
            //                    {
            //                        title="อินโดนีเซีย",
            //                        value2=5.8
            //                    }
            //                }
            //            },
            //            new GroupOfCountryModel()
            //            {
            //                Groupid=12,
            //                Title = "ประเทศคู่แข่ง",
            //                Value2 = 2.5,
            //                CountryIndicatorItems = new List<CountryIndicatorModel>()
            //                {
            //                    new CountryIndicatorModel()
            //                    {
            //                        title="เยอรมัน",
            //                        value2=5.8
            //                    },
            //                    new CountryIndicatorModel()
            //                    {
            //                        title="มาเลเชีย",
            //                        value2=5.8
            //                    },
            //                    new CountryIndicatorModel()
            //                    {
            //                        title="จีน",
            //                        value2=5.8
            //                    }
            //                }
            //            }
            //        }
            //    },
            //    new IndustryGroupModel()
            //    {
            //        Groupid = 2,
            //        Title = "อุตสาหกรรมอิเล็กทรอนิกส์อัจฉริยะ(ไทย)",
            //        Value2 = 2.5,
            //        GroupOfCountryItems = new List<GroupOfCountryModel>()
            //        {
            //            new GroupOfCountryModel()
            //            {
            //                Groupid=11,
            //                Title = "ประเทศคู่ค้า",
            //                Value2 = 2.5,
            //                CountryIndicatorItems = new List<CountryIndicatorModel>()
            //                {
            //                    new CountryIndicatorModel()
            //                    {
            //                        title="ญี่ปุ่น",
            //                        value2=5.8
            //                    },
            //                    new CountryIndicatorModel()
            //                    {
            //                        title="ออสเตรเรีย",
            //                        value2=5.8
            //                    },
            //                    new CountryIndicatorModel()
            //                    {
            //                        title="อินโดนีเซีย",
            //                        value2=5.8
            //                    }
            //                }
            //            },
            //            new GroupOfCountryModel()
            //            {
            //                Groupid=12,
            //                Title = "ประเทศคู่แข่ง",
            //                Value2 = 2.5,
            //                CountryIndicatorItems = new List<CountryIndicatorModel>()
            //                {
            //                    new CountryIndicatorModel()
            //                    {
            //                        title="เยอรมัน",
            //                        value2=5.8
            //                    },
            //                    new CountryIndicatorModel()
            //                    {
            //                        title="มาเลเชีย",
            //                        value2=5.8
            //                    },
            //                    new CountryIndicatorModel()
            //                    {
            //                        title="จีน",
            //                        value2=5.8
            //                    }
            //                }
            //            }
            //        }
            //    },
            //    new IndustryGroupModel()
            //    {
            //        Groupid = 3,
            //        Title = "อุตสาหกรรมเกษตรและเทคโนโลยีชีวภาพ",
            //        Value2 = 2.5,
            //        GroupOfCountryItems = new List<GroupOfCountryModel>()
            //        {
            //            new GroupOfCountryModel()
            //            {
            //                Groupid=11,
            //                Title = "ประเทศคู่ค้า",
            //                Value2 = 2.5,
            //                CountryIndicatorItems = new List<CountryIndicatorModel>()
            //                {
            //                    new CountryIndicatorModel()
            //                    {
            //                        title="ญี่ปุ่น",
            //                        value2=5.8
            //                    },
            //                    new CountryIndicatorModel()
            //                    {
            //                        title="ออสเตรเรีย",
            //                        value2=5.8
            //                    },
            //                    new CountryIndicatorModel()
            //                    {
            //                        title="อินโดนีเซีย",
            //                        value2=5.8
            //                    }
            //                }
            //            },
            //            new GroupOfCountryModel()
            //            {
            //                Groupid=12,
            //                Title = "ประเทศคู่แข่ง",
            //                Value2 = 2.5,
            //                CountryIndicatorItems = new List<CountryIndicatorModel>()
            //                {
            //                    new CountryIndicatorModel()
            //                    {
            //                        title="เยอรมัน",
            //                        value2=5.8
            //                    },
            //                    new CountryIndicatorModel()
            //                    {
            //                        title="มาเลเชีย",
            //                        value2=5.8
            //                    },
            //                    new CountryIndicatorModel()
            //                    {
            //                        title="จีน",
            //                        value2=5.8
            //                    }
            //                }
            //            }
            //        }
            //    }
            //};
        }

        public async Task<PMIReportModel> GetPMIReport(int year, int month, string resultType)
        {
            DateTime currentMonth = new DateTime(year, month, 15);
            DateTime previousMonth = currentMonth.AddMonths(-1);


            var data = new PMIReportModel()
            {
                Overall = new PMIOverallChartDataModel() { },
                ByIndicator = new List<PMIChartDataModel>(),
            };

            var tbData = await _queryRepository.GetPMIReport(year, month, resultType);

            var labels = tbData
                .GroupBy(p => p.MONTH_DATE)
                .OrderBy(o => o.FirstOrDefault().MONTH_DATE)
                .Select(g => g.FirstOrDefault().MONTH_YEAR_NAME)
                .ToList();

            var overallScore = new DataModel()
            {
                Data = (from t in tbData where t.PMIIndicatorID == "99" && t.PMIIndustryID == "99" orderby t.MONTH_DATE select t.Score).ToList<double>(),
                Id = 99
            };

            data.Overall = new PMIOverallChartDataModel()
            {
                Title = "ค่าดัชนีผู้จัดการฝ่ายจัดซื้อภาคอุตสาหกรรมของไทย",
                Labels = labels,
                Summary = "1554"//(from t in tbData where t.PMIIndicatorID == "99" && t.PMIIndustryID == "99" && t.MONTH_DATE == currentMonth select t).FirstOrDefault().Summary
            };

            data.Overall.AddScore(overallScore);

            List<string> indicatorList = tbData
              .GroupBy(p => p.PMIIndicatorID)
              .Select(g => g.FirstOrDefault().PMIIndicatorID)
              .ToList();

            foreach (var indicatorId in indicatorList)
            {
                //ไม่รวมผลรวม
                if (indicatorId == "99")
                    continue;

                var byIndicator = new PMIChartDataModel()
                {
                    Title = (from t in tbData where t.PMIIndicatorID == indicatorId && t.PMIIndustryID == "99" select t.IndicatorNameThai + $"({t.IndicatorName})").FirstOrDefault(),
                    Labels = labels
                };

                var score = new DataModel()
                {
                    Data = (from t in tbData where t.PMIIndicatorID == indicatorId && t.PMIIndustryID == "99" orderby t.MONTH_DATE select t.Score).ToList<double>(),
                    Label = string.Empty,
                    Id = Convert.ToInt32(indicatorId)
                };
                byIndicator.AddScore(score);

                var indicatorSummaryData = (from t in tbData where t.PMIIndicatorID == indicatorId && t.PMIIndustryID == "99" && t.MONTH_DATE == currentMonth select t).FirstOrDefault();

                var IncreasePercent = 125.3;//Math.Round(indicatorSummaryData.IncreaseCount / (indicatorSummaryData.IncreaseCount + indicatorSummaryData.DecreaseCount + indicatorSummaryData.EqualCount) * 100, 1);
                var DecreasePercent = 114.2; // Math.Round(indicatorSummaryData.DecreaseCount / (indicatorSummaryData.IncreaseCount + indicatorSummaryData.DecreaseCount + indicatorSummaryData.EqualCount) * 100, 1);
                var EqualPercent = Math.Round(100 - IncreasePercent - DecreasePercent, 1);


                byIndicator.PieData = new DataModel() { Data = new List<double>() { IncreasePercent, EqualPercent, DecreasePercent } };
                byIndicator.PieLabels = new List<string>() { "% เพิ่มขึ้น", "% เท่าเดิม", "% ลดลง" };
                byIndicator.Summary = "1544"; //indicatorSummaryData.Summary;
                data.ByIndicator.Add(byIndicator);


                data.Overall.AddIndicatorSummary(new PMIIndicatorSummaryDataModel()
                {
                    IndicatorName = byIndicator.Title,
                    CurrentMonthValue = (from t in tbData where t.PMIIndicatorID == indicatorId && t.PMIIndustryID == "99" && t.MONTH_DATE == currentMonth select t.Score).FirstOrDefault(),
                    PreviousMonthValue = (from t in tbData where t.PMIIndicatorID == indicatorId && t.PMIIndustryID == "99" && t.MONTH_DATE == previousMonth select t.Score).FirstOrDefault()
                });
            }

            return data;
        }

        //public async Task<IList<TransIndiGraphModel>> GetTransIndicatorGraph(int year, int industry, int country)
        //{
        //    // var thailandData = await _queryRepository.GetRadarReportAsync(year, industry, (int)Enums.COUNTRY.Thailand);

        //    // var thailand = new DataModel()
        //    // {
        //    //     Data = (from t in thailandData select t.Base_Score).ToList<double>(),
        //    //     Label = (await _countryRepository.GetByIdAsync((int)Enums.COUNTRY.Thailand)).CompanyNameThai
        //    // };

        //    // var compareCountryData = await _queryRepository.GetRadarReportAsync(year, industry, country);

        //    return new ChartDataModel()
        //    {
        //        // Data = new DataModel[] { thailand },
        //        // Labels = (from t in compareCountryData select t.Index_Name).ToArray()
        //    };
        //}


        public async Task<ChartDataModel> GetRadar40Async(int year, int industry)
        {
            var data = await _queryRepository.GetRadar40ReportAsync(year, industry);

            var prev = new DataModel()
            {
                Data = (from t in data select t.PrevScore).ToList<double>(),
                Label = $"ก่อนมีนโยบาย S-curve {(year + 538).ToString()}"
            };

            var current = new DataModel()
            {
                Data = (from t in data select t.CurrentScore).ToList<double>(),
                Label = $"สถานะปัจจุบัน {(year + 543).ToString()}"
            };

            var future = new DataModel()
            {
                Data = (from t in data select t.FutureScore).ToList<double>(),
                Label = $"เป้าหมายภายใน 5 ปีข้างหน้า {(year + 548).ToString()}"
            };

            return new ChartDataModel()
            {
                Data = new DataModel[] { prev, current, future },
                Labels = (from t in data select t.Index_Name).ToArray()
            };
        }


        public async Task<Industry40IndicatorScoreViewModel> GetIndustry40IndicatorScore(int year, int industry)
        {
            var data = await _queryRepository.GetIndustry40IndicatorScore(year, industry);
            var index = data.Where(i => i.IndexLevel == 1).FirstOrDefault();

            var lv1Model = new Industry40IndicatorScoreViewModel()
            {
                IndexName = index.IndexName,
                PrevScore = index.PrevScore,
                CurrentScore = index.CurrentScore,
                FutureScore = index.FutureScore
            };

            var lv2Items = data.Where(i => i.IndexParentID == index.IndexID && i.IndexLevel == 2).ToList();
            foreach (var lv2 in lv2Items)
            {
                var lv2Model = new Industry40IndicatorScoreViewModel()
                {
                    IndexName = lv2.IndexName,
                    PrevScore = lv2.PrevScore,
                    CurrentScore = lv2.CurrentScore,
                    FutureScore = lv2.FutureScore
                };

                var lv3Items = data.Where(i => i.IndexParentID == lv2.IndexID && i.IndexLevel == 3).ToList();
                foreach (var lv3 in lv3Items)
                {
                    var lv3Model = new Industry40IndicatorScoreViewModel()
                    {
                        IndexName = lv3.IndexName,
                        PrevScore = lv3.PrevScore,
                        CurrentScore = lv3.CurrentScore,
                        FutureScore = lv3.FutureScore
                    };

                    lv2Model.Items.Add(lv3Model);

                }

                lv1Model.Items.Add(lv2Model);
            }

            return lv1Model;
        }
    }
}