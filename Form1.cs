using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;


namespace math6
{
    public partial class Form1 : Form
    {

        private const int gridSize = 10;
        private const int cellSize = 30;
        private Button[,] buttons = new Button[gridSize, gridSize];
        private Button[,] compButtons = new Button[gridSize, gridSize];
        private int[,] grid = new int[gridSize, gridSize];
        private int[,] compGrid = new int[gridSize, gridSize];
        int[,] defaultGrid = new int[gridSize, gridSize]
            {
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0, 1, 0, 0, 0, 0 }
            };

        private void InitializeGrid(int[,] initialState, int startX)
        {
            grid = initialState;
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    Button button = new Button();
                    button.Size = new Size(cellSize, cellSize);
                    button.Location = new Point(startX + col * cellSize, row * cellSize);
                    button.BackColor = grid[row, col] == 1 ? Color.Black : Color.White;
                    button.Click += Button_Click;

                    buttons[row, col] = button;

                    this.Controls.Add(button);

                    grid[row, col] = initialState[row, col];
                }
            }
        }

        private void InitializeCompGrid(int[,] initialState, int startX)
        {
            compGrid = initialState;
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    Button button = new Button();
                    button.Size = new Size(cellSize, cellSize);
                    button.Location = new Point(startX + col * cellSize, row * cellSize);
                    button.BackColor = compGrid[row, col] == 1 ? Color.Black : Color.White;
                    button.Click += Button_Click;

                    compButtons[row, col] = button;

                    this.Controls.Add(button);

                    compGrid[row, col] = initialState[row, col];
                }
            }
        }

        private void UpdateCompGrid()
        {
            compGrid = grid;
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    compButtons[row, col].BackColor = compGrid[row, col] == 1 ? Color.Black : Color.White;
                }
            }
        }

        private void InitializeCompButton()
        {
            Button button = new Button();
            button.Size = new Size(60, 30);
            button.Location = new Point(310, 320);
            button.Click += Comp_Button_Click;

            this.Controls.Add(button);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    if (buttons[row, col] == clickedButton)
                    {
                        grid[row, col] = grid[row, col] == 0 ? 1 : 0;

                        clickedButton.BackColor = grid[row, col] == 1 ? Color.Black : Color.White;
                        return;
                    }
                }
            }
        }

        private void Comp_Button_Click(object sender, EventArgs e)
        {
            UpdateCompGrid();
        }

        public Form1()
        {
            InitializeComponent();
            InitializeGrid(defaultGrid, 0);
            InitializeCompGrid(defaultGrid, 400);
            InitializeCompButton();
        }
    }
}


