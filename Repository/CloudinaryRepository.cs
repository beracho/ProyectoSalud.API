using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using ProyectoSalud.API.Data;
using ProyectoSalud.API.Repository.Interfaces;
using ProyectoSalud.API.Helpers;
using ProyectoSalud.API.Models;

namespace ProyectoSalud.API.Repository
{
    public class CloudinaryRepository : ICloudinaryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;
        private readonly ILogger<CloudinaryRepository> _logger;
        public CloudinaryRepository(DataContext context, IConfiguration config, ILogger<CloudinaryRepository> logger, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _config = config;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<Photo> UploadImage(IFormFile imageFile, string route, int personId)
        {
            var imageObject = new Photo();
            imageObject.DateAdded = DateTime.Now;
            try
            {
                var uploadResult = new ImageUploadResult();
                var file = imageFile;

                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, stream),
                        Folder = route,
                        // Transformation = new Transformation().Quality(50).Crop("limit")
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
                imageObject.Url = uploadResult.SecureUrl.ToString();
                imageObject.PublicId = uploadResult.PublicId;
                imageObject.PersonId = personId;

                _context.Photos.Update(imageObject);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("upload_image_failed");
            }
            return imageObject;
        }
    }
}