using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        static string results = string.Empty;
  

        static void Main(string[] args)
        {
            var menu = "Pressione 's' para download síncrono e 'a' para download asincrono, ou 9 para sair.";
            string opcao = string.Empty;
            do
            {
                results = string.Empty;
           
                Console.Clear();
                Console.WriteLine(menu);
                opcao = Console.ReadLine();

                if (opcao == "s")
                    executeSync();
                else if (opcao == "a")
                    executeAsync();

                if (opcao == "a" || opcao == "s")
                {
                    
                    Console.WriteLine(results);

                    Console.ReadLine();
                }

            } while (opcao != "9");
           
        }

        static void executeSync()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            runDownloadSync();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            results += $"Tempo execução síncrono {elapsedMs} ms";
        }

        private static void runDownloadSync()
        {
            var websites = prepData();

            foreach (var website in websites)
            {
                var results = downloadWebsite(website);
                reportWebSiteInfo(results);
            }
        }

        private static void reportWebSiteInfo(WebSiteDataModel data)
        {
            Console.WriteLine( $"{data.WebsiteUrl} baixado: {data.WebsiteData.Length} caractares de conteúdo"); 
        }

        private static WebSiteDataModel downloadWebsite(string website)
        {
            var output = new WebSiteDataModel();
            var client = new WebClient();

            output.WebsiteUrl = website;
            output.WebsiteData = client.DownloadString(website);

            return output;
        }

        static async void executeAsync()
        {

            var watch = System.Diagnostics.Stopwatch.StartNew();

            await runDownloadAsync();

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            results += $"Tempo execução síncrono {elapsedMs} ms";

        }

        private static async Task runDownloadAsync()
        {
            var websites = prepData();
            var tasks = new List<Task<WebSiteDataModel>>();

            foreach (var website in websites)
            {
                tasks.Add(Task.Run(() => downloadWebsite(website)));                
            }

            var results = await Task.WhenAll(tasks);

            foreach (var item in results)
            {
                reportWebSiteInfo(item);
            }
        }

        static List<string> prepData()
        {
            var output = new List<string>();

            output.Add("https://www.yahoo.com");
            output.Add("https://www.google.com");
            output.Add("https://www.microsoft.com");
            output.Add("https://www.cnn.com");
            output.Add("https://www.codeproject.com");
            output.Add("https://www.stackoverflow.com");

            return output;
        }
    }
}
