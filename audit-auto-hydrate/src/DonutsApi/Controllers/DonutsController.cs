﻿using DonutsApi.Application;
using DonutsApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<Donut>> GetById([FromRoute]int id)
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
                Flavor = command.Flavor,
                Price = command.Price
            };
            _unitOfWork.Donuts.Add(donut);
            await _unitOfWork.Complete();
            return CreatedAtRoute(nameof(GetById), new { id = donut.Id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] EditDonut command)
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
        public async Task<IActionResult> Delete([FromRoute]int id)
        {
            var donut = await _unitOfWork.Donuts.FirstOrDefaultAsync(x => x.Id == id);
            if (donut == null) return NotFound();
            _unitOfWork.Donuts.Remove(donut);
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
        public int Id { get; set; }
        [Required]
        public string Flavor { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}