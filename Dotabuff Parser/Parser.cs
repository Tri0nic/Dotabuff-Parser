namespace Dotabuff_Parser
{
    internal class Parser
    {
        //string url = "";

        //"https://www.dotabuff.com/heroes/meepo/counters"

        public static async Task Print(string url)
        {
            Dictionary<string, List<float>> result = await ParsingAsync(url);
            if (result != null)
            {
                Console.WriteLine("Персонаж, disadventage, winrate");
                Console.WriteLine("-----------------------------------------");
                foreach (var item in result)
                {
                    await Console.Out.WriteLineAsync($"{item.Key}, {item.Value[0]}, {item.Value[1]}");
                }
                Console.WriteLine("-----------------------------------------");
            }
        }
        public static async Task<Dictionary<string, List<float>>> ParsingAsync(string url)
        {
            try
            {
                Dictionary<string, List<float>> result = new Dictionary<string, List<float>>();

                using (HttpClientHandler hdl = new HttpClientHandler { AllowAutoRedirect = false, AutomaticDecompression = System.Net.DecompressionMethods.Deflate | System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.None })
                {
                    using (var clnt = new HttpClient(hdl))
                    {
                        // Устанавливаем User-Agent заголовок
                        clnt.DefaultRequestHeaders.Add("User-Agent", "boba");
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
                                            foreach (var row in character)
                                            {
                                                var cells = row.SelectNodes(".//td");
                                                if (cells != null && cells.Count >= 4)  // Убедитесь, что есть достаточно ячеек
                                                {
                                                    string key = cells[1].InnerText.Trim().ToLower();

                                                    string disadventageString = cells[2].InnerText.Trim().Replace('.', ',');
                                                    float disadventage = float.Parse(disadventageString.Substring(0, disadventageString.Length - 1));

                                                    string winrateString = cells[3].InnerText.Trim().Replace('.', ',');
                                                    float winrate = float.Parse(winrateString.Substring(0, winrateString.Length - 1));

                                                    result[key] = new List<float> { disadventage, winrate };
                                                }
                                            }

                                            return result;
                                        }
                                        else
                                        {
                                            Console.WriteLine("No rows");
                                        }
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
