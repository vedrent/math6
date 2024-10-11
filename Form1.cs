using System;
using System.Drawing;
using System.Windows.Forms;


namespace math6
{
    public partial class Form1 : Form
    {

        private const int gridSize = 10;
        private const int cellSize = 30;
        private Button[,] buttons = new Button[gridSize, gridSize];
        private int[,] grid = new int[gridSize, gridSize];

        private void InitializeGrid(int[,] initialState)
        {
            grid = initialState;
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    Button button = new Button();
                    button.Size = new Size(cellSize, cellSize);
                    button.Location = new Point(col * cellSize, row * cellSize);
                    button.BackColor = grid[row, col] == 1 ? Color.Black : Color.White;
                    button.Click += Button_Click;

                    buttons[row, col] = button;

                    this.Controls.Add(button);

                    grid[row, col] = 0;
                }
            }
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

        public Form1()
        {
            InitializeComponent();
            InitializeGrid(new int[gridSize, gridSize]
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
            });
        }
    }
}


