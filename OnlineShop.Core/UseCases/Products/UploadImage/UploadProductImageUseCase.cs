using OnlineShop.Core.Interfaces;
using OnlineShop.Core.Models;
using OnlineShop.Core.UseCases.Upload;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace OnlineShop.Core.UseCases.Products.UploadImage;

public class UploadProductImageUseCase(
    IProductImageRepository imageRepository,
    IUseCase<UploadFileRequest, UploadFileResponse> uploadFileUseCase)
    : IUseCase<UploadProductImageRequest, UploadProductImageResponse>
{
    public async Task<UploadProductImageResponse> Execute(UploadProductImageRequest request)
    {
        if (request.File == null || request.File.Length == 0)
            throw new ArgumentException("File is empty.");

        if (!request.File.ContentType.StartsWith("image/", StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Invalid file type. Only images are allowed.");

        var uploadResponse = await uploadFileUseCase.Execute(
            new UploadFileRequest { File = request.File }
        );

        Image<Rgba32> image;
        try
        {
            image = await Image.LoadAsync<Rgba32>(uploadResponse.FilePath);
        }
        catch (Exception)
        {
            File.Delete(uploadResponse.FilePath);
            throw new ArgumentException("Failed to process the uploaded image. Ensure it is a valid image format.");
        }

        using (image)
        {
            if (image.Width < 600)
            {
                File.Delete(uploadResponse.FilePath);
                throw new ArgumentException("Image width must be at least 600px.");
            }

            double ratio = (double)image.Width / image.Height;
            if (ratio < 4.0 / 3 || ratio > 16.0 / 9)
            {
                File.Delete(uploadResponse.FilePath);
                throw new ArgumentException("Image aspect ratio must be between 4:3 and 16:9.");
            }
        }

        var productImage = new ProductImage
        {
            FileName = uploadResponse.FileName,
            FilePath = uploadResponse.FilePath,
            Size = uploadResponse.Size,
            CreatedAt = uploadResponse.CreatedAt
        };

        await imageRepository.AddAsync(productImage);

        return new UploadProductImageResponse
        {
            Id = productImage.Id,
            FileName = productImage.FileName,
            FilePath = productImage.FilePath,
            Size = productImage.Size,
            CreatedAt = productImage.CreatedAt
        };
    }
}
