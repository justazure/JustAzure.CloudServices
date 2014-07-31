using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace LocalResourceDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult UploadZipFile(HttpPostedFileBase zipFile)
        {
            var localResource = RoleEnvironment.GetLocalResource("ZipFiles");

            // Save zip file.
            var localZipFilePath = Path.Combine(localResource.RootPath, zipFile.FileName);
            zipFile.SaveAs(localZipFilePath);

            // Extract zip file to a random folder.
            var localExtractedPath = Path.Combine(localResource.RootPath, Guid.NewGuid().ToString());
            Directory.CreateDirectory(localExtractedPath);
            System.IO.Compression.ZipFile.ExtractToDirectory(localZipFilePath, localExtractedPath);

            // Upload everything to blob storage.
            foreach (var file in Directory.GetFiles(localExtractedPath, "*", SearchOption.AllDirectories))
            {
                // Upload to blob storage.
            }

            return RedirectToAction("Index");
        }
    }
}