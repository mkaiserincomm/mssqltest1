using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mssqltest1.Models;
using Microsoft.Extensions.Logging;

namespace mssqltest1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryContext _context;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ILogger<CategoryController> logger, CategoryContext context)
        {
            _context = context;
            _logger = logger;
            _logger.LogInformation(new EventId(1001,"Constructor"),"CategoryController");
        }

        // GET: api/category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        {
            return await _context.Category.ToListAsync();
        }

        // GET: api/category/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {                        
            var category = await _context.Category.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/category/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.categoryId)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _context.Category.Add(category);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                _logger.LogError(new EventId(9001, "DbUpdateException"), dbUpdateException, "PostCategory");
                if (CategoryExists(category.categoryId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(new EventId(9002, "Exception"), exception, "PostCategory");
                throw;
            }

            return CreatedAtAction("PostCategory", new { id = category.categoryId }, category);
        }

        // DELETE: api/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Category(int id)
        {
            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Category.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.categoryId == id);
        }
    }
}
