using Newtonsoft.Json;
using PayPadAdministrator.Classes;
using PayPadAdministrator.Helpers;
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

        public async Task<ActionResult> CreateDevice()
        {
            ViewBag.DEVICE_TYPE_ID = new SelectList(await ComboHelper.GetDevicesType(), nameof(DeviceType.DEVICE_TYPE_ID), nameof(DeviceType.APOSTROPHE), 0);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateDevice(Device device)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DEVICE_TYPE_ID = new SelectList(await ComboHelper.GetDevicesType(), nameof(DeviceType.DEVICE_TYPE_ID), nameof(DeviceType.APOSTROPHE), device.DEVICE_TYPE_ID);
                return View(device);
            }

            if (device.ImagePathFile == null)
            {
                ModelState.AddModelError(string.Empty, "Debe ingresar una imagen");
                ViewBag.DEVICE_TYPE_ID = new SelectList(await ComboHelper.GetDevicesType(), nameof(DeviceType.DEVICE_TYPE_ID), nameof(DeviceType.APOSTROPHE), device.DEVICE_TYPE_ID);
                return View(device);
            }

            device.IMAGE = Utilities.GenerateByteArray(device.ImagePathFile.InputStream);
            device.ImagePathFile = null;
            var response = await apiService.InsertPost(device, "CreateDevice");
            if (response.CodeError != 200)
            {
                var data = JsonConvert.DeserializeObject<Exception>(response.Data.ToString());
                ModelState.AddModelError(string.Empty, response.Message);
                ViewBag.DEVICE_TYPE_ID = new SelectList(await ComboHelper.GetDevicesType(), nameof(DeviceType.DEVICE_TYPE_ID), nameof(DeviceType.APOSTROPHE), device.DEVICE_TYPE_ID);
                return View(device);
            }

            var usercurrent = apiService.ValidateUser(User.Identity.Name);
            var url = Request.Url.AbsolutePath.Split('/')[1];
            await NotifyHelper.SaveLog(usercurrent, string.Concat("Se creó el dispositivo ", device.NAME), url);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> GetDeviceTypes()
        {
            List<DeviceType> deviceTypes = new List<DeviceType>();
            var response = await apiService.GetData("GetDeviceTypes");
            if (response.CodeError == 200)
            {
                deviceTypes = JsonConvert.DeserializeObject<List<DeviceType>>(response.Data.ToString());
            }

            return View(deviceTypes);
        }

        public ActionResult CreateDeviceType()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateDeviceType(DeviceType device)
        {
            if (!ModelState.IsValid)
            {
                return View(device);
            }

            device.STATE = true;
            var response = await apiService.InsertPost(device, "CreateDeviceType");
            if (response.CodeError != 200)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                return View(device);
            }

            var usercurrent = apiService.ValidateUser(User.Identity.Name);
            var url = Request.Url.AbsolutePath.Split('/')[1];
            await NotifyHelper.SaveLog(usercurrent, string.Concat("Se creó el tipo de dispositivo ", device.APOSTROPHE), url);
            return RedirectToAction("GetDeviceTypes");
        }

        public async Task<ActionResult> EditDevice(int id)
        {
            var device = new Device();
            var url = string.Concat(Utilities.GetConfiguration("GetDeviceForId"), id);
            var response = await apiService.GetDataV2(url);
            if (response.CodeError == 200)
            {
                device = JsonConvert.DeserializeObject<Device>(response.Data.ToString());
            }

            ViewBag.DEVICE_TYPE_ID = new SelectList(await ComboHelper.GetDevicesType(), nameof(DeviceType.DEVICE_TYPE_ID), nameof(DeviceType.APOSTROPHE), device.DEVICE_TYPE_ID);
            return View(device);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditDevice(Device device)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.DEVICE_TYPE_ID = new SelectList(await ComboHelper.GetDevicesType(), nameof(DeviceType.DEVICE_TYPE_ID), nameof(DeviceType.APOSTROPHE), device.DEVICE_TYPE_ID);
                return View(device);
            }

            if (device.ImagePathFile == null)
            {
                ModelState.AddModelError(string.Empty, "Debe ingresar una imagen");
                ViewBag.DEVICE_TYPE_ID = new SelectList(await ComboHelper.GetDevicesType(), nameof(DeviceType.DEVICE_TYPE_ID), nameof(DeviceType.APOSTROPHE), device.DEVICE_TYPE_ID);
                return View(device);
            }

            device.IMAGE = Utilities.GenerateByteArray(device.ImagePathFile.InputStream);
            device.ImagePathFile = null;
            var response = await apiService.InsertPost(device, "UpdateDevice");
            if (response.CodeError != 200)
            {
                ModelState.AddModelError(string.Empty, response.Message);
                ViewBag.DEVICE_TYPE_ID = new SelectList(await ComboHelper.GetDevicesType(), nameof(DeviceType.DEVICE_TYPE_ID), nameof(DeviceType.APOSTROPHE), device.DEVICE_TYPE_ID);
                return View(device);
            }

            var usercurrent = apiService.ValidateUser(User.Identity.Name);
            var url = Request.Url.AbsolutePath.Split('/')[1];
            await NotifyHelper.SaveLog(usercurrent, string.Concat("Se editó el dispositivo ", device.NAME), url);
            return RedirectToAction("Index");
        }
    }
}