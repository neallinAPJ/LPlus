

using Model.Comment;
using Model.Note;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Server.Note
{
    public interface INoteServer
    {
        List<NoteModel> GetNotes(int pageIndex);
        Task PostCommentAsync(CommentModel comment);
        Task LikeCountAsync(int noteID);
        Task DeleteCommentAsync(int commentID);
        Task PublishNoteAsync(NoteModel note);
    }
}
