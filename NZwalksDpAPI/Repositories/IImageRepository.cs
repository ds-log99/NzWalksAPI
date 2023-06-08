using NZwalksDpAPI.Models.Domain;

namespace NZwalksDpAPI.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
