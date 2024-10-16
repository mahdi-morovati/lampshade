using System.ComponentModel.DataAnnotations;
using _0_framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.Product;

namespace ShopManagement.Application.Contracts.ProductPicture;

public class CreateProductPicture
{
    [Range(1, 100000, ErrorMessage = ValidationMessages.IsRequired)]
    public long ProductId { get; set; }

    [FileExtensionLimitation(new string[] { ".jpeg", ".jpg", ".png" }, ErrorMessage = ValidationMessages.InvalidFileFormat)]
    [MaxFileSize(1 * 1024 * 1024, ErrorMessage = ValidationMessages.MaxFileSize)]
    public IFormFile Picture { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string PictureAlt { get; set; }

    [Required(ErrorMessage = ValidationMessages.IsRequired)]
    public string PictureTitle { get; set; }

    public bool IsRemoved { get; set; }
    public List<ProductViewModel> Products { get; set; }
}