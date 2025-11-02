using System.Diagnostics;

class Program
{
    static (int, int)[] directions = {
        (-1, -1), (-1, 0), (-1, 1),
        (0, -1),           (0, 1),
        (1, -1),  (1, 0),  (1, 1)
    };

    static int dfs(string[] input, bool[,] scanned, int x, int y, int n, int m, ref bool isValid)
    {
        if (x <= 0 || x >= n || y <= 0 || y >= m || input[x][y] != 'x' || scanned[x, y])
            return 0;

        if (x == 0 || y == 0 || y == m - 1 || x == n - 1)
        {
            isValid = false;
        }


        scanned[x, y] = true;
        int area = 1;
        foreach (var (dx, dy) in directions)
        {
            int nx = x + dx, ny = y + dy;
            if (nx >= 0 && nx < n && ny >= 0 && ny < m)
                area += dfs(input, scanned, nx, ny, n, m, ref isValid);
        }

        return area;
    }

    static void scanner(string[] input)
    {
        int n = int.Parse(input[0].Split(' ')[0]); // kolumn
        int m = int.Parse(input[0].Split(' ')[1]); // wierszy

        string[] area = input[1..(input.Length)];
        bool[,] scanned = new bool[n, m];

        int biggestIslandArea = 0;
        int coastIndex = 0;
        for (int i = 0; i < m; i++)
        {
            if (area[i].All(c => c == 'o') && i > coastIndex)
            {
                coastIndex = i;
            }
        }
        for (int i = 0; i < coastIndex; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (area[i][j] == 'x')
                {
                    bool isValid = true;
                    int currentArea = dfs(area, scanned, i, j, n, m, ref isValid);
                    if (isValid)
                        biggestIslandArea = Math.Max(biggestIslandArea, currentArea);
                }
            }
        }
        Console.WriteLine(biggestIslandArea);
    }

    public static void Main(string[] args)
    {
        Stopwatch s = new Stopwatch();
        s.Start();
        scanner(File.ReadAllLines("./in3.txt"));
        s.Stop();
        Console.WriteLine($"elapsed: {s.ElapsedMilliseconds}");
        return;
    }
}
