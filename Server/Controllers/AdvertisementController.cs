using Server.DTOs.AdvertisementDTOs;
using AutoMapper;
using Servers.DTOs.AdvertisementDTOs;
using Servers.Services.PropertyFactory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DataStoreContract;
using Models.Entities;

namespace Server.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("ad/[controller]/[action]")]
    public class AdvertisementController : ControllerBase
    {
        private readonly IDataStore _dataStore;
        private readonly IMapper _mapper;
        private readonly IPropertyFactory _propertyFactory;
        private readonly IWebHostEnvironment _web;
        public AdvertisementController(IDataStore dataStore, IMapper mapper, IPropertyFactory propertyFactory, IWebHostEnvironment web)
        {
            _dataStore = dataStore;
            _mapper = mapper;
            _propertyFactory = propertyFactory;
            _web = web;
        }

        [HttpGet]
        public async Task<ActionResult> GetDetailedAd(string adId) => Ok(await _dataStore.Advertisements.GetDetailedAd(adId));


        [HttpPut]
        [Authorize(Roles = "Enterprise-Agent,Admin")]
        public async Task<ActionResult> UpdateAdvertisement(string adId, PutAdDTO dto)
        {
            try
            {
                var result = await _dataStore.Advertisements.GetDetailedAd(adId);

                if (result == null)
                    return NotFound();

                List<HouseBaseImagePath> imagePaths = new List<HouseBaseImagePath>();

            }
            catch
            {

            }
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult> AddAdvertisement([FromForm] AdDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<HouseBaseImagePath> imagePaths = new List<HouseBaseImagePath>();

                    var request = await Request.ReadFormAsync();

                    var imagesCount = request.Files.Count;

                    if (imagesCount < 3)
                        return BadRequest("Not Enough images");

                    foreach (var image in request.Files)
                        imagePaths.Add(new HouseBaseImagePath { ImagePath = GenerateImagePath(image) });

                    Property property = _propertyFactory.GetFilledProperty(model, imagePaths);

                    await _dataStore.Advertisements.AddAdvertisement(new Advertisement { Property = property });

                    await _dataStore.CompleteAsync();

                    return Ok();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Internal Error Happened", ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            return BadRequest(ModelState);
        }



        [HttpDelete]
        public async Task<ActionResult> DeleteAdvertisement(string adId)
        {
            var result = await _dataStore.Advertisements.GetDetailedAd(adId);
            
            if (result == null)
            {
                return NotFound();
            }

            if(int.TryParse(adId.Split("-").First(), out int id))
            {
                try
                {
                    _dataStore.Advertisements.Delete(id);

                    await _dataStore.CompleteAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Deleting ad" + ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error While Deletion of ad");
                }

                try
                {
                    DeleteImages(result.HouseBase.HouseBaseImagePaths);
                }
                catch
                {
                    Console.WriteLine("Images not loaded successfully.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Error While Deletion of Images");
                }

                return NoContent();
            }

            return BadRequest();
        }


        //[HttpGet]
        //public async Task<ActionResult> SearchDTO([FromQuery]AdSearchDTO dto)
        //{
        //    var result = await _dataStore.Advertisements.FindAllAsync();
        //}


        private void DeleteImages(IEnumerable<HouseBaseImagePath> hbIps)
        {
            foreach (var hbIp in hbIps)
            {
                try
                {
                    if (System.IO.File.Exists(_web.WebRootPath + hbIp.ImagePath))
                        System.IO.File.Delete(_web.WebRootPath + hbIp.ImagePath);
                }
                catch { Console.WriteLine("Images not loaded successfully."); }
            }
        }
        private string GenerateImagePath(IFormFile file)
        {
            var imagePath = @"\Images\Properties\" + Guid.NewGuid().ToString() + file.FileName;
            var imageFolder = _web.WebRootPath + imagePath;
            using (var fs = new FileStream(imageFolder, FileMode.Create))
                file.CopyTo(fs);
            return imagePath;
        }
    }
}
