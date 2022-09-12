using System;

namespace fc_miners_game_core
{
    class Program
    {
        public static int Bombs = 6;
        public static int FlagsUsed = 0;
        public static string[,] Board = {
            {" "," "," "," "," "," "},
            {" "," "," "," "," "," "},
            {" "," "," "," "," "," "},
            {" "," "," "," "," "," "},
            {" "," "," "," "," "," "},
            {" "," "," "," "," "," "},
        };
        public static string[,] BoardUser ={
            {" "," "," "," "," "," "},
            {" "," "," "," "," "," "},
            {" "," "," "," "," "," "},
            {" "," "," "," "," "," "},
            {" "," "," "," "," "," "},
            {" "," "," "," "," "," "},
        };

        public static void DrawBoard(string[,] board, int x, int y)
        {
            Console.SetCursorPosition(y, x);
            for (int i = 0; i < 6; i++)
            {
                System.Console.WriteLine("+-----------+");
                for (int j = 0; j < 6; j++)
                {

                    System.Console.Write($"|{board[i, j]}");
                }
                System.Console.WriteLine("|");
            }
            System.Console.WriteLine("+-----------+");

        }

        public static void SetBombs()
        {
            for (int i = 0; i < Bombs; i++)
            {
                Random r = new Random();
                int x, y;
                //Escolher dois numeros aleatorios (0,5), e verificar se existe bomba no lugar
                // Se não existir, colocar bomba
                // Se Existir, Escolher outra posição
                do
                {
                    x = r.Next(0, 5);
                    y = r.Next(0, 5);


                } while (Board[x, y] != " ");

                Board[x, y] = "*";

            }
        }

        public static void SetNumbers()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (Board[i, j] == "*")
                    {
                        continue;
                    }
                    int counter = 0;
                    if (Board[i, j] == " ")
                    {
                        // Verificação das bombas superiores
                        if (i - 1 >= 0)
                        {
                            if (Board[i - 1, j] == "*") counter++;
                            if (j - 1 >= 0)
                            {
                                if (Board[i - 1, j - 1] == "*") counter++;
                            }
                            if (j + 1 < 6)
                            {
                                if (Board[i - 1, j + 1] == "*") counter++;
                            }
                        }
                        //Lateral esquerda
                        if (j - 1 >= 0)
                        {
                            if (Board[i, j - 1] == "*") counter++;
                        }
                        //Lateral Direita
                        if (j + 1 < 6)
                        {
                            if (Board[i, j + 1] == "*") counter++;
                        }

                        if (i + 1 < 6)
                        {
                            if (Board[i + 1, j] == "*") counter++;
                            if (j - 1 >= 0)
                            {
                                if (Board[i + 1, j - 1] == "*") counter++;
                            }
                            if (j + 1 < 6)
                            {
                                if (Board[i + 1, j + 1] == "*") counter++;
                            }
                        }
                        Board[i, j] = counter.ToString();
                    }
                }
            }
        }

        public static bool checkVictory()
        {
            if (FlagsUsed == Bombs)
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (BoardUser[i, j] == "!")
                        {
                            if (Board[i, j] != "*") return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }

        static void Main(string[] args)
        {
            SetBombs();
            SetNumbers();
            DrawBoard(BoardUser, 0, 0);
            // DrawBoard(Board,16,0); //Debug only
            System.Console.WriteLine();

            Console.SetCursorPosition(1, 1);
            int cursorX = 1, cursorY = 1;

            ConsoleKeyInfo keypress;
            do
            {
                keypress = Console.ReadKey(true);
                int posX = cursorX / 2, posY = cursorY / 2;
                switch (keypress.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (cursorY > 1)
                        {
                            cursorY -= 2;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (cursorY < 11)
                        {
                            cursorY += 2;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (cursorX < 11)
                        {
                            cursorX += 2;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (cursorX > 1)
                        {
                            cursorX -= 2;
                        }
                        break;
                    case ConsoleKey.Spacebar:

                        if (BoardUser[posY, posX] == "!")
                        {
                            BoardUser[posY, posX] = " ";
                            FlagsUsed--;
                        }
                        else
                        {
                            if (FlagsUsed < Bombs)
                            {
                                if (BoardUser[posY, posX] == " ")
                                {
                                    BoardUser[posY, posX] = "!";
                                    FlagsUsed++;
                                }
                            }
                        }
                        DrawBoard(BoardUser, 0, 0);
                        break;

                    case ConsoleKey.Enter:
                        if (BoardUser[posY, posX] == " ")
                        {
                            BoardUser[posY, posX] = Board[posY, posX];
                            if (BoardUser[posY, posX] == "*")
                            {
                                Console.Clear();
                                System.Console.WriteLine("Que pena, você perdeu!");
                                Console.ReadLine();
                                System.Environment.Exit(0);
                            }
                            else
                            {
                                DrawBoard(BoardUser, 0, 0);
                            }
                        }
                        break;
                }
                Console.SetCursorPosition(cursorX, cursorY);
            } while (!checkVictory());

            Console.Clear();
            DrawBoard(Board, 0, 0);
            System.Console.WriteLine("\nParabéns, você venceu!");
            Console.ReadLine();
            System.Environment.Exit(0);
        }
    }
}