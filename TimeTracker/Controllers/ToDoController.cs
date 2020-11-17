using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TimeTracker.Infrastructure.Data;
using TimeTracker.Models;
using TimeTracker.Dependencies.Interfaces;

namespace TimeTracker.Controllers
{
    public class ToDoController : Controller
    {
        private readonly IRepository _repository;
        public ToDoController(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var items = (await _repository.ListAsync<ToDoItem>())
                            .Select(ToDoItemDTO.FromToDoItem).OrderBy(item => item.Status);
            return View(items);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoItem todoItem)
        {
            if (todoItem == null) return NotFound();
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(todoItem);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var todoItem = await _repository.GetByIdAsync<ToDoItem>(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return View(todoItem);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var todoItem = await _repository.GetByIdAsync<ToDoItem>(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return View(todoItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ToDoItem TodoItem)
        {
            if (id != TodoItem.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(TodoItem);
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var TodoItem = await _repository.GetByIdAsync<ToDoItem>(id);
            return View(TodoItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ToDoItem todoItem)
        {
            await _repository.DeleteAsync(todoItem);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Start(int id)
        {
            var todoItem = await _repository.GetByIdAsync<ToDoItem>(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            todoItem.StartTask();
            await _repository.UpdateAsync(todoItem);
            return Ok();
        }

        public async Task<IActionResult> Stop(int id)
        {
            var todoItem = await _repository.GetByIdAsync<ToDoItem>(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            todoItem.StopTask();
            await _repository.UpdateAsync(todoItem);
            return Ok();
        }

        public async Task<IActionResult> Finish(int id)
        {
            var todoItem = await _repository.GetByIdAsync<ToDoItem>(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            todoItem.FinishTask();
            await _repository.UpdateAsync(todoItem);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTime([FromBody] dynamic data)
        {
            UpdateTime requestData = JsonConvert.DeserializeObject<UpdateTime>(data.ToString());

            var todoItem = await _repository.GetByIdAsync<ToDoItem>(requestData.Id);
            if (todoItem == null)
            {
                return NotFound();
            }
            TimeSpan ts = TimeSpan.FromSeconds(requestData.Time);
            todoItem.AddTime(ts);
            await _repository.UpdateAsync(todoItem);
            return Ok();
        }
    }
}