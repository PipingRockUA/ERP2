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
using Excel;

namespace PipingRockERP.Controllers
{
    public class UploadController : Controller
    {
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
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
    }
}
