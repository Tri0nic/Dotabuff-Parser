namespace Dotabuff_Parser
{
    internal class Parser
    {
        //string url = "";

        //"https://www.dotabuff.com/heroes/meepo/counters"

        public static async Task Print(string url)
        {
            Dictionary<string, List<List<string>>> result = await ParsingAsync(url);
            if (result != null)
            {
                foreach (var item in result)
                {
                    Console.WriteLine("-----------------------------------------");
                    //Console.WriteLine(item.Key);
                    Console.WriteLine("-----------------------------------------");
                    item.Value.ForEach(r => Console.WriteLine(string.Join("\t", r)));
                    Console.WriteLine("-----------------------------------------\n");
                }
            }
        }
        private static async Task<Dictionary<string, List<List<string>>>> ParsingAsync(string url)
        {
            int i = 1;
            try
            {
                Dictionary<string, List<List<string>>> result = new Dictionary<string, List<List<string>>>();

                using (HttpClientHandler hdl = new HttpClientHandler { AllowAutoRedirect = false, AutomaticDecompression = System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.None })
                {
                    using (var clnt = new HttpClient(hdl))
                    {
                        // Устанавливаем User-Agent заголовок
                        clnt.DefaultRequestHeaders.Add("User-Agent", "User");
                        using (HttpResponseMessage resp = await clnt.GetAsync(url))
                        {
                            if (resp.IsSuccessStatusCode)
                            {
                                var html = await resp.Content.ReadAsStringAsync();
                                if (!string.IsNullOrEmpty(html))
                                {
                                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                                    doc.LoadHtml(html);

                                    var characters = doc.DocumentNode.SelectSingleNode(".//table[@class='sortable']//tbody");
                                    if (characters != null)
                                    {
                                        var character = characters.SelectNodes(".//tr");
                                        if (character != null && character.Count > 0)
                                        {
                                            var res = new List<List<string>>();

                                            foreach (var row in character)
                                            {
                                                var cells = row.SelectNodes(".//td");
                                                if (cells != null && cells.Count > 0)
                                                {
                                                    List<string> rowData = new List<string>();
                                                    int n = 1;
                                                    foreach (var cell in cells)
                                                    {
                                                        string cellText = cell.InnerText;
                                                        rowData.Add(cellText);
                                                    }

                                                    res.Add(rowData);
                                                }
                                            }

                                            result[$"character {i++}"] = res;
                                        }
                                        return result;
                                    }
                                    else
                                    {
                                        Console.WriteLine("No tables");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return null;
        }
    }
}
