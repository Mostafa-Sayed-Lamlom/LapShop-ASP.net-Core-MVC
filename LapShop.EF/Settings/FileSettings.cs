namespace LapShop.EF.Settings
{
    public class FileSettings
    {
        public const string ImagesPathCategory = @"wwwroot\Admin/upload/images/categories";
        public const string ImagesPathItem = @"wwwroot\Admin/upload/images/items";
        public const string AllowedExtensions = ".jpg,.jpeg,.png,.webp";
        public const int MaxFileSizeInMB = 1;
        public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
    }
}
