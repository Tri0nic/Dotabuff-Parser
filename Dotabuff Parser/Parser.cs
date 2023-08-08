namespace Dotabuff_Parser
{
    public class Parser
    {
        /// <summary>
        /// Создание словаря с полной статистикой противников
        /// </summary>
        /// <param name="firstEnemy">Первый противник</param>
        /// <param name="secondEnemy">Второй противник</param>
        /// <param name="thirdEnemy">Третий противник</param>
        /// <param name="fourthEnemy">Четвертый противник</param>
        /// <returns></returns>
        public static Dictionary<string, List<float>> CharacterStats(Dictionary<string, List<float>> firstEnemy,
                                                                     Dictionary<string, List<float>> secondEnemy,
                                                                     Dictionary<string, List<float>> thirdEnemy,
                                                                     Dictionary<string, List<float>> fourthEnemy,
                                                                     string firstEnemyStr,
                                                                     string secondEnemyStr,
                                                                     string thirdEnemyStr,
                                                                     string fourthEnemyStr)
        {
            // Удаление занятых персонажей
            RemoveOccupiedCharacters(firstEnemy, secondEnemyStr, thirdEnemyStr, fourthEnemyStr);
            RemoveOccupiedCharacters(secondEnemy, firstEnemyStr, thirdEnemyStr, fourthEnemyStr);
            RemoveOccupiedCharacters(thirdEnemy, secondEnemyStr, firstEnemyStr, fourthEnemyStr);
            RemoveOccupiedCharacters(fourthEnemy, secondEnemyStr, thirdEnemyStr, firstEnemyStr);

            // Вывод результата
            return ResultStats(firstEnemy, secondEnemy, thirdEnemy, fourthEnemy);
        }

        /// <summary>
        /// Вычисление словаря с формой: Key: characterName; Value (List): disadv1, disadv2, disadv3, disadv4, sumDisadv, sumWinrate;
        /// </summary>
        /// <param name="firstEnemy"></param>
        /// <param name="secondEnemy"></param>
        /// <param name="thirdEnemy"></param>
        /// <param name="fourthEnemy"></param>
        /// <returns></returns>
        public static Dictionary<string, List<float>> ResultStats(Dictionary<string, List<float>> firstEnemy, Dictionary<string, List<float>> secondEnemy, Dictionary<string, List<float>> thirdEnemy, Dictionary<string, List<float>> fourthEnemy)
        {
            Dictionary<string, List<float>> result = new Dictionary<string, List<float>>();

            #region Добавление четырех disadventage
            if (firstEnemy != null)
            {
                foreach (var kvp1 in firstEnemy)
                {
                    var firstElement = kvp1.Value.First();
                    result.Add(kvp1.Key, new List<float> { firstElement });
                }
            }
            else
            {
                throw new Exception("Введите персонажа");
            }
            MergeFirstListElementInDictionaries(result, secondEnemy);
            MergeFirstListElementInDictionaries(result, thirdEnemy);
            MergeFirstListElementInDictionaries(result, fourthEnemy);
            #endregion
            // Вычисление сумм disadventage и winrate
            Dictionary<string, List<float>> disadventageAndWinrateSum = DisadventageAndWinrateSum(firstEnemy, secondEnemy, thirdEnemy, fourthEnemy);
            // Добавление сумм disadventage и winrate к результирующему словарю
            MergeDictionaries(result, disadventageAndWinrateSum);
            return result;
        }

        /// <summary>
        /// Слияние первого элемента списка словаря в другой словарь
        /// </summary>
        /// <param name="mergedDict">Словарь, в которое происходит слияние</param>
        /// <param name="dict">Сливающийся словарь</param>
        public static void MergeFirstListElementInDictionaries(Dictionary<string, List<float>> mergedDict, Dictionary<string, List<float>> dict)
        {
            if (dict == null)
            {
                foreach (var character in mergedDict)
                {
                    //float firstElement = new float();
                    mergedDict[character.Key].Add(new float());
                }

            }
            else
            {
                foreach (var character in dict)
                {
                    var firstElement = character.Value.First();
                    mergedDict[character.Key].Add(firstElement);
                }
            }

        }

        /// <summary>
        /// Слияние двух словарей
        /// </summary>
        /// <param name="mergedDict">Словарь, в которое происходит слияние</param>
        /// <param name="dict">Сливающийся словарь</param>
        public static void MergeDictionaries(Dictionary<string, List<float>> mergedDict, Dictionary<string, List<float>> dict)
        {
            foreach (var character in dict)
            {
                mergedDict[character.Key].AddRange(character.Value);
            }
        }
        /// <summary>
        /// Вычисление сумм Disadventage и Winrate
        /// </summary>
        /// <param name="firstEnemy"></param>
        /// <param name="secondEnemy"></param>
        /// <param name="thirdEnemy"></param>
        /// <param name="fourthEnemy"></param>
        public static Dictionary<string, List<float>> DisadventageAndWinrateSum(Dictionary<string, List<float>> firstEnemy, Dictionary<string, List<float>> secondEnemy, Dictionary<string, List<float>> thirdEnemy, Dictionary<string, List<float>> fourthEnemy)
        {   // TODO: сделать перегрузку для двух словарей
            Dictionary<string, List<float>> result = new Dictionary<string, List<float>>();

            //firstEnemy ??= new Dictionary<string, List<float>>();
            //secondEnemy ??= new Dictionary<string, List<float>>();
            //thirdEnemy ??= new Dictionary<string, List<float>>();
            //fourthEnemy ??= new Dictionary<string, List<float>>();

            foreach (var character in firstEnemy)
            {
                string key = character.Key.ToLower(); //.Replace("-", " ")

                List<float> value = firstEnemy[key]
                .Zip(secondEnemy?.GetValueOrDefault(key) ?? Enumerable.Repeat(0f, firstEnemy[key].Count), (a, b) => a + b)
                .Zip(thirdEnemy?.GetValueOrDefault(key) ?? Enumerable.Repeat(0f, firstEnemy[key].Count), (ab, c) => ab + c)
                .Zip(fourthEnemy?.GetValueOrDefault(key) ?? Enumerable.Repeat(0f, firstEnemy[key].Count), (abc, d) => abc + d)
                .ToList();

                result[key] = value;
            }
            return result;
        }

        /// <summary>
        /// Парсинг страницы в словарь со статистикой одного противника
        /// </summary>
        /// <param name="url">Ссылка на героя противника</param>
        /// <returns></returns>
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
                                        //else
                                        //{
                                        //    Console.WriteLine("No rows");
                                        //}
                                    }
                                    //else
                                    //{
                                    //    Console.WriteLine("No tables");
                                    //}
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return null;
        }
        
        /// <summary>
        /// Создание ссылки для парсинга
        /// </summary>
        /// <param name="characterName"></param>
        /// <returns></returns>
        public static string UrlCreator(string characterName)
        {
            return $"https://www.dotabuff.com/heroes/{characterName?.Replace(" ", "-")}/counters";
        }

        /// <summary>
        /// Удаление из словаря занятых персонажей
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="firstCharacterStr"></param>
        /// <param name="secondCharacterStr"></param>
        /// <param name="thirdCharacterStr"></param>
        public static void RemoveOccupiedCharacters(Dictionary<string, List<float>> dict, string firstCharacterStr, string secondCharacterStr, string thirdCharacterStr)
        {
            if (firstCharacterStr != null)
                dict?.Remove(firstCharacterStr);
            if (secondCharacterStr != null)
                dict?.Remove(secondCharacterStr);
            if (thirdCharacterStr != null)
                dict?.Remove(thirdCharacterStr);
        }
    }
}
