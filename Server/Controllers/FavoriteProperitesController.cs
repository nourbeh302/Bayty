using Servers.DTOs.FavoritePropertiesDTOs;
using EFCoreModelBuilder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DataStoreContract;
using Models.Entities;

namespace Servers.Controllers
{
    public class FavoriteProperitesController : ControllerBase
    {
        private readonly IDataStore _dataStore;
        public FavoriteProperitesController(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddToMyFavorites(FavoritePropertyDTO dto)
        {
            var fp = new FavoriteProperties
            {
                UserId = dto.UserId,
                AdvertisementId = dto.PropertyId
            };
            try
            {
                await _dataStore.FavoriteProperties.AddAsync(fp);
                await _dataStore.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> RemoveFromMyFavorites(FavoritePropertyDTO dto)
        {
            var fp = new FavoriteProperties
            {
                UserId = dto.UserId,
                AdvertisementId = dto.PropertyId
            };

            try
            {
                var result = await _dataStore.FavoriteProperties.FindOneAsync(fp => fp.UserId == dto.UserId && fp.AdvertisementId == dto.PropertyId);

                if (result == null)
                    return NotFound();

                _dataStore.FavoriteProperties.Delete(result.Id);
                await _dataStore.CompleteAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> GetAllMyFavorites(FavoritePropertyDTO dto)
        {
            try
            {
                var result = await _dataStore.FavoriteProperties.FindAllAsync(u => u.UserId == dto.UserId);

                if (result == null)
                {
                    return NotFound();
                }

                //must send data here.
                return Ok();
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
