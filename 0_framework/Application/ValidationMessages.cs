namespace _0_framework.Application;

/**
 * create static fields for form validation error messages
 */
public static class ValidationMessages
{
    public const string IsRequired = "این مقدار نمی تواند خالی باشد";
    public const string MaxFileSize = "فایل حجیم تر از حد مجاز است";
    public const string InvalidFileFormat = "فرمت فایل مجاز نیست";
    public const string MaxLength = "مقدار وارد شده بیش از طول مجاز است";
}