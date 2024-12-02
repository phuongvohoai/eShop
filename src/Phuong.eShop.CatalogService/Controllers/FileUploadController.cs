namespace Phuong.eShop.CatalogService.Controllers;
static class Constants
{
    public const long TwoMegabytes = 2097152;
}

[Route("api/files")]
public class FileUploadController(ICatalogDbContext context) : BaseApiController
{
    [HttpPost("upload")]
    public async Task<IActionResult> OnPostUploadAsync(IFormFile formFile, CancellationToken cancellationToken)
    {
        using (var memoryStream = new MemoryStream())
        {
            await formFile.CopyToAsync(memoryStream, cancellationToken);
            if (memoryStream.Length < Constants.TwoMegabytes)
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

    [HttpGet("{id}/content")]
    public async Task<IActionResult> GetImageStream(long id, CancellationToken cancellationToken)
    {
        var file = await context.ProductFiles.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        if (file == null)
        {
            return NotFound();
        }
        string fileType = MimeMapping.MimeUtility.GetMimeMapping(file.FileName);
        return File(file.Content, fileType);
    }
}

