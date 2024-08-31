using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalksAPI.CustomActionFlilters;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;
using NZWalksAPI.Models.DTO;
using NZWalksAPI.Repositories;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
namespace NZWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbcontext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbcontext,IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbcontext = dbcontext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [ValidateModel]
        [Authorize(Roles = "Reader,Writer")]
        public async Task<IActionResult> GeAll()
        {
            //var regionsDomain = await dbcontext.Regions.ToListAsync();
            //var regionsDto = new List<RegionDto>();
            //foreach (var regionDomain in regionsDomain)
            //{
            //    regionsDto.Add(new RegionDto()
            //    {
            //        Id = regionDomain.Id,
            //        Name = regionDomain.Name,
            //        Code = regionDomain.Code,
            //        RegionImageURL = regionDomain.RegionImageURL,
            //    });
            //}
            var regionsDomain = await regionRepository.GetAllAsync();
            // var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
        }
        [HttpGet]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Reader,Writer")]
        public async  Task<IActionResult> GetById([FromRoute] Guid id) 
        {
            //var regions = dbcontext.Regions.Find(id);
            //var regionDomain = await dbcontext.Regions.FirstOrDefaultAsync(r => r.Id == id);
            var regionDomain = await regionRepository.GetById(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            else
            {
                //var regionsDto = new RegionDto
                //{
                //    Id =regionDomain.Id,
                //    Name = regionDomain.Name,
                //    Code = regionDomain.Code,
                //    RegionImageURL=regionDomain.RegionImageURL,
                //};
                return Ok(mapper.Map<RegionDto>(regionDomain));
            }
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto) 
        {
            //var regionDomainModel = new Region
            //{
            //    Code = addRegionRequestDto.Code,
            //    Name = addRegionRequestDto.Name,
            //    RegionImageURL = addRegionRequestDto.RegionImageURL,
            //};
            //await dbcontext.Regions.AddAsync(regionDomainModel);
            //await dbcontext.SaveChangesAsync();
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageURL = regionDomainModel.RegionImageURL,
            //};
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
                await regionRepository.CreateAsync(regionDomainModel);
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //var regionDomainModel = await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            //var regionDomainModel = new Region
            //{
            //    Code = updateRegionRequestDto.Code,
            //    Name = updateRegionRequestDto.Name,
            //    RegionImageURL = updateRegionRequestDto.RegionImageURL,
            //};
            //regionDomainModel.Code = updateRegionRequestDto.Code;
            //regionDomainModel.Name = updateRegionRequestDto.Name;
            //regionDomainModel.RegionImageURL = updateRegionRequestDto.RegionImageURL;
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageURL = regionDomainModel.RegionImageURL,
            //};
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<RegionDto>(regionDomainModel));

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) 
        {
            //var regionDomainModel = await dbcontext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            //dbcontext.Regions.Remove(regionDomainModel);
            //await dbcontext.SaveChangesAsync();
            //var regionDto = new RegionDto
            //{
            //    Id = regionDomainModel.Id,
            //    Code = regionDomainModel.Code,
            //    Name = regionDomainModel.Name,
            //    RegionImageURL = regionDomainModel.RegionImageURL,
            //};
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }
}
