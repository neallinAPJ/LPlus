using Model.User;
using System;

namespace Model.Comment
{
    public class CommentModel: BaseModel
    {
        public int ID { get; set; }
        public int? ParentID { get; set; }
        public int? UserID { get; set; }
        public string Content { get; set; }
        public UserModel User { get; set; }
        public string UserPicture { get; set; }
        public int? NoteID { get; set; }
    }
}
