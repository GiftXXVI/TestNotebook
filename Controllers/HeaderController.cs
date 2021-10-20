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
    public class HeaderController : Controller
    {
        private readonly TestNoteBookContext _db;
        public HeaderController(TestNoteBookContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Header> headers = _db.Header.ToList();
            return View(headers);
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
                Header header = _db.Header.Find(id);
                if (header == null)
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
                List<Detail> details = _db.Detail.Include(d => d.Question).Include(d => d.Question.Control).Include(d => d.Field).Include(d => d.Field.Screen).Include(d=>d.Result).Where(d => d.HeaderId == id).ToList();
                ViewBag.DetailsList = details;
                return View(header);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(string title)
        {
            if (String.IsNullOrEmpty(title))
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
            try
            {
                Header header = new Header() { Title = title.Trim() };
                _db.Header.Add(header);
                _db.SaveChanges();
                int id = header.Id;
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
                Header header = _db.Header.Find(id);
                if (header == null)
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
                    return View(header);
                }
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, string title)
        {
            if (String.IsNullOrEmpty(title) || String.IsNullOrEmpty(id.ToString()))
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
                Header header = _db.Header.Find(id);
                if (header == null)
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
                        header.Title = title;
                        _db.Entry(header).State = EntityState.Modified;
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
                    catch (Exception)
                    {
                        ErrorViewModel ev = new ErrorViewModel()
                        {
                            Controller = ControllerContext.ActionDescriptor.ControllerName,
                            Action = ControllerContext.ActionDescriptor.ActionName,
                            StatusCode = (int)HttpStatusCode.InternalServerError,
                            StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.InternalServerError),
                            Id = String.IsNullOrEmpty(id.ToString()) ? 0 : (int)id
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
                Header header = _db.Header.Find(id);
                if (header == null)
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
                List<Detail> details = _db.Detail.Include(d => d.Question).Include(d => d.Question.Control).Where(d => d.HeaderId == id).ToList();
                ViewBag.DetailsList = details;
                return View(header);
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
                Header header = _db.Header.Find(id);
                if (header == null)
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
                        _db.Header.Remove(header);
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