﻿using Htmx;
using Microsoft.AspNetCore.Mvc;
using Swats.Controllers;

namespace Swats.Areas.Admin.Controllers;

[Area("admin")]
public class DepartmentController : FrontEndController
{
    public IActionResult Index()
    {
        return Request.IsHtmx()
             ? PartialView("~/Areas/Admin/Views/Department/_IndexPartial.cshtml")
             : View();
    }

    public IActionResult Create()
    {
        return Request.IsHtmx()
             ? PartialView("~/Areas/Admin/Views/Department/_CreatePartial.cshtml")
             : View();
    }
}
