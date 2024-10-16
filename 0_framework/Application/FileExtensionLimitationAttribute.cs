using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace _0_framework.Application;

public class FileExtensionLimitationAttribute : ValidationAttribute, IClientModelValidator
{
    private readonly string[] _validExtentions;

    public FileExtensionLimitationAttribute(string[] validExtentions)
    {
        _validExtentions = validExtentions;
    }

    public override bool IsValid(object? value)
    {
        var file = value as IFormFile;
        if (file == null) return true;
        var fileExtention = Path.GetExtension(file.FileName);
        return _validExtentions.Contains(fileExtention);
    }

    public void AddValidation(ClientModelValidationContext context)
    {
        //context.Attributes.Add("data-val", "true");
        context.Attributes.Add("data-val-fileExtentionLimit", ErrorMessage);
    }
}