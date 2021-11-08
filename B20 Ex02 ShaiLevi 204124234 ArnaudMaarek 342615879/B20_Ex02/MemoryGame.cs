using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace B20_Ex02
{
    internal class MemoryGame
    {
        private Board m_BoardGame;
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private ePlayerTurn m_PlayerTurn;
        private eModeGame m_ModeGame;

        public ePlayerTurn PlayerTurn
        {
            get { return m_PlayerTurn; }
            set { m_PlayerTurn = value; }
        }

        public eModeGame ModeGame
        {
            get { return m_ModeGame; }
            set { m_ModeGame = value; }
        }

        public Board BoardGame
        {
            get { return m_BoardGame; }
            set { m_BoardGame = value; }
        }

        public Player PlayerOne
        {
            get { return m_PlayerOne; }
            set { m_PlayerOne = value; }
        }

        public Player PlayerTwo
        {
            get { return m_PlayerTwo; }
            set { m_PlayerTwo = value; }
        }

        public enum eModeGame
        {
            againstPlayer = 1, againstComputer
        }

        public enum ePlayerTurn
        {
            playerOne, playerTwo
        }

        public MemoryGame()
        {
            m_BoardGame = new Board();
            m_PlayerOne = new Player();
            m_PlayerTwo = new Player();
            m_PlayerTurn = ePlayerTurn.playerOne;
        }

        internal class Player
        {
            private string m_PlayerName;
            private int m_NumberOfPoints;

            public Player()
            {
                m_PlayerName = " ";
                m_NumberOfPoints = 0;
            }

            public string PlayerName
            {
                get { return m_PlayerName; }
                set { m_PlayerName = value; }
            }

            public int NumberOfPoints
            {
                get { return m_NumberOfPoints; }
                set { m_NumberOfPoints = value; }
            }
        }

        internal class Board
        {
            private int m_Width;
            private int m_Height;
            private Cell[,] m_BoardCell;

            public Board()
            {
                m_Width = 0;
                m_Height = 0;
            }

            public int Width
            {
                get { return m_Width; }
                set { m_Width = value; }
            }

            public int Height
            {
                get { return m_Height; }
                set { m_Height = value; }
            }

            public Cell[,] BoardCell
            {
                get { return m_BoardCell; }
                set { m_BoardCell = value; }
            }
        }

        internal class Cell
        {
            private int m_Index;
            private bool m_ToShow;

            public Cell()
            {
                m_Index = -1;
                m_ToShow = false;
            }

            public int Index
            {
                get { return m_Index; }
                set { m_Index = value; }
            }

            public bool ToShow
            {
                get { return m_ToShow; }
                set { m_ToShow = value; }
            }
        }

        public bool IsTheGameOver()
        {
            const bool v_Flag = true;
            bool isOver = !v_Flag;
            int pointsOfAllPlayer = m_PlayerOne.NumberOfPoints + m_PlayerTwo.NumberOfPoints;

            if (pointsOfAllPlayer == (m_BoardGame.Height * m_BoardGame.Width) / 2)
            {
                isOver = v_Flag;
            }

            return isOver;
        }

        public void InitializeBoard()
        {
            m_BoardGame.BoardCell = new MemoryGame.Cell[m_BoardGame.Height, m_BoardGame.Width];
            for (int i = 0; i < m_BoardGame.Height; i++)
            {
                for (int j = 0; j < m_BoardGame.Width; j++)
                {
                    MemoryGame.Cell cellToAdd = new MemoryGame.Cell();
                    m_BoardGame.BoardCell[i, j] = cellToAdd;
                }
            }
        }

        public void MakeRandomGame()
        {
            int size = (m_BoardGame.Height * m_BoardGame.Width) / 2;
            var makeRandom = new Random();
            int counterForObject, heightRand, widthRand;

            for (int i = 0; i < size; i++)
            {
                counterForObject = 0;
                while (counterForObject != 2)
                {
                    widthRand = makeRandom.Next(m_BoardGame.Width);
                    heightRand = makeRandom.Next(m_BoardGame.Height);

                    if (m_BoardGame.BoardCell[heightRand, widthRand].Index == -1)
                    {
                        m_BoardGame.BoardCell[heightRand, widthRand].Index = i;
                        counterForObject++;
                    }
                }
            }
        }

        public void MakeRandomMove(ref int o_HeightRand, ref int o_WidthRand)
        {
            var random1 = new Random();
            do
            {
                o_HeightRand = random1.Next(m_BoardGame.Height);
                o_WidthRand = random1.Next(m_BoardGame.Width);
            }
            while (m_BoardGame.BoardCell[o_HeightRand, o_WidthRand].ToShow);
        }

        public bool IsTheSameObject(Cell i_FisrtObject, Cell i_SecondObject)
        {
            const bool v_Flag = true;
            bool v_EqualFlag = !v_Flag;
            if (i_FisrtObject.Index == i_SecondObject.Index)
            {
                v_EqualFlag = v_Flag;
                if (m_PlayerTurn == ePlayerTurn.playerOne)
                {
                    m_PlayerOne.NumberOfPoints++;
                }
                else
                {
                    m_PlayerTwo.NumberOfPoints++;
                }
            }

            return v_EqualFlag;
        }

        public void ChangePlayer()
        {
            if (m_PlayerTurn == ePlayerTurn.playerOne)
            {
                m_PlayerTurn = ePlayerTurn.playerTwo;
            }
            else
            {
                m_PlayerTurn = ePlayerTurn.playerOne;
            }
        }
    }
}
