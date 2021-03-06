﻿using System;
using Bazaar.Common.Posts;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Bazaar.MongoDbStorage.Posts
{
    public class MongoDbPostComment : IPostComment
    {
        public MongoDbPostComment()
        {
            _id = ObjectId.GenerateNewId();
            //Create indeces
        }

        [BsonId]
        public ObjectId _id { get; set; }
        public bool IsDeleted { get; set; }
        public long VotesCount { get; set; }
        public string Text { get; set; }  
        public string ProfileName { get; set; }
        public string ProfilePhoto { get; set; }
        public string IdentityName { get; set; }
        public ObjectId PostId { get; set; }
        public ObjectId ReplyTo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}