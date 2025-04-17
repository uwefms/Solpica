using Microsoft.AspNetCore.Mvc;

using FileServer.ViewModel;

namespace FileServer.Controllers
{
	/* The `[Route("api/[controller]")]` attribute in C# is used to define a route template for the
    controller. In this case, it specifies that the controller should be accessible under the `/api/`
    route, where `[controller]` will be replaced with the name of the controller class without the
    "Controller" suffix. */
    [Route("api/[controller]")]
	[ApiController]
	public class FileController : ControllerBase
	{
		[HttpPost("upload"), DisableRequestSizeLimit]        
		public async Task<IActionResult> UploadFile([FromForm] FileUploadModel model)            
        {
            if (model.File == null && model.File!.Length == 0)
            {
                return BadRequest("Ungültige Datei");
            }

            // var folderName = Path.Combine("Resources", "AllFiles");
            var folderName = Path.Combine("Resources", model.FileDir);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }

            var fileName = model.File.FileName;
            
            var fullPath = Path.Combine(pathToSave, fileName);
            var dbPath = Path.Combine(folderName, fileName);

            if (System.IO.File.Exists(fullPath))
            {
                return BadRequest("Datei bereits vorhanden");
            }

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {            
                await model.File.CopyToAsync(stream);
            }

            return Ok(new { dbPath });

		} // end of upload

		[HttpPost("multipleupload"), DisableRequestSizeLimit]        
        public async Task<IActionResult> MultiipleUploadFile([FromForm] MultipleUploadModel model)
        {
            var response = new Dictionary<string, string>();

            if (model.Files == null || model.Files.Count == 0)
            {
                return BadRequest("Ungültige Datei");
            }

            foreach (var file in model.Files)
            {
                // var folderName = Path.Combine("Resources", "AllFiles");
                var folderName = Path.Combine("Resources", model.FileDir);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (!Directory.Exists(pathToSave))
                {
                    Directory.CreateDirectory(pathToSave);
                }

                var fileName = file.FileName;
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);

                if (!System.IO.File.Exists(fullPath))
                {
                    using var memoryStream = new MemoryStream();
                    await file.CopyToAsync(memoryStream);
                    await System.IO.File.WriteAllBytesAsync(fullPath, memoryStream.ToArray());
                    response.Add(fileName, dbPath);
                }
                else
                {
                    response.Add(fileName, "Datei bereits vorhanden!");
                }
            }

            return Ok(new { response });

		} // end of multiple upload

		[HttpGet("download")]                
        public async Task<IActionResult> DownloadByName([FromQuery] string dir, [FromQuery] string filename)
        {            
            var folderName = Path.Combine("Resources", dir);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
         
            var fileName = filename!;
            var fullPath = Path.Combine(pathToSave, fileName);

            if (!System.IO.File.Exists(fullPath))
            {                
                return BadRequest("Datei " + filename + " existiert nicht!");
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            var fileContentResult = new FileContentResult(fileBytes, "application/octet-stream")

            {
                FileDownloadName = fileName,
            };

            return fileContentResult;

		} // end of download by name
  
        [HttpDelete("delete")]
        public IActionResult DeleteFile([FromBody] DirectoryFileModel model)
        {            
            var folderName = Path.Combine("Resources", model.DirName!);
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fileName = model.FileName;
            var fullPath = Path.Combine(pathToSave, fileName!);

            if (!System.IO.File.Exists(fullPath))
            {
                return BadRequest("Datei existiert nicht!");
            }

            System.IO.File.Delete(fullPath);

            return Ok(new { message = "Datei " + model.FileName + " erfolgreich gelöscht!" });

		} // end of delete file

        [HttpPost("createdir")]        
        public IActionResult CreateDir([FromBody] DirectoryModel model)
        {
            if (string.IsNullOrWhiteSpace(model.DirName))
            {
                return BadRequest("Ungültiger Verzeichnisname");
            }

            var folderName = Path.Combine("Resources", model.DirName);
            var pathToCreate = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (Directory.Exists(pathToCreate))
            {
                return BadRequest("Verzeichnis " + model.DirName + " existiert bereits");
            }

            Directory.CreateDirectory(pathToCreate);

            return Ok(new { message = "Verzeichnis " + model.DirName + " erfolgreich erstellt!", path = folderName });
        }

		[HttpDelete("deletedir")]        
		public IActionResult DeleteDir([FromBody] DirectoryModel model)
		{
			if (string.IsNullOrWhiteSpace(model.DirName))
			{
				return BadRequest("Ungültiger Verzeichnisname");
			}
			var folderName = Path.Combine("Resources", model.DirName);
			var pathToDelete = Path.Combine(Directory.GetCurrentDirectory(), folderName);

			if (!Directory.Exists(pathToDelete))
			{
				return BadRequest("Verzeichnis " + model.DirName + " existiert nicht");
			}

			Directory.Delete(pathToDelete, true);
			return Ok(new { message = "Verzeichnis " + model.DirName + " erfolgreich gelöscht!" });
		}

		[HttpGet("getallfilesbydir")]
		// public IActionResult GetAllFilesByDir([FromQuery] string dir)
        public IEnumerable<DirectoryFileModel> GetAllFilesByDir([FromQuery] string dir)
		{
			var folderName = Path.Combine(Directory.GetCurrentDirectory(), "Resources", dir);
			var fullfiles = Directory.GetFiles(folderName);
            var files  = new List<string>();
            
            List<DirectoryFileModel> oRetList1 = new List<DirectoryFileModel>();

			foreach (var file in fullfiles)
			{       
                var oRet = new DirectoryFileModel();

				var tmpfile = Path.GetFileName(file);

                // files.Add(tmpfile);
                oRet.DirName = dir;
                oRet.FileName = tmpfile;

                oRetList1.Add(oRet);

				// files.Add(tmpfile);
			}

			// return Ok(new { files });
            return oRetList1;
		}

        [HttpGet("getalldirs")]        		
        public IEnumerable<DirectoryModel> GetAllDirs()
		{
			var folderName = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
			var fulldirs = Directory.GetDirectories(folderName);
        
            List<DirectoryModel> oRetList = new List<DirectoryModel>();

			foreach (var dir in fulldirs)
			{   
				string lastDir = dir.Split("\\").Last();

                oRetList.Add(new DirectoryModel { DirName = lastDir });                
			}
			
            return oRetList;
		}

		[HttpGet("getallfilesindirs")]		
        public IEnumerable<DirectoryFileModel> GetAllFilesInDir()
		{
			var folderName       = Path.Combine(Directory.GetCurrentDirectory(), "Resources");
			var dirs           = Directory.GetDirectories(folderName);
			var files       = new List<string>();
            var fullfiles   = new List<string>();

            List<DirectoryFileModel> oRetList = new List<DirectoryFileModel>();

			foreach (var dir in dirs)
			{
                fullfiles.AddRange(Directory.GetFiles(dir));
                                
				string lastDir = dir.Split("\\").Last();

				foreach (var fullstring in fullfiles)
				{
                    var oRet = new DirectoryFileModel();

					// get the filename only
					var file = Path.GetFileName(fullstring);

					// files.Add(lastDir + "/" + file);
                    //files.Add(lastDir + "//" + file);
                    oRet.DirName = lastDir;
                    oRet.FileName = file;

                    oRetList.Add(oRet); 
                }

                fullfiles.Clear();
			}

			// return Ok(new { files });
            return oRetList;
		}

	} // end of class

} // end of namespace


/*

// Sample call from client side
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("X-Api-Key", "bubu1");

        var response = await client.GetAsync("http://yourserver/api/file/getalldir");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
        }
    }
} 
*/