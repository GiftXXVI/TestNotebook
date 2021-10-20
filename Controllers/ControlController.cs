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
    public class ControlController : Controller
    {
        private readonly TestNoteBookContext _db;
        public ControlController(TestNoteBookContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Control> controls = _db.Control.ToList();
            return View(controls);
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
                Control control = _db.Control.Include(c => c.Questions).FirstOrDefault(c => c.Id == id);
                if (control == null)
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
                return View(control);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(string name)
        {
            if (String.IsNullOrEmpty(name))
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
                    Control control = new Control() { Name = name.Trim() };
                    _db.Control.Add(control);
                    _db.SaveChanges();
                    int id = control.Id;
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
                Control control = _db.Control.Find(id);
                if (control == null)
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
                    return View(control);
                }

            }
        }
        [HttpPost]
        public IActionResult Edit(int id, string name)
        {
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(id.ToString()))
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
                Control control = _db.Control.Find(id);
                if (control == null)
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
                        control.Name = name;
                        _db.Entry(control).State = EntityState.Modified;
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
                Control control = _db.Control.Find(id);
                if (control == null)
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
                return View(control);
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
                Control control = _db.Control.Find(id);
                if (control == null)
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
                        _db.Control.Remove(control);
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