using Model.Comment;
using Model.User;
using System;
using System.Collections.Generic;

namespace Model.Note
{
    public class NoteModel: BaseModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Address { get; set; }
        public string Content { get; set; }
        public UserModel User { get; set; }
        public IEnumerable<CommentModel> Comments { get; set; }
        public int LikeCount { get; set; }
        public string UserPicture { get; set; }
    }
}
