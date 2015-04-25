﻿using System;
using Newtonsoft.Json;

namespace Com.Ericmas001.Games
{
    public class GameCard
    {
        public static GameCard NoCard { get { return new GameCard(GameCardSpecial.Null); } }
        public static GameCard Hidden { get { return new GameCard(GameCardSpecial.Hidden); } }

        private GameCardSpecial m_Special;
        private GameCardKind m_Kind;
        private GameCardValue m_Value;

        public GameCardSpecial Special
        {
            get { return m_Special; }
            set { m_Special = value; }
        }
        public GameCardKind Kind
        {
            get { return m_Kind; }
            set { m_Kind = value; }
        }

        public GameCardValue Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        public GameCard()
        {
        }

        public GameCard(GameCardKind kind, GameCardValue val)
        {
            m_Kind = kind;
            m_Value = val;
            m_Special = GameCardSpecial.None;
        }

        public GameCard(GameCardSpecial special)
        {
            m_Special = special;
        }
        public GameCard(int id)
        {
            if (id < 0)
                m_Special = (GameCardSpecial)(0 - id);
            else
            {
                m_Kind = (GameCardKind)(id / 13);
                m_Value = (GameCardValue)(id % 13);
            }
        }

        [JsonIgnore]
        public int Id
        {
            get
            {
                if (m_Special != GameCardSpecial.None)
                    return 0 - (int)m_Special;
                return ((int)m_Kind * 13) + (int)m_Value;
            }
        }
        public override string ToString()
        {
            if (m_Special != GameCardSpecial.None)
                return GameCardUtility.SpecialStr(m_Special);
            return String.Format("{0}{1}", GameCardUtility.ValueChar(m_Value), GameCardUtility.KindChar(m_Kind));
        }
    }
}
