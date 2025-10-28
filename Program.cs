using System.Diagnostics;

class Program
{

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
        area += dfs(input, scanned, x + 1, y, n, m, ref isValid); // po prawej
        area += dfs(input, scanned, x - 1, y, n, m, ref isValid); // po lewej
        area += dfs(input, scanned, x, y + 1, n, m, ref isValid); // wyzej
        area += dfs(input, scanned, x, y - 1, n, m, ref isValid); // nizej
        area += dfs(input, scanned, x + 1, y + 1, n, m, ref isValid); // po prawej i wyzej
        area += dfs(input, scanned, x - 1, y - 1, n, m, ref isValid); // po lewej i nizej
        area += dfs(input, scanned, x + 1, y - 1, n, m, ref isValid); // po prawej i nizej
        area += dfs(input, scanned, x - 1, y + 1, n, m, ref isValid); // po lewej i wyzej

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
        scanner(File.ReadAllLines("./in2.txt"));
        s.Stop();
        System.Console.WriteLine(s.ElapsedMilliseconds);
        return;
    }
}
