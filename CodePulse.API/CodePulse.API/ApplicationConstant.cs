namespace CodePulse.API
{
    public static class ApplicationConstant
    {
        /// <summary>
        /// List of allowed image file extensions in our app
        /// </summary>
        public static readonly string[] allowedImageExtensions = new string[] {
            ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" 
        };

        /// <summary>
        /// Limit size for Image upload to 10MB
        /// </summary>
        public static readonly long maxImgageSize = 10 * 1024 * 1024; //10MB
    }
}
