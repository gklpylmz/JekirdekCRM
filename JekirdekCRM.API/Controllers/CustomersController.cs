using AutoMapper;
using JekirdekCRM.BLL.ManagerServices.Abstracts;
using JekirdekCRM.DTO.CustomerDtos;
using JekirdekCRM.ENTITIES.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JekirdekCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerManager _customerManager;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ICustomerManager customerManager, IMapper mapper, ILogger<CustomersController> logger)
        {
            _customerManager = customerManager;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetCustomerListAll()
        {
            try
            {
                var values = _customerManager.GetListAll();
                _logger.LogInformation("Tüm müşteriler listelendi.");
                return Ok(_mapper.Map<List<GetCustomerDto>>(values));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteriler listelenirken hata oluştu.");
                return StatusCode(500, "Müşteriler listelenirken bir hata oluştu.");
            }
            
           
        }

        [HttpGet("GetCustomerByRegion")]
        public IActionResult GetCustomerByRegion(string region)
        {
            try
            {
                var values = _customerManager.GetCustomerByRegion(region);
                _logger.LogInformation("Müşteri bölgeye göre listelendi.");
                return Ok(_mapper.Map<List<GetCustomerDto>>(values));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bölgeye göre müşteri listelenirken hata oluştu.");
                return StatusCode(500, "Müşteriler listelenirken bir hata oluştu.");
            }
            
            
        }
        [HttpGet("GetCustomerByFirstName")]
        public IActionResult GetCustomerByFirstName(string firstName)
        {
            try
            {
                var values = _customerManager.GetCustomerByFirtName(firstName);
                _logger.LogInformation("Müşteri isime göre listelendi.");
                return Ok(_mapper.Map<List<GetCustomerDto>>(values));
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "İsme göre müşteri listelenirken hata oluştu.");
                return StatusCode(500, "Müşteriler listelenirken bir hata oluştu.");
            }
            
        }
        [HttpGet("{id}")]
        public IActionResult GetCustomerById(int id)
        {
            try
            {
                var value = _customerManager.GetById(id);
                if (value ==null)
                {
                    _logger.LogInformation("Belirtilen müşteri bulunamadı.");
                    return NotFound();
                }
                _logger.LogInformation("Belirtilen müşteri getirildi.");
                return Ok(_mapper.Map<GetCustomerDto>(value));
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Müşteri ID ile filtreleme yapılırken hata oluştu.");
                return StatusCode(500, "Müşteri bilgisi alınırken hata oluştu.");
            }
           
        }

        [HttpPut]
        public IActionResult UpdateCustomer(UpdateCustomerDto updateCustomerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Geçersiz güncelleme isteği.");
                    return BadRequest();
                }
                var customer = _customerManager.GetById(updateCustomerDto.Id);

                if (customer == null)
                {
                    _logger.LogWarning("Güncellenmek istenen müşteri bulunamadı.");
                    return NotFound();
                }
                _mapper.Map(updateCustomerDto, customer);
                _customerManager.Update(customer);
                _logger.LogInformation("Müşteri başarıyla güncellendi");
                return Ok(customer);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Müşteri güncellenirken hata oluştu.");
                return StatusCode(500, "Müşteri güncellenirken hata oluştu.");
            }
                       
            
        }

        [HttpPost]
        public IActionResult CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var customer = _mapper.Map<Customer>(createCustomerDto);
                _customerManager.Create(customer);
                return Ok();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Kullanıcı oluşturulurken hata oluştu.");
                return StatusCode(500, "Kullanıcı oluşturulurken bir hata oluştu lütfen tekrar deneyiniz");
            }
            
        }


    }
}
