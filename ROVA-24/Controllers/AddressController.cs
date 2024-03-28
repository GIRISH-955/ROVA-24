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
        public async Task<IActionResult> addAddress(AddressRequestDTO addressRequestDTO)
        {
            var result = await addressServices.addAddressAsync(addressRequestDTO);
            if (result.Success)
                return Created("", new { status = "success", message = "Add successfully" });
            if (!result.Success && result.Message == "Customer does not exist")
                return BadRequest(new { status = "error", message = "Customer not found" });
            return BadRequest(new { status = "error", message = "Invalid request payload" });
        }

        [EnableCors("CORSPolicy")]
        [HttpGet("getAllAddresses")]
        public async Task<IActionResult> getAllAddresses()
        {
            var result = await addressServices.getAllAddressAsync();
            if (result.Success)
                return Ok(result.Data);
            return BadRequest(result.Message);
        }

        [EnableCors("CORSPolicy")]
        [HttpGet("getAddress{addressId}")]
        public async Task<IActionResult> getAddressById(int addressId)
        {
            var result = await addressServices.getAddressesByIdAsync(addressId);
            if (result.Success)
                return Ok(result.Data);
            if (!result.Success && result.Message == "Address not found")
                return BadRequest(new { status = "error", message = "Address not found" });
            return BadRequest(new { status = "error", message = "Invalid request payload" });
        }

        [EnableCors("CORSPolicy")]
        [HttpPut("{addressId}")]
        public async Task<IActionResult> updateAddressById(int addressId, [FromBody] UpdateAddressDTO updateAddressDTO)
        {
            var result = await addressServices.updateAddressesByIdAsync(addressId,updateAddressDTO);
            if (result.Success)
                return Ok(new { status = "success", message = "Update successfully" });
            if (!result.Success && result.Message == "Customer does not exist")
                return BadRequest(new { status = "error", message = "Customer not found" });
            if (!result.Success && result.Message == "Address does not exist for this customer")
                return BadRequest(new { status = "error", message = "Address not found for this customer" });
            return BadRequest(new { status = "error", message = "Invalid request payload" });
        }

        [EnableCors("CORSPolicy")]
        [HttpDelete("DeleteAddress")]
        public async Task<IActionResult> deleteAddressById(int addressId)
        {
            var result = await addressServices.deleteAddressByIdAsync(addressId);
            if (result.Success)
                return Ok(new { status = "success", message = "Address is deleted" });
            if (result.Success && result.Message == "Address Does not exist")
                return BadRequest(new { status = "error", message = "Address Does not exist" });
            return BadRequest(new { status = "error", message = "Invalid request payload" });
        }

    }
}
