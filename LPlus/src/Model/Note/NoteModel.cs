﻿using Model.Comment;
using Model.User;
using System;
using System.Collections.Generic;

namespace Model.Note
{
    public class NoteModel
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Address { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public UserModel User { get; set; }
        public IEnumerable<CommentModel> Comments { get; set; }
        public int Like { get; set; }
        public string UserPicture { get; set; }
    }
}