﻿using Blogger.Domain.Common.Exceptions;

namespace Blogger.Application.Comments.ApproveReply;
public class CommentNotFoundException : BlogException
{
    private const string _message = "Comment not found.";

    public CommentNotFoundException() : base(_message)
    {

    }
}
