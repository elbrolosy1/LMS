namespace BLL.Setting
{
	public static class FileSetting
	{
		public const string ImagesPath = "Assest/Images";

		public const string AllowedImages= ".jpg,.jpeg,.png";

		public const int MaxImageSizeMB = 1;

		public const int MaxImageSizeByte = MaxImageSizeMB * 1024 * 1024;
	}
}
