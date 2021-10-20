using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.WebUtilities;
using TestNotebook.Models;
using TestNotebook.Data;

namespace TestNotebook.Controllers
{
    public class ResultController : Controller
    {
        private readonly TestNoteBookContext _db;
        public ResultController(TestNoteBookContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Result> results = _db.Result.ToList();
            return View(results);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(id.ToString()) ? 0 : (int)id
                };
                return View("Error", ev);
            }
            else
            {
                Result result = _db.Result.Find(id);
                if (result == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(id.ToString()) ? 0 : (int)id
                    };
                    return View("Error", ev);
                }
                return View(result);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(string description)
        {
            if (String.IsNullOrEmpty(description))
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest)
                };
                return View("Error", ev);
            }
            else
            {
                try
                {
                    Result result = new Result() { description = description.Trim() };
                    _db.Result.Add(result);
                    _db.SaveChanges();
                    int id = result.Id;
                    SuccessViewModel sc = new SuccessViewModel()
                    {
                        StatusCode = (int)HttpStatusCode.Created,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.Created),
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        Id = String.IsNullOrEmpty(id.ToString()) ? 0 : (int)id
                    };
                    return View("Success", sc);
                }
                catch (Exception e)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.InternalServerError),
                        Message = e.Message.ToString()
                    };
                    return View("Error", ev);
                }
            }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (String.IsNullOrEmpty(id.ToString()))
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(id.ToString()) ? 0 : (int)id
                };
                return View("Error", ev);
            }
            else
            {
                Result result = _db.Result.Find(id);
                if (result == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(id.ToString()) ? 0 : (int)id
                    };
                    return View("Error", ev);
                }
                else
                {
                    return View(result);
                }

            }
        }
        [HttpPost]
        public IActionResult Edit(int id, string description)
        {
            if (String.IsNullOrEmpty(description) || String.IsNullOrEmpty(id.ToString()))
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(id.ToString()) ? 0 : (int)id
                };
                return View("Error", ev);
            }
            else
            {
                Result result = _db.Result.Find(id);
                if (result == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(id.ToString()) ? 0 : (int)id
                    };
                    return View("Error", ev);
                }
                else
                {
                    try
                    {
                        result.description = description;
                        _db.Entry(result).State = EntityState.Modified;
                        _db.SaveChanges();
                        SuccessViewModel sc = new SuccessViewModel()
                        {
                            StatusCode = (int)HttpStatusCode.Accepted,
                            StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.Accepted),
                            Controller = ControllerContext.ActionDescriptor.ControllerName,
                            Action = ControllerContext.ActionDescriptor.ActionName,
                            Id = String.IsNullOrEmpty(id.ToString()) ? 0 : (int)id
                        };
                        return View("Success", sc);
                    }
                    catch (Exception e)
                    {
                        ErrorViewModel ev = new ErrorViewModel()
                        {
                            Controller = ControllerContext.ActionDescriptor.ControllerName,
                            Action = ControllerContext.ActionDescriptor.ActionName,
                            StatusCode = (int)HttpStatusCode.InternalServerError,
                            StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.InternalServerError),
                            Id = String.IsNullOrEmpty(id.ToString()) ? 0 : (int)id,
                            Message = e.Message.ToString()
                        };
                        return View("Error", ev);
                    }
                }
            }
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(id.ToString()) ? 0 : (int)id
                };
                return View("Error", ev);
            }
            else
            {
                Result result = _db.Result.Find(id);
                if (result == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(id.ToString()) ? 0 : (int)id
                    };
                    return View("Error", ev);
                }
                return View(result);
            }

        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(id.ToString()) ? 0 : (int)id
                };
                return View("Error", ev);
            }
            else
            {
                Result result = _db.Result.Find(id);
                if (result == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(id.ToString()) ? 0 : (int)id
                    };
                    return View("Error", ev);
                }
                else
                {
                    try
                    {
                        _db.Result.Remove(result);
                        _db.SaveChanges();
                        SuccessViewModel sc = new SuccessViewModel()
                        {
                            StatusCode = (int)HttpStatusCode.Accepted,
                            StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.Accepted),
                            Controller = ControllerContext.ActionDescriptor.ControllerName,
                            Action = ControllerContext.ActionDescriptor.ActionName,
                            Id = String.IsNullOrEmpty(id.ToString()) ? 0 : (int)id
                        };
                        return View("Success", sc);
                    }
                    catch (Exception e)
                    {
                        ErrorViewModel ev = new ErrorViewModel()
                        {
                            Controller = ControllerContext.ActionDescriptor.ControllerName,
                            Action = ControllerContext.ActionDescriptor.ActionName,
                            StatusCode = (int)HttpStatusCode.BadRequest,
                            StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                            Id = String.IsNullOrEmpty(id.ToString()) ? 0 : (int)id,
                            Message = e.Message.ToString()
                        };
                        return View("Error", ev);
                    }
                }
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}