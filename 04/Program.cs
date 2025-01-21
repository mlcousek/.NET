using System;
using System.Threading;
using static PNE04.ReadersWriters;
using static PNE04.DatasetCompare;

//CONST
int THREAD_COUNT = 3;

Console.WriteLine("Hello");

for (int i = 0; i < THREAD_COUNT; i++)
{
    new Thread(Read).Start();
    new Thread(Write).Start();
}



//jen taková blbost, ať se to pak na výstupu nemíchá
await Task.Delay((THREAD_COUNT + 1) * 1000);
Console.WriteLine("\n");



string[] urls = {
                "https://archive.ics.uci.edu/ml/machine-learning-databases/flags/flag.data",
                "https://archive.ics.uci.edu/ml/machine-learning-databases/breast-cancer-wisconsin/breast-cancer-wisconsin.data",
                "https://archive.ics.uci.edu/ml/machine-learning-databases/mushroom/agaricus-lepiota.data",
                "https://archive.ics.uci.edu/ml/machine-learning-databases/zoo/zoo.data",
                "https://archive.ics.uci.edu/ml/machine-learning-databases/voting-records/house-votes-84.data"
            };

Console.WriteLine("\nStahuji datasety sekvenčně!\n");
var watch = System.Diagnostics.Stopwatch.StartNew();
foreach (var url in urls)
{
    await DownloadAndAnalyze(url);
}
watch.Stop();
Console.WriteLine($"\nSekvenční doba: {watch.Elapsed.TotalSeconds}s\n");

Console.WriteLine("Stahuji datasety paralelně!\n");
watch.Restart();
await Task.WhenAll(urls.Select(url => DownloadAndAnalyze(url)));
watch.Stop();
Console.WriteLine($"\nParalelně to trvalo: {watch.Elapsed.TotalSeconds}s");