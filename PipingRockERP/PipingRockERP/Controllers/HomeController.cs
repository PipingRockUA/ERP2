// <copyright file="HomeController.cs" company="None">  
// Copyright (c) Allow to distribute this code.   
// </copyright>  
// <author>Asma Khalid</author>  
//-----------------------------------------------------------------------   
namespace PipingRockERP.Controllers
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    /// <summary>  
    /// Home controller class.   
    /// </summary>  
    [Authorize]
    public class HomeController : Controller
    {
        #region Index method.  
        /// <summary>  
        /// Index method.   
        /// </summary>  
        /// <returns>Returns - Index view</returns>  
        public ActionResult Index()
        {
            return this.View();
        }
        #endregion
    }
}