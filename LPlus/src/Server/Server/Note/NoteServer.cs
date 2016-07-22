

using Dapper;
using Model.Comment;
using Model.Note;
using Server.DataProvide;
using System.Collections.Generic;

namespace Server.Server.Note
{
    public class NoteServer: INoteServer
    {
        private IMySqlContext _context;
        public NoteServer(IMySqlContext context)
        {
            _context = context;
        }
        public List<NoteModel> GetNotes()
        {
            string queryStr = @"SELECT * FROM Note;SELECT * FROM Comment where NoteID in (select ID from Note);";
            //string queryStr = "select * from Note";
            var mapped = _context.Connection.QueryMultiple(queryStr, null, null).Map<NoteModel, CommentModel, int>
            (
               note => note.ID,
               comment => comment.NoteID,
               (note, comments) => { note.Comments = comments; }
            );
            return mapped.AsList<NoteModel>(); ;
        }
    }
}
