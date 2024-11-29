using Microsoft.AspNetCore.Http.HttpResults;

namespace Phuong.eShop.CatalogService.Controllers;

[Route("api/files")]
public class FileUploadController(ICatalogDbContext context) : BaseApiController
{
    [HttpPost("upload")]
    public async Task<IActionResult> OnPostUploadAsync(IFormFile formFile, CancellationToken cancellationToken)
    {
        using (var memoryStream = new MemoryStream())
        {
            await formFile.CopyToAsync(memoryStream, cancellationToken);
            if (memoryStream.Length < 2097152)
            {
                var file = new ProductFile()
                {
                    FileName = formFile.FileName,
                    Content = memoryStream.ToArray()
                };
                context.ProductFiles.Add(file);
                await context.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return BadRequest();
            }
        }
        return Ok();
    }

    [HttpGet("view/{Id:long}")]
    public async Task<IActionResult> GetImageStream(long Id, CancellationToken cancellationToken)
    {
        var file = await context.ProductFiles.FirstOrDefaultAsync(e => e.Id == Id, cancellationToken);
        if (file == null)
        {
            return NotFound();
        }
        return File(file.Content, "image/jpeg");
    }

}

