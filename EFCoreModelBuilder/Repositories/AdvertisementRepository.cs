using Microsoft.EntityFrameworkCore;
using Models.Constants;
using Models.Entities;
using Models.IRepositories;
using EFCoreModelBuilder.Helpers;

namespace EFCoreModelBuilder.Repositories
{
    public class AdvertisementRepository : 
        GenericRepository<Advertisement, int> , IAdvertismentRepository
    {
        private readonly DataContext _context;

        public AdvertisementRepository(DataContext context) : base(context) => _context = context;

        public async Task AddAdvertisement(Advertisement ad) => await _context.AddAsync(ad.Property);

        public async Task<IEnumerable<Advertisement>> GetTwentyAd(int pageSize, int pageNumber)
        {
            return await _context.Advertisements.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<Advertisement>> SearchAds(float? minPrice, float? maxPrice, PropertyType? type, OrderDirection? order, bool? isRent)
        {
            var query = _context.Advertisements.Include("HouseBase").AsQueryable();

            if (minPrice.HasValue)
            {
                query = query.Where(a => a.HouseBase.Price >= minPrice);
            }

            if (maxPrice.HasValue) 
            {
                query = query.Where(a => a.HouseBase.Price <= maxPrice);
            }

            if (type.HasValue)
            {
                query = query.Where(a => a.PropertyType == type);
            }

            if (isRent.HasValue)
            {
                query = query.Where(a => a.HouseBase.IsForRent == isRent);
            }


            if (order.HasValue)
            {
                switch(order.Value)
                {
                    case OrderDirection.Ascending:
                        query = query.OrderBy(a => a.HouseBase.Price);
                        break;
                    case OrderDirection.Descending:
                        query = query.OrderByDescending(a => a.HouseBase.Price);
                        break;
                    default:
                        break;
                }
            }
            return await query.ToListAsync();
            
        }

        public async Task<Advertisement> GetDetailedAd(string adId)
        {
            int id = int.Parse(adId.Split("-").FirstOrDefault()!);
            
            Advertisement ad;
            
            Property property;

            switch (adId.Split("-").Last())
            {
                case "00":
                    var building = await _context.Buildings.Include(b => b.HouseBase)
                        .ThenInclude(hb => hb.Advertisement).ThenInclude(b=>b.HouseBase.HouseBaseImagePaths).FirstOrDefaultAsync(c => c.HouseBase.Advertisement.Id == id);
                    ad = building.HouseBase.Advertisement;
                    ad.Property = building;
                    ad.PropertyType = PropertyType.Building;
                    break;
                case "11":
                    var villa = await _context.Villas.Include(b => b.HouseBase)
                        .ThenInclude(hb => hb.Advertisement).ThenInclude(b => b.HouseBase.HouseBaseImagePaths).FirstOrDefaultAsync(c => c.HouseBase.Advertisement.Id == id);
                    ad = villa.HouseBase.Advertisement;
                    ad.Property = villa;
                    ad.PropertyType = PropertyType.Villa;
                    break;
                case "22":
                    var apartment = await _context.Apartments.Include(b => b.HouseBase)
                        .ThenInclude(hb => hb.Advertisement).ThenInclude(b => b.HouseBase.HouseBaseImagePaths).FirstOrDefaultAsync(c => c.HouseBase.Advertisement.Id == id);
                    ad = apartment.HouseBase.Advertisement;
                    ad.Property = apartment;
                    ad.PropertyType = PropertyType.Apartment;
                    break;
                default:
                    return null;
            }

            return ad;

        }
    }
}
