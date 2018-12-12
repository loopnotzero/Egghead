﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Egghead.Common;
using Egghead.Common.Articles;
using Egghead.Models.Articles;
using Egghead.MongoDbStorage.Articles;
using Egghead.MongoDbStorage.Stores;
using MongoDB.Bson;

namespace Egghead.Managers
{
    public class ArticlesCommentsManager<T> : IDisposable where T : class
    {
        private bool _disposed;
        
        protected virtual CancellationToken CancellationToken => CancellationToken.None;

        /// <summary>
        /// Gets or sets the persistence store the manager operates over.
        /// </summary>
        /// <value>The persistence store the manager operates over.</value>
        protected internal IArticlesCommentsStore<T> Store { get; set; }
        
        public ArticlesCommentsManager(IArticlesCommentsStore<T> store)
        {
            Store = store ?? throw new ArgumentNullException(nameof(store));
        }
        
        public async Task CreateArticleComment(string collectionName, T entity)
        {
            ThrowIfDisposed();

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var articleComments = Store.GetArticleCommentsCollection(collectionName, CancellationToken);

            await articleComments.CreateArticleCommentAsync(entity, CancellationToken);
        }
        
        public async Task UpdateArticleCommentByIdAsync(string collectionName, ObjectId commentId, T entity)
        {
            ThrowIfDisposed();

            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var articleComments = Store.GetArticleCommentsCollection(collectionName, CancellationToken);

            await articleComments.UpdateArticleCommentByIdAsync(commentId, entity, CancellationToken);
        }

        public async Task<T> FindArticleCommentById(string collectionName, ObjectId commendId)
        {
            ThrowIfDisposed();

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }
            
            if (commendId.Equals(ObjectId.Empty))
            {
                throw new ArgumentNullException(nameof(commendId));
            }

            return await Store.GetArticleCommentsCollection(collectionName, CancellationToken).FindArticleCommentByIdAsync(commendId, CancellationToken);
        }
        
        public async Task<long> EstimatedArticleCommentsByArticleIdAsync(string collectionName)
        {
            ThrowIfDisposed();

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            return await Store.GetArticleCommentsCollection(collectionName, CancellationToken).EstimatedArticleCommentsCountAsync(CancellationToken);
        }
        
        public async Task<List<T>> FindArticleCommentsAsync(string collectionName, int? howManyElements)
        {
            ThrowIfDisposed();

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            return await Store.GetArticleCommentsCollection(collectionName, CancellationToken).FindArticleCommentsAsync(howManyElements, CancellationToken);
        }
        
        public async Task<List<T>> FindArticleCommentsAsync(string collectionName, int offset, int? howManyElements)
        {
            ThrowIfDisposed();

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            return await Store.GetArticleCommentsCollection(collectionName, CancellationToken).FindArticleCommentsAsync(offset, howManyElements, CancellationToken);
        }
        
        public async Task<List<T>> FindArticleCommentsAsync(string collectionName, int offset, int? howManyElements, SortDefinition sortDef)
        {
            ThrowIfDisposed();

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            return await Store.GetArticleCommentsCollection(collectionName, CancellationToken).FindArticleCommentsAsync(offset, howManyElements, sortDef, CancellationToken);
        }
        
        public async Task<List<T>> FindArticleCommentsByProfileIdAsync(string collectionName, ObjectId profileId)
        {
            ThrowIfDisposed();

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentNullException(nameof(collectionName));
            }

            if (profileId.Equals(ObjectId.Empty))
            {
                throw new ArgumentNullException(nameof(profileId)); 
            }

            return await Store.GetArticleCommentsCollection(collectionName, CancellationToken).FindArticleCommentsByProfileIdAsync(profileId, CancellationToken);
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

        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }   
    }
}