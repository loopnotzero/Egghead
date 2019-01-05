﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Advert.Common;
using Advert.Common.Posts;
using Advert.MongoDbStorage.Stores;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Advert.Managers
{
    public class PostsVotesManager<T> : IDisposable where T : IPostVote
    {
        private bool _disposed;

        /// <summary>
        /// The <see cref="T:Microsoft.AspNetCore.Identity.ILookupNormalizer" /> used to normalize things like user and role names.
        /// </summary>
        private ILookupNormalizer KeyNormalizer { get; set; }
        
        protected virtual CancellationToken CancellationToken => CancellationToken.None;

        /// <summary>
        /// Gets or sets the persistence store the manager operates over.
        /// </summary>
        /// <value>The persistence store the manager operates over.</value>
        protected internal IPostsVotesStore<T> Store { get; set; }

        public PostsVotesManager(IPostsVotesStore<T> store, ILookupNormalizer keyNormalizer)
        {
            Store = store ?? throw new ArgumentNullException(nameof(store));
            KeyNormalizer = keyNormalizer;
        }

        public async Task CreatePostVoteAsync(T entity)
        {
            ThrowIfDisposed();

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await Store.CreatePostVoteAsync(entity, CancellationToken);
        }

        public async Task<T> FindPostVoteAsync(ObjectId postId, ObjectId profileId)
        {
            ThrowIfDisposed();

            if (postId == ObjectId.Empty)
            {
                throw new ArgumentNullException(nameof(postId));
            }

            if (profileId == ObjectId.Empty)
            {
                throw new ArgumentNullException(nameof(profileId));
            }

            return await Store.FindPostVoteAsync(postId, profileId, CancellationToken);
        }
        
        public async Task<List<T>> FindPostsVotesAsync(ObjectId profileId)
        {
            ThrowIfDisposed();

            if (profileId == ObjectId.Empty)
            {
                throw new ArgumentNullException(nameof(profileId));
            }

            return await Store.FindPostsVotesAsync(profileId, CancellationToken);
        }

        public async Task<long> CountPostVotesAsync(ObjectId postId, VoteType voteType)
        {
            ThrowIfDisposed();

            if (postId == ObjectId.Empty)
            {
                throw new ArgumentNullException(nameof(postId));
            }

            return await Store.CountPostVotesAsync(postId, voteType, CancellationToken);
        }

        public async Task<DeleteResult> DeletePostVoteByIdAsync(ObjectId voteId)
        {
            ThrowIfDisposed();

            if (voteId == ObjectId.Empty)
            {
                throw new ArgumentNullException(nameof(voteId));
            }
            
            return await Store.DeletePostVoteByIdAsync(voteId, CancellationToken);
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing || _disposed)
                return;

            Store.Dispose();

            _disposed = true;
        }
        
        private string NormalizeKey(string key)
        {
            return KeyNormalizer != null ? KeyNormalizer.Normalize(key) : key;
        }

        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}