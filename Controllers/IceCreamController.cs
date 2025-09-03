using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Http.HttpResults;
using MyWebApi.Models;
using MyWebApi.Interfaces;
using MyWebApi.Services;

namespace MyWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class IceCreamController : ControllerBase
{
    private IIceCreamService IceCreamService;
    public IceCreamController(IIceCreamService IceCreamService)
    {
        this.IceCreamService = IceCreamService;
    }

    [HttpGet]
    [Authorize(Policy = "User")]
    public ActionResult<List<IceCream>> GetAll() {
        return IceCreamService.GetAll(int.Parse(User.FindFirst("id")?.Value!));
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult<IceCream> Get(int id)
    {
        var iceCream = IceCreamService.GetById(id);
        if (iceCream == null)
            return NotFound();

        return iceCream;
    }

    [HttpPost]
    public ActionResult Create(IceCream newItem)
    {
        IceCreamService.Add(newItem,newItem.userId);
        return CreatedAtAction(nameof(Create), new {id=newItem.id}, newItem);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, IceCream newItem)
    {
        if (id != newItem.id)
            return BadRequest();

        var existingIceCream = IceCreamService.GetById(id);
        if (existingIceCream is null)
            return  NotFound();

        IceCreamService.Update(id,newItem,newItem.userId);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var iceCream = IceCreamService.GetById(id);
        if (iceCream is null)
            return  NotFound();

        IceCreamService.Delete(id);
        
        return Content(IceCreamService.Count.ToString());
    }
}
