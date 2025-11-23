using RenStore.Domain.Enums;
using RenStore.Domain.Enums.Clothes;

namespace RenStore.Domain.DTOs.Product.FullPage;

public record ProductClothDto
(
    Guid Id,
    Gender? Gender,
    Season? Season,
    Neckline? Neckline,
    TheCut? TheCut,
    Guid ProductId
);