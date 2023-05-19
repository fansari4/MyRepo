using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRepo.Data;
using MyRepo.Models;
using Microsoft.EntityFrameworkCore;

namespace MyRepo.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly ModelDbContext _context;

        public IssueController(ModelDbContext dbContext)
        { 
             _context = dbContext;

        }


        #region SQL API

        [HttpGet("getallissues")]
        public async Task<IEnumerable<Issue>> GetAll() => await _context.Issues.ToListAsync();

        [HttpGet("getissuebyid")]
        [ProducesResponseType(typeof(Issue), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            return issue == null ? NotFound() : Ok(issue);
        }

        [HttpPost("createissue")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Issue issue)
        {
            await _context.Issues.AddAsync(issue);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = issue.Id }, issue);
        }

        [HttpPut("updateissue")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, Issue issue)
        {
            if (id != issue.Id) return BadRequest();

            _context.Entry(issue).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("deleteissue")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null) return NotFound();

            _context.Remove(issue);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        #endregion
    }
}
