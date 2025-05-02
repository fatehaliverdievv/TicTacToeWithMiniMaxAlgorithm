using System;
using System.Reflection.Metadata;

class Program
{
    static char[,] board = new char[3, 3];
    static char humanPlayer = 'X';
    static char aiPlayer = 'O';
    static void Main(string[] args)
    {
        InitializeBoard();
        while (true)
        {
            PrintBoard();
            PlayerMove(humanPlayer);
            if (CheckForWin(humanPlayer))
            {
                PrintBoard();
                Console.WriteLine("You win!");
                break;
            }
            if (IsBoardFull())
            {
                PrintBoard();
                Console.WriteLine("It's a draw!");
                break;
            }
            AIMove(aiPlayer);
            if (CheckForWin(aiPlayer))
            {
                PrintBoard();
                Console.WriteLine("AI wins!");
                break;
            }
        }
    }
    static void InitializeBoard()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                board[i, j] = ' ';
            }
        }
    }
    static void PrintBoard()
    {
        Console.WriteLine("   1|2|3");
        for (int i = 0; i < 3; i++)
        {
            Console.Write(i + 1 + ". ");
            for (int j = 0; j < 3; j++)
            {
                Console.Write(board[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
    static bool IsBoardFull()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == ' ')
                {
                    return false;
                }
            }
        }
        return true;
    }
    static bool CheckForWin(char player)
    {
        for (int i = 0; i < 3; i++)
        {
            if (board[i, 0] == player && board[i, 1] == player && board[i, 2] == player)
            {
                return true; // Check rows
            }
            if (board[0, i] == player && board[1, i] == player && board[2, i] == player)
            {
                return true; // Check columns
            }
        }
        if (board[0, 0] == player && board[1, 1] == player && board[2, 2] == player)
        {
            return true; // Check diagonal
        }
        if (board[0, 2] == player && board[1, 1] == player && board[2, 0] == player)
        {
            return true; // Check reverse diagonal
        }

        return false;
    }
    static void PlayerMove(char player)
    {
        bool validMove = false;
        while (!validMove)
        {
            Console.Write($"Enter your move ({player}): ");
            string input = Console.ReadLine();
            if (input.Length == 2 && char.IsDigit(input[0]) && char.IsDigit(input[1]))
            {
                int row = input[0] - '1';
                int col = input[1] - '1';
                if (row >= 0 && row < 3 && col >= 0 && col < 3 && board[row, col] == ' ')
                {
                    board[row, col] = player;
                    validMove = true;
                }
                else
                {
                    Console.WriteLine("Invalid move. Try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Use row (1-3) and column (1-3).");
            }
        }
    }
    static int MiniMax(char[,] currentBoard, char player)
    {
        if (CheckForWin(humanPlayer))
        {
            return -1;
        }
        else if (CheckForWin(aiPlayer))
        {
            return 1;
        }
        else if (IsBoardFull())
        {
            return 0;
        }
        int bestScore = (player == aiPlayer) ? int.MinValue : int.MaxValue;
        /* bestScore------ dəyişeni, AI və ya insan oyunçusu üçün ən yaxşı nəticəni (ən böyük və ya ən kiçik) saxlamaq üçün istifadə olunur. 
         İlk dəyər, aiPlayer üçün ən kiçik nəticə (int.MinValue) və ya humanPlayer üçün ən böyük nəticə (int.MaxValue) əldə edir. 
         Bu, daha sonra ən yaxşı nəticəni yeniləmək üçün istifadə ediləcəkdir.
        */

        /* 1. `MiniMax` funksiyası rekursiv bir struktura sahibdir və oyun vəziyyəti ilə derin araşdırma aparır. Oyun vəziyyəti, `currentBoard` olaraq verilir
        və `player` parametri, hansı oyunçunun ən yaxşı hərəkəti etməyə çalışdığını göstərir(insan və ya AI).
        2.Funksiya üç əsas vəziyyəti yoxlayır:
        -Əgər insan oyunçusu(X) qalib gəlirsə, funksiya `-1` qiymətini qaytarır.
        -Əgər AI oyunçusu(O) qalib gəlirsə, funksiya `1` qiymətini qaytarır.
        -Əgər laqod dolu oldu və qalib heç bir oyunçu yoxdursa, funksiya `0` qiymətini qaytarır(berabərlik)
        3.Əgər sıra AI oyunçusundadırsa, funksiya ən yaxşı hərəkəti seçməyə çalışmaq üçün bütün boş xanaları dolaşır.Hər bir boş xana üçün
        bir müvəqqəti hərəkət edir və rekursiv olaraq `MiniMax` funksiyasını çağırır. Bu şəkildə bütün ehtimallı oyun vəziyyətlərini yoxlayır və ən yüksək qiyməti seçir.
        4.Əgər sıra insan oyunçusundadırsa, funksiya oxşar bir şəkildə bütün boş xanaları dolaşaraq ən pis hərəkəti(ən aşağı qiyməti) seçir.
        5. `AIMove` funksiyası, AI üçün ən yaxşı hərəkəti seçmək üçün `MiniMax` funksiyasını istifadə edir və bu hərəkəti laqodda tətbiq edir.
        AI, bütün ehtimallı hərəkətləri qiymətləyərək insan oyunçusuna qarşı ən yaxşı hərəkəti seçməyə çalışır.
        Beləliklə, Minimax alqoritmi hər hərəkətin nəticələrini proqnoz edərək oyunu ən yaxşı şəkildə oynamağa çalışır.*/



        //İki dövrə (nested for dövrü) ilə taxtanın bütün xanalarını gəzirik. Hər bir xana, bir potensial hərəkət kimi götürülür.
        //if (currentBoard[i, j] == ' ') şərti, əgər xana boşdursa  əməliyyatın icra edilməli olduğunu yoxlayır.
        //Əgər xana boşdursa, həmin xanaya müvəqqəti bir hərəkət edilir və rekursiv olaraq MiniMax funksiyasını çağırırıq. Bu, xananın doludulması halında
        //oyunun nəticələrini qiymətləndirir. Yəni, bu blok sadəcə hər bir potensial hərəkətin nəticələrini qiymətləndirmək üçün tahtanı müvəqqəti dəyişdirir
        //və nəticəni score dəyişəninə yaddaşa salır.
        // */
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (currentBoard[i, j] == ' ')
                {
                    currentBoard[i, j] = player;
                    int score = MiniMax(currentBoard, (player == aiPlayer) ? humanPlayer : aiPlayer);
                    // Daha sonra, müvəqqəti hərəkət geri qaytarılır(currentBoard[i, j] = ' ') çünki bu addım yalnız hərəkətin qiymətləndirilməsi üçündür və əsas
                    // taxtanı dəyişdirməməlidir
                    currentBoard[i, j] = ' ';
                    //Axırda, bestScore dəyəri, player'ın (AI və ya insan) ən yaxşı hərəkətini seçmək üçün istifadə olunur. Əgər player AI-dirsə, ən böyük nəticəni 
                    //(ən yaxşı hərəkəti) Math.Max funksiyası ilə yeniləyirik. Əgər player insan - dirsə, ən kiçik nəticəni(ən pis hərəkəti) Math.Min funksiyası ilə yeniləyirik.
                    bestScore = (player == aiPlayer) ? Math.Max(score, bestScore) : Math.Min(score, bestScore);
                }
            }
        }
        //Bu dövr, bütün boş xanaları gəzərək ən yaxşı hərəkəti seçmək üçün istifadə olunur. bestScore, nihayətində ən yaxşı hərəkətin qiymətini daşıyacaq və
       // bu qiymət, AI-in ən yaxşı hərəkəti seçməsi üçün AIMove funksiyası tərəfindən istifadə edilir.
        return bestScore;
     }
    static void AIMove(char player)
    {
        int bestScore = int.MinValue;
        int[] move = new int[] { -1, -1 };

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == ' ')
                {
                    board[i, j] = player;
                    int score = MiniMax(board, humanPlayer);
                    board[i, j] = ' ';

                    if (score > bestScore)
                    {
                        bestScore = score;
                        move[0] = i;
                        move[1] = j;
                    }
                }
            }
        }
        board[move[0], move[1]] = player;
    }
}
