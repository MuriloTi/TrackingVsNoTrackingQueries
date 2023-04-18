### Benchmark for Tracking vs No-Tracking Queries

|                 Method | NumPosts |       Mean |    Error |   StdDev | Ratio | RatioSD |    Gen0 |   Gen1 | Allocated | Alloc Ratio |
|----------------------- |--------- |-----------:|---------:|---------:|------:|--------:|--------:|-------:|----------:|------------:|
|             AsTracking |      200 | 1,297.1 us | 33.12 us | 96.10 us |  1.00 |    0.00 | 93.7500 |      - |  293.6 KB |        1.00 |
|           AsNoTracking |      200 |   654.8 us | 13.04 us | 24.81 us |  0.51 |    0.04 | 39.0625 |      - | 121.79 KB |        0.41 |
|             FromSqlRaw |      200 | 1,287.0 us | 25.56 us | 46.09 us |  0.99 |    0.07 | 97.6563 | 1.9531 | 299.62 KB |        1.02 |
| FromSqlRawAsNoTracking |      200 |   656.8 us | 11.52 us | 27.39 us |  0.51 |    0.04 | 41.0156 |      - | 127.84 KB |        0.44 |
|                 Dapper |      200 |   444.4 us |  8.87 us | 15.06 us |  0.34 |    0.03 |  9.7656 |      - |  31.05 KB |        0.11 |
