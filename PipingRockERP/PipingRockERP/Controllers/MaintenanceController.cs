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
    public class MaintenanceController : Controller
    {
        //Book book = new BinBook();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(string param)
        {
            return View(param);
        }

        //#region Units of Measures
        //public ActionResult UnitOfMeasures()
        //{
        //    PipingRockEntities db = new PipingRockEntities();

        //    var measures = (from UnitOfMeasure in db.UnitOfMeasures select UnitOfMeasure).ToList();

        //    return View(measures);
        //}

        //public ActionResult ExportUnitOfMeasures()
        //{
        //    PipingRockEntities db = new PipingRockEntities();
        //    try
        //    {
        //        Excel.Application excelApplication = new Excel.Application();

        //        Excel.Workbook excelWorkBook = excelApplication.Workbooks.Add();

        //        Excel.Worksheet excelWorkSheet = (Excel.Worksheet)excelWorkBook.Worksheets.get_Item(1);

        //        Excel.Range Line = (Excel.Range)excelWorkSheet.Rows[3];
        //        Line.Insert();
        //        var table = (from UnitOfMeasure in db.UnitOfMeasures
        //                     select new
        //                     {
        //                         ID = UnitOfMeasure.UnitOfMeasureId,
        //                         UnitOfMeasure = UnitOfMeasure.UnitOfMeasure1,
        //                         Abbreviation = UnitOfMeasure.UnitOfMeasureAbbreviation,
        //                         AddedDate = UnitOfMeasure.UnitOfMeasureAddedDate,
        //                         ChangedDate = UnitOfMeasure.UnitOfMeasureChangedDate,
        //                         DeletedDate = UnitOfMeasure.UnitOfMeasureDeletedDate,
        //                         ModifiedById = UnitOfMeasure.UnitOfMeasureModifiedById,
        //                         isDeleted = (UnitOfMeasure.isDeleted ? 1 : 0)
        //                     }).ToList();

        //        excelApplication.Cells[1, 1] = "ID";
        //        excelApplication.Cells[1, 2] = "UnitOfMeasure";
        //        excelApplication.Cells[1, 3] = "Abbreviation";
        //        excelApplication.Cells[1, 4] = "AddedDate";
        //        excelApplication.Cells[1, 5] = "ChangedDate";
        //        excelApplication.Cells[1, 6] = "DeletedDate";
        //        excelApplication.Cells[1, 7] = "ModifiedById";
        //        excelApplication.Cells[1, 8] = "isDeleted";

        //        for (int j = 1; j < 9; j++)
        //        {
        //            excelWorkSheet.Columns[j].ColumnWidth = 18;
        //            switch (j)
        //            {
        //                case 1:
        //                    {
        //                        for (int i = 2; i < table.Count + 1; i++)
        //                        {
        //                            excelApplication.Cells[i, j] = table[i - 2].ID;
        //                        }
        //                        break;
        //                    }
        //                case 2:
        //                    {
        //                        for (int i = 2; i < table.Count + 1; i++)
        //                        {
        //                            excelApplication.Cells[i, j] = table[i - 2].UnitOfMeasure;
        //                        }
        //                        break;
        //                    }
        //                case 3:
        //                    {
        //                        for (int i = 2; i < table.Count + 1; i++)
        //                        {
        //                            excelApplication.Cells[i, j] = table[i - 2].Abbreviation;
        //                        }
        //                        break;
        //                    }
        //                case 4:
        //                    {
        //                        for (int i = 2; i < table.Count + 1; i++)
        //                        {
        //                            excelApplication.Cells[i, j] = table[i - 2].AddedDate.ToString("MM'/'dd'/'yyyy");
        //                        }
        //                        break;
        //                    }
        //                case 5:
        //                    {
        //                        for (int i = 2; i < table.Count + 1; i++)
        //                        {
        //                            excelApplication.Cells[i, j] = table[i - 2].ChangedDate.ToString("MM'/'dd'/'yyyy");
        //                        }
        //                        break;
        //                    }
        //                case 6:
        //                    {
        //                        for (int i = 2; i < table.Count + 1; i++)
        //                        {
        //                            excelApplication.Cells[i, j] = table[i - 2].DeletedDate.ToString();
        //                        }
        //                        break;
        //                    }
        //                case 7:
        //                    {
        //                        for (int i = 2; i < table.Count + 1; i++)
        //                        {
        //                            excelApplication.Cells[i, j] = table[i - 2].ModifiedById;
        //                        }
        //                        break;
        //                    }
        //                case 8:
        //                    {
        //                        for (int i = 2; i < table.Count + 1; i++)
        //                        {
        //                            excelApplication.Cells[i, j] = table[i - 2].isDeleted;
        //                        }
        //                        break;
        //                    }
        //            }
        //        }
        //        excelWorkBook.SaveAs("UnitOfMeasures.xlsx", Excel.XlFileFormat.xlOpenXMLWorkbook, Missing.Value,
        //Missing.Value, false, false, Excel.XlSaveAsAccessMode.xlNoChange,
        //Excel.XlSaveConflictResolution.xlUserResolution, true,
        //Missing.Value, Missing.Value, Missing.Value);
        //        excelWorkBook.Close(Missing.Value, Missing.Value, Missing.Value);
        //    }
        //    catch(Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //    }
        //    return RedirectToAction("UnitOfMeasures");
        //}
        //#endregion

        #region Users Profiles
        public ActionResult Users()
        {
            PipingRockEntities db = new PipingRockEntities();

            var users = (from User in db.Users select User).ToList();

            return View(users);
        }

        public ActionResult UserAdd()
        {
            PipingRockEntities db = new PipingRockEntities();

            var roles = (from UserRole in db.UserRoles
                         select UserRole).ToList();

            ViewBag.Roles = roles;

            return View();
        }

        public ActionResult UserAddRole(int userId, int roleId)
        {
            PipingRockEntities db = new PipingRockEntities();

            db.AddRoleUserID(userId, roleId);

            return RedirectToAction("Edit", new { userId = userId.ToString() });
        }

        public ActionResult Edit(string userId)
        {
            PipingRockEntities db = new PipingRockEntities();
            int ID = Int32.Parse(userId);

            var roles = (from User in db.Users
                         join User_UserRole in db.User_UserRole on User.UserId equals User_UserRole.UserId
                         join UserRole in db.UserRoles on User_UserRole.UserRoleId equals UserRole.UserRoleId
                         where User_UserRole.UserId == ID
                         orderby UserRole.UserRoleId
                         select new UsersAndRolesModel
                         {
                             UserID = User.UserId,
                             UserName = User.UserName,
                             RoleID = UserRole.UserRoleId,
                             RoleName = UserRole.UserRoleName
                         }).ToList();

            if (roles.Count == 0)
            {
                var user = db.GetUserByID(ID).ToList();
                roles.Add(new UsersAndRolesModel
                {
                    UserID = ID,
                    UserName = user[0].UserName,
                    RoleID = 0,
                    RoleName = "Don't have any roles"
                });
            }

            var otherRoles = (from UserRole in db.UserRoles
                              select UserRole).ToList();

            ViewBag.OtherRoles = db.GetNonActiveRoles(ID);
            ViewBag.ActiveRoles = roles;

            return View();
        }

        public ActionResult RemoveUserRole(int userId, int roleId)
        {
            PipingRockEntities db = new PipingRockEntities();

            var usersAndRole = from User_UserRole in db.User_UserRole
                               where User_UserRole.UserId == userId && User_UserRole.UserRoleId == roleId
                               select User_UserRole;

            foreach (var row in usersAndRole)
            {
                db.User_UserRole.Remove(row);
            }

            db.SaveChanges();

            return RedirectToAction("Edit", new { userId = userId.ToString() });
        }

        public ActionResult UserSubmitAdd(string userName, string roleName)
        {
            PipingRockEntities db = new PipingRockEntities();

            var roleId = (from UserRole in db.UserRoles
                          where UserRole.UserRoleName == roleName
                          select UserRole.UserRoleId).Single();

            db.AddUser(userName);
            var userId = (from User in db.Users
                          where User.UserName == userName
                          select User.UserId).Single();
            db.AddRoleUserID(userId, roleId);
            return RedirectToAction("Users");
        }
        #endregion

        #region Settings
        public ActionResult Settings()
        {
            return View();
        }
        #endregion
    }
}