using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ROVA_24.DTO.AddressDTO;
using ROVA_24.DTO.DeleteAddressDTO;
using ROVA_24.DTO.UpdateAddressDTO;
using ROVA_24.IServices;

namespace ROVA_24.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressServices addressServices;
        public AddressController(IAddressServices addressServices)
        {
            this.addressServices = addressServices;
        }
        [EnableCors("CORSPolicy")]
        [HttpPost("AddAddress")]
        public async Task<IActionResult> addAddressAsync(AddressRequestDTO addressRequestDTO)
        {
            var result = await addressServices.AddAddressAsync(addressRequestDTO);
            if (result.Success)
                return Ok(result);
            if(result.Success && result.Message == "Customer does not exist")
                return BadRequest(result.Message);
            return BadRequest(result);

        }
        
        [EnableCors("CORSPolicy")]
        [HttpGet("getAllAddresses")]
        public async Task<IActionResult> GetAllAddressesAsync()
        {
            var result = await addressServices.GetAllAddressAsync();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [EnableCors("CORSPolicy")]
        [HttpGet("getAddress{addressId}")]
        public async Task<IActionResult> GetAddressByIdAsync(int addressId)
        {
            var result = await addressServices.GetAddressesByIdAsync(addressId);
            if (result.Success)
                return Ok(result);
            if(result.Success&& result.Message == "Address not found")
                 return BadRequest(result);
            return BadRequest(result);
        }
        [EnableCors("CORSPolicy")]
        [HttpPut("{addressId}")]
        public async Task<IActionResult> UpdateAddressByIdAsync(int addressId, [FromBody] UpdateAddressDTO updateAddressDTO)
        {
            var result = await addressServices.UpdateAddressesByIdAsync(addressId,updateAddressDTO);
            if (result.Success)
                return Ok(result);
            if (result.Success && result.Message == "Customer does not exist")
                return BadRequest(result);
            if (result.Success && result.Message == "Address does not exist for this customer")
                return BadRequest(result);
            return BadRequest(result);
        }
        [EnableCors("CORSPolicy")]
        [HttpDelete("DeleteAddress")]
        public async Task<IActionResult> DeleteAddressByIdAsync(int addressId)
        {
            var result = await addressServices.DeleteAddressByIdAsync(addressId);
            if(result.Success)
                return Ok(result);
            if (result.Success && result.Message == "Address Does not exist")
                return BadRequest(result);
            return BadRequest(result);
        }

    }
}
