using Com.Ericmas001.Util;
using System.Collections.Generic;

namespace Com.Ericmas001.Games
{
    public static class GameCardUtility
    {
        public static List<GameCard> GetSortedDeck(bool jokers)
        {
            var deck = new List<GameCard>();
            for (var i = 0; i < 4; ++i)
                for (var j = 0; j < 13; ++j)
                    deck.Add(new GameCard((GameCardKind)i, (GameCardValue)j));
            if (jokers)
            {
                deck.Add(new GameCard(GameCardSpecial.JokerColor));
                deck.Add(new GameCard(GameCardSpecial.JokerDark));
            }
            return deck;
        }

        public static Stack<GameCard> GetShuffledDeck(bool jokers)
        {
            var deck = new Stack<GameCard>();
            var restantes = GetSortedDeck(jokers);
            while (restantes.Count > 0)
            {
                var id = Hasard.RandomWithMax(restantes.Count - 1);
                deck.Push(restantes[id]);
                restantes.RemoveAt(id);
            }
            return deck;
        }

        public static char ValueChar(GameCardValue v)
        {
            var chars = "23456789TJQKA";
            return chars[(int)v];
        }

        public static char KindChar(GameCardKind k)
        {
            var chars = "cdhs";
            return chars[(int)k];
        }

        public static string SpecialStr(GameCardSpecial s)
        {
            string[] strs = { "", "--", "??", "*C", "*D" };
            return strs[(int)s];
        }
    }
}
