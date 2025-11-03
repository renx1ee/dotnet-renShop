using System.ComponentModel.DataAnnotations;

namespace RenStore.WebApi.ViewModels;

public class CategoryViewModel
{
    [Required]
    public Guid Id { get; set; }
    [Display(Name = "CategoryEntity Name*")]
    [Required(ErrorMessage = "Please enter CategoryEntity Name!")]
    public string? Name { get; set; }
    [Display(Name = "CategoryEntity Description*")]
    [Required(ErrorMessage = "Please enter CategoryEntity Description!")]
    public string? Description { get; set; }
    [Display(Name = "CategoryEntity Image*")]
    [Required(ErrorMessage = "Please enter CategoryEntity Image!")]
    public string? ImagePath { get; set; }
}