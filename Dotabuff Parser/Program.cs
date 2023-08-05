using Dotabuff_Parser;
using System;

class Program
{
    public static async Task Main()
    {
        //string heroName = Characters.characters[15];
        //Console.WriteLine(heroName);
        //Console.WriteLine("---------");
        //Parser parser = new Parser();
        //await Parser.Print(url);
        string firstUrl = "https://www.dotabuff.com/heroes/elder-titan/counters";
        string secondUrl = "https://www.dotabuff.com/heroes/leshrac/counters";
        string thirdUrl = "https://www.dotabuff.com/heroes/troll-warlord/counters";
        string fourthUrl = "https://www.dotabuff.com/heroes/earthshaker/counters";
        Dictionary<string, List<float>> firstEnemy = await Parser.ParsingAsync(firstUrl);
        Dictionary<string, List<float>> secondEnemy = await Parser.ParsingAsync(secondUrl);
        Dictionary<string, List<float>> thirdEnemy = await Parser.ParsingAsync(thirdUrl);
        Dictionary<string, List<float>> fourthEnemy = await Parser.ParsingAsync(fourthUrl);

        //await Console.Out.WriteLineAsync("elder-titan");
        //await Parser.Print(firstUrl);
        //await Console.Out.WriteLineAsync("leshrac");
        //await Parser.Print(secondUrl);
        //await Console.Out.WriteLineAsync("troll-warlord");
        //await Parser.Print(thirdUrl);
        //await Console.Out.WriteLineAsync("earthshaker");
        //await Parser.Print(fourthUrl);

        bestHero();

        ///int count1 = firstEnemy.Count;
        ///Console.WriteLine($"Количество элементов в словаре: {count1}");
        ///int count2 = secondEnemy.Count;
        ///Console.WriteLine($"Количество элементов в словаре: {count2}");
        ///int count3 = thirdEnemy.Count;
        ///Console.WriteLine($"Количество элементов в словаре: {count3}");
        ///int count4 = fourthEnemy.Count;
        ///Console.WriteLine($"Количество элементов в словаре: {count4}");
        // 123

        void bestHero()
        {
            Dictionary<string, List<float>> result = new Dictionary<string, List<float>>();
            // TODO: переписать --- ошибка возникает, тк ищем персонажа, которого нет
            firstEnemy.Remove("leshrac");
            firstEnemy.Remove("troll warlord");
            firstEnemy.Remove("earthshaker");
            foreach (var character in firstEnemy)
            {
                string key = character.Key.ToLower();
                List<float> value = firstEnemy[key].Zip(secondEnemy[key], (a, b) => a + b).ToList()
                    .Zip(thirdEnemy[key.ToLower()], (ab, c) => ab + c)
                    .Zip(fourthEnemy[key.ToLower()], (abc, d) => abc + d)
                    .ToList();
                result[key] = value;
            }

            var resultData = result.OrderByDescending(kv => kv.Value[0]).ToDictionary(kv => kv.Key, kv => kv.Value);

            if (resultData != null)
            {
                Console.WriteLine("Персонаж, disadventage, winrate");
                Console.WriteLine("-----------------------------------------");
                foreach (var item in resultData)
                {
                    Console.Out.WriteLineAsync($"{item.Key}, {item.Value[0]}, {item.Value[1]}");
                }
                Console.WriteLine("-----------------------------------------");
            }
        }
    }
}