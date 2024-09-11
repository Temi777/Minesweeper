using System;
using System.Drawing;

namespace Minesweeper
{
    //*************************************************************
    //Enums
    //*************************************************************
    public enum MenuStates { Start, Instructions, EndWon, EndLost, None1, None2 };

    internal class Menu
    {
        //*************************************************************
        //Fields
        //*************************************************************
        private MenuStates mMenuState;

        //*************************************************************
        //Properties
        //*************************************************************
        public MenuStates MenuState
        {
            get { return mMenuState; }
            set { mMenuState = value; }
        }


        //*************************************************************
        //Constructors
        //*************************************************************
        public Menu()
        {
            mMenuState = MenuStates.Start;
        }
        public Menu(MenuStates MenuStates)
        {
            mMenuState = MenuState;
        }


        //*************************************************************
        //Methods
        //*************************************************************
        public void Draw(Graphics g, int X, int Y)
        {
            Font oFont = new Font("Ink Free", 24);
            Brush oBrush = new SolidBrush(Color.Black);
            string TextMessage = string.Empty;
            SizeF TextSize;


            if (mMenuState == MenuStates.Start)
            {
                //Start Menu
                oFont = new Font("Ink Free", 33);
                TextMessage = "Mine Sweeper\n\nPress 1 To Start Lv1\nPress 2 To Start Lv2\n\nPress I For Instructions\nPress Q To Quit";
            }
            else if (mMenuState == MenuStates.Instructions)
            {
                //Instructions Menu
                oFont = new Font("Ink Free", 14);
                TextMessage = "Game Instructions\n\nMines are randomly hidden under the mine fields.\nReveal a field by left clicking on the field if you think there is no bomb on it."+
                                "\nIf the clicked field is not a bomb and it has no bombs around it, it will be blank.\nThe number on a revealed field tells how many bombs are around that field." +
                                "\nIf you click on a bomb, a series of explosions will occur and you will lose." + "\nRight click on a field if you think a bomb is there to flag it." +
                                "\nWhen all fields are uncovered except for the mines, the player wins.\n\nPress M to go back to Main Menu";
            }
            else if (mMenuState == MenuStates.EndWon)
            {
                //Won Game Menu
                TextMessage = "YOU WON!!!\n\nPress 1 To Play Level 1\nPress 2 To Play Level 2\n\nPress M To Open Main Menu\nPress Q To Quit";
            }
            else if (mMenuState == MenuStates.EndLost)
            {
                //Lost Game Menu
                TextMessage = "You Lost...\n\nPress 1 To Play Level 1\nPress 2 To Play Level 2\n\nPress M To Open Main Menu\nPress Q To Quit";
            }
            else if (mMenuState == MenuStates.None1)
            {
                TextMessage = string.Empty;
            }
            else if (mMenuState == MenuStates.None2)
            {
                TextMessage = string.Empty;
            }
            TextSize = g.MeasureString(TextMessage, oFont);
            g.DrawString(TextMessage, oFont, oBrush, X, Y);
            oFont.Dispose();
            oBrush.Dispose();
        }

    }
}
