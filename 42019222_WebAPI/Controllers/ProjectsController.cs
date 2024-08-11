using _42019222_WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _42019222_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobTelemetryController : ControllerBase
    {
        private readonly _42019222dbContext _context;

        public JobTelemetryController(_42019222dbContext context)
        {
            _context = context;
        }

        // GET: api/JobTelemetry
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobTelemetry>>> GetJobTelemetries()
        {
            return await _context.JobTelemetries.ToListAsync();
        }

        // GET: api/JobTelemetry/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<JobTelemetry>> GetJobTelemetry(int id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);

            if (jobTelemetry == null)
            {
                return NotFound();
            }

            return jobTelemetry;
        }

        // POST: api/JobTelemetry
        [HttpPost]
        public async Task<ActionResult<JobTelemetry>> PostJobTelemetry(JobTelemetry jobTelemetry)
        {
            // Validate ProjectId is provided
            if (jobTelemetry.ProjectId == Guid.Empty)
            {
                return BadRequest("ProjectId is required.");
            }

            // Optionally, check if the ProjectId exists in the Project table
            var projectExists = await _context.Projects.AnyAsync(p => p.ProjectId == jobTelemetry.ProjectId);
            if (!projectExists)
            {
                return NotFound("Project not found.");
            }

            _context.JobTelemetries.Add(jobTelemetry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJobTelemetry), new { id = jobTelemetry.Id }, jobTelemetry);
        }

        // PATCH: api/JobTelemetry/{id}
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchJobTelemetry(int id, [FromBody] JsonPatchDocument<JobTelemetry> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);
            if (jobTelemetry == null)
            {
                return NotFound();
            }

            patchDoc.ApplyTo(jobTelemetry, (error) =>
            {
                ModelState.AddModelError("JsonPatchError", error.ErrorMessage);
            });

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelemetryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/JobTelemetry/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobTelemetry(int id)
        {
            var jobTelemetry = await _context.JobTelemetries.FindAsync(id);
            if (jobTelemetry == null)
            {
                return NotFound();
            }

            _context.JobTelemetries.Remove(jobTelemetry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/JobTelemetry/GetSavings
        [HttpGet("GetSavings")]
        public async Task<ActionResult<SavingsResult>> GetSavings(Guid projectId, DateTime startDate, DateTime endDate)
        {
            var telemetryEntries = await _context.JobTelemetries
                .Where(jt => jt.JobId == projectId.ToString() && jt.EntryDate >= startDate && jt.EntryDate <= endDate)
                .ToListAsync();

            return CalculateSavings(telemetryEntries);
        }

        // GET: api/JobTelemetry/GetSavingsByClient
        [HttpGet("GetSavingsByClient")]
        public async Task<ActionResult<SavingsResult>> GetSavingsByClient(Guid clientId, DateTime startDate, DateTime endDate)
        {
            var telemetryEntries = await _context.JobTelemetries
                .Where(jt => jt.EntryDate >= startDate && jt.EntryDate <= endDate)
                .ToListAsync();

            return CalculateSavings(telemetryEntries);
        }

        private ActionResult<SavingsResult> CalculateSavings(List<JobTelemetry> telemetryEntries)
        {
            if (telemetryEntries == null || telemetryEntries.Count == 0)
            {
                return NotFound("No telemetry data found for the given criteria.");
            }

            var totalTimeSaved = telemetryEntries
                .Where(jt => jt.ExcludeFromTimeSaving == false || jt.ExcludeFromTimeSaving == null)
                .Sum(jt => jt.HumanTime ?? 0);

            decimal hourlyRate = 50m;
            var totalCostSaved = totalTimeSaved * hourlyRate;

            return new SavingsResult
            {
                TotalTimeSaved = totalTimeSaved,
                TotalCostSaved = totalCostSaved
            };
        }

        private bool TelemetryExists(int id)
        {
            return _context.JobTelemetries.Any(e => e.Id == id);
        }
    }

    public class SavingsResult
    {
        public int TotalTimeSaved { get; set; }
        public decimal TotalCostSaved { get; set; }
    }
}







