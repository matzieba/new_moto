using System.Diagnostics;

// See https://aka.ms/new-console-template for more information

// reading a txt file and returning an array of strings
var lines = File.ReadAllLines("Words.txt");

//generating 8 random numbers and chossing 8 random words and putting them to array
Random random = new Random();
string[] list_rand_words = new string[8];
string[] list_rand_words_easy = new string[4];
for (int i = 0; i <= 7; i++)
{
    int rand = random.Next(1, lines.Length);
    list_rand_words[i] = lines[rand];
}
Random rnd = new Random();
string[] list_rand_words_schuffle = list_rand_words.OrderBy(x => rnd.Next()).ToArray();
//generating 4 random numbers and chossing 4 random words and putting them to array
for (int i = 0; i <= 3; i++)
{
    int rand = random.Next(1, lines.Length);
    list_rand_words_easy[i] = lines[rand];
}
Random rnd1 = new Random();
string[] list_rand_words_schuffle_easy = list_rand_words_easy.OrderBy(x => rnd.Next()).ToArray();
//FUNKTIONS
//Converting user char input to matrix index
static int Conversion(char letter)
{
    int numb = 1;
    if (letter.Equals('A'))
    {
        numb = 1;
    }
    else if (letter.Equals('B'))
    {
        numb = 2;
    }
    else if (letter.Equals('C'))
    {
        numb = 3;
    }
    else if (letter.Equals('D'))
    {
        numb = 4;
    }
    return numb;
}
//Displaying Matrix
static void Display(string[,] matrix, string dificulty, int chances, int score)
{
    Console.WriteLine($"Level: {dificulty}\nGuess chances:{chances}\nScore:{score}\n\n");
    int rowLength = matrix.GetLength(0);
    int colLength = matrix.GetLength(1);

    for (int i = 0; i < rowLength; i++)
    {
        for (int j = 0; j < colLength; j++)
        {
            Console.Write(string.Format("{0} ", matrix[i, j]));
        }
        Console.Write(Environment.NewLine + Environment.NewLine);
    }

}
//Game Eingine a Single Game
static void Game(string[,] matrix, string[,] matrix_words, string dificulty, int chances, int score, int n)
{
    List<string> winning_choices = new List<string>();
    bool istrue = true;
    int chances_start = chances;
    Stopwatch sw = new Stopwatch();
    sw.Start();
    while (istrue ==true)
    {
        
        if (chances > 0)
        {
         
            // colecting  and validating user input
            Console.Clear();
            Display(matrix, dificulty, chances,score);
            Console.WriteLine("Please chose your field eg 'b1'");
            
            
            string input = Console.ReadLine();
            bool choice_valid = true;
            bool negation = false;
            int lenght = input.Length;
            char input_row = 'a';
            char input_row_upper = 'A';
            int input_col = 1;
            
            do
            {
                if (lenght == 2 && (!winning_choices.Contains(input)))
                {
                    if ((input[1] == '1' || input[1] == '2' || input[1] == '3' || input[1] == '4') && (input_row_upper == 'A' || input_row_upper == 'B' || input_row_upper == 'C' || input_row_upper == 'D'))
                    {
                        input_col = Convert.ToInt32(new string(input[1], 1));
                        input_row = input[0];
                        input_row_upper = Char.ToUpper(input_row);
                        choice_valid = true;

                        if (lenght > 2 || lenght == 0)
                        {
                            choice_valid = false;
                            Console.WriteLine("Please chose a valid field eg 'b1'");
                            input = Console.ReadLine();
                        }
                    }

                    else
                    {
                        choice_valid = false;
                        Console.WriteLine("Please chose a valid field eg 'b1'");
                        input = Console.ReadLine();
                    }
                }
                else 
                {
                    choice_valid = false;
                    Console.WriteLine("Please chose a valid field eg 'b1'");
                    input = Console.ReadLine();
                    lenght = input.Length;
                }
            } while (choice_valid == false);
            
            int input_row_numb = Conversion(Char.ToUpper(input_row));
            //displaying
            matrix[input_row_numb, input_col] = matrix_words[input_row_numb, input_col];
            Console.Clear();
            Display(matrix, dificulty, chances, score);

            //colecting second user input
            Console.WriteLine("Please chose your field eg 'b1'");
            string input1 = Console.ReadLine();
            int lenght1 = input1.Length;

            char second_input_row = 'a';
            char second_input_row_upper = 'A';
            int second_input_col = 1;
            
            do
            {
                if (lenght1 == 2 && (!winning_choices.Contains(input1)))
                {
                    if ((input1[1] == '1' || input1[1] == '2' || input1[1] == '3' || input1[1] == '4') && (second_input_row_upper == 'A' || second_input_row_upper == 'B' || second_input_row_upper == 'C' || second_input_row_upper == 'D'))
                    {
                        second_input_col = Convert.ToInt32(new string(input1[1], 1));
                        second_input_row = input1[0];
                        second_input_row_upper = Char.ToUpper(second_input_row);
                        choice_valid = true;

                        if (lenght1 > 2 || lenght1 == 0)
                        {
                            choice_valid = false;
                            Console.WriteLine("Please chose a valid field eg 'b1'");
                            input1 = Console.ReadLine();
                        }
                    }

                    else
                    {
                        choice_valid = false;
                        Console.WriteLine("Please chose a valid field eg 'b1'");
                        input1 = Console.ReadLine();
                    }
                }
                else
                {
                    choice_valid = false;
                    Console.WriteLine("Please chose a valid field eg 'b1'");
                    input1 = Console.ReadLine();
                    lenght1 = input1.Length;
                }
            } while (choice_valid == false);
            int second_input_row_numb = Conversion(Char.ToUpper(second_input_row));
            //displaying
            matrix[second_input_row_numb, second_input_col] = matrix_words[second_input_row_numb, second_input_col];
            Console.Clear();
            Display(matrix, dificulty, chances, score);

            //Checking if the answer is right
            //is right
            if (matrix_words[input_row_numb, input_col].Equals(matrix_words[second_input_row_numb, second_input_col]))
            {
                matrix[second_input_row_numb, second_input_col] = matrix_words[second_input_row_numb, second_input_col];
                Console.WriteLine("You were right! grats");
                
                score = score + 1;
                winning_choices.Add(input);
                winning_choices.Add(input1);
                
            }
            //is wrong
            else
            {
                matrix[input_row_numb, input_col] = "X";
                matrix[second_input_row_numb, second_input_col] = "X";
                Console.WriteLine("I am sorry you were wrong. Try again");
                
                chances = chances - 1;
                Console.Clear();
                Display(matrix, dificulty, chances, score);
            }
            //win scenario
            if ((dificulty.Equals("easy") && score == 4)||(dificulty.Equals("hard") && score == 8))
            {
                sw.Stop();
                Console.WriteLine($"Congrats you won! It took {chances_start - chances} chances and your time equals {sw.Elapsed}\nDo you want to play again? (y/n)");
                Console.WriteLine("do you want to save your score?");
       
                string antwort_won = Console.ReadLine();
                if (antwort_won.Equals("y"))
                {
                    winning_choices = new List<string>();
                    if (dificulty.Equals("easy"))
                    {
                        score = 0;
                        chances = 10;
                        for (int i = 1; i <= n; i++)
                        {
                            for (int j = 1; j <= 4; j++)
                            {
                                matrix[i, j] = "X";
                            }
                        }
                    }

                    else if (dificulty.Equals("hard"))
                    {
                        chances = 15;
                        score = 0;
                        for (int i = 1; i <= n; i++)
                        {
                            for (int j = 1; j <= 4; j++)
                            {
                                matrix[i, j] = "X";
                            }
                        }
                    }

                    Console.Clear();

                }
                else if (antwort_won.Equals("n"))
                {
                    Console.WriteLine("See you next time!");
                    istrue = false;
                }
                else
                {
                    Console.WriteLine("Please make a Valid Choice");

                }
            }
        }
        //loose scenario
        else
        {
            Console.WriteLine("I am sorry you are run out of chances. Do you want to play again? (y/n)");
            string antwort = Console.ReadLine();
            if (antwort.Equals("y"))
            {
                if (dificulty.Equals("easy"))
                {
                    chances = 10;
                }
               
                else if (dificulty.Equals("hard"))
                {
                    chances = 15;
                }
                
                Console.Clear();
            }
            else if (antwort.Equals("n"))
            {
                Console.WriteLine("See you next time!");
                istrue = false;
            }
            else
            {
                Console.WriteLine("Please make a Valid Choice");

            }

        }

            
    }
}


//Begining
Console.WriteLine("Greetings! Do you wont to play a game?");
Console.WriteLine("If so, please chose your destiny (dificulty level: hard/easy)");
string dificulty = Console.ReadLine();



//Coletcing entry data and seting start values
//Dificulty
int n = 1;
int score = 0;
int chances =10;
bool game_on = false;
while (game_on == false)
{
    
    

    if (dificulty.Equals("easy"))
    {
        n = 2;
        game_on = true;
    }
    else if (dificulty.Equals("hard"))
    {
        n = 4;
        chances = 15;
        game_on = true;
    }
    else
    {
        Console.WriteLine("Please make a valid chocie (hard/easy)");
        game_on = false;
        dificulty = Console.ReadLine();
    }
}


//Prepering data for game matrix
while (game_on)
{
    string[] row_numb = { "A", "B", "C", "D", "a", "b", "c", "d" };
    string[,] matrix = new string[5, 5];
    for (int i = 0; i <= 4; i++)
    {
        matrix[0, i] = i.ToString();
    }
    matrix[0, 0] = " ";
    for (int i = 1; i <= n; i++)
    {
        matrix[i, 0] = row_numb[i - 1];
    }
    for (int i = 1; i <= n; i++)
    {
        for (int j = 1; j <= 4; j++)
        {
            matrix[i, j] = "X";
        }
    }

    //Matrix words
    string[,] matrix_words = new string[5, 5];
    for (int i = 0; i <= 4; i++)
    {
        matrix_words[0, i] = i.ToString();
    }
    matrix_words[0, 0] = " ";
    for (int i = 1; i <= n; i++)
    {
        matrix_words[i, 0] = row_numb[i - 1];
    }
    if (n == 2)
    {
        for (int i = 1; i <= 4; i++)
        {
            matrix_words[1, i] = list_rand_words_easy[i - 1];
            matrix_words[2, i] = list_rand_words_schuffle_easy[i - 1];
        }
    }
    if (n == 4)
    {
        for (int i = 1; i <= 4; i++)
        {
            matrix_words[1, i] = list_rand_words[i - 1];
            matrix_words[2, i] = list_rand_words[i + 3];
            matrix_words[3, i] = list_rand_words_schuffle[i - 1];
            matrix_words[4, i] = list_rand_words_schuffle[i + 3];
        }
    }


    //Launch
    
    Game(matrix, matrix_words, dificulty, chances, score, n);
}





















