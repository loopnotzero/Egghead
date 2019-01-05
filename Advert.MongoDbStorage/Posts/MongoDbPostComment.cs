﻿using System;
using Advert.Common.Posts;
using MongoDB.Bson;

namespace Advert.MongoDbStorage.Posts
{
    public class MongoDbPostComment : IPostComment
    {
        public MongoDbPostComment()
        {
            Id = ObjectId.GenerateNewId();
            //Create indeces
        }

        public bool IsDeleted { get; set; }
        public long VotesCount { get; set; }
        public string Text { get; set; }  
        public string ProfileName { get; set; }
        public string ProfileImagePath { get; set; }
        public ObjectId Id { get; set; }
        public ObjectId ReplyTo { get; set; }
        public ObjectId ProfileId { get; set; }
        public ObjectId PostId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ChangedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}