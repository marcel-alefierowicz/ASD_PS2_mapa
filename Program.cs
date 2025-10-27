class Program
{

    static int dfs(string[] input, bool[,] scanned, int x, int y, int n, int m)
    {
        if (x <= 0 || x >= n || y <= 0 || y >= m || input[x][y] != 'x' || scanned[x, y])
            return 0;
        scanned[x, y] = true;
        int area = 1;
        area += dfs(input, scanned, x + 1, y, n, m); // po prawej
        area += dfs(input, scanned, x - 1, y, n, m); // po lewej
        area += dfs(input, scanned, x, y + 1, n, m); // wyzej
        area += dfs(input, scanned, x, y - 1, n, m); // nizej
        area += dfs(input, scanned, x + 1, y + 1, n, m); // po prawej i wyzej
        area += dfs(input, scanned, x - 1, y - 1, n, m); // po lewej i nizej
        area += dfs(input, scanned, x + 1, y - 1, n, m); // po prawej i nizej
        area += dfs(input, scanned, x - 1, y + 1, n, m); // po lewej i wyzej

        return area;
    }

    static void scanner(string[] input)
    {
        int n = int.Parse(input[0].Split(' ')[0]); // kolumn
        int m = int.Parse(input[0].Split(' ')[1]); // wierszy

        string[] area = input[1..(input.Length)];
        bool[,] scanned = new bool[n, m];

        int biggestIslandArea = 0;
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (area[i][j] == 'x')
                {
                    int currentArea = dfs(area, scanned, i, j, n, m);
                    biggestIslandArea = Math.Max(biggestIslandArea, currentArea);
                }
            }
        }
        Console.WriteLine(biggestIslandArea);
    }

    public static void Main(string[] args)
    {
        scanner(File.ReadAllLines("./in.txt"));
        return;
    }
}
