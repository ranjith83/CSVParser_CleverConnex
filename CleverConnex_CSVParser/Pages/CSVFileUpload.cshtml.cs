using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AXTConverter.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CleverConnex_CSVParser.Pages
{
    public class CSVFileUploadModel : PageModel
    {
        public IWebHostEnvironment _environment { get; }
        public ICSVFileParser _csvFileParser { get; }

        public CSVFileUploadModel(IWebHostEnvironment environment, ICSVFileParser csvFileParser)
        {
            _environment = environment;
            _csvFileParser = csvFileParser;
        }


        [BindProperty]
        public IFormFile UploadCSV { get; set; }

        public async Task OnPostAsync()
        {
            try
            {
                var postedFileExtension = Path.GetExtension(UploadCSV.FileName);
                if (string.Equals(postedFileExtension, ".axt", StringComparison.OrdinalIgnoreCase))
                {

                    var file = Path.Combine(_environment.WebRootPath, "fileuploads", UploadCSV.FileName);
                    using (var fileStream = new FileStream(file, FileMode.Create))
                    {
                        await UploadCSV.CopyToAsync(fileStream);
                    }


                    _csvFileParser.TransformCSV(file);
                    TempData["success"] = "File Uploaded successfully";
                }
                else
                    TempData["error"] = "Please upload only AXT";
                
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
           

            ModelState.AddModelError("test", "failed");
        }
        
    }
}