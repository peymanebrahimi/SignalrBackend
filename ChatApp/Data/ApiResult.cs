using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Threading.Tasks;

namespace ChatApp.Data
{
    public class ApiResult<T>where T:IHaveUserId
    {
        public List<T> Data { get; }
        public long TotalCount { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
        public string SortColumn { get; }
        public string SortOrder { get; }
        public string FilterColumn { get; }
        public string FilterQuery { get; }
        public int TotalPages { get; }

        public ApiResult(List<T> data, long count, int pageIndex, int pageSize, string sortColumn,
            string sortOrder, string filterColumn, string filterQuery)
        {
            Data = data;
            TotalCount = count;
            PageIndex = pageIndex;
            PageSize = pageSize;
            SortColumn = sortColumn;
            SortOrder = sortOrder;
            FilterColumn = filterColumn;
            FilterQuery = filterQuery;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public static async Task<ApiResult<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize,
            string sortColumn = null, string sortOrder = null, string filterColumn = null,
            string filterQuery = null)
        {
            if (!string.IsNullOrEmpty(filterColumn) &&
                !string.IsNullOrEmpty(filterQuery) &&
                IsValidProperty(filterColumn))
            {
                //source = source.Where($"{filterColumn}.Contains(@{filterQuery})");

                source = source.Where($"{filterColumn}.Contains(@0)", filterQuery);
            }


            int count = await source.CountAsync();

            if (!string.IsNullOrEmpty(sortColumn) && IsValidProperty(sortColumn))
            {
                sortOrder = !string.IsNullOrEmpty(sortOrder) &&
                            sortOrder.ToUpper() == "ASC"
                    ? "ASC"
                    : "DESC";
                source = source.OrderBy($"{sortColumn} {sortOrder}");
            }

            source = source
                .Skip(pageIndex * pageSize)
                .Take(pageSize);
            var data = await source.ToListAsync();

            return new ApiResult<T>(data, count, pageIndex, pageSize, sortColumn, sortOrder, filterColumn, filterQuery);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 0);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return ((PageIndex + 1) < TotalPages);
            }
        }

        public static bool IsValidProperty(string propertyName, bool throwExceptionIfNotFound = true)
        {
            var prop = typeof(T).GetProperty(propertyName,
                BindingFlags.IgnoreCase |
                BindingFlags.Public |
                BindingFlags.Instance);

            if (prop == null && throwExceptionIfNotFound)
            {
                throw new NotSupportedException($"ERROR: Property {propertyName} does not exist.");
            }

            return prop != null;
        }

        public static async Task<ApiResult<T>> CreateAsync(IMongoCollection<T> source, string userId, int pageIndex,
            int pageSize, string sortColumn, string sortOrder, string filterColumn, string filterQuery)
        {
            if (string.IsNullOrEmpty(sortColumn))
            {
                sortColumn = "id";
            }

            var filterDefinition = FilterDefinition<T>.Empty;
            var userFilter = Builders<T>.Filter.Eq(x => x.UserId, userId);
            
            if (!string.IsNullOrEmpty(filterColumn) &&
                !string.IsNullOrEmpty(filterQuery))
            {
                //filterDefinition = Builders<T>.Filter.Regex(filterColumn, $".*{filterQuery}.*");
                filterDefinition = Builders<T>.Filter.Regex(filterColumn,
                    new BsonRegularExpression($".*{filterQuery}.*", "i"));
            }

            var combineFilters = Builders<T>.Filter.And(userFilter, filterDefinition);

            var count = await source.CountDocumentsAsync(combineFilters);

            var sort = Builders<T>.Sort.Ascending(sortColumn);

            if (!string.IsNullOrEmpty(sortColumn))// && IsValidProperty(sortColumn))
            {
                sortOrder = !string.IsNullOrEmpty(sortOrder) &&
                            sortOrder.ToUpper() == "ASC"
                    ? "ASC" : "DESC";
                if (sortOrder == "DESC")
                {
                    sort = Builders<T>.Sort.Descending(sortColumn);
                }
            }

            var data = await source.Find(combineFilters).Sort(sort)
                .Skip(pageIndex * pageSize)
                .Limit(pageSize)
                .ToListAsync();

            return new ApiResult<T>(data, count, pageIndex, pageSize, sortColumn, sortOrder, filterColumn, filterQuery);
        }
    }

}
