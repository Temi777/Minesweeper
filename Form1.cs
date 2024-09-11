using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        public int CellSize = 25;
        Menu oMenu;
        MineField oMineField;
        Mine oMine;
        Mine ClickedMine;
        int RowIndex = 0;
        int ColumnIndex = 0;
        public bool Started = false;
        bool Won = false;


        public Form1()
        {
            InitializeComponent();

            //initalize menu
            oMenu = new Menu();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //Check If initializing or refreshing
            if (Started == false)
            {
                //Initialize level 1
                if (oMenu.MenuState == MenuStates.None1)
                {
                    oMineField = new MineField(10, 10, CellSize);
                    oMine = new Mine();
                    oMineField.AssignBombs1(MenuStates.None1);
                    oMineField.AssignFields();
                    Started = true;

                }
                //Initialize level 2
                else if (oMenu.MenuState == MenuStates.None2)
                {
                    oMineField = new MineField(20, 20, CellSize);
                    oMine = new Mine();
                    oMineField.AssignBombs1(MenuStates.None2);
                    oMineField.AssignFields();
                    oMineField.Draw(e.Graphics, 0, 0);
                    Started = true;
                }
            }

            //if game state is game over, minefield becomes null and shows menu
            if (oMenu.MenuState == MenuStates.EndLost || oMenu.MenuState == MenuStates.EndWon)
            {
                oMineField = null;
            }

            //refreshing/redrawing
            if (oMineField != null)
            {
                oMineField.Draw(e.Graphics, 0, 0);
            }
            else
            {
                oMenu.Draw(e.Graphics, 50, 50);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Key Events for Menu
            if (oMenu.MenuState == MenuStates.Start)
            {
                //Play level 1
                if (e.KeyCode == Keys.D1)
                {
                    oMenu.MenuState = MenuStates.None1;
                }
                //Play level 2
                else if (e.KeyCode == Keys.D2)
                {
                    oMenu.MenuState = MenuStates.None2;
                }
                //Open Instructions
                else if (e.KeyCode == Keys.I)
                {
                    oMenu.MenuState = MenuStates.Instructions;
                }
                //Quit Game
                else if (e.KeyCode == Keys.Q)
                {
                    Application.Exit();
                }
            }
            else if (oMenu.MenuState == MenuStates.Instructions)
            {
                //Back to menu
                if (e.KeyCode == Keys.M)
                {
                    oMenu.MenuState = MenuStates.Start;
                }
            }
            if (oMenu.MenuState == MenuStates.EndWon)
            {
                Started = false;

                //Play level 1
                if (e.KeyCode == Keys.D1)
                {
                    oMenu.MenuState = MenuStates.None1;
                }
                //Play level 2
                else if (e.KeyCode == Keys.D2)
                {
                    oMenu.MenuState = MenuStates.None2;
                }
                //Open Menu
                else if (e.KeyCode == Keys.M)
                {
                    oMenu.MenuState = MenuStates.Start;
                }
                //Quit
                else if (e.KeyCode == Keys.Q)
                {
                    Application.Exit();
                }
            }
            if (oMenu.MenuState == MenuStates.EndLost)
            {
                Started = false;

                //Play level 1
                if (e.KeyCode == Keys.D1)
                {
                    oMenu.MenuState = MenuStates.None1;
                }
                //Play level 2
                else if (e.KeyCode == Keys.D2)
                {
                    oMenu.MenuState = MenuStates.None2;
                }
                //Menu
                else if (e.KeyCode == Keys.M)
                {
                    oMenu.MenuState = MenuStates.Start;
                }
                //Quit
                else if (e.KeyCode == Keys.Q)
                {
                    Application.Exit();
                }
            }
            this.Refresh();
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (oMineField != null)
            {
                base.OnMouseClick(e);

                int XPos = e.X;
                int YPos = e.Y;


                if (e.X > CellSize * oMineField.Columns || e.Y > CellSize * oMineField.Rows)
                {
                    lblOutput.Text = "You Clicked\nOut Of\nParameters";
                }
                else
                {
                    lblOutput.Text = string.Empty;

                    //Get clicked mine
                    ColumnIndex = XPos / CellSize;
                    RowIndex = YPos / CellSize;

                    ClickedMine = oMineField.GetMine(RowIndex, ColumnIndex);

                    //Check if left mouse is clicked
                    if (e.Button == MouseButtons.Left)
                    {
                        //Check selected mine
                        CheckSelected();

                        //Check if game is won
                        if (oMenu.MenuState == MenuStates.None1)
                        {
                            Won = oMineField.CheckIfWon(MenuStates.None1);
                            if (Won == true)
                            {
                                GameEnd();
                            }
                        }
                        else if (oMenu.MenuState == MenuStates.None2)
                        {
                            Won = oMineField.CheckIfWon(MenuStates.None2);
                            if (Won == true)
                            {
                                GameEnd();
                            }
                        }
                    }
                    //Check if right mouse is clicked
                    if (e.Button == MouseButtons.Right)
                    {
                        //If right mouse is clicked, place or remove flag
                        if (ClickedMine.Selected == false)
                        {
                            ClickedMine.Flagged = !ClickedMine.Flagged;
                            ClickedMine.CheckMineType();
                        }
                    }
                }
                this.Refresh();
            }
        }

        public void CheckSelected()
        {
            //if mine is unselected and unflagged, check mine
            if (ClickedMine.Selected == false && ClickedMine.Flagged == false)
            {
                ClickedMine.CheckMineType();

                //if selected mine is a mine, game lost, end game
                if (ClickedMine.MineType == MineTypes.Mine)
                {
                    ClickedMine.MineType = MineTypes.Explode;
                    Won = false;
                    ClickedMine.CheckMineType();

                    GameEnd();
                }
                //if selected mine is a black space, recursively reveal all other blank spaces beside it
                else if (ClickedMine.MineType == MineTypes.None)
                {
                    oMineField.RevealEmptySpace(RowIndex, ColumnIndex);
                }

                //mine becomes selected
                ClickedMine.Selected = true;
            }
            this.Refresh();
        }


        public void GameEnd()
        {
            oMineField.Show(Won);

            //enable timer
            tmrEnd.Interval = 2000;
            tmrEnd.Enabled = true;
            tmrEnd.Tick += new EventHandler(tmrEnd_Tick);

            this.Refresh();
        }


        private void tmrEnd_Tick(object sender, EventArgs e)
        {
            //when timer goes off, menustate will change to game over
            if (Won == true)
            {
                oMenu.MenuState = MenuStates.EndWon;
            }
            else
            {
                oMenu.MenuState = MenuStates.EndLost;
            }

            //disable timer
            tmrEnd.Enabled = false;
            this.Refresh();

        }
    }
}
