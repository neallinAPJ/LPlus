using Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Model.Comment
{
    public class CommentModel
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public int UserID { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public UserModel User { get; set; }
        public string UserPicture { get; set; }
        public int NoteID { get; set; }
    }
}
