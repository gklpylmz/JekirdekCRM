using AutoMapper;
using JekirdekCRM.BLL.ManagerServices.Abstracts;
using JekirdekCRM.DTO.CustomerDtos;
using JekirdekCRM.ENTITIES.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("GetCustomerStatistics")]
        public IActionResult GetCustomerStatistics()
        {
            try
            {
                var customerStats = new CustomerStatisticDto
                {
                    ActiveCustomerCount = _customerManager.GetActiveCustomerCount(),
                    PassiveCustomerCount = _customerManager.GetPassiveCustomerCount(),
                    DistinctRegionCustomerCount = _customerManager.GetDistinctRegionCount(),

                };
                _logger.LogInformation("Müşteri istatistikleri getirildi.");
                return Ok(customerStats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteriler istatistikleri alınırken hata oluştu.");
                return StatusCode(500, "Müşteriler istatistikleri alınırken hata oluştu.");
            }

        }

        [HttpGet("GetCustomerByFirstNameAndRegion")]
        public IActionResult GetCustomerByFirstNameAndRegion(string? firstName,string? region)
        {
            try
            {
                var values = _customerManager.GetCustomerByFirstNameAndRegion(firstName,region);
                _logger.LogInformation("Filtrelere göre müşteriler listelendi.");
                return Ok(_mapper.Map<List<GetCustomerDto>>(values));
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Filtrelere göre müşteri listelenirken hata oluştu.");
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
                    _logger.LogInformation("Belirtilen müşteri bulunamadı. Müşteri Id :"+id);
                    return NotFound();
                }
                _logger.LogInformation("Belirtilen müşteri getirildi.");
                return Ok(_mapper.Map<GetCustomerDto>(value));
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Müşteri ID ile filtreleme yapılırken hata oluştu. Müşteri Id :" + id);
                return StatusCode(500, "Müşteri bilgisi alınırken hata oluştu.");
            }
           
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult UpdateCustomer(UpdateCustomerDto updateCustomerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Geçersiz güncelleme isteği. Müşteri Id :" + updateCustomerDto.Id);
                    return BadRequest();
                }
                var customer = _customerManager.GetById(updateCustomerDto.Id);

                if (customer == null)
                {
                    _logger.LogWarning("Güncellenmek istenen müşteri bulunamadı. Müşteri Id :" + updateCustomerDto.Id);
                    return NotFound();
                }
                _mapper.Map(updateCustomerDto, customer);
                _customerManager.Update(customer);
                _logger.LogInformation("Müşteri başarıyla güncellendi. Müşteri Id :"+updateCustomerDto.Id);
                return Ok(customer);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Müşteri güncellenirken hata oluştu. Müşteri Id :" + updateCustomerDto.Id);
                return StatusCode(500, "Müşteri güncellenirken hata oluştu.");
            }
                       
            
        }
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [HttpDelete("soft/{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                var value = _customerManager.GetById(id);
                if (value ==null)
                {
                    _logger.LogWarning("Müşteri bulunamadı. Müşteri Id : "+id);
                    return NotFound("Müşteri bulunamadı");
                }

                _customerManager.Delete(value);
                _logger.LogInformation("Müşteri başarıyla pasife çekildi. Müşteri Id : " + id);
                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri pasife çekme işlemi sırasında hata oluştu. Müşteri Id : " + id);
                return StatusCode(500,"Sunucu hatası oluştu");
            }
          
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("hard/{id}")]
        public IActionResult DestroyCustomer(int id)
        {
            try
            {
                var value = _customerManager.GetById(id);
                if (value == null)
                {
                    _logger.LogWarning("Müşteri bulunamadı. Müşteri Id : " + id);
                    return NotFound("Müşteri bulunamadı");
                }

                _customerManager.Destroy(value);
                _logger.LogInformation("Müşteri başarıyla silindi. Müşteri Id : " + id);
                return Ok();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Müşteri silme işlemi sırasında hata oluştu. Müşteri Id : " + id);
                return StatusCode(500, "Sunucu hatası oluştu");
            }

        }
    }
}
