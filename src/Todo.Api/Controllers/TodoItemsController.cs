using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Interfaces.Services;
using Todo.Domain.Entities;
using Todo.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Todo.Api.Controllers
{
    [ApiController] // This class is an API controller
    [Route("todo-items/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemsController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemRequest>>> GetAll()
        {
            var items = await _todoItemService.GetAllAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemRequest>> GetById(Guid id)
        {
            var item = await _todoItemService.GetByIdAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemRequest>> Create([FromBody] TodoItemRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Request cannot be null.");
                }

                TodoItem createdItem = await _todoItemService.CreateAsync(request.Title, request?.Description);
                return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
            }
            catch (ArgumentException ex) // Handling request values validation errors
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItemRequest>> Update(Guid id, [FromBody] TodoItemRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest("Request cannot be null.");
                }

                var updatedItem = await _todoItemService.UpdateAsync(id, request.Title, request?.Description);

                if (updatedItem == null)
                {
                    return NotFound($"Todo item with ID {id} not found.");
                }

                return Ok(updatedItem);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPatch("{id}/complete")]
        public async Task<ActionResult<TodoItemRequest>> Complete(Guid id)
        {
            try
            {
                var completedItem = await _todoItemService.ToggleIsCompleted(id);

                if (completedItem == null)
                {
                    return NotFound($"Todo item with ID {id} not found.");
                }

                return Ok(completedItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _todoItemService.DeleteAsync(id);

                if (!deleted)
                {
                    return NotFound($"Todo item with ID {id} not found.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
