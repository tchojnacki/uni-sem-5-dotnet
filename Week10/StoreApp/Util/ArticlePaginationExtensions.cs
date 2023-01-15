using System.Collections.Generic;
using System.Linq;
using StoreApp.Models;

namespace StoreApp.Util
{
    public static class ArticlePaginationExtensions
    {
        private const int N = 5;

        public static IEnumerable<Article> GetPage(this IQueryable<Article> source, int page = 0) =>
            source.OrderBy(e => e.Id).Skip(page * N).Take(N).AsEnumerable();
    }
}
