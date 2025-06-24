using MediatR;
using RenStore.Domain.Entities;
using RenStore.Domain.Entities.Products;

namespace RenStore.Application.Features.Product.Commands.Update;

public class UpdateProductCommand : IRequest
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public decimal OldPrice { get; set; }
    public string Description { get; set; }
    public uint InStock { get; set; }
    public string? ImagePath { get; set; }
    public string ImageMiniPath { get; set; }
    public string CategoryName { get; set; }
    public uint SellerId { get; set; }
    public ProductDetail ProductDetail { get; set; }
    public ClothesProduct ClothesProduct { get; set; }
}
