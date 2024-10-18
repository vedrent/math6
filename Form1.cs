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
        private Button[,] playerButtons = new Button[gridSize, gridSize];
        private Button[,] compButtons = new Button[gridSize, gridSize];
        private int[,] playerGrid = new int[gridSize, gridSize];
        private int[,] compGrid = new int[gridSize, gridSize];
        private Random random = new Random();
        private bool placingShips = true;
        private int shipsToPlace = 20;
        private TextBox textBox;

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

        private void InitializeGrid(int[,] grid, Button[,] buttons, int startX)
        {
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    Button button = new Button();
                    button.Size = new Size(cellSize, cellSize);
                    button.Location = new Point(startX + col * cellSize, row * cellSize);
                    button.BackColor = Color.LightBlue;
                    button.Click += Button_Click;

                    buttons[row, col] = button;

                    this.Controls.Add(button);
                }
            }
        }

        private void InitializeTextBox()
        {
            textBox = new TextBox();
            textBox.Location = new Point(310, 350);
            textBox.Size = new Size(300, 100);
            textBox.ReadOnly = true;
            textBox.Text = "������� ���";

            this.Controls.Add(textBox);
        }

        private void InitializeCompButton()
        {
            Button startButton = new Button();
            startButton.Size = new Size(120, 30);
            startButton.Location = new Point(310, 320);
            startButton.Text = "������ ����";
            startButton.Click += StartGame;

            this.Controls.Add(startButton);
        }

        private void StartGame(object sender, EventArgs e)
        {
            if (shipsToPlace > 0)
            {
                this.textBox.Text = "�� �� ���������� ��� �������!";
                return;
            }
            placingShips = false;
            ResetGrid(compGrid, compButtons);
            PlaceShips(compGrid);
            this.textBox.Text = "������ ����";
        }

        private void ResetGrid(int[,] grid, Button[,] buttons)
        {
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    grid[row, col] = 0;
                    buttons[row, col].BackColor = Color.LightBlue;
                    buttons[row, col].Enabled = true;
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (placingShips)
            {
                for (int row = 0; row < gridSize; row++)
                {
                    for (int col = 0; col < gridSize; col++)
                    {
                        if (playerButtons[row, col] == clickedButton && playerGrid[row, col] == 0)
                        {
                            if (shipsToPlace > 0)
                            {
                                playerGrid[row, col] = 1; // ���������� �������
                                clickedButton.BackColor = Color.Green;
                                shipsToPlace--;

                                if (shipsToPlace == 0)
                                {
                                    this.textBox.Text = "��� ������� ���������! ����� ������ ����";
                                    placingShips = false;  // ��������� ����� �����������
                                }
                            }
                            return;
                        }
                    }
                }
            }

            else
            {
                for (int row = 0; row < gridSize; row++)
                {
                    for (int col = 0; col < gridSize; col++)
                    {
                        if (compButtons[row, col] == clickedButton)
                        {
                            if (compGrid[row, col] == 1)
                            {
                                compGrid[row, col] = 3;            // ���������
                                clickedButton.BackColor = Color.Red;
                                clickedButton.Enabled = false;
                                this.textBox.Text = "���������!";
                            }
                            else if (compGrid[row, col] == 0)
                            {
                                compGrid[row, col] = 2;            // ����
                                clickedButton.BackColor = Color.Gray;
                                clickedButton.Enabled = false;
                                this.textBox.Text = "����!";
                                ComputerTurn();
                            }

                            return;
                        }
                    }
                }
            }
        }

        private void ComputerTurn()
        {
            System.Threading.Thread.Sleep(1000);
            int row, col;
            do
            {
                row = random.Next(gridSize);
                col = random.Next(gridSize);
            } while (playerGrid[row, col] == 2 || playerGrid[row, col] == 3); // ���� �� ������ ��������� ������

            if (playerGrid[row, col] == 1)
            {
                playerGrid[row, col] = 3; // ���������
                playerButtons[row, col].BackColor = Color.Red;
                this.textBox.Text = "��������� �����!";
                ComputerTurn();
            }
            else
            {
                playerGrid[row, col] = 2; // ����
                playerButtons[row, col].BackColor = Color.Gray;
                this.textBox.Text = "��������� �����������!";
            }
        }

        private void PlaceShips(int[,] grid)
        {
            PlaceShip(grid, 4); // ���� 4-� ���������� �������
            for (int i = 0; i < 2; i++) PlaceShip(grid, 3); // ��� 3-� ����������
            for (int i = 0; i < 3; i++) PlaceShip(grid, 2); // ��� 2-� ����������
            for (int i = 0; i < 4; i++) PlaceShip(grid, 1); // ������ ���������
        }

        // ����� ��� ���������� ������ ������� �� ����
        private void PlaceShip(int[,] grid, int shipSize)
        {
            bool placed = false;

            while (!placed)
            {
                int row = random.Next(gridSize);  // ��������� ��������� �������
                int col = random.Next(gridSize);
                bool horizontal = random.Next(2) == 0; // ��������� ����� ����������� (�������������� ��� ������������)

                if (CanPlaceShip(grid, row, col, shipSize, horizontal))
                {
                    // ��������� ������� �� ����
                    for (int i = 0; i < shipSize; i++)
                    {
                        if (horizontal)
                            grid[row, col + i] = 1; // �������������� �������
                        else
                            grid[row + i, col] = 1; // ������������ �������
                    }
                    placed = true;
                }
            }
        }

        // ��������, ����� �� ���������� ������� �� ������ �������
        private bool CanPlaceShip(int[,] grid, int row, int col, int shipSize, bool horizontal)
        {
            if (horizontal)
            {
                if (col + shipSize > gridSize) return false; // ����� �� ������� ����
                for (int i = 0; i < shipSize; i++)
                {
                    if (!IsCellFree(grid, row, col + i)) return false; // �������� ������ ������ � � �������
                }
            }
            else
            {
                if (row + shipSize > gridSize) return false; // ����� �� ������� ����
                for (int i = 0; i < shipSize; i++)
                {
                    if (!IsCellFree(grid, row + i, col)) return false; // �������� ������ ������ � � �������
                }
            }
            return true;
        }

        // ��������, �������� �� ������ � � ������ (��� ����, ����� ������� �� ��������)
        private bool IsCellFree(int[,] grid, int row, int col)
        {
            for (int i = Math.Max(0, row - 1); i <= Math.Min(gridSize - 1, row + 1); i++)
            {
                for (int j = Math.Max(0, col - 1); j <= Math.Min(gridSize - 1, col + 1); j++)
                {
                    if (grid[i, j] != 0) return false; // ���� ������ ��� � ����� ��� ������
                }
            }
            return true;
        }





        public Form1()
        {
            InitializeComponent();
            InitializeGrid(playerGrid, playerButtons, 0);
            InitializeGrid(compGrid, compButtons, 400);
            PlaceShips(compGrid);
            InitializeCompButton();
            InitializeTextBox();
        }
    }
}


