using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    internal class MineField
    {
        //*************************************************************
        //Fields
        //*************************************************************
        private Mine[,] mMineField;
        private int mRows;
        private int mColumns;
        private int mCellSize;
        int mSelectNum = 0;
        

        //*************************************************************
        //Properties
        //*************************************************************
        public int Rows
        {
            get { return mRows; }
            set { mRows = value; }
        }
        public int Columns
        {
            get { return mColumns; }
            set { mColumns = value; }
        }
        public int CellSize
        {
            get { return mCellSize; }
        }

        //*************************************************************
        //Constructors
        //*************************************************************
        public MineField()
        {
            mRows = 10;
            mColumns = 10;
            mCellSize = CellSize;
        }
        public MineField(int Rows, int Columns, int CellSize)
        {
            mRows = Rows;
            mColumns = Columns;
            mCellSize = CellSize;

            mMineField = new Mine[mRows, mColumns];

            for (int i = 0; i < mRows; i++)
            {
                for (int j = 0; j < mColumns; j++)
                {
                    mMineField[i, j] = new Mine(CellSize, MineTypes.Square, false, false, false, false);//, Resource1.square);
                }
            }


        }


        //*************************************************************
        //Methods
        //*************************************************************
        public void Draw(Graphics g, int X, int Y)
        {
            //this draws the grid of mines on the surface g at the location x and y 

            int XPos = X;
            int YPos = Y;

            for (int i = 0; i < mRows; i++)
            {
                for (int j = 0; j < mColumns; j++)
                {
                    mMineField[i, j].Draw(g, XPos, YPos);
                    XPos += CellSize;
                }
                XPos = X;
                YPos += CellSize;
            }
        }

        public Mine GetMine(int Row, int Column)
        {
            return mMineField[Row, Column];
        }

        public void AssignBombs1(MenuStates MenuState)
        {
            int Bombs = 0;

            //bombs for level 1
            if (MenuState == MenuStates.None1)
            {
                Bombs = 10;
            }
            //bombs for level 2
            else if (MenuState == MenuStates.None2)
            {
                Bombs = 40;
            }

            //Place bombs in minefield
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < mMineField.GetLength(1); j++)
                {
                    if (Bombs != 0)
                    {
                        mMineField[i, j].MineType = MineTypes.Mine;
                        Bombs--;
                    }
                }
            }

            //shuffle placement of bombs
            Shuffle(MenuState);
        }
        public void Shuffle(MenuStates MenuState)
        {
            Mine Temp;
            Random oRandom = new Random();
            int RandRow = oRandom.Next(0, 9);
            int RandCol = oRandom.Next(0, 9);


            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < mMineField.GetLength(1); j++)
                {
                    if (MenuState == MenuStates.None1)
                    {
                        RandRow = oRandom.Next(0, 9);
                        RandCol = oRandom.Next(0, 9);
                    }
                    else if (MenuState == MenuStates.None2)
                    {
                        RandRow = oRandom.Next(0, 19);
                        RandCol = oRandom.Next(0, 19);
                    }

                    Temp = mMineField[i, j];
                    mMineField[i, j] = mMineField[RandRow, RandCol];
                    mMineField[RandRow, RandCol] = Temp;
                }
            }
        }

        public void AssignFields()
        {
            //Counts how many bombs are around each field and changes the minetype based on that
            
            for (int i = 0; i < mRows; i++)
            {
                for (int j = 0; j < mColumns; j++)
                {
                    if (mMineField[i, j].MineType != MineTypes.Mine)
                    {
                        //Counts bombs around each field
                        int Bombs = CountBombs(i, j);

                        if (Bombs == 0)
                        {
                            mMineField[i, j].MineType = MineTypes.None;
                        }
                        if (Bombs == 1)
                        {
                            mMineField[i, j].MineType = MineTypes.Field1;
                        }
                        if (Bombs == 2)
                        {
                            mMineField[i, j].MineType = MineTypes.Field2;
                        }
                        if (Bombs == 3)
                        {
                            mMineField[i, j].MineType = MineTypes.Field3;
                        }
                        if (Bombs == 4)
                        {
                            mMineField[i, j].MineType = MineTypes.Field4;
                        }
                        if (Bombs == 5)
                        {
                            mMineField[i, j].MineType = MineTypes.Field5;
                        }
                        if (Bombs == 6)
                        {
                            mMineField[i, j].MineType = MineTypes.Field6;
                        }
                        if (Bombs == 7)
                        {
                            mMineField[i, j].MineType = MineTypes.Field7;
                        }
                        if (Bombs == 8)
                        {
                            mMineField[i, j].MineType = MineTypes.Field8;
                        }
                    }
                }
            }
        }
        public int CountBombs(int R, int C)
        {
            int Bombs = 0;

            if ((R - 1) >= 0)
            {
                if (mMineField[R - 1, C].MineType == MineTypes.Mine)
                {
                    Bombs++;
                }
            }
            if ((R + 1) < mRows)
            {
                if (mMineField[R + 1, C].MineType == MineTypes.Mine)
                {
                    Bombs++;
                }
            }
            if ((C - 1) >= 0)
            {
                if (mMineField[R, C - 1].MineType == MineTypes.Mine)
                {
                    Bombs++;
                }
            }
            if ((C + 1) < mColumns)
            {
                if (mMineField[R, C + 1].MineType == MineTypes.Mine)
                {
                    Bombs++;
                }
            }
            if ((R + 1) < mRows && (C + 1) < mColumns)
            {
                if (mMineField[R + 1, C + 1].MineType == MineTypes.Mine)
                {
                    Bombs++;
                }
            }
            if ((R - 1) >= 0 && (C - 1) >= 0)
            {
                if (mMineField[R - 1, C - 1].MineType == MineTypes.Mine)
                {
                    Bombs++;
                }
            }
            if ((R - 1) >= 0 && (C + 1) < mColumns)
            {
                if (mMineField[R - 1, C + 1].MineType == MineTypes.Mine)
                {
                    Bombs++;
                }
            }
            if ((R + 1) < mRows && (C - 1) >= 0)
            {
                if (mMineField[R + 1, C - 1].MineType == MineTypes.Mine)
                {
                    Bombs++;
                }
            }
            return Bombs;
        }

        public void RevealEmptySpace(int r, int c)
        {
            //recursively reveals empty fields (make them selected)
            if (r < 0 || r >= mRows || c < 0 || c >= mColumns)
            {
                return;
            }
            if (mMineField[r, c].MineType == MineTypes.Mine)
            {
                return;
            }
            if (mMineField[r, c].MineType != MineTypes.None)
            {
                mMineField[r, c].Selected = true;
                return;
            }
            if (mMineField[r, c].Selected == true)
            {
                return;
            }
            mMineField[r, c].Selected = true;

            RevealEmptySpace(r + 1, c);
            RevealEmptySpace(r - 1, c);
            RevealEmptySpace(r, c + 1);
            RevealEmptySpace(r, c - 1);
        }


        public bool CheckIfWon(MenuStates MenuState)
        {
            //Count how many fields have been revealed/selected
            mSelectNum = 0;

            for (int i = 0; i < mRows; i++)
            {
                for (int j = 0; j < mColumns; j++)
                {
                    if (mMineField[i, j].Selected == true)
                    {
                        mSelectNum++;
                    }
                }
            }
            //in level 1, if it is equal to 90, then return true (game over & won)
            if (mSelectNum == 90 && MenuState == MenuStates.None1)
            {
                return true;
            }
            //in level 2, if it is equal to 360, then return true (game over & won)
            else if (mSelectNum == 360 && MenuState == MenuStates.None2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void Show(bool Won)
        {
            int XPos = 0;
            int YPos = 0;

            for (int i = 0; i < mRows; i++)
            {
                for (int j = 0; j < mColumns; j++)
                {
                    //if game lost, show all mines exploded
                    if (mMineField[i, j].MineType == MineTypes.Mine && mMineField[i, j].Flagged == false && Won == false)
                    {
                        mMineField[i, j].Exploded = true;
                    }
                    //if game lost, show if flagged fields are wrong
                    if (mMineField[i, j].Flagged == true && mMineField[i, j].MineType != MineTypes.Mine && Won == false)
                    {
                        mMineField[i, j].Wrong = true;
                    }
                    //if game won, show all mines intact
                    if (Won == true && mMineField[i, j].MineType == MineTypes.Mine)
                    {
                        mMineField[i, j].Reveal = true;
                    }

                    mMineField[i, j].CheckMineType();
                    XPos += CellSize;
                }
                XPos = 0;
                YPos += CellSize;
            }

        }


    }
}
