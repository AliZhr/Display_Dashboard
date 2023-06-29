using Display_Dashboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.HttpContext;


namespace Display_Dashboard.Controllers
{
    public class HomeController : Controller
    {
        public db_dashboardContext _ctx2 = new db_dashboardContext();
        public static string langSelected;
        public static List<int> listShiftIdViewData = new List<int>();
        public static int offsetinput;
        public static string select_report_input;
        public IActionResult Index(string? lang)
        {
            langSelected = lang ?? "EN";

            ViewBag.langSelected = langSelected;
            ViewBag.ListOfReports = _ctx2.TConfigs.Where(s => (s.FValue == 12) || (s.FValue == 13) || (s.FValue == 14));

            ViewBag.Submit = _ctx2.TTranslations.Where(s => (s.FLabelid == 2) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.Number = _ctx2.TTranslations.Where(s => (s.FLabelid == 3) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.NumberPH = _ctx2.TTranslations.Where(s => (s.FLabelid == 4) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.report = _ctx2.TTranslations.Where(s => (s.FLabelid == 47) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            return View();
        }
        public IActionResult setOffset(int offset, string selected_report)
        {
            select_report_input = selected_report;
            offsetinput = offset;
            return RedirectToAction("DisplayData", new { offset = offsetinput, reportselected = select_report_input, lang = langSelected });
        }

        public IActionResult DisplayData(int offset, string reportselected, string lang)

        {


            select_report_input = reportselected ?? select_report_input;
            langSelected = lang ?? "EN";

            DateTime todayDate = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0);
            DateTime targetDate = todayDate.AddDays(offsetinput);
            int occur = 0;
            ViewBag.date = targetDate;
            ViewBag.langSelected = langSelected;

            if (select_report_input.Equals("headcount_production"))
            {
                /***********************/
                int IdAMGShift1;
                int IdAMGShift2;
                int IdAMGShift3;


                int Id40Shift1SectionA;
                int Id40Shift1SectionC;
                int Id40Shift1Prod;
                int Id40Shift2SectionA;
                int Id40Shift2SectionC;
                int Id40Shift2Prod;
                int Id40Shift3SectionA;
                int Id40Shift3SectionC;
                int Id40Shift3Prod;

                int Id41Shift1SectionA;
                int Id41Shift1SectionC;
                int Id41Shift1Prod;
                int Id41Shift2SectionA;
                int Id41Shift2SectionC;
                int Id41Shift2Prod;
                int Id41Shift3SectionA;
                int Id41Shift3SectionC;
                int Id41Shift3Prod;

                offsetinput = offset;
                ViewBag.langSelected = langSelected;
                listShiftIdViewData.Clear();
                ViewBag.DataTypes = _ctx2.TRecordtypes.ToList();
                var date1 = new DateTime();
                int validationToDisplay;

                foreach (var item in _ctx2.TShifts)
                {
                    date1 = targetDate;
                    int result = DateTime.Compare(item.FDate, date1);
                    if (result == 0)
                    {
                        listShiftIdViewData.Add(item.FShiftid);

                        /*************HALL AMG****************/

                        if ((item.FHall.Equals("Hall AMG")) && (item.FShift.Equals("Layer 1")))
                        {
                            IdAMGShift1 = item.FShiftid;
                            ViewBag.IdAMGShift1 = IdAMGShift1;

                            string sql = "EXEC dbo.Get_Data @ShiftId";
                            List<SqlParameter> parms = new List<SqlParameter>
                            {new SqlParameter { ParameterName = "@ShiftId", Value = IdAMGShift1 }};
                            List<TDatarecord> data_AMG_Shift1 = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                            foreach (var elt in data_AMG_Shift1)
                            {
                                if (elt.FRecordtypeid == 1)
                                {
                                    TDataplant temp = _ctx2.TDataplants.Where(s => (DateTime.Compare(s.FDate, date1) == 0) && (s.FTypeid == 49)).FirstOrDefault();
                                    if (temp != null)
                                    {
                                        elt.FRecordvalue = temp.FValue;
                                    }
                                }
                            }

                            ViewBag.data_AMG_Shift1 = data_AMG_Shift1;


                        }
                        if ((item.FHall.Equals("Hall AMG")) && (item.FShift.Equals("Layer 2")))
                        {
                            IdAMGShift2 = item.FShiftid;
                            ViewBag.IdAMGShift2 = IdAMGShift2;

                            string sql = "EXEC dbo.Get_Data @ShiftId";
                            List<SqlParameter> parms = new List<SqlParameter>
                            {new SqlParameter { ParameterName = "@ShiftId", Value = IdAMGShift2 }};
                            List<TDatarecord> data_AMG_Shift2 = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                            foreach (var elt in data_AMG_Shift2)
                            {
                                if (elt.FRecordtypeid == 1)
                                {
                                    TDataplant temp = _ctx2.TDataplants.Where(s => (DateTime.Compare(s.FDate, date1) == 0) && (s.FTypeid == 50)).FirstOrDefault();
                                    if (temp != null)
                                    {
                                        elt.FRecordvalue = temp.FValue;
                                    }
                                }
                            }
                            ViewBag.data_AMG_Shift2 = data_AMG_Shift2;
                        }
                        if ((item.FHall.Equals("Hall AMG")) && (item.FShift.Equals("Layer 3")))
                        {
                            IdAMGShift3 = item.FShiftid;
                            ViewBag.IdAMGShift3 = IdAMGShift3;

                            string sql = "EXEC dbo.Get_Data @ShiftId";
                            List<SqlParameter> parms = new List<SqlParameter>
                            {new SqlParameter { ParameterName = "@ShiftId", Value = IdAMGShift3 }};
                            List<TDatarecord> data_AMG_Shift3 = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                            foreach (var elt in data_AMG_Shift3)
                            {
                                if (elt.FRecordtypeid == 1)
                                {
                                    TDataplant temp = _ctx2.TDataplants.Where(s => (DateTime.Compare(s.FDate, date1) == 0) && (s.FTypeid == 51)).FirstOrDefault();
                                    if (temp != null)
                                    {
                                        elt.FRecordvalue = temp.FValue;
                                    }
                                }
                            }
                            ViewBag.data_AMG_Shift3 = data_AMG_Shift3;
                        }

                        /*************HALL 4.0****************/

                        if ((item.FHall.Equals("Hall 4.0")) && (item.FShift.Equals("Layer 1")))
                        {
                            if (item.FSection.Equals("A"))
                            {
                                Id40Shift1SectionA = item.FShiftid;
                                ViewBag.Id40Shift1SectionA = Id40Shift1SectionA;

                                string sql = "EXEC dbo.Get_Data @ShiftId";
                                List<SqlParameter> parms = new List<SqlParameter>
                                {new SqlParameter { ParameterName = "@ShiftId", Value = Id40Shift1SectionA }};
                                List<TDatarecord> data_40_Shift1_SectionA = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                                foreach (var elt in data_40_Shift1_SectionA)
                                {
                                    if (elt.FRecordtypeid == 1)
                                    {
                                        TDataplant temp = _ctx2.TDataplants.Where(s => (DateTime.Compare(s.FDate, date1) == 0) && (s.FTypeid == 33)).FirstOrDefault();
                                        if (temp != null)
                                        {
                                            elt.FRecordvalue = temp.FValue;
                                        }
                                    }
                                }
                                ViewBag.data_40_Shift1_SectionA = data_40_Shift1_SectionA;

                            }

                            if (item.FSection.Equals("C"))
                            {
                                Id40Shift1SectionC = item.FShiftid;
                                ViewBag.Id40Shift1SectionC = Id40Shift1SectionC;

                                string sql = "EXEC dbo.Get_Data @ShiftId";
                                List<SqlParameter> parms = new List<SqlParameter>
                                {new SqlParameter { ParameterName = "@ShiftId", Value = Id40Shift1SectionC }};
                                List<TDatarecord> data_40_Shift1_SectionC = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                                foreach (var elt in data_40_Shift1_SectionC)
                                {
                                    if (elt.FRecordtypeid == 1)
                                    {
                                        TDataplant temp = _ctx2.TDataplants.Where(s => (DateTime.Compare(s.FDate, date1) == 0) && (s.FTypeid == 42)).FirstOrDefault();
                                        if (temp != null)
                                        {
                                            elt.FRecordvalue = temp.FValue;
                                        }
                                    }
                                }
                                ViewBag.data_40_Shift1_SectionC = data_40_Shift1_SectionC;

                            }

                            if (item.FSection.Equals("None"))
                            {
                                Id40Shift1Prod = item.FShiftid;
                                ViewBag.Id40Shift1Prod = Id40Shift1Prod;

                                string sql = "EXEC dbo.Get_Data @ShiftId";
                                List<SqlParameter> parms = new List<SqlParameter>
                                {new SqlParameter { ParameterName = "@ShiftId", Value = Id40Shift1Prod }};
                                List<TDatarecord> data_40_Shift1_Prod = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                                ViewBag.data_40_Shift1_Prod = data_40_Shift1_Prod;

                            }


                        }
                        if ((item.FHall.Equals("Hall 4.0")) && (item.FShift.Equals("Layer 2")))
                        {

                            if (item.FSection.Equals("A"))
                            {
                                Id40Shift2SectionA = item.FShiftid;
                                ViewBag.Id40Shift2SectionA = Id40Shift2SectionA;

                                string sql = "EXEC dbo.Get_Data @ShiftId";
                                List<SqlParameter> parms = new List<SqlParameter>
                                {new SqlParameter { ParameterName = "@ShiftId", Value = Id40Shift2SectionA }};
                                List<TDatarecord> data_40_Shift2_SectionA = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                                foreach (var elt in data_40_Shift2_SectionA)
                                {
                                    if (elt.FRecordtypeid == 1)
                                    {
                                        TDataplant temp = _ctx2.TDataplants.Where(s => (DateTime.Compare(s.FDate, date1) == 0) && (s.FTypeid == 34)).FirstOrDefault();
                                        if (temp != null)
                                        {
                                            elt.FRecordvalue = temp.FValue;
                                        }
                                    }
                                }
                                ViewBag.data_40_Shift2_SectionA = data_40_Shift2_SectionA;

                            }
                            if (item.FSection.Equals("C"))
                            {
                                Id40Shift2SectionC = item.FShiftid;
                                ViewBag.Id40Shift2SectionC = Id40Shift2SectionC;

                                string sql = "EXEC dbo.Get_Data @ShiftId";
                                List<SqlParameter> parms = new List<SqlParameter>
                                {new SqlParameter { ParameterName = "@ShiftId", Value = Id40Shift2SectionC }};
                                List<TDatarecord> data_40_Shift2_SectionC = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                                foreach (var elt in data_40_Shift2_SectionC)
                                {
                                    if (elt.FRecordtypeid == 1)
                                    {
                                        TDataplant temp = _ctx2.TDataplants.Where(s => (DateTime.Compare(s.FDate, date1) == 0) && (s.FTypeid == 44)).FirstOrDefault();
                                        if (temp != null)
                                        {
                                            elt.FRecordvalue = temp.FValue;
                                        }
                                    }
                                }
                                ViewBag.data_40_Shift2_SectionC = data_40_Shift2_SectionC;

                            }

                            if (item.FSection.Equals("None"))
                            {
                                Id40Shift2Prod = item.FShiftid;
                                ViewBag.Id40Shift2Prod = Id40Shift2Prod;

                                string sql = "EXEC dbo.Get_Data @ShiftId";
                                List<SqlParameter> parms = new List<SqlParameter>
                                {new SqlParameter { ParameterName = "@ShiftId", Value = Id40Shift2Prod }};
                                List<TDatarecord> data_40_Shift2_Prod = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                                ViewBag.data_40_Shift2_Prod = data_40_Shift2_Prod;

                            }

                        }
                        if ((item.FHall.Equals("Hall 4.0")) && (item.FShift.Equals("Layer 3")))
                        {
                            if (item.FSection.Equals("A"))
                            {
                                Id40Shift3SectionA = item.FShiftid;
                                ViewBag.Id40Shift3SectionA = Id40Shift3SectionA;

                                string sql = "EXEC dbo.Get_Data @ShiftId";
                                List<SqlParameter> parms = new List<SqlParameter>
                                {new SqlParameter { ParameterName = "@ShiftId", Value = Id40Shift3SectionA }};
                                List<TDatarecord> data_40_Shift3_SectionA = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                                foreach (var elt in data_40_Shift3_SectionA)
                                {
                                    if (elt.FRecordtypeid == 1)
                                    {
                                        TDataplant temp = _ctx2.TDataplants.Where(s => (DateTime.Compare(s.FDate, date1) == 0) && (s.FTypeid == 35)).FirstOrDefault();
                                        if (temp != null)
                                        {
                                            elt.FRecordvalue = temp.FValue;
                                        }
                                    }
                                }
                                ViewBag.data_40_Shift3_SectionA = data_40_Shift3_SectionA;

                            }

                            if (item.FSection.Equals("C"))
                            {
                                Id40Shift3SectionC = item.FShiftid;
                                ViewBag.Id40Shift3SectionC = Id40Shift3SectionC;

                                string sql = "EXEC dbo.Get_Data @ShiftId";
                                List<SqlParameter> parms = new List<SqlParameter>
                                {new SqlParameter { ParameterName = "@ShiftId", Value = Id40Shift3SectionC }};
                                List<TDatarecord> data_40_Shift3_SectionC = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                                foreach (var elt in data_40_Shift3_SectionC)
                                {
                                    if (elt.FRecordtypeid == 1)
                                    {
                                        TDataplant temp = _ctx2.TDataplants.Where(s => (DateTime.Compare(s.FDate, date1) == 0) && (s.FTypeid == 45)).FirstOrDefault();
                                        if (temp != null)
                                        {
                                            elt.FRecordvalue = temp.FValue;
                                        }
                                    }
                                }
                                ViewBag.data_40_Shift3_SectionC = data_40_Shift3_SectionC;

                            }

                            if (item.FSection.Equals("None"))
                            {
                                Id40Shift3Prod = item.FShiftid;
                                ViewBag.Id40Shift3Prod = Id40Shift3Prod;

                                string sql = "EXEC dbo.Get_Data @ShiftId";
                                List<SqlParameter> parms = new List<SqlParameter>
                                {new SqlParameter { ParameterName = "@ShiftId", Value = Id40Shift3Prod }};
                                List<TDatarecord> data_40_Shift3_Prod = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                                ViewBag.data_40_Shift3_Prod = data_40_Shift3_Prod;

                            }

                        }

                        /*************HALL 4.1****************/

                        if ((item.FHall.Equals("Hall 4.1")) && (item.FShift.Equals("Layer 1")))
                        {
                            if (item.FSection.Equals("A"))
                            {
                                Id41Shift1SectionA = item.FShiftid;
                                ViewBag.Id41Shift1SectionA = Id41Shift1SectionA;

                                string sql = "EXEC dbo.Get_Data @ShiftId";
                                List<SqlParameter> parms = new List<SqlParameter>
                                {new SqlParameter { ParameterName = "@ShiftId", Value = Id41Shift1SectionA }};
                                List<TDatarecord> data_41_Shift1_SectionA = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                                foreach (var elt in data_41_Shift1_SectionA)
                                {
                                    if (elt.FRecordtypeid == 1)
                                    {
                                        TDataplant temp = _ctx2.TDataplants.Where(s => (DateTime.Compare(s.FDate, date1) == 0) && (s.FTypeid == 36)).FirstOrDefault();
                                        if (temp != null)
                                        {
                                            elt.FRecordvalue = temp.FValue;
                                        }
                                    }
                                }
                                ViewBag.data_41_Shift1_SectionA = data_41_Shift1_SectionA;

                            }
                            if (item.FSection.Equals("C"))
                            {
                                Id41Shift1SectionC = item.FShiftid;
                                ViewBag.Id41Shift1SectionC = Id41Shift1SectionC;

                                string sql = "EXEC dbo.Get_Data @ShiftId";
                                List<SqlParameter> parms = new List<SqlParameter>
                                {new SqlParameter { ParameterName = "@ShiftId", Value = Id41Shift1SectionC }};
                                List<TDatarecord> data_41_Shift1_SectionC = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                                foreach (var elt in data_41_Shift1_SectionC)
                                {
                                    if (elt.FRecordtypeid == 1)
                                    {
                                        TDataplant temp = _ctx2.TDataplants.Where(s => (DateTime.Compare(s.FDate, date1) == 0) && (s.FTypeid == 46)).FirstOrDefault();
                                        if (temp != null)
                                        {
                                            elt.FRecordvalue = temp.FValue;
                                        }
                                    }
                                }
                                ViewBag.data_41_Shift1_SectionC = data_41_Shift1_SectionC;

                            }

                            if (item.FSection.Equals("None"))
                            {
                                Id41Shift1Prod = item.FShiftid;
                                ViewBag.Id41Shift1Prod = Id41Shift1Prod;

                                string sql = "EXEC dbo.Get_Data @ShiftId";
                                List<SqlParameter> parms = new List<SqlParameter>
                                {new SqlParameter { ParameterName = "@ShiftId", Value = Id41Shift1Prod }};
                                List<TDatarecord> data_41_Shift1_Prod = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                                ViewBag.data_41_Shift1_Prod = data_41_Shift1_Prod;

                            }

                        }

                        if ((item.FHall.Equals("Hall 4.1")) && (item.FShift.Equals("Layer 2")))
                        {
                            if (item.FSection.Equals("A"))
                            {
                                Id41Shift2SectionA = item.FShiftid;
                                ViewBag.Id41Shift2SectionA = Id41Shift2SectionA;

                                string sql = "EXEC dbo.Get_Data @ShiftId";
                                List<SqlParameter> parms = new List<SqlParameter>
                                {new SqlParameter { ParameterName = "@ShiftId", Value = Id41Shift2SectionA }};
                                List<TDatarecord> data_41_Shift2_SectionA = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                                foreach (var elt in data_41_Shift2_SectionA)
                                {
                                    if (elt.FRecordtypeid == 1)
                                    {
                                        TDataplant temp = _ctx2.TDataplants.Where(s => (DateTime.Compare(s.FDate, date1) == 0) && (s.FTypeid == 37)).FirstOrDefault();
                                        if (temp != null)
                                        {
                                            elt.FRecordvalue = temp.FValue;
                                        }
                                    }
                                }
                                ViewBag.data_41_Shift2_SectionA = data_41_Shift2_SectionA;
                            }
                            if (item.FSection.Equals("C"))
                            {
                                Id41Shift2SectionC = item.FShiftid;
                                ViewBag.Id41Shift2SectionC = Id41Shift2SectionC;

                                string sql = "EXEC dbo.Get_Data @ShiftId";
                                List<SqlParameter> parms = new List<SqlParameter>
                                {new SqlParameter { ParameterName = "@ShiftId", Value = Id41Shift2SectionC }};
                                List<TDatarecord> data_41_Shift2_SectionC = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                                foreach (var elt in data_41_Shift2_SectionC)
                                {
                                    if (elt.FRecordtypeid == 1)
                                    {
                                        TDataplant temp = _ctx2.TDataplants.Where(s => (DateTime.Compare(s.FDate, date1) == 0) && (s.FTypeid == 47)).FirstOrDefault();
                                        if (temp != null)
                                        {
                                            elt.FRecordvalue = temp.FValue;
                                        }
                                    }
                                }
                                ViewBag.data_41_Shift2_SectionC = data_41_Shift2_SectionC;

                            }

                            if (item.FSection.Equals("None"))
                            {
                                Id41Shift2Prod = item.FShiftid;
                                ViewBag.Id41Shift2Prod = Id41Shift2Prod;

                                string sql = "EXEC dbo.Get_Data @ShiftId";
                                List<SqlParameter> parms = new List<SqlParameter>
                                {new SqlParameter { ParameterName = "@ShiftId", Value = Id41Shift2Prod }};
                                List<TDatarecord> data_41_Shift2_Prod = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                                ViewBag.data_41_Shift2_Prod = data_41_Shift2_Prod;

                            }

                        }

                        if ((item.FHall.Equals("Hall 4.1")) && (item.FShift.Equals("Layer 3")))
                        {
                            if (item.FSection.Equals("A"))
                            {
                                Id41Shift3SectionA = item.FShiftid;
                                ViewBag.Id41Shift3SectionA = Id41Shift3SectionA;

                                string sql = "EXEC dbo.Get_Data @ShiftId";
                                List<SqlParameter> parms = new List<SqlParameter>
                                {new SqlParameter { ParameterName = "@ShiftId", Value = Id41Shift3SectionA }};
                                List<TDatarecord> data_41_Shift3_SectionA = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                                foreach (var elt in data_41_Shift3_SectionA)
                                {
                                    if (elt.FRecordtypeid == 1)
                                    {
                                        TDataplant temp = _ctx2.TDataplants.Where(s => (DateTime.Compare(s.FDate, date1) == 0) && (s.FTypeid == 38)).FirstOrDefault();
                                        if (temp != null)
                                        {
                                            elt.FRecordvalue = temp.FValue;
                                        }
                                    }
                                }
                                ViewBag.data_41_Shift3_SectionA = data_41_Shift3_SectionA;

                            }
                            if (item.FSection.Equals("C"))
                            {
                                Id41Shift3SectionC = item.FShiftid;
                                ViewBag.Id41Shift3SectionC = Id41Shift3SectionC;

                                string sql = "EXEC dbo.Get_Data @ShiftId";
                                List<SqlParameter> parms = new List<SqlParameter>
                                {new SqlParameter { ParameterName = "@ShiftId", Value = Id41Shift3SectionC }};
                                List<TDatarecord> data_41_Shift3_SectionC = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                                foreach (var elt in data_41_Shift3_SectionC)
                                {
                                    if (elt.FRecordtypeid == 1)
                                    {
                                        TDataplant temp = _ctx2.TDataplants.Where(s => (DateTime.Compare(s.FDate, date1) == 0) && (s.FTypeid == 48)).FirstOrDefault();
                                        if (temp != null)
                                        {
                                            elt.FRecordvalue = temp.FValue;
                                        }
                                    }
                                }
                                ViewBag.data_41_Shift3_SectionC = data_41_Shift3_SectionC;
                            }

                            if (item.FSection.Equals("None"))
                            {
                                Id41Shift3Prod = item.FShiftid;
                                ViewBag.Id41Shift3Prod = Id41Shift3Prod;

                                string sql = "EXEC dbo.Get_Data @ShiftId";
                                List<SqlParameter> parms = new List<SqlParameter>
                                {new SqlParameter { ParameterName = "@ShiftId", Value = Id41Shift3Prod }};
                                List<TDatarecord> data_41_Shift3_Prod = _ctx2.TDatarecords.FromSqlRaw<TDatarecord>(sql, parms.ToArray()).ToList();
                                ViewBag.data_41_Shift3_Prod = data_41_Shift3_Prod;

                            }

                        }
                        occur += 1;
                    }

                }



                if (occur == 0)
                {
                    validationToDisplay = 0;
                }
                else
                {
                    validationToDisplay = 1;
                }

                ViewBag.ok = validationToDisplay;
                ViewBag.listShiftToViewData = listShiftIdViewData;
                ViewBag.DateSelected = date1.ToLongDateString();
            }

        else
            {
                ViewBag.ok = 0;
                ViewBag.DateSelected = targetDate.ToLongDateString();
            }




            ViewBag.ErrorExist = _ctx2.TTranslations.Where(s => (s.FLabelid == 5) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.Prod40 = _ctx2.TTranslations.Where(s => (s.FLabelid == 6) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.Prod41 = _ctx2.TTranslations.Where(s => (s.FLabelid == 7) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.ProdAMG = _ctx2.TTranslations.Where(s => (s.FLabelid == 8) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.Shift = _ctx2.TTranslations.Where(s => (s.FLabelid == 9) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.SOLLProd = _ctx2.TTranslations.Where(s => (s.FLabelid == 10) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.ISTProd = _ctx2.TTranslations.Where(s => (s.FLabelid == 11) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.TL = _ctx2.TTranslations.Where(s => (s.FLabelid == 12) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.PD = _ctx2.TTranslations.Where(s => (s.FLabelid == 13) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.NewMA = _ctx2.TTranslations.Where(s => (s.FLabelid == 14) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.SpeMeas = _ctx2.TTranslations.Where(s => (s.FLabelid == 15) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.AS = _ctx2.TTranslations.Where(s => (s.FLabelid == 16) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.TrainerZNA = _ctx2.TTranslations.Where(s => (s.FLabelid == 17) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.Vac = _ctx2.TTranslations.Where(s => (s.FLabelid == 18) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.VacIND = _ctx2.TTranslations.Where(s => (s.FLabelid == 19) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.Sick = _ctx2.TTranslations.Where(s => (s.FLabelid == 20) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.TempW = _ctx2.TTranslations.Where(s => (s.FLabelid == 21) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.SumPerSec = _ctx2.TTranslations.Where(s => (s.FLabelid == 22) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.Sec = _ctx2.TTranslations.Where(s => (s.FLabelid == 23) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.Prod90 = _ctx2.TTranslations.Where(s => (s.FLabelid == 24) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.ZNASecA = _ctx2.TTranslations.Where(s => (s.FLabelid == 25) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.ZNASecC = _ctx2.TTranslations.Where(s => (s.FLabelid == 26) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.TotSets = _ctx2.TTranslations.Where(s => (s.FLabelid == 27) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.BuffLev = _ctx2.TTranslations.Where(s => (s.FLabelid == 28) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.TeamDiss = _ctx2.TTranslations.Where(s => (s.FLabelid == 29) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.ShiftDurBreak = _ctx2.TTranslations.Where(s => (s.FLabelid == 30) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.CycleTimeTargActual = _ctx2.TTranslations.Where(s => (s.FLabelid == 31) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.ZQB = _ctx2.TTranslations.Where(s => (s.FLabelid == 32) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.SKD = _ctx2.TTranslations.Where(s => (s.FLabelid == 33) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.Valmet = _ctx2.TTranslations.Where(s => (s.FLabelid == 34) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.Sindel = _ctx2.TTranslations.Where(s => (s.FLabelid == 35) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.Rastatt = _ctx2.TTranslations.Where(s => (s.FLabelid == 36) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.RA41 = _ctx2.TTranslations.Where(s => (s.FLabelid == 37) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.Kecs = _ctx2.TTranslations.Where(s => (s.FLabelid == 38) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.RA40 = _ctx2.TTranslations.Where(s => (s.FLabelid == 39) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.CycleTimeTarg = _ctx2.TTranslations.Where(s => (s.FLabelid == 40) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.CycleTimeActual = _ctx2.TTranslations.Where(s => (s.FLabelid == 41) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.ShiftDur = _ctx2.TTranslations.Where(s => (s.FLabelid == 42) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.Break = _ctx2.TTranslations.Where(s => (s.FLabelid == 43) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.SOLLGes = _ctx2.TTranslations.Where(s => (s.FLabelid == 44) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.ISTGes = _ctx2.TTranslations.Where(s => (s.FLabelid == 45) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();
            ViewBag.SumProd = _ctx2.TTranslations.Where(s => (s.FLabelid == 46) && (s.FLanguage.Equals(langSelected))).FirstOrDefault();


            return View();
        }

        public IActionResult SwitchLangToEN()
        {
            langSelected = "EN";
            ViewBag.langSelected = langSelected;
            string temp = ControllerContext.HttpContext.Request.Headers["Referer"].ToString();
            if (temp.Contains("Home"))
            {
                string action;
                int start_index = temp.IndexOf("Home") + 5;
                int firstparam_index = temp.IndexOf("?");
                action = temp.Substring(start_index, firstparam_index - start_index);
                string gnewtemp = temp.Substring(firstparam_index + 8, temp.Length - (firstparam_index + 8));
                int secondparam_index = gnewtemp.IndexOf("&");
                string firstparam_value_str = gnewtemp.Substring(0, secondparam_index);
                int firstparam_value = (int)Convert.ToInt64(firstparam_value_str);
                return RedirectToAction(action, new { offset = firstparam_value, reportselected = select_report_input, lang = "EN" });
            }
            else if (temp.Substring(temp.Length - 2, 2).Equals("DE"))
            {
                temp = temp.Substring(0, temp.Length - 2) + "EN";
                return Redirect(temp);
            }
            else if (temp.Substring(temp.Length - 2, 2).Equals("FR"))
            {
                temp = temp.Substring(0, temp.Length - 2) + "EN";
                return Redirect(temp);

            }
            else if (temp.Substring(temp.Length - 2, 2).Equals("EN"))
            {
                temp = temp.Substring(0, temp.Length - 2) + "EN";
                return Redirect(temp);

            }
            else
            {
                return Redirect((ControllerContext.HttpContext.Request.Headers["Referer"].ToString()) + "?lang=EN");
            }
        }


        public IActionResult SwitchLangToDE()
        {
            langSelected = "DE";
            ViewBag.langSelected = langSelected;

            string temp = ControllerContext.HttpContext.Request.Headers["Referer"].ToString();

            if (temp.Contains("Home"))
            {
                string action;
                int start_index = temp.IndexOf("Home") + 5;
                int firstparam_index = temp.IndexOf("?");
                action = temp.Substring(start_index, firstparam_index - start_index);
                string gnewtemp = temp.Substring(firstparam_index + 8, temp.Length - (firstparam_index + 8));
                int secondparam_index = gnewtemp.IndexOf("&");
                string firstparam_value_str = gnewtemp.Substring(0, secondparam_index);
                int firstparam_value = (int)Convert.ToInt64(firstparam_value_str);
                return RedirectToAction(action, new { offset = firstparam_value, reportselected = select_report_input, lang = "DE" });
            }
            else if (temp.Substring(temp.Length - 2, 2).Equals("DE"))
            {
                temp = temp.Substring(0, temp.Length - 2) + "DE";
                return Redirect(temp);
            }
            else if (temp.Substring(temp.Length - 2, 2).Equals("FR"))
            {
                temp = temp.Substring(0, temp.Length - 2) + "DE";
                return Redirect(temp);

            }
            else if (temp.Substring(temp.Length - 2, 2).Equals("EN"))
            {
                temp = temp.Substring(0, temp.Length - 2) + "DE";
                return Redirect(temp);

            }
            else
            {
                return Redirect((ControllerContext.HttpContext.Request.Headers["Referer"].ToString()) + "?lang=DE");
            }
        }

        public IActionResult SwitchLangToFR()
        {
            langSelected = "FR";
            ViewBag.langSelected = langSelected;

            string temp = ControllerContext.HttpContext.Request.Headers["Referer"].ToString();

            if (temp.Contains("Home"))
            {
                string action;
                int start_index = temp.IndexOf("Home") + 5;
                int firstparam_index = temp.IndexOf("?");
                action = temp.Substring(start_index, firstparam_index - start_index);
                string gnewtemp = temp.Substring(firstparam_index + 8, temp.Length - (firstparam_index + 8));
                int secondparam_index = gnewtemp.IndexOf("&");
                string firstparam_value_str = gnewtemp.Substring(0, secondparam_index);
                int firstparam_value = (int)Convert.ToInt64(firstparam_value_str);
                return RedirectToAction(action, new { offset = firstparam_value, reportselected = select_report_input, lang = "FR" });
            }
            else if (temp.Substring(temp.Length - 2, 2).Equals("DE"))
            {
                temp = temp.Substring(0, temp.Length - 2) + "FR";
                return Redirect(temp);
            }
            else if (temp.Substring(temp.Length - 2, 2).Equals("FR"))
            {
                temp = temp.Substring(0, temp.Length - 2) + "FR";
                return Redirect(temp);

            }
            else if (temp.Substring(temp.Length - 2, 2).Equals("EN"))
            {
                temp = temp.Substring(0, temp.Length - 2) + "FR";
                return Redirect(temp);

            }
            else
            {
                return Redirect((ControllerContext.HttpContext.Request.Headers["Referer"].ToString()) + "?lang=FR");
            }
        }


        public IActionResult ErrorExist()
        {

            return View();
        }
        public IActionResult IncOffset()
        {
            offsetinput = offsetinput + 1;
            return RedirectToAction("DisplayData", new { offset = offsetinput, reportselected = select_report_input, lang = langSelected });
        }
        public IActionResult DecOffset()
        {
            offsetinput = offsetinput - 1;
            return RedirectToAction("DisplayData", new { offset = offsetinput, reportselected = select_report_input, lang = langSelected });
        }
    }
}
