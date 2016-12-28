using PipingRockERP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Globalization;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using Exc = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Net;
using Excel;
using System.Data.SqlClient;
using System.Configuration;
using ClosedXML.Excel;

namespace PipingRockERP.Controllers
{
    public class PurchasingController : Controller
    {
        #region Helpful methods
        public DataTable queryToDataTable<t>(IEnumerable<t> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (t rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others 
                // will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        #endregion

        // GET: Purchasing
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(string param)
        {
            return View(param);
        }

        #region Units of Measures
        public ActionResult UnitOfMeasures()
        {
            PipingRockEntities db = new PipingRockEntities();

            var measures = (from UnitOfMeasure in db.UnitOfMeasures select UnitOfMeasure).ToList();

            return View(measures);
        }
        #endregion

        #region Bottle Chart
        public ActionResult BottleChart()
        {
            PipingRockEntities db = new PipingRockEntities();
            var bottles = (from Bottle in db.Bottle2 select Bottle).ToList();

            ViewBag.Bottles = bottles;

            return View();
        }

        public ActionResult BottleEdit(string bottleId)
        {
            PipingRockEntities db = new PipingRockEntities();
            int ID = Int32.Parse(bottleId);

            var bottle = (from Bottle in db.Bottle2
                          where Bottle.BottleId == ID
                          select Bottle).ToList();

            ViewBag.Bottle = bottle;

            return View();
        }

        public ActionResult SubmitBottleAdd(string BottleItemKey,
                                              string BottleDescription,
                                              int BottlesSmallTray,
                                              int BottlesLargeTray,
                                              int WrappedBottlesTrayLarge,
                                              int WrappedBottlesTraySmall,

                                              int LayersUnWrapped,
                                              int LayersWrapped,

                                              string BottleLengthInches,
                                              string BottleWidthInches,
                                              string BottleHieghtInches,
                                              string BottleCubicInches,

                                              string BottleLengthCm,
                                              string BottleWidthCm,
                                              string BottleHieghtCm,
                                              string BottleCubicCm,

                                              string BottleLengthWrappedCm,
                                              string BottleWidthWrappedCm,
                                              string BottleDepthWrappedCm,
                                              string BottleCubicInchWrappedCm,

                                              string BottleLengthWrappedInches,
                                              string BottleWidthWrappedInches,
                                              string BottleDepthWrappedInches,
                                              string BottleCubicInchWrappedInches,

                                              string BottleLabelSquareInches,
                                              string LabelSquareInches,
                                              string LabelSquareCm,

                                              string BottleSize,
                                              int PrintFrames,
                                              int NumberOfPrintingPositions)
        {
            PipingRockEntities db = new PipingRockEntities();

            var bottle = new Bottle2()
            {
                BottleItemKey = BottleItemKey,
                BottleDescription = BottleDescription,
                BottlesSmallTray = BottlesSmallTray,
                BottlesLargeTray = BottlesLargeTray,
                WrappedBottlesTrayLarge = WrappedBottlesTrayLarge,
                WrappedBottlesTraySmall = WrappedBottlesTraySmall,
                ItemStatusId = 3,
                ItemTypeId = 2,
                ItemSubTypeId = 1,

                LayersUnWrapped = LayersUnWrapped,
                LayersWrapped = LayersWrapped,

                BottleLengthInches = Convert.ToDecimal(BottleLengthInches.Replace(".", ",")),
                BottleWidthInches = Convert.ToDecimal(BottleWidthInches.Replace(".", ",")),
                BottleHieghtInches = Convert.ToDecimal(BottleHieghtInches.Replace(".", ",")),
                BottleCubicInches = Convert.ToDecimal(BottleCubicInches.Replace(".", ",")),

                BottleLengthCm = Convert.ToDecimal(BottleLengthCm.Replace(".", ",")),
                BottleWidthCm = Convert.ToDecimal(BottleWidthCm.Replace(".", ",")),
                BottleHieghtCm = Convert.ToDecimal(BottleHieghtCm.Replace(".", ",")),
                BottleCubicCm = Convert.ToDecimal(BottleCubicCm.Replace(".", ",")),

                BottleLengthWrappedInches = Convert.ToDecimal(BottleLengthWrappedInches.Replace(".", ",")),
                BottleWidthWrappedInches = Convert.ToDecimal(BottleWidthWrappedInches.Replace(".", ",")),
                BottleDepthWrappedInches = Convert.ToDecimal(BottleDepthWrappedInches.Replace(".", ",")),
                BottleCubicInchWrappedInches = Convert.ToDecimal(BottleCubicInchWrappedInches.Replace(".", ",")),

                BottleLengthWrappedCm = Convert.ToDecimal(BottleLengthWrappedCm.Replace(".", ",")),
                BottleWidthWrappedCm = Convert.ToDecimal(BottleWidthWrappedCm.Replace(".", ",")),
                BottleDepthWrappedCm = Convert.ToDecimal(BottleDepthWrappedCm.Replace(".", ",")),
                BottleCubicInchWrappedCm = Convert.ToDecimal(BottleCubicInchWrappedCm.Replace(".", ",")),

                BottleLabelSquareInches = Convert.ToDecimal(BottleLabelSquareInches.Replace(".", ",")),
                LabelSquareInches = Convert.ToDecimal(LabelSquareInches.Replace(".", ",")),
                LabelSquareCm = Convert.ToDecimal(LabelSquareCm.Replace(".", ",")),

                BottleSize = BottleSize,
                PrintFrames = PrintFrames,
                NumberOfPrintingPositions = NumberOfPrintingPositions,

                BottleAddedDate = DateTime.Now,
                BottleChangedDate = DateTime.Now,
                BottleModifiedById = 1,
            };

            db.Bottle2.Add(bottle);
            db.SaveChanges();
            return RedirectToAction("BottleChart");
        }

        public ActionResult ImportBottle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportBottle(HttpPostedFileBase upload)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (upload != null && upload.ContentLength > 0)
                    {
                        // ExcelDataReader works with the binary Excel file, so it needs a FileStream
                        // to get started. This is how we avoid dependencies on ACE or Interop:
                        Stream stream = upload.InputStream;

                        // We return the interface, so that
                        IExcelDataReader reader = null;


                        if (upload.FileName.EndsWith(".xls"))
                        {
                            reader = ExcelReaderFactory.CreateBinaryReader(stream);
                        }
                        else if (upload.FileName.EndsWith(".xlsx"))
                        {
                            reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                        }
                        else
                        {
                            ModelState.AddModelError("File", "This file format is not supported");
                            return View();
                        }

                        reader.IsFirstRowAsColumnNames = true;

                        DataSet result = reader.AsDataSet();
                        reader.Close();

                        var data = new Bottle1();
                        PipingRockEntities db = new PipingRockEntities();
                        //db.prc_ExcelUpload_Bottle(1);
                        //db.Bottle1.
                        for (int i = 0; i < result.Tables[0].Rows.Count; i++)
                        {
                            data.BottleId = Int32.Parse(result.Tables[0].Rows[i][0].ToString().Trim());
                            data.BottleItemKey = result.Tables[0].Rows[i][1].ToString().Trim();
                            data.BottleDescription = result.Tables[0].Rows[i][2].ToString().Trim();
                            data.BottlesSmallTray = Int32.Parse(result.Tables[0].Rows[i][3].ToString().Trim());
                            data.BottlesLargeTray = Int32.Parse(result.Tables[0].Rows[i][4].ToString().Trim()); 
                            data.WrappedBottlesTrayLarge = Int32.Parse(result.Tables[0].Rows[i][5].ToString().Trim());
                            data.WrappedBottlesTraySmall = Int32.Parse(result.Tables[0].Rows[i][6].ToString().Trim());
                            data.BottleLengthInches = Decimal.Parse(result.Tables[0].Rows[i][7].ToString().Trim());
                            data.BottleWidthInches = Decimal.Parse(result.Tables[0].Rows[i][8].ToString().Trim());
                            data.BottleHieghtInches = Decimal.Parse(result.Tables[0].Rows[i][9].ToString().Trim());
                            data.BottleCubicInches = Decimal.Parse(result.Tables[0].Rows[i][10].ToString().Trim());
                            data.ItemStatusId = 3; 
                            data.ItemTypeId = 2; 
                            data.ItemSubTypeId = 1;
                            data.BottleLengthCm = Decimal.Parse(result.Tables[0].Rows[i][11].ToString().Trim());
                            data.BottleWidthCm = Decimal.Parse(result.Tables[0].Rows[i][12].ToString().Trim());
                            data.BottleHieghtCm = Decimal.Parse(result.Tables[0].Rows[i][13].ToString().Trim());
                            data.BottleCubicCm = Decimal.Parse(result.Tables[0].Rows[i][14].ToString().Trim());
                            data.BottleLengthWrappedInches = Decimal.Parse(result.Tables[0].Rows[i][15].ToString().Trim());
                            data.BottleWidthWrappedInches = Decimal.Parse(result.Tables[0].Rows[i][16].ToString().Trim());
                            data.BottleDepthWrappedInches = Decimal.Parse(result.Tables[0].Rows[i][17].ToString().Trim());
                            data.BottleCubicInchWrappedInches = Decimal.Parse(result.Tables[0].Rows[i][18].ToString().Trim());
                            data.BottleLengthWrappedCm = Decimal.Parse(result.Tables[0].Rows[i][19].ToString().Trim());
                            data.BottleWidthWrappedCm = Decimal.Parse(result.Tables[0].Rows[i][20].ToString().Trim());
                            data.BottleDepthWrappedCm = Decimal.Parse(result.Tables[0].Rows[i][21].ToString().Trim()); ;
                            data.BottleCubicInchWrappedCm = Decimal.Parse(result.Tables[0].Rows[i][22].ToString().Trim());
                            data.BottleLabelSquareInches = Decimal.Parse(result.Tables[0].Rows[i][23].ToString().Trim());
                            data.LayersUnWrapped = Int32.Parse(result.Tables[0].Rows[i][24].ToString().Trim());
                            data.LayersWrapped = Int32.Parse(result.Tables[0].Rows[i][25].ToString().Trim());
                            data.LabelSquareInches = 0;
                            data.LabelSquareCm = 0;
                            data.BottleSize = ""; 
                            data.PrintFrames = Int32.Parse(result.Tables[0].Rows[i][27].ToString().Trim()); 
                            data.NumberOfPrintingPositions = Int32.Parse(result.Tables[0].Rows[i][28].ToString().Trim());
                            //data.BottleAddedDate = DateTime.Now;
                            //data.BottleChangedDate = DateTime.Now;
                            data.BottleModifiedById = 1;

                            add(data);
                            //db.Bottle1.Add(data);
                            //db.SaveChanges();
                        }
                        //db.SaveChanges();
                        db.prc_ExcelUpload_Bottle(0);
                        return View(result.Tables[0]);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "Please Upload Your file");
                    }
                }
            }
            catch (Exception ex)
            {
                // Info    
                Console.Write(ex);
            }
            return View();
        }

        public void add(Bottle1 b)
        {
            PipingRockEntities db = new PipingRockEntities();
            db.Bottle1.Add(b);
            db.SaveChanges();
        }

        public ActionResult SubmitBottleUpdate(string bottleId,
                                              string BottleItemKey,
                                              string BottleDescription,
                                              int BottlesSmallTray,
                                              int BottlesLargeTray,
                                              int WrappedBottlesTrayLarge,
                                              int WrappedBottlesTraySmall,

                                              int LayersUnWrapped,
                                              int LayersWrapped,

                                              string BottleLengthInches,
                                              string BottleWidthInches,
                                              string BottleHieghtInches,
                                              string BottleCubicInches,

                                              string BottleLengthCm,
                                              string BottleWidthCm,
                                              string BottleHieghtCm,
                                              string BottleCubicCm,

                                              string BottleLengthWrappedCm,
                                              string BottleWidthWrappedCm,
                                              string BottleDepthWrappedCm,
                                              string BottleCubicInchWrappedCm,

                                              string BottleLengthWrappedInches,
                                              string BottleWidthWrappedInches,
                                              string BottleDepthWrappedInches,
                                              string BottleCubicInchWrappedInches,

                                              string BottleLabelSquareInches,
                                              string LabelSquareInches,
                                              string LabelSquareCm,

                                              string BottleSize,
                                              int PrintFrames,
                                              int NumberOfPrintingPositions)
        {
            PipingRockEntities db = new PipingRockEntities();
            int ID = Int32.Parse(bottleId);
            var bottle = (from Bottle in db.Bottle2
                          where Bottle.BottleId == ID
                          select Bottle).Single();

            bottle.BottleItemKey = BottleItemKey;
            bottle.BottleDescription = BottleDescription;
            bottle.BottlesSmallTray = BottlesSmallTray;
            bottle.BottlesLargeTray = BottlesLargeTray;
            bottle.WrappedBottlesTrayLarge = WrappedBottlesTrayLarge;
            bottle.WrappedBottlesTraySmall = WrappedBottlesTraySmall;
            bottle.ItemStatusId = 3;
            bottle.ItemTypeId = 2;
            bottle.ItemSubTypeId = 1;

            bottle.LayersUnWrapped = LayersUnWrapped;
            bottle.LayersWrapped = LayersWrapped;

            bottle.BottleLengthInches = Convert.ToDecimal(BottleLengthInches.Replace(".", ","));
            bottle.BottleWidthInches = Convert.ToDecimal(BottleWidthInches.Replace(".", ","));
            bottle.BottleHieghtInches = Convert.ToDecimal(BottleHieghtInches.Replace(".", ","));
            bottle.BottleCubicInches = Convert.ToDecimal(BottleCubicInches.Replace(".", ","));

            bottle.BottleLengthCm = Convert.ToDecimal(BottleLengthCm.Replace(".", ","));
            bottle.BottleWidthCm = Convert.ToDecimal(BottleWidthCm.Replace(".", ","));
            bottle.BottleHieghtCm = Convert.ToDecimal(BottleHieghtCm.Replace(".", ","));
            bottle.BottleCubicCm = Convert.ToDecimal(BottleCubicCm.Replace(".", ","));

            bottle.BottleLengthWrappedInches = Convert.ToDecimal(BottleLengthWrappedInches.Replace(".", ","));
            bottle.BottleWidthWrappedInches = Convert.ToDecimal(BottleWidthWrappedInches.Replace(".", ","));
            bottle.BottleDepthWrappedInches = Convert.ToDecimal(BottleDepthWrappedInches.Replace(".", ","));
            bottle.BottleCubicInchWrappedInches = Convert.ToDecimal(BottleCubicInchWrappedInches.Replace(".", ","));

            bottle.BottleLengthWrappedCm = Convert.ToDecimal(BottleLengthWrappedCm.Replace(".", ","));
            bottle.BottleWidthWrappedCm = Convert.ToDecimal(BottleWidthWrappedCm.Replace(".", ","));
            bottle.BottleDepthWrappedCm = Convert.ToDecimal(BottleDepthWrappedCm.Replace(".", ","));
            bottle.BottleCubicInchWrappedCm = Convert.ToDecimal(BottleCubicInchWrappedCm.Replace(".", ","));

            bottle.BottleLabelSquareInches = Convert.ToDecimal(BottleLabelSquareInches.Replace(".", ","));
            bottle.LabelSquareInches = Convert.ToDecimal(LabelSquareInches.Replace(".", ","));
            bottle.LabelSquareCm = Convert.ToDecimal(LabelSquareCm.Replace(".", ","));

            bottle.BottleSize = BottleSize;
            bottle.PrintFrames = PrintFrames;
            bottle.NumberOfPrintingPositions = NumberOfPrintingPositions;

            bottle.BottleChangedDate = DateTime.Now;
            bottle.BottleModifiedById = 1;

            db.Entry(bottle).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("BottleEdit", new { bottleId = bottleId });
        }

        public ActionResult ExportBottle()
        {
            PipingRockEntities db = new PipingRockEntities();
            var qt = (from Bottle in db.Bottle2
                      select new
                      {
                          ID = Bottle.BottleId,
                          ItemKey = Bottle.BottleItemKey,
                          Description = Bottle.BottleDescription,
                          BottlesSmallTray = Bottle.BottlesSmallTray,
                          BottlesLargeTray = Bottle.BottlesLargeTray,
                          WrappedBottlesTraySmall = Bottle.WrappedBottlesTraySmall,
                          WrappedBottlesTrayLarge = Bottle.WrappedBottlesTrayLarge,
                          ItemStatusId = Bottle.ItemStatusId,
                          ItemTypeId = Bottle.ItemTypeId,
                          ItemSubTypeId = Bottle.ItemSubTypeId,
                          BottleLengthInches = Bottle.BottleLengthInches,
                          BottleWidthInches = Bottle.BottleWidthInches,
                          BottleHieghtInches = Bottle.BottleHieghtInches,
                          BottleCubicInches = Bottle.BottleCubicInches,
                          BottleLengthCm = Bottle.BottleLengthCm,
                          BottleWidthCm = Bottle.BottleWidthCm,
                          BottleHieghtCm = Bottle.BottleHieghtCm,
                          BottleCubicCm = Bottle.BottleCubicCm,
                          BottleLengthWrappedInches = Bottle.BottleLengthWrappedInches,
                          BottleWidthWrappedInches = Bottle.BottleWidthWrappedInches,
                          BottleDepthWrappedInches = Bottle.BottleDepthWrappedInches,
                          BottleCubicInchWrappedInches = Bottle.BottleCubicInchWrappedInches,
                          BottleLengthWrappedCm = Bottle.BottleLengthWrappedCm,
                          BottleWidthWrappedCm = Bottle.BottleWidthWrappedCm,
                          BottleDepthWrappedCm = Bottle.BottleDepthWrappedCm,
                          BottleCubicInchWrappedCm = Bottle.BottleCubicInchWrappedCm,
                          BottleLabelSquareInches = Bottle.BottleLabelSquareInches,
                          LabelSquareInches = Bottle.LabelSquareInches,
                          LabelSquareCm = Bottle.LabelSquareCm,
                          BottleSize = Bottle.BottleSize,
                          PrintFrames = Bottle.PrintFrames,
                          NumberOfPrintingPositions = Bottle.NumberOfPrintingPositions,
                          AddedDate = Bottle.BottleAddedDate,
                          ChangedDate = Bottle.BottleChangedDate,
                          DeletedDate = Bottle.BottleDeletedDate,
                          ModifiedById = Bottle.BottleModifiedById
                      }).AsEnumerable();
            DataTable dt = new DataTable();
            dt.TableName = "Bottles";
            dt = queryToDataTable(qt);

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet("Bottles");
                wb.Worksheet(1).Cell(1, 1).InsertTable(dt);
                wb.Worksheet(1).Rows().AdjustToContents();
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;
                wb.Style.DateFormat.Format = "MM/dd/yyyy";

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= Bottles.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Bottles", "Purchasing");
        }
        #endregion

        #region Quarantine Types
        public ActionResult Quarantines()
        {
            PipingRockEntities db = new PipingRockEntities();

            var quarantineTypes = (from Quarantine in db.Quarantines select Quarantine).ToList();

            return View(quarantineTypes);
        }

        public ActionResult QuarantineEdit(string qtId)
        {
            PipingRockEntities db = new PipingRockEntities();
            int ID = Int32.Parse(qtId);

            var qt = (from Quarantine in db.Quarantines
                      where Quarantine.QuarantineId == ID
                      select Quarantine).ToList();

            ViewBag.Quarantine = qt;

            return View();
        }

        public ActionResult SubmitQuarantineAdd(string qtname)
        {
            PipingRockEntities db = new PipingRockEntities();

            var qt = new Quarantine()
            {
                Quarantine1 = qtname,
                QuarantineAddedDate = DateTime.Now,
                QuarantineChangedDate = DateTime.Now,
                QuarantineModifiedById = 0,
                isDeleted = false
            };
            db.Quarantines.Add(qt);
            db.SaveChanges();
            return RedirectToAction("Quarantines");
        }

        public ActionResult SubmitQuarantineUpdate(string qtId, string qtname)
        {
            PipingRockEntities db = new PipingRockEntities();

            int ID = Int32.Parse(qtId);
            var qt = (from Quarantine in db.Quarantines
                      where Quarantine.QuarantineId == ID
                      select Quarantine).Single();

            qt.Quarantine1 = qtname;
            qt.QuarantineChangedDate = DateTime.Now;

            db.Entry(qt).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("QuarantineEdit", new { qtId = qtId });
        }
        
        public ActionResult ExportQuarantine()
        {
            PipingRockEntities db = new PipingRockEntities();
            var qt = (from Quarantine in db.Quarantines
                      select new
                      {
                          ID = Quarantine.QuarantineId,
                          Quarantine = Quarantine.Quarantine1,
                          AddedDate = Quarantine.QuarantineAddedDate,
                          ChangedDate = Quarantine.QuarantineChangedDate,
                          DeletedDate = Quarantine.QuarantineDeletedDate,
                          ModifiedById = Quarantine.QuarantineModifiedById
                      }).AsEnumerable();
            DataTable dt = new DataTable();
            dt.TableName = "Quarantine";
            dt = queryToDataTable(qt);

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.AddWorksheet("Quarantines");
                wb.Worksheet(1).Cell(1, 1).InsertTable(dt);
                wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                wb.Style.Font.Bold = true;

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename= Quarantine.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
            return RedirectToAction("Quarantines", "Purchasing");
        }
        #endregion

        #region Storage Conditions
        public ActionResult StorageConditions()
        {
            PipingRockEntities db = new PipingRockEntities();

            var sc = (from StorageCondition in db.StorageConditions select StorageCondition).ToList();

            return View(sc);
        }

        public ActionResult StorageConditionEdit(string scId)
        {
            PipingRockEntities db = new PipingRockEntities();
            int ID = Int32.Parse(scId);

            var sc = (from StorageCondition in db.StorageConditions
                      where StorageCondition.StorageConditionId == ID
                      select StorageCondition).ToList();

            ViewBag.StorageCondition = sc;

            return View();
        }

        public ActionResult SubmitStorageConditionAdd(string scname, string scdesc)
        {
            PipingRockEntities db = new PipingRockEntities();

            var sc = new StorageCondition()
            {
                StorageCondition1 = scname,
                StorageConditionDescription = scdesc,
                StorageConditionAddedDate = DateTime.Now,
                StorageConditionChangedDate = DateTime.Now,
                StorageConditionModifiedById = 0,
                isDeleted = false
            };
            db.StorageConditions.Add(sc);
            db.SaveChanges();
            return RedirectToAction("StorageConditions");
        }

        public ActionResult SubmitStorageConditionUpdate(string scId, string scname, string scdesc)
        {
            PipingRockEntities db = new PipingRockEntities();

            int ID = Int32.Parse(scId);
            var sc = (from StorageCondition in db.StorageConditions
                      where StorageCondition.StorageConditionId == ID
                      select StorageCondition).Single();

            sc.StorageCondition1 = scname;
            sc.StorageConditionDescription = scdesc;
            sc.StorageConditionChangedDate = DateTime.Now;

            db.Entry(sc).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("StorageConditionEdit", new { scId = scId });
        }
        #endregion

        #region Brands
        public ActionResult Brands()
        {
            PipingRockEntities db = new PipingRockEntities();

            var brands = (from Brand in db.Brands select Brand).ToList();

            return View(brands);
        }

        public ActionResult BrandEdit(string brandId)
        {
            PipingRockEntities db = new PipingRockEntities();
            int ID = Int32.Parse(brandId);

            var brand = (from Brand in db.Brands
                         where Brand.BrandID == ID
                         select Brand).ToList();

            ViewBag.Brand = brand;

            return View();
        }

        public ActionResult SubmitBrandAdd(string brandName, string brandCode)
        {
            PipingRockEntities db = new PipingRockEntities();

            var brand = new Brand()
            {
                Brand1 = brandName,
                BrandCode = brandCode,
                BrandAddedDate = DateTime.Now,
                BrandChangedDate = DateTime.Now,
                BrandModifiedById = 0,
                isDeleted = false
            };
            db.Brands.Add(brand);
            db.SaveChanges();
            return RedirectToAction("Brands");
        }

        public ActionResult SubmitBrandUpdate(string brandId, string brandName, string brandCode)
        {
            PipingRockEntities db = new PipingRockEntities();

            int ID = Int32.Parse(brandId);
            var brand = (from Brand in db.Brands
                         where Brand.BrandID == ID
                         select Brand).Single();

            brand.Brand1 = brandName;
            brand.BrandCode = brandCode;
            brand.BrandChangedDate = DateTime.Now;

            db.Entry(brand).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("BrandEdit", new { brandId = brandId });
        }
        #endregion
    }
}