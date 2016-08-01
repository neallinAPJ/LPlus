
using System.Linq;
using Dapper;
using Model.Comment;
using Model.Note;
using Server.DataProvide;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Server.Note
{
    public class NoteServer: INoteServer
    {
        private IMySqlContext _context;
        public NoteServer(IMySqlContext context)
        {
            _context = context;
        }
        public List<NoteModel> GetNotes(int pageIndex)
        {
            pageIndex = pageIndex * 5;
            string queryStr =string.Format("SELECT * FROM Note ORDER BY CreateDate DESC LIMIT {0},{1};SELECT * FROM Comment where NoteID in (select t.ID from (select * from Note ORDER BY CreateDate DESC LIMIT {0},{1})as t);", pageIndex,5);
            //string queryStr = "select * from Note";
            var mapped = _context.Connection.QueryMultiple(queryStr, null, null).Map<NoteModel, CommentModel, int>
            (
               note => note.ID,
               comment => comment.NoteID.Value,
               (note, comments) => { note.Comments = comments; }
            );
            return mapped.ToList(); ;
        }
        public async Task PostCommentAsync(CommentModel comment)
        {
            try
            {
                if (string.IsNullOrEmpty(comment.UserPicture))
                {
                    comment.UserPicture = "/css/comment/image/tieba/1.jpg";
                }
                comment.UpdateBy = comment.CreateBy;
                comment.CreateDate = comment.UpdateDate = DateTime.Now;
                string sqlStr = "INSERT INTO Comment(ID,UserID,Content,CreateDate,CreateBy,UpdateDate,UpdateBy,ParentID,NoteID,UserPicture)" +
                    " VALUES(@ID,@UserID,@Content,@CreateDate,@CreateBy,@UpdateDate,@UpdateBy,@ParentID,@NoteID,@UserPicture);SELECT last_insert_id();";
                var id=await _context.Connection.ExecuteScalarAsync<int>(sqlStr, comment);
                comment.ID = id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task LikeCountAsync(int noteID)
        {
            try
            {
                string sqlStr = "UPDATE Note SET LikeCount=LikeCount+1 WHERE ID=@NoteID";
                await _context.Connection.ExecuteAsync(sqlStr, new { NoteID = noteID });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteCommentAsync(int commentID)
        {
            try
            {
                string sqlStr = "DELETE FROM Comment WHERE ID=@CommentID";
                await _context.Connection.ExecuteAsync(sqlStr, new { CommentID = commentID });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task PublishNoteAsync(NoteModel note)
        {
            try
            {
                if (string.IsNullOrEmpty(note.UserPicture))
                {
                    note.UserPicture = "/css/comment/image/tieba/1.jpg";
                }
                note.UpdateBy = note.CreateBy;
                note.CreateDate = note.UpdateDate = DateTime.Now;
                string sqlStr = "INSERT INTO Note(ID,UserID,Content,CreateDate,CreateBy,UpdateDate,UpdateBy,Address,LikeCount,UserPicture)" +
                    " VALUES(@ID,@UserID,@Content,@CreateDate,@CreateBy,@UpdateDate,@UpdateBy,@Address,@LikeCount,@UserPicture);";
                await _context.Connection.ExecuteAsync(sqlStr, note);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
