namespace ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[ApiController]
[Route("api/[controller]")]

public class ToDoItemsController : ControllerBase
{
    private static readonly List<ToDoItem> items = [];

    [HttpPost]
    public IActionResult Create(ToDoItemCreateRequestDto request)
    {
        var item = request.ToDomain();
        try
        {
            item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
            items.Add(item);
        }
        catch(Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return Created();
    }

    [HttpGet]
    public IActionResult Read()
    {
        try
        {

            if (items == null)
            {
                return NotFound();
            }

            //toto je spatne pochopene zadani (vlivem preklepu v nem pravdepodobne, v Tip ma byt misto funcke Add urcite Select) + mi to hlasi kompilacni error necekane :)
            /*var ToDoItem = items.ToDoItemGetResponseDto.FromDomain.ToList();
            items.Add(ToDoItem);
            return Ok(ToDoItem);*/

            /*
            Pojdme si to udelat spolu :) Funkce read ma delat
                - `200 OK` s listem všech úkolů ve formě DTO List<ToDoItemGetResponseDto>

                co ten muj radek kodu dela
                Select = metoda pro iterovatelne objekty (takze i List) co na zaklade toho objektu vytvori novy iterovatelny objekt

                novy_objekt = puvodni_objekt.Select(operace co ma delat s kazdym objektem v puvodnim Listu ktery pak prida do puvodniho listu)

                priklad:
                List<int> cisla = new List<int>(){1,2,3} = list s inty ktery v sobe ma cisla 1,2,3
                var novaCisla = cisla.Select(o => -1 * o) = nova cisla jsou vytvorena tak ze kazde cislo v listu cisla je vynasobeno -1 a pridano do noveho listu
                takze novaCisla v sobe maji {-1,-2,-3}
            */
            var itemsToReturn = items.Select(ToDoItemGetResponseDto.FromDomain);
            return Ok(itemsToReturn);

            /*
            BONUS
            jde do udelat vsechno na jeden radek pomoci terarniho operatoru ?: ktery funguje podminka ? plati : neplati
            takze to jde udelat jako

            var itemsToReturn = items.Select(ToDoItemGetResponseDto.FromDomain);
            return items == null ? NotFound() : Ok(itemsToReturn)

            nebo to dokonce zkratit na
            return items == null ? NotFound() : Ok(items.Select(ToDoItemGetResponseDto.FromDomain))
            ale tohle je uz pro fajnsmekry :)
            */
        }
        catch (Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

    }

    [HttpGet("{toDoItemId:int}")]
    public IActionResult ReadById(int toDoItemId, [FromBody] ToDoItemGetResponseDto request)
    {
        //[FromBody] ToDoItemGetResponseDto request urcite nebudeme potrebovat -> chceme pouze Id ukolu co chceme precist

        //prosim dodelat :)
        return Ok();
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        //prosim dodelat :)
        return Ok();
    }

    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {

        try
        {
            var toDoItem = items.Find(item => item.ToDoItemId == toDoItemId);

            if (toDoItem == null)
            {
                return NotFound();
            }
            items.Remove(toDoItem);
            return NoContent();
        }
        catch(Exception ex)
        {
            return this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

    }
}


