using APIQueroOficina.Services;
using APIQueroOficina.Models;
using Microsoft.AspNetCore.Mvc;



namespace APIQueroOficina.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkShopsController : ControllerBase
    {
        private readonly WorkShopService _workShopService; // readonly evita a edição nas informações
        public WorkShopsController(WorkShopService workShopService) 
        {
            _workShopService = workShopService;
        }
        [HttpGet]
        public async Task<List<WorkShop>> Get() =>
            await _workShopService.GetAsync();

        [HttpGet("{id:length(24)}")]
            public async Task<ActionResult<WorkShop>> Get(string id)
        {
            var workshop = await _workShopService.GetAsync(id);
            if (workshop == null)
            {
                return NotFound();
            }
            return workshop;
        }
        [HttpPost]
        public async Task<IActionResult> Post(WorkShop newWorkShop)
        {
            await _workShopService.CreateAsync(newWorkShop);

            return CreatedAtAction(nameof(Get), new { id = newWorkShop.Id }, newWorkShop);
        }
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, WorkShop updatedWorkShop)
        {
            var workshop = await _workShopService.GetAsync(id);
            if (workshop == null)

                return NotFound();
            updatedWorkShop.Id = workshop.Id;
            await _workShopService.UpdateAsync(id, updatedWorkShop);
            return NoContent();
            
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var workshop = await _workShopService.GetAsync(id);
            if (workshop == null)
                return NotFound();
            
            await _workShopService.RemoveAsync(id);

            return NoContent();
        }
    }
}
