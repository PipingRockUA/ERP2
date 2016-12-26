using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Exc = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace PipingRockERP.Controllers
{
    public class NutritionController : Controller
    {
        // GET: Nutrition
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(string param)
        {
            return View(param);
        }

        #region Allergens
        public ActionResult Allergens()
        {
            PipingRockEntities db = new PipingRockEntities();

            var allergens = (from Allergen in db.Allergens select Allergen).ToList();

            return View(allergens);
        }

        public ActionResult AllergenEdit(string allergenId)
        {
            PipingRockEntities db = new PipingRockEntities();
            int ID = Int32.Parse(allergenId);

            var allergen = (from Allergen in db.Allergens
                            where Allergen.AllergenId == ID
                      select Allergen).ToList();

            ViewBag.Allergen = allergen;

            return View();
        }

        public ActionResult SubmitAllergenAdd(string allergenName)
        {
            PipingRockEntities db = new PipingRockEntities();

            var allergen = new Allergen()
            {
                Allergen1 = allergenName,
                AllergenAddedDate = DateTime.Now,
                AllergenChangedDate = DateTime.Now,
                AllergenModifiedById = 0,
                isDeleted = false
            };
            db.Allergens.Add(allergen);
            db.SaveChanges();
            return RedirectToAction("Allergens");
        }

        public ActionResult SubmitAllergenUpdate(string allergenId, string allergenName)
        {
            PipingRockEntities db = new PipingRockEntities();

            int ID = Int32.Parse(allergenId);
            var qt = (from Allergen in db.Allergens
                      where Allergen.AllergenId == ID
                      select Allergen).Single();

            qt.Allergen1 = allergenName;
            qt.AllergenChangedDate = DateTime.Now;

            db.Entry(qt).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Allergens");
        }

        public ActionResult ExportAllergen()
        {
            PipingRockEntities db = new PipingRockEntities();

            try
            {
                Exc.Application excelApplication = new Exc.Application();

                Exc.Workbook excelWorkBook = excelApplication.Workbooks.Add();

                Exc.Worksheet excelWorkSheet = (Exc.Worksheet)excelWorkBook.Worksheets.get_Item(1);

                Exc.Range Line = (Exc.Range)excelWorkSheet.Rows[3];
                Line.Insert();
                var table = (from Allergen in db.Allergens
                             select new
                             {
                                 Id = Allergen.AllergenId,
                                 Allergen = Allergen.Allergen1,
                                 AddedDate = Allergen.AllergenAddedDate,
                                 ChangedDate = Allergen.AllergenChangedDate,
                                 DeletedDate = Allergen.AllergenDeletedDate,
                                 ModifiedById = Allergen.AllergenModifiedById,
                                 isDeleted = (Allergen.isDeleted ? 1 : 0)
                             }).ToList();

                excelApplication.Cells[1, 1] = "ID";
                excelApplication.Cells[1, 2] = "Allergen";
                excelApplication.Cells[1, 3] = "AddedDate";
                excelApplication.Cells[1, 4] = "ChangedDate";
                excelApplication.Cells[1, 5] = "DeletedDate";
                excelApplication.Cells[1, 6] = "ModifiedById";
                excelApplication.Cells[1, 7] = "isDeleted";

                for (int j = 1; j < 9; j++)
                {
                    excelWorkSheet.Columns[j].ColumnWidth = 18;
                    switch (j)
                    {
                        case 1:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].Id;
                                }
                                break;
                            }
                        case 2:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].Allergen;
                                }
                                break;
                            }
                        case 3:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].AddedDate.ToString("MM'/'dd'/'yyyy");
                                }
                                break;
                            }
                        case 4:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].ChangedDate.ToString("MM'/'dd'/'yyyy");
                                }
                                break;
                            }
                        case 5:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].DeletedDate.ToString();
                                }
                                break;
                            }
                        case 6:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].ModifiedById;
                                }
                                break;
                            }
                        case 7:
                            {
                                for (int i = 2; i < table.Count + 1; i++)
                                {
                                    excelApplication.Cells[i, j] = table[i - 2].isDeleted;
                                }
                                break;
                            }
                    }
                }
                excelWorkBook.SaveAs("Allergens.xlsx", Exc.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
        Missing.Value, false, false, Exc.XlSaveAsAccessMode.xlNoChange,
        Exc.XlSaveConflictResolution.xlUserResolution, true,
        Missing.Value, Missing.Value, Missing.Value);
                excelWorkBook.Close(Missing.Value, Missing.Value, Missing.Value);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            return RedirectToAction("Allergens");
        }
        #endregion

        #region Raw Material
        public ActionResult RawMaterials()
        {
            PipingRockEntities db = new PipingRockEntities();

            var rawMaterials = (from RawMaterial in db.RawMaterials select RawMaterial).ToList();

            return View(rawMaterials);
        }

        public ActionResult EditRawMaterial(string rawMaterialId)
        {
            PipingRockEntities db = new PipingRockEntities();
            int ID = Int32.Parse(rawMaterialId);

            var rawMaterial = (from RawMaterial in db.RawMaterials
                            where RawMaterial.RawMaterialId == ID
                            select RawMaterial).ToList();

            ViewBag.RawMaterial = rawMaterial;

            return View();
        }

        public ActionResult SubmitRawMaterialAdd(string allergenName)
        {
            PipingRockEntities db = new PipingRockEntities();

            var allergen = new Allergen()
            {
                Allergen1 = allergenName,
                AllergenAddedDate = DateTime.Now,
                AllergenChangedDate = DateTime.Now,
                AllergenModifiedById = 0,
                isDeleted = false
            };
            db.Allergens.Add(allergen);
            db.SaveChanges();
            return RedirectToAction("Allergens");
        }

        public ActionResult SubmitRawMaterialUpdate(string allergenId, string allergenName)
        {
            PipingRockEntities db = new PipingRockEntities();

            int ID = Int32.Parse(allergenId);
            var qt = (from Allergen in db.Allergens
                      where Allergen.AllergenId == ID
                      select Allergen).Single();

            qt.Allergen1 = allergenName;
            qt.AllergenChangedDate = DateTime.Now;

            db.Entry(qt).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Allergens");
        }

        //public ActionResult ExportRawMaterial()
        //{
        //    PipingRockEntities db = new PipingRockEntities();

        //    try
        //    {
        //        Exc.Application excelApplication = new Exc.Application();

        //        Exc.Workbook excelWorkBook = excelApplication.Workbooks.Add();

        //        Exc.Worksheet excelWorkSheet = (Exc.Worksheet)excelWorkBook.Worksheets.get_Item(1);

        //        Exc.Range Line = (Exc.Range)excelWorkSheet.Rows[3];
        //        Line.Insert();
        //        var table = (from RawMaterial in db.RawMaterials
        //                     select new
        //                     {
        //                     }).ToList();

        //        excelApplication.Cells[1, 1] = "ID";
        //        excelApplication.Cells[1, 2] = "Allergen";
        //        excelApplication.Cells[1, 3] = "AddedDate";
        //        excelApplication.Cells[1, 4] = "ChangedDate";
        //        excelApplication.Cells[1, 5] = "DeletedDate";
        //        excelApplication.Cells[1, 6] = "ModifiedById";
        //        excelApplication.Cells[1, 7] = "isDeleted";

        //        for (int j = 1; j < 9; j++)
        //        {
        //            excelWorkSheet.Columns[j].ColumnWidth = 18;
        //            switch (j)
        //            {
        //                case 1:
        //                    {
        //                        for (int i = 2; i < table.Count + 1; i++)
        //                        {
        //                            excelApplication.Cells[i, j] = table[i - 2].Id;
        //                        }
        //                        break;
        //                    }
        //                case 2:
        //                    {
        //                        for (int i = 2; i < table.Count + 1; i++)
        //                        {
        //                            excelApplication.Cells[i, j] = table[i - 2].Allergen;
        //                        }
        //                        break;
        //                    }
        //                case 3:
        //                    {
        //                        for (int i = 2; i < table.Count + 1; i++)
        //                        {
        //                            excelApplication.Cells[i, j] = table[i - 2].AddedDate.ToString("MM'/'dd'/'yyyy");
        //                        }
        //                        break;
        //                    }
        //                case 4:
        //                    {
        //                        for (int i = 2; i < table.Count + 1; i++)
        //                        {
        //                            excelApplication.Cells[i, j] = table[i - 2].ChangedDate.ToString("MM'/'dd'/'yyyy");
        //                        }
        //                        break;
        //                    }
        //                case 5:
        //                    {
        //                        for (int i = 2; i < table.Count + 1; i++)
        //                        {
        //                            excelApplication.Cells[i, j] = table[i - 2].DeletedDate.ToString();
        //                        }
        //                        break;
        //                    }
        //                case 6:
        //                    {
        //                        for (int i = 2; i < table.Count + 1; i++)
        //                        {
        //                            excelApplication.Cells[i, j] = table[i - 2].ModifiedById;
        //                        }
        //                        break;
        //                    }
        //                case 7:
        //                    {
        //                        for (int i = 2; i < table.Count + 1; i++)
        //                        {
        //                            excelApplication.Cells[i, j] = table[i - 2].isDeleted;
        //                        }
        //                        break;
        //                    }
        //            }
        //        }
        //        excelWorkBook.SaveAs("RawMaterials.xlsx", Exc.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
        //Missing.Value, false, false, Exc.XlSaveAsAccessMode.xlNoChange,
        //Exc.XlSaveConflictResolution.xlUserResolution, true,
        //Missing.Value, Missing.Value, Missing.Value);
        //        excelWorkBook.Close(Missing.Value, Missing.Value, Missing.Value);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }

        //    return RedirectToAction("RawMaterials");
        //}
        #endregion
    }
}