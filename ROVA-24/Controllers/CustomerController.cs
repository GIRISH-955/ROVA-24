using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ROVA_24.IService;
using ROVA_24.Model;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Collections.Generic;
using ROVA_24.DTO;
using System.Net;

namespace ROVA_24.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("Add Customers")]
        public async Task<ActionResult<Customer>> AddCustomer([FromBody] CustomerDTO customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var addedCustomer = await _customerService.AddCustomer(customerDto);
                return Ok(addedCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the customer.");
            }
        }
    
        [HttpGet("All Customers")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _customerService.GetAllCustomers();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer(int customerId, [FromBody] UpdateCustomerDTO request)
        {
            try
            {
                var customer = new Customer
                {
                    CustomerId = customerId,
                    Name = request.Name,
                    email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Addresses = request.Addresses.Select(a => new Address
                    {
                        AddressId = a.AddressId,
                        Street = a.Street,
                        City = a.City,
                        State = a.State,
                        Country = a.Country,
                        ZipCode = a.ZipCode
                    }).ToList()
                };

                await _customerService.UpdateCustomer(customerId, customer);

                return Ok(new { status = "success", message = "Updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { status = "error", message = ex.Message });
            }
        }
    [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomer(id);
            return NoContent();
        }
    }
}
