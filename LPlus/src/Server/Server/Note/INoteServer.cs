

using Model.Note;
using System.Collections.Generic;

namespace Server.Server.Note
{
    public interface INoteServer
    {
        List<NoteModel> GetNotes();
    }
}
