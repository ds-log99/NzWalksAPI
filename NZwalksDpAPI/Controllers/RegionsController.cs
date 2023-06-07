using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZwalksDpAPI.Data;
using NZwalksDpAPI.Models.Domain;
using NZwalksDpAPI.Models.DTO;
using NZwalksDpAPI.Repositories;
using NZwalksDpAPI.CustomActionFilters;

namespace NZwalksDpAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationDbContext db;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(ApplicationDbContext _db, IRegionRepository regionRepository, IMapper mapper)
        {
            this.db = _db;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
             var regionsDomaain = await regionRepository.GetAllAsync();

            return Ok(mapper.Map<List<RegionDto>>(regionsDomaain));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var region = await regionRepository.GetByIdAsync(id);
            if (region == null) {
                return NotFound("Region Not found 404");
            }

            return Ok(mapper.Map<RegionDto>(region));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] CreateRegionDto addRegion)
        {  
               // map dto to domain model
                var regionDmoain = mapper.Map<Region>(addRegion);
                // use domain model to create a record in the database 
                regionDmoain = await regionRepository.CreateAsync(regionDmoain);

                // map Domain model back to Dto
                var regionDto = mapper.Map<RegionDto>(regionDmoain);

                return CreatedAtAction(nameof(GetById), new { id = regionDmoain.Id }, regionDto);       
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegion ) {
            if (ModelState.IsValid)
            {
                var regionModel = mapper.Map<Region>(updateRegion);

                regionModel = await regionRepository.UpdateAsync(id, regionModel);
                if (regionModel == null)
                {
                    return NotFound("Region Not found 404");
                }

                var regionDto = mapper.Map<RegionDto>(regionModel);

                return Ok(regionDto);
            }
            else return BadRequest(ModelState);


        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {

            var regionModel = await regionRepository.DeleteAsync(id);            
            if (regionModel == null)
            {
                return NotFound("Region Not found 404");
            }

            var regionDto = mapper.Map<RegionDto>(regionModel);

            return Ok(regionDto);
        }
    }
}
