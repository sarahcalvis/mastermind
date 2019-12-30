using System;
/*
 * Create a C# console application that is a simple version of Mastermind.
 * The randomly generated answer should be four (4) digits in length, 
 * with each digit between the numbers 1 and 6.  
 * After the player enters a combination,
 * a minus (-) sign should be printed for every digit that is correct but in the wrong position,
 * and a plus (+) sign should be printed for every digit that is both correct and in the correct position.
 * Nothing should be printed for incorrect digits.  
 * The player has ten (10) attempts to guess the number correctly
 * before receiving a message that they have lost.
 */
namespace mastermind
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate a correct answer
            int[] answer = Program.generateAnswer();
            
            // Play Mastermind
            Boolean guessedCorrectAnswer = gameLoop(answer);

            // Print results
            if (guessedCorrectAnswer) {
                Console.WriteLine("You win.");
            } else {
                Console.WriteLine("You lose.");
            }
        }
        
        public static Boolean gameLoop(int[] answer) {
            Boolean guessedCorrectAnswer = false;

            // Let the user guess 10 times
            for (int i = 0; i < 10; i++) {
                // Stop allowing the user to guess once he has guessed the correct answer
                if (!guessedCorrectAnswer) {

                    // Get a guess from the user
                    String unparsedGuess = Program.takeInput();

                    // Validate guess
                    Boolean isValidGuess = Program.validateGuess(unparsedGuess);

                    // If invalid guess, wait for a valid guess
                    while(!isValidGuess) {
                        Console.Write("Invalid guess. Try again.\n");
                        unparsedGuess = Program.takeInput();
                        isValidGuess = Program.validateGuess(unparsedGuess);
                    }

                    // Parse the guess
                    int[] guess = Program.parseGuess(unparsedGuess);

                    // Compare the guess to the answer
                    String grade = Program.gradeGuess(answer, guess);

                    // Print the results to the console for the user
                    Console.WriteLine(grade);

                    // If the user guessed correctly, record this
                    if (grade.Equals("++++")) {
                        guessedCorrectAnswer = true;
                    }
                }
            }
            
            // Return whether the user guessed correctly
            return guessedCorrectAnswer;
        }

        /*
         * Generate a secret code
         * Params: none
         * Return: An array of four random integers between 1 and 6
         */
        public static int[] generateAnswer() {
            int[] answer = new int[4];
            Random rand = new Random();

            for(int i = 0; i < answer.Length; i++) {
                answer[i] = rand.Next(1, 7);
            }

            return answer;
        }

        /*
         * Get a guess from the user
         * Params: none
         * Return: a String holding the user's guess
         */
        public static String takeInput() {
            Console.Write("Enter a guess. Your guess must be four numbers between 1 and 6. ");
            String unparsedGuess = Console.ReadLine();
            return unparsedGuess;
        }

        /*
         * Validate the user's guess
         * Makes sure the guess is only four characters
         * Makes sure those characters integers between 1 and 6
         * Params: the guess, yet unparsed
         * Return: a Boolean telling whether the guess was good
         */
        public static Boolean validateGuess(String unparsedGuess) {
            Boolean isValidGuess = true;

            // Ensure the guess is length 4
            if (unparsedGuess.Length != 4) {
                isValidGuess = false;
            }

            // Ensure the guess is only integers in the range from 1 to 6
            else {
                char[] charGuess = unparsedGuess.ToCharArray();
                for (int i = 0; i < 4; i++) {
                    int guess = (int)Char.GetNumericValue(charGuess[i]);
                    if (guess < 1 || guess > 6) {
                        isValidGuess = false;
                    }
                }
            }

            return isValidGuess;
        }
        
        /*
         * Parses a validated guess
         * Params: a String containing a valid guess
         * Return: an Array of integers containing the guess
         */
        public static int[] parseGuess(String unparsedGuess) {
            int[] guess = new int[4];

            // Split the guess into a char array
            char[] charGuess = unparsedGuess.ToCharArray();

            // Cast the chars into integers
            for (int i = 0; i < 4; i++) {
                guess[i] = (int)Char.GetNumericValue(charGuess[i]);
            }

            return guess;
        }

        /*
         * Assesses the guess
         * Params: an array of integers containing the answer and an array of integers containing the guess
         * Return: a String signifying the correctness fo the guess
         */
        public static String gradeGuess(int[] answer, int[] guess) {
            String grade = "";
            
            // Make a copy of the answer we can mutate
            int[] answerCopy = new int[4];
            Array.Copy(answer, answerCopy, 4);

            // Record the correct guesses
            for (int i = 0; i < 4; i++) {
                if (answer[i] == guess[i]) {
                    grade += "+";
                    answerCopy[i] = -1;
                }
            }

            // Record the partially correct guesses
            for (int i = 0; i < 4; i++) {
                int valIndex = contains(answerCopy, guess[i]);
                if (valIndex != -1) {
                    answerCopy[valIndex] = -1;
                    grade += "-";
                }
            }

            return grade;
        }

        /*
         * Tells the last index where an array stores a value
         * Params: an array of integers and a target integer value
         * Return: -1 if the value is not found; otherwise the location where the value is stored in the array
         */
        public static int contains(int[] array, int value) {
            int retVal = -1;

            for (int i = 0; i < array.Length; i++) {
                if (array[i] == value) {
                    retVal = i;
                }
            }
            
            return retVal;
        }
    }
}
