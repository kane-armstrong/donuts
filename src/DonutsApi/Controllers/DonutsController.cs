using DonutsApi.Application;
using DonutsApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace DonutsApi.Controllers
{
    [ApiController]
    [Route("api/donuts")]
    public class DonutsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public DonutsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<Donut>>> Get()
        {
            var donuts = await _unitOfWork.Donuts.ToListAsync();
            return Ok(donuts);
        }

        [HttpGet("{id}", Name = nameof(GetById))]
        public async Task<ActionResult<Donut>> GetById([FromRoute]Guid id)
        {
            var donut = await _unitOfWork.Donuts.FirstOrDefaultAsync(x => x.Id == id);
            if (donut != null) return Ok(donut);
            return NotFound();
        }

        [HttpPost("")]
        public async Task<IActionResult> Create([FromBody] CreateDonut command)
        {
            var donut = new Donut
            {
                Id = Guid.NewGuid(),
                Flavor = command.Flavor,
                Price = command.Price
            };
            _unitOfWork.Donuts.Add(donut);
            await _unitOfWork.Complete();
            return CreatedAtRoute(nameof(GetById), new { id = donut.Id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] EditDonut command)
        {
            if (command.Id != id) return BadRequest();
            var donut = await _unitOfWork.Donuts.FirstOrDefaultAsync(x => x.Id == id);
            if (donut == null) return NotFound();
            donut.Flavor = command.Flavor;
            donut.Price = command.Price;
            await _unitOfWork.Complete();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var exists = await _unitOfWork.Donuts.AnyAsync(x => x.Id == id);
            if (!exists) return NotFound();
            var entry = _unitOfWork.GetEntry(new Donut { Id = id });
            entry.State = EntityState.Deleted;
            await _unitOfWork.Complete();
            return NoContent();
        }
    }

    public class CreateDonut
    {
        [Required]
        public string Flavor { get; set; }
        [Required]
        public decimal Price { get; set; }
    }

    public class EditDonut
    {
        public Guid Id { get; set; }
        [Required]
        public string Flavor { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
