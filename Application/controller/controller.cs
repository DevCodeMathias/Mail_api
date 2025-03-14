using mail_api.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using mail_api.Domain.Interfaces;
using mail_api.Domain.Model;

namespace mail_api.Application.controller
{

    [ApiController]
    [Route("api/[controller]")]
    public class Cepcontroller : ControllerBase
    {

        private readonly ICepService _cepService;
        public Cepcontroller(ICepService cepService)
        {
            _cepService = cepService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCep(cepRequest cepRequest)
        {
            try
            {


                if (!Request.Headers.ContainsKey("Cep"))
                {
                    return BadRequest("CEP header is missing.");
                }

                string cep = Request.Headers["Cep"].ToString();


                var result = await _cepService.GetByCep(cepRequest.Cep);
                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound("CEP not found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] cepRequest cepRequest)
        {
            try
            {

                bool isCepInDatabase = await _cepService.PostAddressByCep(cepRequest);

                if (!isCepInDatabase)
                {

                    return Conflict("CEP already exists in the database.");
                }


                return Ok("Data successfully saved");
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Internal error: {ex.Message}");
            }
        }
    }
}
