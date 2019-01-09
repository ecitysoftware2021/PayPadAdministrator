using Newtonsoft.Json;
using PayPadAdministrator.Classes;
using PayPadAdministrator.Models;
using PayPadAdministrator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PayPadAdministrator.Controllers
{
    public class DevicesController : Controller
    {
        static ApiService apiService = new ApiService();
        // GET: Devices
        public async Task<ActionResult> Index()
        {
            List<Device> devices = new List<Device>();
            var response = await apiService.GetData("GetDevices");
            if (response.CodeError == 200)
            {
                devices = JsonConvert.DeserializeObject<List<Device>>(response.Data.ToString());
            }

            return View(devices);
        }

        public ActionResult CreateDevice()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateDevice(Device device)
        {
            if (!ModelState.IsValid)
            {
                return View(device);
            }

            if (device.ImagePathFile == null)
            {
                ModelState.AddModelError(string.Empty, "Debe ingresar una imagen");
                return View(device);
            }

            device.IMAGE = Utilities.GenerateByteArray(device.ImagePathFile.InputStream);
            device.ImagePathFile = null;
            var response = await apiService.InsertPost(device, "CreateDevice");
            if (response.CodeError != 200)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(device);
            }

            return RedirectToAction("Index");
        }
    }
}