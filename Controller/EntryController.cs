using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prac.Data;
using prac.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prac.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntriesController : ControllerBase
    {
        private readonly TimeTrackingContext _context;

        public EntriesController(TimeTrackingContext context)
        {
            _context = context;
        }

        // Получение списка всех проводок
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Entry>>> GetEntries()
        {
            return await _context.Entries.Include(e => e.Task).ToListAsync();
        }

        // Создание новой проводки
        [HttpPost]
        public async Task<ActionResult<Entry>> CreateEntry(Entry entry)
        {
            // Проверка, что суммарно проводок за день не более 24 часов
            var totalHoursForDay = await _context.Entries
                .Where(e => e.Date.Date == entry.Date.Date && e.Id != entry.Id)
                .SumAsync(e => e.Hours);

            if (totalHoursForDay + entry.Hours > 24)
            {
                return BadRequest("Суммарно проводок за день не может превышать 24 часа.");
            }

            // Проверка, что задача активна
            var task = await _context.Tasks.FindAsync(entry.TaskId);
            if (task == null || !task.IsActive)
            {
                return BadRequest("Выбранная задача не существует или неактивна.");
            }

            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEntries), new { id = entry.Id }, entry);
        }

        // Обновление проводки
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEntry(int id, Entry entry)
        {
            if (id != entry.Id)
            {
                return BadRequest();
            }

            // Проверка, что задача не стала неактивной
            var existingEntry = await _context.Entries.FindAsync(id);
            var task = await _context.Tasks.FindAsync(entry.TaskId);
            if (existingEntry != null && task != null && !task.IsActive)
            {
                return BadRequest("Выбранная задача стала неактивной и не может быть изменена.");
            }

            _context.Entry(entry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Entries.Any(e => e.Id == id))
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

        // Удаление проводки
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            var entry = await _context.Entries.FindAsync(id);
            if (entry == null)
            {
                return NotFound();
            }

            _context.Entries.Remove(entry);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
