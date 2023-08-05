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
        string url = "https://www.dotabuff.com/heroes/meepo/counters";
        await Parser.Print(url);
    }
}