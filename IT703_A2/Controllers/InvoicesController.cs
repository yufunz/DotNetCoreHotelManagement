using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IT703_A2.Data;
using IT703_A2.Models.Invoices;
using IT703_A2.Services;

namespace IT703_A2.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly IInvoicesService invoiceService;

        public InvoicesController(IInvoicesService iService)
        {
            this.invoiceService = iService;
        }

        public IActionResult All([FromQuery] AllInvoicesQueryModel query)
        {

            var allInvoices = invoiceService.All(query);

            return this.View(allInvoices);
        }

        public IActionResult Details(string id)
        {
            var currentInvoice = this.invoiceService.Details(id);

            return this.View(currentInvoice);
        }

        public IActionResult Delete(string id)
        {
            this.invoiceService.Delete(id);

            return this.RedirectToAction("All", "Invoices");
        }

        public IActionResult Pay(string id)
        {
            this.invoiceService.Pay(id);

            return this.RedirectToAction("All", "Invoices");
        }

    }
}
