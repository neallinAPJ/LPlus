using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Model.Note;
using Server.Server.Note;
using Model.Comment;
using System.Threading.Tasks;
using Common.Helper;

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
            List<NoteModel> data = _server.GetNotes(pageIndex:0);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> PostComment(CommentModel comment)
        {
            comment.UserPicture = UserHelper.GetUserPicture(HttpContext);
            comment.UserID = UserHelper.GetUserID(HttpContext);
            await _server.PostCommentAsync(comment);
            return PartialView("_Comment",comment);
        }
        [HttpPost]
        public async Task<IActionResult> PublishNote(NoteModel note)
        {
            note.UserPicture = UserHelper.GetUserPicture(HttpContext);
            note.UserID = UserHelper.GetUserID(HttpContext);
            await _server.PublishNoteAsync(note);
            List<NoteModel> data = _server.GetNotes(pageIndex: 0);
            return PartialView("_Note", data);
        }
        public async Task LikeCount(int noteID)
        {
            await _server.LikeCountAsync(noteID);
        }
        public async Task DeleteComment(int commentID)
        {
            await _server.DeleteCommentAsync(commentID);
        }
        public IActionResult GetNotes(int pageIndex)
        {
            List<NoteModel> data = _server.GetNotes(pageIndex);
            if(data.Count()>0)
            {
                var datas = data.OrderByDescending(o => o.CreateDate).GroupBy(i => i.CreateDate.Date);
                return PartialView("_TimeLineData", datas);
            }
            return Content("NULL");
        }
    }
}
