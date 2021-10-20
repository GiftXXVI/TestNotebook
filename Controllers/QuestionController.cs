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
    public class QuestionController : Controller
    {
        private readonly TestNoteBookContext _db;
        public QuestionController(TestNoteBookContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Details(int? id, int? controlid)
        {
            if (id == null || controlid == null)
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid
                };
                return View("Error", ev);
            }
            else
            {
                Question question = _db.Question.Include(q => q.Control).FirstOrDefault(q => q.Id == id);
                if (question == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid
                    };
                    return View("Error", ev);
                }
                return View(question);
            }
        }
        [HttpGet]
        public IActionResult Create(int? controlid)
        {
            List<Control> controls = _db.Control.ToList();
            if (controls.Count == 0)
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid
                };
                return View("Error", ev);
            }
            IEnumerable<SelectListItem> items = controls.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            ViewBag.Controls = items;
            ViewBag.ControlId = controlid;
            return View();
        }
        [HttpPost]
        public IActionResult Create(int? controlid, string description)
        {
            if (controlid == null || String.IsNullOrEmpty(description))
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid
                };
                return View("Error", ev);
            }
            else
            {
                try
                {
                    Question question = new Question() { ControlId = (int)controlid, Description = description };
                    _db.Question.Add(question);
                    _db.SaveChanges();
                    int id = question.Id;
                    SuccessViewModel sc = new SuccessViewModel()
                    {
                        StatusCode = (int)HttpStatusCode.Created,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.Created),
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid
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
                        Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid,
                        Message = e.Message.ToString()
                    };
                    return View("Error", ev);
                }
            }
        }

        [HttpGet]
        public IActionResult Edit(int? id, int? controlid)
        {
            if (String.IsNullOrEmpty(id.ToString()))
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid
                };
                return View("Error", ev);
            }
            else
            {
                Question question = _db.Question.Include(q => q.Control).FirstOrDefault(q => q.Id == id);
                if (question == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid
                    };
                    return View("Error", ev);
                }
                else
                {
                    List<Control> controls = _db.Control.ToList();
                    if (controls.Count == 0)
                    {
                        ErrorViewModel ev = new ErrorViewModel()
                        {
                            Controller = ControllerContext.ActionDescriptor.ControllerName,
                            Action = ControllerContext.ActionDescriptor.ActionName,
                            StatusCode = (int)HttpStatusCode.BadRequest,
                            StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                            Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid
                        };
                        return View("Error", ev);
                    }
                    IEnumerable<SelectListItem> items = controls.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = c.Id == question.ControlId ? true : false });
                    ViewBag.Controls = items;
                    return View(question);
                }
            }
        }
        [HttpPost]
        public IActionResult Edit(int id, int controlid, string description)
        {
            if (String.IsNullOrEmpty(description) || String.IsNullOrEmpty(id.ToString()) || String.IsNullOrEmpty(controlid.ToString()))
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid
                };
                return View("Error", ev);
            }
            else
            {
                Question question = _db.Question.Find(id);
                if (question == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid
                    };
                    return View("Error", ev);
                }
                else
                {
                    try
                    {
                        question.Description = description;
                        question.ControlId = controlid;
                        _db.Entry(question).State = EntityState.Modified;
                        _db.SaveChanges();
                        SuccessViewModel sc = new SuccessViewModel()
                        {
                            StatusCode = (int)HttpStatusCode.Accepted,
                            StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.Accepted),
                            Controller = ControllerContext.ActionDescriptor.ControllerName,
                            Action = ControllerContext.ActionDescriptor.ActionName,
                            Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid
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
                            Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid,
                            Message = e.Message.ToString()
                        };
                        return View("Error", ev);
                    }
                }
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id, int? controlid)
        {
            if (id == null)
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid
                };
                return View("Error", ev);
            }
            else
            {
                Question question = _db.Question.Include(q => q.Control).FirstOrDefault(q => q.Id == id);
                if (question == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid
                    };
                    return View("Error", ev);
                }
                return View(question);
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id, int? controlid)
        {
            if (id == null)
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid
                };
                return View("Error", ev);
            }
            else
            {
                Question question = _db.Question.Find(id);
                if (question == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid
                    };
                    return View("Error", ev);
                }
                else
                {
                    try
                    {
                        _db.Question.Remove(question);
                        _db.SaveChanges();
                        SuccessViewModel sc = new SuccessViewModel()
                        {
                            StatusCode = (int)HttpStatusCode.Accepted,
                            StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.Accepted),
                            Controller = ControllerContext.ActionDescriptor.ControllerName,
                            Action = ControllerContext.ActionDescriptor.ActionName,
                            Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid
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
                            Id = String.IsNullOrEmpty(controlid.ToString()) ? 0 : (int)controlid,
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