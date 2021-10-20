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
    public class DetailController : Controller
    {
        private readonly TestNoteBookContext _db;

        public DetailController(TestNoteBookContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Details(int? id, int? headerid)
        {
            if (id == null || headerid == null)
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid
                };
                return View("Error", ev);
            }
            else
            {
                Detail detail = _db.Detail.Include(d => d.Header)
                                          .Include(d => d.Question)
                                          .Include(d => d.Question.Control)
                                          .Include(d => d.Field)
                                          .Include(d => d.Field.Screen)
                                          .Include(d => d.Result)
                                          .FirstOrDefault(d => d.Id == id);
                if (detail == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid
                    };
                    return View("Error", ev);
                }
                return View(detail);
            }
        }

        [HttpGet]
        public IActionResult Create(int? headerid)
        {
            List<Control> controls = _db.Control.Include(c => c.Questions).ToList();
            List<Screen> screens = _db.Screen.Include(s => s.Fields).ToList();
            List<Result> results = _db.Result.ToList();
            if (headerid == null || (controls.Count == 0) || (screens.Count == 0) || (results.Count() == 0))
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid
                };
                return View("Error", ev);
            }
            IEnumerable<SelectListItem> controlsList = controls.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            IEnumerable<SelectListItem> questionsList = _db.Question.Include(q => q.Control).Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Control.Name + ": " + q.Description });
            IEnumerable<SelectListItem> screensList = screens.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
            IEnumerable<SelectListItem> fieldsList = _db.Field.Include(f => f.Screen).Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Screen.Name + ": " + q.Name });
            IEnumerable<SelectListItem> resultsList = results.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.description });
            ViewBag.Controls = controlsList;
            ViewBag.Questions = questionsList;
            ViewBag.Screens = screensList;
            ViewBag.Fields = fieldsList;
            ViewBag.Results = resultsList;
            ViewBag.HeaderId = headerid;
            return View();
        }

        [HttpPost]
        public IActionResult Create(int? headerid, int? controlid, int? questionid, int? screenid, int? fieldid, int? resultid, string answeryn, string answertext)
        {
            if (headerid == null || controlid == null || questionid == null || screenid == null || fieldid == null || resultid == null || String.IsNullOrEmpty(answeryn) || String.IsNullOrEmpty(answertext))
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid
                };
                return View("Error", ev);
            }
            else
            {
                try
                {
                    bool isvalidquestion = _db.Question.Where(q => q.Id == questionid && q.ControlId == controlid).Count() > 0;
                    bool isvalidfield = _db.Field.Where(f => f.Id == fieldid && f.ScreenId == screenid).Count() > 0;
                    if (isvalidquestion && isvalidfield)
                    {
                        Detail detail = new Detail() { HeaderId = (int)headerid, QuestionId = (int)questionid, FieldId = (int)fieldid, ResultId = (int)resultid, AnswerYN = answeryn == "yes" ? true : false, AnswerText = answertext };
                        _db.Detail.Add(detail);
                        _db.SaveChanges();
                        int id = detail.Id;
                        SuccessViewModel sc = new SuccessViewModel()
                        {
                            StatusCode = (int)HttpStatusCode.Created,
                            StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.Created),
                            Controller = ControllerContext.ActionDescriptor.ControllerName,
                            Action = ControllerContext.ActionDescriptor.ActionName,
                            Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid
                        };
                        return View("Success", sc);
                    }
                    else
                    {
                        ErrorViewModel ev = new ErrorViewModel()
                        {
                            Controller = ControllerContext.ActionDescriptor.ControllerName,
                            Action = ControllerContext.ActionDescriptor.ActionName,
                            StatusCode = (int)HttpStatusCode.BadRequest,
                            StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                            Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid
                        };
                        return View("Error", ev);
                    }
                }
                catch (Exception e)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.InternalServerError,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.InternalServerError),
                        Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid,
                        Message = e.Message.ToString()
                    };
                    return View("Error", ev);
                }
            }
        }

        [HttpGet]
        public IActionResult Edit(int? id, int? headerid)
        {
            List<Control> controls = _db.Control.Include(c => c.Questions).ToList();
            List<Screen> screens = _db.Screen.Include(s => s.Fields).ToList();
            List<Result> results = _db.Result.ToList();

            if (id == null || headerid == null || (controls.Count == 0) || (screens.Count() == 0) || (results.Count() == 0))
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid
                };
                return View("Error", ev);
            }
            else
            {
                Detail detail = _db.Detail.Include(d => d.Field).Include(d => d.Question).Include(d => d.Result).FirstOrDefault(d => d.Id == id);
                if (detail == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid
                    };
                    return View("Error", ev);
                }
                else
                {
                    IEnumerable<SelectListItem> controlsList = controls.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name, Selected = (c.Id == detail.Question.ControlId) ? true : false });
                    IEnumerable<SelectListItem> questionsList = _db.Question.Include(q => q.Control).Select(q => new SelectListItem { Value = q.Id.ToString(), Text = q.Control.Name + ": " + q.Description, Selected = (q.Id == detail.QuestionId) ? true : false });
                    IEnumerable<SelectListItem> screensList = screens.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name, Selected = (s.Id == detail.Field.ScreenId) ? true : false });
                    IEnumerable<SelectListItem> fieldsList = _db.Field.Include(f => f.Screen).Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Screen.Name + ": " + f.Name, Selected = (f.Id == detail.FieldId) ? true : false });
                    IEnumerable<SelectListItem> resultsList = results.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.description, Selected = (r.Id == detail.ResultId) ? true : false });
                    ViewBag.Controls = controlsList;
                    ViewBag.Questions = questionsList;
                    ViewBag.Screens = screensList;
                    ViewBag.Fields = fieldsList;
                    ViewBag.Results = resultsList;
                    return View(detail);
                }
            }
        }
        [HttpPost]
        public IActionResult Edit(int? id, int? headerid, int? controlid, int? questionid, int? screenid, int? fieldid, int? resultid, bool? answeryn, string answertext)
        {
            if (id == null || headerid == null || controlid == null || questionid == null || screenid == null || fieldid == null || resultid == null || answeryn == null
             || String.IsNullOrEmpty(headerid.ToString()) || String.IsNullOrEmpty(answertext))
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid
                };
                return View("Error", ev);
            }
            else
            {
                bool isvalidquestion = _db.Question.Where(q => q.Id == questionid && q.ControlId == controlid).Count() > 0;
                bool isvalidfield = _db.Field.Where(f => f.Id == fieldid && f.ScreenId == screenid).Count() > 0;
                if (isvalidquestion && isvalidfield)
                {
                    try
                    {
                        Detail detail = _db.Detail.Find(id);
                        detail.HeaderId = (int)headerid;
                        detail.QuestionId = (int)questionid;
                        detail.FieldId = (int)fieldid;
                        detail.ResultId = (int)resultid;
                        detail.AnswerYN = (bool)answeryn;
                        detail.AnswerText = answertext;
                        _db.Entry(detail).State = EntityState.Modified;
                        _db.SaveChanges();
                        SuccessViewModel sc = new SuccessViewModel()
                        {
                            StatusCode = (int)HttpStatusCode.Accepted,
                            StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.Accepted),
                            Controller = ControllerContext.ActionDescriptor.ControllerName,
                            Action = ControllerContext.ActionDescriptor.ActionName,
                            Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid
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
                            Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid,
                            Message = e.Message.ToString()
                        };
                        return View("Error", ev);
                    }
                }
                else
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                        Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid
                    };
                    return View("Error", ev);
                }
            }
        }

        [HttpGet]
        public IActionResult Delete(int? id, int? headerid)
        {
            if (id == null)
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid
                };
                return View("Error", ev);
            }
            else
            {
                Detail detail = _db.Detail.Include(d => d.Header).Include(d => d.Question).Include(d => d.Question.Control).Include(d => d.Field).Include(d => d.Field.Screen).FirstOrDefault(d => d.Id == id);
                if (detail == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid
                    };
                    return View("Error", ev);
                }
                return View(detail);
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id, int? headerid)
        {
            if (id == null)
            {
                ErrorViewModel ev = new ErrorViewModel()
                {
                    Controller = ControllerContext.ActionDescriptor.ControllerName,
                    Action = ControllerContext.ActionDescriptor.ActionName,
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.BadRequest),
                    Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid
                };
                return View("Error", ev);
            }
            else
            {
                Detail detail = _db.Detail.Find(id);
                if (detail == null)
                {
                    ErrorViewModel ev = new ErrorViewModel()
                    {
                        Controller = ControllerContext.ActionDescriptor.ControllerName,
                        Action = ControllerContext.ActionDescriptor.ActionName,
                        StatusCode = (int)HttpStatusCode.NotFound,
                        StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.NotFound),
                        Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid
                    };
                    return View("Error", ev);
                }
                else
                {
                    try
                    {
                        _db.Detail.Remove(detail);
                        _db.SaveChanges();
                        SuccessViewModel sc = new SuccessViewModel()
                        {
                            StatusCode = (int)HttpStatusCode.Accepted,
                            StatusMessage = ReasonPhrases.GetReasonPhrase((int)HttpStatusCode.Accepted),
                            Controller = ControllerContext.ActionDescriptor.ControllerName,
                            Action = ControllerContext.ActionDescriptor.ActionName,
                            Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid
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
                            Id = String.IsNullOrEmpty(headerid.ToString()) ? 0 : (int)headerid
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