using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;

namespace Minesweeper
{
    //*************************************************************
    //Enum for the type of Mines
    //*************************************************************
    public enum MineTypes
    {
        Square, None, Field1, Field2, Field3, Field4, Field5, Field6,
        Field7, Field8, Flag, Mine, Question, Wrong, Explode
    };

    internal class Mine
    {
        //*************************************************************
        //Fields
        //*************************************************************
        private int mSize;
        private MineTypes mMineType;
        private Image mField;
        private bool mSelected;
        private bool mFlagged;
        private bool mExploded;
        private bool mWrong;
        private bool mReveal;

        //*************************************************************
        //Properties
        //*************************************************************
        public int Size
        {
            get { return mSize; }
        }
        public MineTypes MineType
        {
            get { return mMineType; }
            set { mMineType = value; }
        }
        public Image Field
        {
            get { return mField; }
            set { mField = value; }
        }
        public bool Selected
        {
            get { return mSelected; }
            set { mSelected = value; }
        }
        public bool Flagged
        {
            get { return mFlagged; }
            set { mFlagged = value; }
        }
        public bool Exploded
        {
            get { return mExploded; }
            set { mExploded = value; }
        }
        public bool Wrong
        {
            get { return mWrong; }
            set { mWrong = value; }
        }
        public bool Reveal
        {
            get { return mReveal; }
            set { mReveal = value; }
        }
        

        //*************************************************************
        //Constructors
        //*************************************************************
        public Mine()
        {
            mSize = Size;
            mMineType = MineTypes.Square;
            mSelected = false;
            mFlagged = false;
            mExploded = false;
            mReveal = false;
            mWrong = false;
        }
        public Mine(int CellSize, MineTypes MineType, bool Flagged, bool Reveal, bool Exploded, bool Wrong)
        {
            mSize = CellSize;
            mMineType = MineType;
            mField = Resource1.square;
            mFlagged = Flagged;
            mReveal = Reveal;
            mExploded = Exploded;
            mWrong = Wrong;
            

            if (mSelected == true)
            {
                CheckMineType();
            }
            else if (mFlagged == true)
            {
                mField = Resource1.flag;
            }
            else if (mExploded == true)
            {
                mField = Resource1.explode;
            }
            else
            {
                mField = Resource1.square;
            }
            if (mWrong == true)
            {
                mField = Resource1.wrong;
            }
            if (mReveal == true)
            {
                mField = Resource1.mine;
            }
        }


        //*************************************************************
        //Methods
        //*************************************************************

        public void Draw(Graphics g, int X, int Y)
        {
            //this draws the mine on the surface g at
            //the location x and y
            Pen BorderPen = new Pen(Color.Black);

            //if mine is selected
            if (Selected == true)
            {
                CheckMineType();
            }
            //if mine is flagged
            else if (mFlagged == true)
            {
                mField = Resource1.flag;
            }
            //if mine exploded (happens when player loses)
            else if (mExploded == true)
            {
                mField = Resource1.explode;
            }
            //if mine is not selected
            else if (Selected == false)
            {
                mField = Resource1.square;
            }
            //if flagged mine is wrong (happens after player loses)
            if (mWrong == true)
            {
                mField = Resource1.wrong;
            }
            //show intact bomb if player won
            if (mReveal == true)
            {
                mField = Resource1.mine;
            }
            g.DrawRectangle(BorderPen, X, Y, mSize, mSize);
            g.DrawImage(mField, X, Y, mSize, mSize);

            BorderPen.Dispose();

        }
        public void CheckMineType()
        {
            //Check minetype and assign image to mField based on minetype

            if (mMineType == MineTypes.None)
            {
                mField = Resource1.field0;
            }
            else if (mMineType == MineTypes.Field1)
            {
                mField = Resource1.field1;
            }
            else if (mMineType == MineTypes.Field2)
            {
                mField = Resource1.field2;
            }
            else if (mMineType == MineTypes.Field3)
            {
                mField = Resource1.field3;
            }
            else if (mMineType == MineTypes.Field4)
            {
                mField = Resource1.field4;
            }
            else if (mMineType == MineTypes.Field5)
            {
                mField = Resource1.field5;
            }
            else if (mMineType == MineTypes.Field6)
            {
                mField = Resource1.field6;
            }
            else if (mMineType == MineTypes.Field7)
            {
                mField = Resource1.field7;
            }
            else if (mMineType == MineTypes.Field8)
            {
                mField = Resource1.field8;
            }
            else if (mMineType == MineTypes.Mine)
            {
                mField = Resource1.mine;
            }
            else if (mMineType == MineTypes.Flag)
            {
                mField = Resource1.flag;
            }
            else if (mMineType == MineTypes.Explode)
            {
                mField = Resource1.explode;
            }
        }


    }
}
