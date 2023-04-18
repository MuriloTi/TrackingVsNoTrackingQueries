using BenchmarkDotNet.Attributes;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QueryPerformance.Data;
using QueryPerformance.Models;

namespace QueryPerformance
{
    [MemoryDiagnoser]
    public class QueryPerformance
    {
        [Params(200)]
        public int NumPosts { get; set; }


        [Benchmark(Baseline = true)]
        public List<Post> AsTracking()
        {
            using var context = new BloggingContext();

            return context.Posts.ToList();
        }

        [Benchmark]
        public List<Post> AsNoTracking()
        {
            using var context = new BloggingContext();

            return context.Posts.AsNoTracking().ToList();
        }

        [Benchmark]
        public List<Post> FromSqlRaw()
        {
            using var context = new BloggingContext();

            return context.Posts.FromSqlRaw("SELECT * FROM Posts").ToList();
        }

        [Benchmark]
        public List<Post> FromSqlRawAsNoTracking()
        {
            using var context = new BloggingContext();

            return context.Posts.FromSqlRaw("SELECT * FROM Posts").AsNoTracking().ToList();
        }

        [Benchmark]
        public async Task<List<Post>> Dapper()
        {
            string connectionString = @"Server=(localdb)\mssqllocaldb;Database=Blogging;Trusted_Connection=True";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return (await connection.QueryAsync<Post>("SELECT * FROM Posts")).ToList();
            }
        }

        /** Results
         * 
BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19044.2846/21H2/November2021Update)
Intel Core i5-2500K CPU 3.30GHz (Sandy Bridge), 1 CPU, 4 logical and 4 physical cores
.NET SDK=7.0.203
  [Host]     : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX
  DefaultJob : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX


|                 Method | NumPosts |       Mean |    Error |   StdDev | Ratio | RatioSD |    Gen0 |   Gen1 | Allocated | Alloc Ratio |
|----------------------- |--------- |-----------:|---------:|---------:|------:|--------:|--------:|-------:|----------:|------------:|
|             AsTracking |      200 | 1,297.1 us | 33.12 us | 96.10 us |  1.00 |    0.00 | 93.7500 |      - |  293.6 KB |        1.00 |
|           AsNoTracking |      200 |   654.8 us | 13.04 us | 24.81 us |  0.51 |    0.04 | 39.0625 |      - | 121.79 KB |        0.41 |
|             FromSqlRaw |      200 | 1,287.0 us | 25.56 us | 46.09 us |  0.99 |    0.07 | 97.6563 | 1.9531 | 299.62 KB |        1.02 |
| FromSqlRawAsNoTracking |      200 |   656.8 us | 11.52 us | 27.39 us |  0.51 |    0.04 | 41.0156 |      - | 127.84 KB |        0.44 |
|                 Dapper |      200 |   444.4 us |  8.87 us | 15.06 us |  0.34 |    0.03 |  9.7656 |      - |  31.05 KB |        0.11 |

         */

    }
}
