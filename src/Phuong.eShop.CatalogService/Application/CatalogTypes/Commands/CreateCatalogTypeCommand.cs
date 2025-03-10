﻿using Phuong.eShop.CatalogService.Application.CatalogTypes.Models;

namespace Phuong.eShop.CatalogService.Application.CatalogTypes.Commands
{
    public record CreateCatalogTypeCommand(string Name) : IRequest<ApiResponse<CatalogTypeDto>>;

    public class CreateCatalogTypeCommandHandler(ICatalogDbContext context, IUserService userService) : IRequestHandler<CreateCatalogTypeCommand, ApiResponse<CatalogTypeDto>>
    {
        public async Task<ApiResponse<CatalogTypeDto>> Handle(CreateCatalogTypeCommand request, CancellationToken cancellationToken)
        {
            var catalogType = new CatalogType
            {
                Name = request.Name,
                CreatedAt = DateTime.UtcNow,
                CreatedBy =  userService.Name,
            };
            context.CatalogTypes.Add(catalogType);
            await context.SaveChangesAsync(cancellationToken);
            return catalogType.Adapt<CatalogTypeDto>();
        }
    }
}
