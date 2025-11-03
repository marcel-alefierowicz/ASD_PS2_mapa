using System.Diagnostics;

class Program
{
    static int operations = 0;
    static (int, int)[] diagDirections = { // do islandDFS
        (-1, -1), (-1, 0), (-1, 1),
        (0, -1),           (0, 1),
        (1, -1),  (1, 0),  (1, 1)
    };

    static (int, int)[] directions = { (-1, 0), (0, -1), (0, 1), (1, 0) }; // do riverDFS

    static int islandDFS(string[] input, bool[,] scanned, int x, int y, int n, int m, ref bool isValid)
    {

        if (x < 0 || x >= n || y < 0 || y >= m || input[x][y] != 'x' || scanned[x, y])
            return 0;
        scanned[x, y] = true;

        if (x == 0 || y == 0 || y == m - 1 || x == n - 1)
        {
            isValid = false;
            return 0;
        }

        int area = 1;
        foreach ((int dx, int dy) in diagDirections)
        {
            int nx = x + dx, ny = y + dy;
            if (nx >= 0 && nx < n && ny >= 0 && ny < m)
            {
                Program.operations++;
                area += islandDFS(input, scanned, nx, ny, n, m, ref isValid);
            }
        }

        return area;
    }
    static int riverDFS(string[] map, bool[,] visited, int x, int y, int n, int m, int length)
    {
        if (x < 0 || x >= n || y < 0 || y >= m || map[x][y] != 'u' || visited[x, y])
            return length - 1;

        visited[x, y] = true;
        int maxLen = length;

        foreach (var (dx, dy) in directions)
        {
            int nx = x + dx, ny = y + dy;
            Program.operations++;
            maxLen = Math.Max(maxLen, riverDFS(map, visited, nx, ny, n, m, length + 1));
        }

        visited[x, y] = false; // resetujemy na false aby umozliwic ponowny ruch przez te pole
        return maxLen;
    }
    static void scanner(string[] input)
    {
        int n = int.Parse(input[0].Split(' ')[0]); // kolumn
        int m = int.Parse(input[0].Split(' ')[1]); // wierszy

        string[] area = input[1..input.Length];
        bool[,] scanned = new bool[m, n];


        int biggestIslandArea = 0;
        int coastIndex = 0;
        int longestRiver = 0;

        for (int i = 0; i < m; i++)
        {
            if (area[i].All(c => c == 'o') && i > coastIndex)
            {
                coastIndex = i; // znajdujemy ostatnia linie stojacej wody
            }
        }

        for (int i = 0; i < coastIndex; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (area[i][j] == 'x')
                {
                    bool isValid = true;
                    int currentArea = islandDFS(area, scanned, i, j, n, m, ref isValid);
                    if (isValid)
                        biggestIslandArea = Math.Max(biggestIslandArea, currentArea);
                }
            }
        }
        System.Console.WriteLine(Program.operations);
        Program.operations = 0;

        for (int j = 0; j < n; j++)
        {

            if (area[coastIndex + 1][j] == 'u')
            {
                int currentLen = riverDFS(area, scanned, coastIndex + 1, j, n, m, 1);
                longestRiver = Math.Max(longestRiver, currentLen);
            }
        }
        System.Console.WriteLine($"{biggestIslandArea} {longestRiver}");
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
