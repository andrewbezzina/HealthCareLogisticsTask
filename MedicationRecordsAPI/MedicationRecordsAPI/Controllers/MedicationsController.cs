
using Microsoft.AspNetCore.Mvc;
using MedicationRecords.Domain.Models;
using MedicationRecords.Contracts.Requests;
using Azure.Core;
using MedicationRecords.Application.Services;

namespace MedicationRecords.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        private readonly IDataInitializationService _dataInitializationService;
        private readonly IMedicationService _medicationService;

        public MedicationsController(IDataInitializationService dataInitializationService, IMedicationService medicationService)
        {
            _dataInitializationService = dataInitializationService;
            _medicationService = medicationService;
        }

        // POST: api/Medications/PopulateDatabase
        [HttpPost("PopulateDatabase")]
        public async Task<IActionResult> PopulateDatabase()
        {
            var result = await _dataInitializationService.LoadInitialDataAsync();
            if (result.IsSuccess) 
            {
                return Ok();
            }
            
            return BadRequest(result.Errors);
        }

        // GET: api/Medications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medication>>> GetMedications()
        {
            return await _medicationService.GetAllAsync();
        }

        // GET: api/Medications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Medication>> GetMedication(int id)
        {
            var result = await _medicationService.GetAsync(id);


            if (result.IsFailed)
            {
                return NotFound(result.Errors);
            }

            return result.Value;
        }

        // POST: api/Medications
        [HttpPost]
        public async Task<ActionResult<Medication>> PostMedication(CreateMedicationRequest request)
        {
            var result = await _medicationService.CreateAndSaveAsync(request);
            if (result.IsFailed) 
            {
                return BadRequest(result.Errors);
            }

            return CreatedAtAction("GetMedication", new { id = result.Value.Id }, result.Value);
        }

        // PUT: api/Medications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedication(int id, UpdateMedicationRequest request)
        {
            var result = await _medicationService.UpdateAsync(id, request);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }

        // DELETE: api/Medications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedication(int id)
        {
            var result = await _medicationService.DeleteAsync(id);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

    }
}
