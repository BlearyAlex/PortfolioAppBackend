using AutoMapper;
using PortfolioAppBackend.DTO;
using PortfolioAppBackend.Models;
using PortfolioAppBackend.Repositories.Interfaces;
using PortfolioAppBackend.Services.Interfaces;

namespace PortfolioAppBackend.Services
{
    public class ImageService : IImageService
    {
        private readonly IGenericRepository<Image> _imageRepository;
        private readonly IMapper _mapper;

        public ImageService(IGenericRepository<Image> imageRepository, IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<List<ImageDTO>> Lista()
        {
            try
            {
                var queryImage = await _imageRepository.Lista();
                var imageDto = _mapper.Map<List<ImageDTO>>(queryImage);
                return imageDto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ImageDTO> GetById(int id)
        {
            try
            {
                var image = await _imageRepository.Obtener(i => i.IdImage == id);
                var imageGet = _mapper.Map<ImageDTO>(image);
                return imageGet;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string> SaveImageAndGetUrl(IFormFile imageFile)
        {
            try
            {
                // Generar un nombre único para la imagen
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(imageFile.FileName)}";
                var filePath = Path.Combine("wwwroot/images", fileName);

                // Guardar la imagen en el servidor
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                // Devolver la URL de la imagen
                return $"/images/{fileName}";
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsImage(IFormFile file)
        {
            var formatosPermitidos = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return formatosPermitidos.Contains(extension);
        }

        public async Task<ImageDTO> Create(ImageDTO imageDto)
        {
            try
            {
                if(imageDto.ImageFile == null || imageDto.ImageFile.Length ==0)
                    throw new Exception("No se ha proporcionado ninguna imagen");

                if(!IsImage(imageDto.ImageFile))
                    throw new Exception("El archivo proporcionado no es una imagen");

                // Guardar la imagen en el servidor y obtener la URL
                var imageUrl = await SaveImageAndGetUrl(imageDto.ImageFile);

                // Asignar la URL de la imagen al DTO
                imageDto.Url = imageUrl;

                // Mapear el DTO a la entidad y guardarla en la base de datos
                var imageEntity = _mapper.Map<Image>(imageDto);
                var imageCreated = await _imageRepository.Crear(imageEntity);

                // Asignar el ID de la imagen creada en la base de datos al DTO
                imageDto.IdImage = imageCreated.IdImage;

                return imageDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Update(ImageDTO imageDto)
        {
            try
            {
                // Validar si se proporciona un archivo de imagen
                if (imageDto.ImageFile != null && imageDto.ImageFile.Length > 0)
                {
                    if (!IsImage(imageDto.ImageFile))
                    {
                        throw new Exception("El archivo subido no es una imagen válida.");
                    }

                    // Guardar la nueva imagen en el servidor y obtener la nueva URL
                    var imageUrl = await SaveImageAndGetUrl(imageDto.ImageFile);
                    imageDto.Url = imageUrl;
                }

                // Buscar la imagen existente en la base de datos
                var imageExisting = await _imageRepository.ObtenerById(imageDto.IdImage);
                if (imageExisting == null)
                {
                    throw new Exception("La imagen no existe");
                }

                // Mapear los nuevos datos al modelo de entidad
                imageExisting.Name = imageDto.Name;
                imageExisting.Type = imageDto.Type;
                imageExisting.Url = imageDto.Url ?? imageExisting.Url; // Mantener la URL existente si no se proporciona un nuevo archivo

                // Actualizar la imagen en el repositorio
                var successfulupdate = await _imageRepository.Editar(imageExisting);

                return successfulupdate; // Retorna true si la actualización fue exitosa, de lo contrario false
            }
            catch (Exception)
            {

                throw; 
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var imageFound = await _imageRepository.Obtener(i => i.IdImage == id);

                if (imageFound == null)
                    throw new Exception("La imagen no existe");

                var result = await _imageRepository.Eliminar(imageFound);

                if (!result)
                    throw new Exception("Error al eliminar la imagen");

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la imagen", ex);
            }
        }
    }
}
