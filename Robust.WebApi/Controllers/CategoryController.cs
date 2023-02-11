using Microsoft.AspNetCore.Mvc;
using Robust.DataAccess.Repository.IRepository;
using Robust.Models;

namespace Robust.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryController(IUnitOfWork unitOfWork)
    {
        _unitOfWork= unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Category> objCategoryList = _unitOfWork.Category.GetAll();
        //return View(objCategoryList);
        return Ok(objCategoryList);
    }

    //GET
    public IActionResult Create()
    {
        //return View();
        return Ok();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();
            //TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        //return View(obj);
        return Ok(obj);
    }

    //GET
    public IActionResult Edit(int? id)
    {
        if(id==null || id == 0)
        {
            return NotFound();
        }
        var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u=>u.Id==id);

        if (categoryFromDbFirst == null)
        {
            return NotFound();
        }

        //return View(categoryFromDbFirst);
        return Ok(categoryFromDbFirst);
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();
            //TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }
        //return View(obj);
        return Ok(obj);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var categoryFromDbFirst = _unitOfWork.Category.GetFirstOrDefault(u=>u.Id==id);

        if (categoryFromDbFirst == null)
        {
            return NotFound();
        }

        //return View(categoryFromDbFirst);
        return Ok(categoryFromDbFirst);
    }

    //POST
    [HttpPost,ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePOST(int? id)
    {
        var obj = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);
        if (obj == null)
        {
            return NotFound();
        }

        _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
        //TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
        
    }
}
