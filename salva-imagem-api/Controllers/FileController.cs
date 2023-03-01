using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

namespace api.Controllers;

[ApiController]
[Route("api/[action]")]
public class FileController : ControllerBase
{
    private readonly IWebHostEnvironment _hostingEnvironment;
    public FileController(IWebHostEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }

    [HttpPost(Name = "SendFile")]
    public string SendFile([FromForm] FileViewModel viewmodel)
    {
        if(viewmodel.File != null){
            string caminho = UploadFile(viewmodel.File);
            return "deu boa: "+caminho;
            //salva num banco de dados o caminho
        }       
        return ";/";
    }

    private string UploadFile(IFormFile file){

        string uploadsFolder = _hostingEnvironment.ContentRootPath + "/images";
        
        var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

        string filePath = uploadsFolder + "/" + uniqueFileName;

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(fileStream);
        }
        return filePath;
    }
}
