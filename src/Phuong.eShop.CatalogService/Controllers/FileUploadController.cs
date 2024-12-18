using System.Formats.Asn1;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Phuong.eShop.CatalogService.Application.CatalogItems.Queries;

namespace Phuong.eShop.CatalogService.Controllers;
static class Constants
{
    public const long TwoMegabytes = 2097152;
}

[Route("api/files")]
public class FileUploadController(ICatalogDbContext context, IWebHostEnvironment env, IUserService userService) : BaseApiController
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
        return Ok("Upload Successfully");
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

    [HttpGet("{id:int}/pic")]
    public async Task<IActionResult> GetItemPicture(int id)
    {
        var catalogItem = await Mediator.Send(new GetCatalogItemByIdQuery(id));
        if (catalogItem is null)
        {
            return NotFound();
        }
        var path = Path.Combine(env.ContentRootPath, "Pics", $"{id}.webp");
        return PhysicalFile(path, "image/webp");
    }

    [HttpPost("uploadCsvFile")]
    public async Task<IActionResult> UploadCsvFile(IFormFile formFile, CancellationToken cancellationToken)
    {
        var catalogItemList = new List<CatalogCsvItem>();
        using (var memoryStream = new MemoryStream())
        {
            await formFile.CopyToAsync(memoryStream, cancellationToken);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
            };
            memoryStream.Position = 0;
            using (var reader = new StreamReader(memoryStream))
            using (var csv = new CsvReader(reader, config))
            {
                while (csv.Read())
                {
                    var record = csv.GetRecord<CatalogCsvItem>();
                    catalogItemList.Add(record);
                }
            }
        }
        foreach (var catalogItem in catalogItemList)
        {
            var catalogItemEntity = new CatalogItem
            {
                Name = catalogItem.Name,
                Price = catalogItem.Price,
                Description = catalogItem.Description,
                CatalogBrandId = catalogItem.CatalogBrandId,
                CatalogTypeId = catalogItem.CatalogTypeId,
                AvailableStock = catalogItem.AvailableStock,
                PictureUri = catalogItem.PictureUri ?? string.Empty,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = userService.Name,
            };
            context.CatalogItems.Add(catalogItemEntity);
        }
        await context.SaveChangesAsync(cancellationToken);
        return Ok("Upload Successfully");
    }
}
