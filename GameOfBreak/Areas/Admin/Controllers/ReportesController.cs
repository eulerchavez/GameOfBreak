using GameOfBreak.Models.GoB;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace GameOfBreak.Areas.Admin.Controllers {


    [Authorize(Roles = "Administrador, Gerente, Subgerente, Vendedor")]
    public class ReportesController : Controller {

        private GameOfBreakModel _context;

        public ReportesController () {
            this._context = new GameOfBreakModel();
        }

        // GET: Admin/Reportes
        public ActionResult Index () {
            return View();
        }

        public ActionResult Reporte () {

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            reportViewer.LocalReport.ReportPath = Server.MapPath(Request.ApplicationPath) + @"Reports\Clientes.rdlc";

            ReportDataSource reportDataSource = new ReportDataSource("ClientesDataSet", this._context.Clientes.AsEnumerable());

            reportViewer.LocalReport.DataSources.Add(reportDataSource);

            ViewBag.ReportViewer = reportViewer;

            return View();

        }

        public ActionResult Reporte1 () {

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            reportViewer.LocalReport.ReportPath = Server.MapPath(Request.ApplicationPath) + @"Reports\DetalleVentas.rdlc";

            ReportDataSource reportDataSource = new ReportDataSource("DetalleVentasDataSet", this._context.DetallesVentas.AsEnumerable());

            reportViewer.LocalReport.DataSources.Add(reportDataSource);

            ViewBag.ReportViewer = reportViewer;

            return View();

        }

        public ActionResult Reporte2 () {

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            reportViewer.LocalReport.ReportPath = Server.MapPath(Request.ApplicationPath) + @"Reports\Empleados.rdlc";

            ReportDataSource reportDataSource = new ReportDataSource("EmpleadosDataSet", this._context.Empleados.AsEnumerable());

            reportViewer.LocalReport.DataSources.Add(reportDataSource);

            ViewBag.ReportViewer = reportViewer;

            return View();

        }

        public ActionResult Reporte3 () {

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            reportViewer.LocalReport.ReportPath = Server.MapPath(Request.ApplicationPath) + @"Reports\Inventario.rdlc";

            ReportDataSource reportDataSource = new ReportDataSource("InventarioDataSet", this._context.Inventario.AsEnumerable());

            reportViewer.LocalReport.DataSources.Add(reportDataSource);

            ViewBag.ReportViewer = reportViewer;

            return View();

        }

        public ActionResult Reporte4 () {

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            reportViewer.LocalReport.ReportPath = Server.MapPath(Request.ApplicationPath) + @"Reports\Productos.rdlc";

            ReportDataSource reportDataSource = new ReportDataSource("ProductosDataSet", this._context.Productos.AsEnumerable());

            reportViewer.LocalReport.DataSources.Add(reportDataSource);

            ViewBag.ReportViewer = reportViewer;

            return View();

        }

        public ActionResult Reporte5 () {

            ReportViewer reportViewer = new ReportViewer();
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.SizeToReportContent = true;
            reportViewer.Width = Unit.Percentage(100);
            reportViewer.Height = Unit.Percentage(100);

            reportViewer.LocalReport.ReportPath = Server.MapPath(Request.ApplicationPath) + @"Reports\Ventas.rdlc";

            ReportDataSource reportDataSource = new ReportDataSource("VentasDataSet", this._context.Ventas.AsEnumerable());

            reportViewer.LocalReport.DataSources.Add(reportDataSource);

            ViewBag.ReportViewer = reportViewer;

            return View();

        }

    }

}