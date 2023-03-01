using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

namespace api.Controllers;

[ApiController]
[Route("api/[action]")]
public class FileController : ControllerBase
{
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly AppContext _context;
    public FileController(IWebHostEnvironment hostingEnvironment, AppContext context)
    {
        _context = context;
        _hostingEnvironment = hostingEnvironment;
    }
    [HttpGet(Name = "GetFiles")]
    public IActionResult GetFiles()
    {
       return Ok(_context.Files.ToList());
    }

    [HttpPost(Name = "SendFile")]
    public IActionResult SendFile([FromForm] FileViewModel viewmodel)
    {

        if(viewmodel.File != null){
           try{

                string caminho = UploadFile(viewmodel.File);
                Console.WriteLine(caminho);
                var file = new AppFile{
                    Id = Guid.NewGuid(),
                    CaminhoDoArquivo = caminho
                };
                _context.Files.Add(file);
                _context.SaveChanges();
                return Ok();
           }catch(Exception e){
               return BadRequest(e.Message);
           }
        }       
        return BadRequest();
    }

    private string UploadFile(IFormFile file){

        string uploadsFolder = _hostingEnvironment.ContentRootPath + "/wwwroot/images";
        
        var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

        string filePath = uploadsFolder + "/" + uniqueFileName;

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            file.CopyTo(fileStream);
        }
        return "images/"+uniqueFileName;
    }
}
