using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.WebUtilities;
using TestNotebook.Models;
using TestNotebook.Data;

namespace TestNotebook.Controllers
{
    public class FieldController : Controller
    {
        private readonly TestNoteBookContext _db;
        public FieldController(TestNoteBookContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Details(int? id, int? screenid)
        {
            if (id == null || screenid == null)
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid
                };
                return View("Error", ev);
            }
            else
            {
                Field field = _db.Field.Include(q => q.Screen).FirstOrDefault(q => q.Id == (int)id);
                if (field == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid
                    };
                    return View("Error", ev);
                }
                return View(field);
            }
        }

        [HttpGet]
        public IActionResult Create(int? screenid)
        {
            List<Screen> screens = _db.Screen.ToList();
            if (screens.Count == 0 || screenid == null)
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid
                };
                return View("Error", ev);
            }
            IEnumerable<SelectListItem> items = screens.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name });
            ViewBag.Screens = items;
            ViewBag.ScreenId = screenid;
            return View();
        }
        [HttpPost]
        public IActionResult Create(int? screenid, string name)
        {
            if (screenid == null || String.IsNullOrEmpty(name))
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid
                };
                return View("Error", ev);
            }
            else
            {
                try
                {
                    Field field = new Field() { ScreenId = (int)screenid, Name = name };
                    _db.Field.Add(field);
                    _db.SaveChanges();
                    int id = field.Id;
                    SuccessViewModel sc = new SuccessViewModel()
                    {
                        StatusCode = (int)HttpStatusCode.Created,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.Created),
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid
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
                        Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid,
                        Message = e.Message.ToString()
                    };
                    return View("Error", ev);
                }
            }
        }

        [HttpGet]
        public IActionResult Edit(int? id, int? screenid)
        {
            if (id == null || screenid == null)
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid
                };
                return View("Error", ev);
            }
            else
            {
                Field field = _db.Field.Include(q => q.Screen).FirstOrDefault(q => q.Id == (int)id);
                if (field == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid
                    };
                    return View("Error", ev);
                }
                else
                {
                    List<Screen> screens = _db.Screen.ToList();
                    if (screens.Count == 0)
                    {
                        ErrorViewModel ev = new ErrorViewModel()
                        {
                            Controller = ControllerContext.ActionDescriptor.ControllerName,
                            Action = ControllerContext.ActionDescriptor.ActionName,
                            StatusCode = (int)HttpStatusCode.BadRequest,
                            StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                            Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid
                        };
                        return View("Error", ev);
                    }
                    IEnumerable<SelectListItem> items = screens.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == field.ScreenId ? true : false });
                    ViewBag.Screens = items;
                    return View(field);
                }
            }
        }
        [HttpPost]
        public IActionResult Edit(int? id, int? screenid, string name)
        {
            if (String.IsNullOrEmpty(name) || id == null || screenid == null)
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid
                };
                return View("Error", ev);
            }
            else
            {
                Field field = _db.Field.Find(id);
                if (field == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid
                    };
                    return View("Error", ev);
                }
                else
                {
                    try
                    {
                        field.Name = name;
                        field.ScreenId = (int)screenid;
                        _db.Entry(field).State = EntityState.Modified;
                        _db.SaveChanges();
                        SuccessViewModel sc = new SuccessViewModel()
                        {
                            StatusCode = (int)HttpStatusCode.Accepted,
                            StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.Accepted),
                            Controller = ControllerContext.ActionDescriptor.ControllerName,
                            Action = ControllerContext.ActionDescriptor.ActionName,
                            Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid
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
                            Message = e.Message.ToString(),
                            Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid
                        };
                        return View("Error", ev);
                    }
                }
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id, int? screenid)
        {
            if (id == null || screenid == null)
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid
                };
                return View("Error", ev);
            }
            else
            {
                Field field = _db.Field.Include(q => q.Screen).FirstOrDefault(q => q.Id == id);
                if (field == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid
                    };
                    return View("Error", ev);
                }
                return View(field);
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id, int? screenid)
        {
            if (id == null || screenid == null)
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid
                };
                return View("Error", ev);
            }
            else
            {
                Field field = _db.Field.Find(id);
                if (field == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid
                    };
                    return View("Error", ev);
                }
                else
                {
                    try
                    {
                        _db.Field.Remove(field);
                        _db.SaveChanges();
                        SuccessViewModel sc = new SuccessViewModel()
                        {
                            StatusCode = (int)HttpStatusCode.Accepted,
                            StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.Accepted),
                            Controller = ControllerContext.ActionDescriptor.ControllerName,
                            Action = ControllerContext.ActionDescriptor.ActionName,
                            Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid
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
                            Message = e.Message.ToString(),
                            Id = String.IsNullOrEmpty(screenid.ToString()) ? 0 : (int)screenid
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