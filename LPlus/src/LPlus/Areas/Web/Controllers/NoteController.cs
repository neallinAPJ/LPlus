using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Note;
using Server.Server.Note;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace LPlus.Areas.Web.Controllers
{
    [Area("Web")]
    public class NoteController : Controller
    {
        private INoteServer _server;
        public NoteController(INoteServer server)
        {
            _server = server;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<NoteModel> data = _server.GetNotes();
            return View(data);
        }
    }
}
