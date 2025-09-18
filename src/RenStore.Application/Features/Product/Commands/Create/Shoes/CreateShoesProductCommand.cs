using MediatR;
using RenStore.Domain.Entities;
using RenStore.Domain.Entities.Products;

namespace RenStore.Application.Features.Product.Commands.Create.Shoes;

public class CreateShoesProductCommand : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public decimal OldPrice { get; set; }
    public string Description { get; set; }
    public uint InStock { get; set; }
    public string ImagePath { get; set; }
    public string ImageMiniPath { get; set; }
    public int SellerId { get; set; }
    public ProductDetail ProductDetail { get; set; }
    public ShoesProduct ShoesProduct { get; set; }
}