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
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Net;

namespace PipingRockERP.Controllers
{
    public class PurchasingController : Controller
    {
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

        public ActionResult ExportUnitOfMeasures()
        {
            PipingRockEntities db = new PipingRockEntities();
            try
            {
                Excel.Application excelApplication = new Excel.Application();

                Excel.Workbook excelWorkBook = excelApplication.Workbooks.Add();

                Excel.Worksheet excelWorkSheet = (Excel.Worksheet)excelWorkBook.Worksheets.get_Item(1);

                Excel.Range Line = (Excel.Range)excelWorkSheet.Rows[3];
                Line.Insert();
                var table = (from UnitOfMeasure in db.UnitOfMeasures
                             select new
                             {
                                 ID = UnitOfMeasure.UnitOfMeasureId,
                                 UnitOfMeasure = UnitOfMeasure.UnitOfMeasure1,
                                 Abbreviation = UnitOfMeasure.UnitOfMeasureAbbreviation,
                                 AddedDate = UnitOfMeasure.UnitOfMeasureAddedDate,
                                 ChangedDate = UnitOfMeasure.UnitOfMeasureChangedDate,
                                 DeletedDate = UnitOfMeasure.UnitOfMeasureDeletedDate,
                                 ModifiedById = UnitOfMeasure.UnitOfMeasureModifiedById,
                                 isDeleted = (UnitOfMeasure.isDeleted ? 1 : 0)
                             }).ToList();

                excelApplication.Cells[1, 1] = "ID";
                excelApplication.Cells[1, 2] = "UnitOfMeasure";
                excelApplication.Cells[1, 3] = "Abbreviation";
                excelApplication.Cells[1, 4] = "AddedDate";
                excelApplication.Cells[1, 5] = "ChangedDate";
                excelApplication.Cells[1, 6] = "DeletedDate";
                excelApplication.Cells[1, 7] = "ModifiedById";
                excelApplication.Cells[1, 8] = "isDeleted";

                for (int j = 1; j < 9; j++)
                {
                    excelWorkSheet.Columns[j].ColumnWidth = 18;
                    switch (j)
                    {
                        case 1:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].ID;
                                }
                                break;
                            }
                        case 2:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].UnitOfMeasure;
                                }
                                break;
                            }
                        case 3:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].Abbreviation;
                                }
                                break;
                            }
                        case 4:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].AddedDate.ToString("MM'/'dd'/'yyyy");
                                }
                                break;
                            }
                        case 5:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].ChangedDate.ToString("MM'/'dd'/'yyyy");
                                }
                                break;
                            }
                        case 6:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].DeletedDate.ToString();
                                }
                                break;
                            }
                        case 7:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].ModifiedById;
                                }
                                break;
                            }
                        case 8:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].isDeleted;
                                }
                                break;
                            }
                    }
                }
                excelWorkBook.SaveAs("UnitOfMeasures.xlsx", Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
        Missing.Value, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
        Excel.XlSaveConflictResolution.xlUserResolution, true,
        Missing.Value, Missing.Value, Missing.Value);
                excelWorkBook.Close(Missing.Value, Missing.Value, Missing.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return RedirectToAction("UnitOfMeasures");
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

            try
            {
                Excel.Application excelApplication = new Excel.Application();

                Excel.Workbook excelWorkBook = excelApplication.Workbooks.Add();

                Excel.Worksheet excelWorkSheet = (Excel.Worksheet)excelWorkBook.Worksheets.get_Item(1);

                Excel.Range Line = (Excel.Range)excelWorkSheet.Rows[3];
                Line.Insert();
                var table = (from Bottle in db.Bottle2
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
                                 ModifiedById = Bottle.BottleModifiedById,
                             }).ToList();

                excelApplication.Cells[1, 1] = "ID";
                excelApplication.Cells[1, 2] = "ItemKey";
                excelApplication.Cells[1, 3] = "Description";
                excelApplication.Cells[1, 4] = "BottlesSmallTray";
                excelApplication.Cells[1, 5] = "BottlesLargeTray";
                excelApplication.Cells[1, 6] = "WrappedBottlesTraySmall";
                excelApplication.Cells[1, 7] = "WrappedBottlesTrayLarge";
                excelApplication.Cells[1, 8] = "BottleLengthInches";
                excelApplication.Cells[1, 9] = "BottleWidthInches";
                excelApplication.Cells[1, 10] = "BottleHieghtInches";
                excelApplication.Cells[1, 11] = "BottleCubicInches";
                excelApplication.Cells[1, 12] = "BottleLengthCm";
                excelApplication.Cells[1, 13] = "BottleWidthCm";
                excelApplication.Cells[1, 14] = "BottleHieghtCm";
                excelApplication.Cells[1, 15] = "BottleCubicCm";
                excelApplication.Cells[1, 16] = "BottleLengthWrappedInches";
                excelApplication.Cells[1, 17] = "BottleWidthWrappedInches";
                excelApplication.Cells[1, 18] = "BottleDepthWrappedInches";
                excelApplication.Cells[1, 19] = "BottleCubicInchWrappedInches";
                excelApplication.Cells[1, 20] = "BottleLengthWrappedCm";
                excelApplication.Cells[1, 21] = "BottleWidthWrappedCm";
                excelApplication.Cells[1, 22] = "BottleDepthWrappedCm";
                excelApplication.Cells[1, 23] = "BottleCubicInchWrappedCm";
                excelApplication.Cells[1, 24] = "BottleLabelSquareInches";
                excelApplication.Cells[1, 25] = "LabelSquareInches";
                excelApplication.Cells[1, 26] = "LabelSquareCm";
                excelApplication.Cells[1, 27] = "BottleSize";
                excelApplication.Cells[1, 28] = "PrintFrames";
                excelApplication.Cells[1, 29] = "NumberOfPrintingPositions";
                excelApplication.Cells[1, 30] = "AddedDate";
                excelApplication.Cells[1, 31] = "ChangedDate";
                excelApplication.Cells[1, 32] = "DeletedDate";
                excelApplication.Cells[1, 33] = "ModifiedById";

                for (int j = 1; j < 34; j++)
                {
                    excelWorkSheet.Columns[j].ColumnWidth = 18;
                    switch (j)
                    {
                        case 1:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].ID;
                                break;
                            }
                        case 2:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].ItemKey;
                                break;
                            }
                        case 3:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].Description;
                                break;
                            }
                        case 4:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottlesSmallTray;
                                break;
                            }
                        case 5:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottlesLargeTray;
                                break;
                            }
                        case 6:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].WrappedBottlesTraySmall;
                                break;
                            }
                        case 7:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].WrappedBottlesTrayLarge;
                                break;
                            }
                        case 8:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottleLengthInches;
                                break;
                            }
                        case 9:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottleWidthInches;
                                break;
                            }
                        case 10:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottleHieghtInches;
                                break;
                            }
                        case 11:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottleCubicInches;
                                break;
                            }
                        case 12:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottleLengthCm;
                                break;
                            }
                        case 13:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottleWidthCm;
                                break;
                            }
                        case 14:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottleHieghtCm;
                                break;
                            }
                        case 15:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottleCubicCm;
                                break;
                            }
                        case 16:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottleLengthWrappedInches;
                                break;
                            }
                        case 17:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottleWidthWrappedCm;
                                break;
                            }
                        case 18:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottleDepthWrappedInches;
                                break;
                            }
                        case 19:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottleCubicInchWrappedInches;
                                break;
                            }
                        case 20:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottleLengthWrappedCm;
                                break;
                            }
                        case 21:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottleWidthWrappedCm;
                                break;
                            }
                        case 22:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottleDepthWrappedCm;
                                break;
                            }
                        case 23:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottleCubicInchWrappedCm;
                                break;
                            }
                        case 24:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottleLabelSquareInches;
                                break;
                            }
                        case 25:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].LabelSquareInches;
                                break;
                            }
                        case 26:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].LabelSquareCm;
                                break;
                            }

                        case 27:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].BottleSize;
                                break;
                            }
                        case 28:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].PrintFrames;
                                break;
                            }
                        case 29:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].NumberOfPrintingPositions;
                                break;
                            }
                        case 30:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].AddedDate.ToString("MM'/'dd'/'yyyy");
                                break;
                            }
                        case 31:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].ChangedDate.ToString("MM'/'dd'/'yyyy");
                                break;
                            }
                        case 32:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].DeletedDate.ToString();
                                break;
                            }
                        case 33:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                    excelApplication.Cells[i, j] = table[i - 2].ModifiedById;
                                break;
                            }
                    }
                }
                excelWorkSheet.Columns[1].ColumnWidth = 12;
                excelWorkSheet.Columns[2].ColumnWidth = 25;
                excelWorkSheet.Columns[3].ColumnWidth = 50;
                excelWorkBook.SaveAs("Bottles.xlsx", Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
        Missing.Value, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
        Excel.XlSaveConflictResolution.xlUserResolution, true,
        Missing.Value, Missing.Value, Missing.Value);
                excelWorkBook.Close(Missing.Value, Missing.Value, Missing.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return RedirectToAction("BottleChart");
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

            try
            {
                Excel.Application excelApplication = new Excel.Application();
                Excel.Workbook excelWorkBook = excelApplication.Workbooks.Add();
                Excel.Worksheet excelWorkSheet = (Excel.Worksheet)excelWorkBook.Worksheets.get_Item(1);

                var table = (from Quarantine in db.Quarantines
                             select new
                             {
                                 Id = Quarantine.QuarantineId,
                                 Quarantine = Quarantine.Quarantine1,
                                 AddedDate = Quarantine.QuarantineAddedDate,
                                 ChangedDate = Quarantine.QuarantineChangedDate,
                                 DeletedDate = Quarantine.QuarantineDeletedDate,
                                 ModifiedById = Quarantine.QuarantineModifiedById,
                                 isDeleted = (Quarantine.isDeleted ? 1 : 0)
                             }).ToList();

                excelApplication.Cells[1, 1] = "ID";
                excelApplication.Cells[1, 2] = "Quarantine";
                excelApplication.Cells[1, 3] = "AddedDate";
                excelApplication.Cells[1, 4] = "ChangedDate";
                excelApplication.Cells[1, 5] = "DeletedDate";
                excelApplication.Cells[1, 6] = "ModifiedById";
                excelApplication.Cells[1, 7] = "isDeleted";

                string[,] data = new string[table.Count, 7];
                for (int i = 0; i < table.Count; i++)
                {
                    data[i, 0] = table[i].Id.ToString();
                    data[i, 1] = table[i].Quarantine.ToString();
                    data[i, 2] = table[i].AddedDate.ToString("MM'/'dd'/'yyyy");
                    data[i, 3] = table[i].ChangedDate.ToString("MM'/'dd'/'yyyy");
                    data[i, 4] = table[i].DeletedDate.ToString();
                    data[i, 5] = table[i].ModifiedById.ToString();
                    data[i, 6] = table[i].isDeleted.ToString();
                }
                string end = "G" + (table.Count + 1);
                excelWorkSheet.Range["A2", end].Value = data;
                for (int j = 1; j < 8; j++)
                {
                    excelWorkSheet.Columns[j].ColumnWidth = 18;
                }
                excelWorkSheet.Columns[2].ColumnWidth = 25;
                excelWorkBook.SaveAs("Quarantines.xlsx", Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
                                        Missing.Value, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                                        Excel.XlSaveConflictResolution.xlUserResolution, true,
                                        Missing.Value, Missing.Value, Missing.Value);
                excelWorkBook.Close(Missing.Value, Missing.Value, Missing.Value);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return RedirectToAction("Quarantines");
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

        public ActionResult ExportStorageConditions()
        {
            PipingRockEntities db = new PipingRockEntities();

            try
            {
                Excel.Application excelApplication = new Excel.Application();
                Excel.Workbook excelWorkBook = excelApplication.Workbooks.Add();
                Excel.Worksheet excelWorkSheet = (Excel.Worksheet)excelWorkBook.Worksheets.get_Item(1);

                var table = (from StorageCondition in db.StorageConditions
                             select new
                             {
                                 ID = StorageCondition.StorageConditionId,
                                 StorageCondition = StorageCondition.StorageCondition1,
                                 Description = StorageCondition.StorageConditionDescription,
                                 AddedDate = StorageCondition.StorageConditionAddedDate,
                                 ChangedDate = StorageCondition.StorageConditionChangedDate,
                                 DeletedDate = StorageCondition.StorageConditionDeletedDate,
                                 ModifiedById = StorageCondition.StorageConditionModifiedById,
                                 isDeleted = (StorageCondition.isDeleted ? 1 : 0)
                             }).ToList();

                excelApplication.Cells[1, 1] = "ID";
                excelApplication.Cells[1, 2] = "StorageCondition";
                excelApplication.Cells[1, 3] = "Description";
                excelApplication.Cells[1, 4] = "AddedDate";
                excelApplication.Cells[1, 5] = "ChangedDate";
                excelApplication.Cells[1, 6] = "DeletedDate";
                excelApplication.Cells[1, 7] = "ModifiedById";
                excelApplication.Cells[1, 8] = "isDeleted";

                string[,] data = new string[table.Count, 8];
                for (int i = 0; i < table.Count; i++)
                {
                    data[i, 0] = table[i].ID.ToString();
                    data[i, 1] = table[i].StorageCondition.ToString();
                    data[i, 2] = table[i].Description.ToString();
                    data[i, 3] = table[i].AddedDate.ToString("MM'/'dd'/'yyyy");
                    data[i, 4] = table[i].ChangedDate.ToString("MM'/'dd'/'yyyy");
                    data[i, 5] = table[i].DeletedDate.ToString();
                    data[i, 6] = table[i].ModifiedById.ToString();
                    data[i, 7] = table[i].isDeleted.ToString();
                }
                string end = "H" + (table.Count + 1);
                excelWorkSheet.Range["A2", end].Value = data;
                for (int j = 1; j < 9; j++)
                {
                    excelWorkSheet.Columns[j].ColumnWidth = 18;
                }
                excelWorkSheet.Columns[2].ColumnWidth = 25;

                excelWorkBook.SaveAs("StorageConditions.xlsx", Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
                                        Missing.Value, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                                        Excel.XlSaveConflictResolution.xlUserResolution, true,
                                        Missing.Value, Missing.Value, Missing.Value);
                excelWorkBook.Close(Missing.Value, Missing.Value, Missing.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return RedirectToAction("StorageConditions");
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

        public ActionResult ExportBrand()
        {
            PipingRockEntities db = new PipingRockEntities();

            try
            {
                Excel.Application excelApplication = new Excel.Application();

                Excel.Workbook excelWorkBook = excelApplication.Workbooks.Add();

                Excel.Worksheet excelWorkSheet = (Excel.Worksheet)excelWorkBook.Worksheets.get_Item(1);

                Excel.Range Line = (Excel.Range)excelWorkSheet.Rows[3];
                Line.Insert();
                var table = (from Brand in db.Brands
                             select new
                             {
                                 ID = Brand.BrandID,
                                 BrandCode = Brand.BrandCode,
                                 Brand = Brand.Brand1,
                                 AddedDate = Brand.BrandAddedDate,
                                 ChangedDate = Brand.BrandChangedDate,
                                 DeletedDate = Brand.BrandDeletedDate,
                                 ModifiedById = Brand.BrandModifiedById,
                                 isDeleted = (Brand.isDeleted ? 1 : 0)
                             }).ToList();

                excelApplication.Cells[1, 1] = "ID";
                excelApplication.Cells[1, 2] = "BrandCode";
                excelApplication.Cells[1, 3] = "Brand";
                excelApplication.Cells[1, 4] = "AddedDate";
                excelApplication.Cells[1, 5] = "ChangedDate";
                excelApplication.Cells[1, 6] = "DeletedDate";
                excelApplication.Cells[1, 7] = "ModifiedById";
                excelApplication.Cells[1, 8] = "isDeleted";

                for (int j = 1; j < 9; j++)
                {
                    excelWorkSheet.Columns[j].ColumnWidth = 18;
                    switch (j)
                    {
                        case 1:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].ID;
                                }
                                break;
                            }
                        case 2:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].BrandCode;
                                }
                                break;
                            }
                        case 3:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].Brand;
                                }
                                break;
                            }
                        case 4:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].AddedDate.ToString("MM'/'dd'/'yyyy");
                                }
                                break;
                            }
                        case 5:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].ChangedDate.ToString("MM'/'dd'/'yyyy");
                                }
                                break;
                            }
                        case 6:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].DeletedDate.ToString();
                                }
                                break;
                            }
                        case 7:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].ModifiedById;
                                }
                                break;
                            }
                        case 8:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].isDeleted;
                                }
                                break;
                            }
                    }
                }
                excelWorkBook.SaveAs("Brands.xlsx", Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
        Missing.Value, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
        Excel.XlSaveConflictResolution.xlUserResolution, true,
        Missing.Value, Missing.Value, Missing.Value);
                excelWorkBook.Close(Missing.Value, Missing.Value, Missing.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return RedirectToAction("Brands");
        }
        #endregion
    }
}