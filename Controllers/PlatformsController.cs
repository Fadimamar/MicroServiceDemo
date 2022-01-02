using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Models;
using PlatformService.Dtos;
using PlatformService.Data;

namespace  PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController: ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatfroms()
        {
            Console.WriteLine("-->Getting Platforms..");
            var platformItems = _repository.GetallPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));

        }
        [HttpGet("{id}", Name="GetPlatformByID")]
        public ActionResult<PlatformReadDto> GetPlatfromByID(int id)
        {
           
            var platformItem = _repository.GetPlatformById(id);
            if (platformItem != null)
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));
            }
            return NotFound();

        }
        [HttpPost]
        public ActionResult<PlatformReadDto> CreatePlatform(PlatformCreateDto platformCreateDto )
        {
            var platformModel = _mapper.Map <Platform>(platformCreateDto);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChanges();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
            return CreatedAtRoute(nameof(GetPlatfromByID), new { Id = platformReadDto.ID }, platformReadDto);



        }
        
    }
}