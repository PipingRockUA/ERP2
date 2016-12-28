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
        #endregion
    }
}