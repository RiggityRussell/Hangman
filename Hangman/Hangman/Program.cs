using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApplication6
{
    class Program
    {
        static void Main(string[] args)
        {
            

            string[] hangman =
            {
              @"
    +---+
        |
        |
        |
       ===",
              @"
    +---+
    o   |
        |
        |
       ===",
              @"
    +---+
    o   |
    |   |
        |
       ===",
              @"
    +---+
    o   |
   /|   |
        |
       ===",
              @"
    +---+
    o   |
   /|\  |
        |
       ===",
              @"
    +---+
    o   |
   /|\  |
   /    |
       ===",
              @"
    +---+
    o   |
   /|\  |
   / \  |
     ===",
              @"
     ___
    /   \
   | x x |
   |  _  |
    \___/"
            }; //The ASCII is built to create a facsimile of the hangman in a simple way

            string[] wordBank = { 
            "guitar",
            "xylophone",
            "bass",
            "drums",
            "microphone",
            "beats",
            "orchestra",
            "crescendo",
            "alto",
            "rhythm",
            "tempo",
            "chords",
            "melody",
            "harmony",
            "classical",
            "timbre",
            "tonality",
            "jazz", }; // creating a array of words to be randomly chosen from
            Play(wordBank, hangman); //bring both of the arrays into the Play method

            static void Play(string[] wordBank, string[] hangman)//Where the game begins
            {
                Random random = new Random((int)DateTime.Now.Ticks); //create a random variable
                string wordToGuess = wordBank[random.Next(0, wordBank.Length)]; //create a variable for the random word to guess
                string wordToGuessUppercase = wordToGuess.ToUpper(); //create an uppercase version of the word

                StringBuilder displayToPlayer = new StringBuilder(wordToGuess.Length); // Create a stringbuilder variable to build a string from chars
                for (int i = 0; i < wordToGuess.Length; i++) //take the length of the word to guess and iterate through it
                displayToPlayer.Append('_'); //put underscores in

                List<char> correctGuesses = new List<char>(); //create a list for correct guesses
                List<char> incorrectGuesses = new List<char>(); // create a list for incorrect guesses
                List<char> bothGuesses = new List<char>(); //create a list for BOTH!

                int lives = hangman.Length -1; //once they guess wrong 8 times the game ends
                bool won = false; // bool
                int lettersRevealed = 0; // int for holding number of letters revealed
                int numwrong = 0; //in showing number wrong
                string playerInput; //a string to hold the player playerInput
                char guess; //guess is a single character
                Console.WriteLine("\t\tWelcome to MUSICAL Hangman! All words will have some musical meaning."); // Welcome to the gam
            while (!won && lives > 0) //while notwon and lives are over 0
            {
                Console.Write("\n\tGuess a letter: "); // we ask them to guess a letter
                Console.WriteLine("\t" + hangman[numwrong]); //we put the associated ascii on the screen with the number of guesses wrong
                Console.WriteLine(displayToPlayer.ToString()); //we show them how many letters are in the word they are gueessing with _
                playerInput = Console.ReadLine().ToUpper(); //we take their guess and make it uppercase
                if (playerInput == "" || playerInput.Length != 1) // if they enter nothing, OR they enter more than one letter
                {
                    Console.WriteLine("\tplease enter a single letter"); //make em try again
                    continue; //go back to the beginning.
                }
                else if (Regex.IsMatch(playerInput, "^[a-zA-Z]") != true) // use regex to ensure that they are using ONLY letters.
                                                                          // (this regex says, looking at the beginning of the string, return true if there is a letter there)
                                                                          // we then test this by checking if the player input of only letters is NOT equal to true, and if so

                    {
                        Console.WriteLine("\tLetters only please!");// tell em to enter a letter only
                        continue;// restart
                    }
                else//
                { 
                    guess = playerInput[0];//all good we make guess equal to the playerinput at the associated range
                }
                if (correctGuesses.Contains(guess)) //if correctGuesses has already had the guess added to it
                {
                    Console.WriteLine($"\tYou've already tried '{guess}', and it was correct!\n"); //remind them
                    continue; //try again
                }
                else if (incorrectGuesses.Contains(guess)) //if incorrectGuesses already had the guess added to it
                {
                    Console.WriteLine($"\tYou've already tried '{guess}', and it was wrong!\n"); // remind them
                    continue; //try again
                }

                if (wordToGuessUppercase.Contains(guess)) //if the guessed letter is in the word to guess
                {
                    correctGuesses.Add(guess); // add to the array
                    bothGuesses.Add(guess); // add to the array

                    for (int i = 0; i < wordToGuess.Length; i++)
                    {
                        if (wordToGuessUppercase[i] == guess) //if the guess is equal to the value of one or more of the chars in the string wordtoguess
                        {
                            displayToPlayer[i] = wordToGuess[i]; //show them the letter
                            lettersRevealed++; //increase the number of letters revealed to track when we have won
                        }
                    }

                    if (lettersRevealed == wordToGuess.Length) // if the length of these match
                    {
                        won = true; //flip the bool
                    }
                }
                else
                {
                    incorrectGuesses.Add(guess); //add to array
                    bothGuesses.Add(guess); //add to array
                    Console.WriteLine($"\tNope, there's no '{guess}' in it!\n"); //tell them if they blew it
                    numwrong++; //increase the numberwrong to reflect the ascii
                    lives--; //decrease lives

                }

                if (!won) // if we have not won, show the already guessed letters
                {
                    Console.WriteLine("\n\tAlready guessed letters: "); //here they are
                    foreach (char letter in bothGuesses)
                    {
                        Console.Write(letter);//SHOW THEM THEIR FAILURE
                    }
                }
            }

                if (won) //if we have won!
                {
                    Console.WriteLine($"\t\tYou won! It was '{wordToGuess}'"); //tell them we are proud and show them the word
                }

                else //if we have lost.....
                {
                    Console.WriteLine(hangman[7]); //show them the final ascii dead face
                    Console.WriteLine($"\t\tYou lost! It was '{wordToGuess}'"); //show them the correct word
                }
            
 
            while (true) 
            {   
                Console.Write("\tDo you want to play again? [yes/no]\n\t"); // see if they want to play again
                string yOrN = Console.ReadLine(); //create that string value

                if (yOrN == "yes" || yOrN == "Yes") //if they enter yes
                    {
                        Console.Clear();
                        Play(wordBank, hangman); //send em back!
                    }
                else if (yOrN == "no" || yOrN == "No")// if they enter no
                    {
                        Console.WriteLine("\tOkie Dokie Artichokie! Bye bye!"); //bye bye
                        Environment.Exit(0); //exit application
                        
                    }
                else
                    {
                        Console.WriteLine("\tEnter yes or no."); // make em enter yes or no
                    }

                }
            }
        }
    }
}