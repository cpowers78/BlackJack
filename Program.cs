
using System;
//Warning: The following program does not account for the singularity of the cards

namespace ConsoleProjectHealthStream
{
    class Program
    {

        //Function that generates the suit for the card
        static string Generatesuit()
        {
            string[] suits = { "♥", "♦", "♣", "♠" };
            Random rnd = new Random();
            int suit_index = rnd.Next(4);
            return suits[suit_index];

        }
        //Function that generates the number index of the card from A-10 and the face cards
        static int Generate_number()
        {
            Random rnd = new Random();
            int numb_value = rnd.Next(13);
            return numb_value;
        }

        //Represents a card object and its properties
        class Card
        {
            public int cardnum = Generate_number();
            public int cardvalue = 0;
            string cardsuit = Generatesuit();
            //Generates the card as a string combination of a suit and a number/face and assigns it a value for the game
            public string Generate_card()
            {
                if (cardnum == 0)
                {
                    cardvalue = 11;
                }
                else if (1 <= cardnum & cardnum <= 9)
                {
                    cardvalue = cardnum + 1;
                }
                else if (cardnum >= 10)
                {
                    cardvalue = 10;
                }
                string[] cardlist = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
                return cardlist[cardnum] + cardsuit;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("||                        ♥ ♦ ♣ ♠ WELCOME TO BLACK JACK ♥ ♦ ♣ ♠                             ||");
            Console.WriteLine("||                      You will be playing against me, the dealer.                         ||");
            Console.WriteLine("||                                                                                          ||");
            Console.WriteLine("||           Rules: You will be given $100 to play. I will deal you two cards.              ||");
            Console.WriteLine("|| Once you are given your cards, you can choose 'S' for 'Stand' to not take any more cards.||");
            Console.WriteLine("||                     Or, you can choose 'H' to receive another card.                      ||");
            Console.WriteLine("||                  Whoever's cards add up to 21 or are closest to 21 wins.                 ||");
            Console.WriteLine("||              If you win, you receive 1.5 times your bet. An Ace counts as 11.             ||");

            double wallet = 100.00;
            const double win_rate = 1.5;
            bool game = true;

            while (game == true)
            {
                if (wallet == 0)
                {
                    Console.WriteLine("You lost all of your money! Game over!");
                    game = false;
                    break;
                }
                Console.Write("Place your bet: ");
                double initial_bet = Convert.ToDouble(Console.ReadLine());

                while (initial_bet > wallet)
                {
                    Console.WriteLine("You do not have sufficient funds to place this bet. Please place another:");
                    initial_bet = Convert.ToDouble(Console.ReadLine());
                }
              
                Console.WriteLine("Great! Let's Get Started.");

                double playertotal = 0.0;
                double dealertotal = 0.0;

                Card playercard1 = new Card();
                Card dealercard1 = new Card();
                Card playercard2 = new Card();
                Card dealercard2 = new Card();

                //Generate the dealer and the players first two cards and their totals
                string dealerscard1 = dealercard1.Generate_card();
                string dealerscard2 = dealercard2.Generate_card();

                dealertotal = dealercard1.cardvalue + dealercard2.cardvalue;

                string playerscard1 = playercard1.Generate_card();
                string playerscard2 = playercard2.Generate_card();

                playertotal = playercard1.cardvalue + playercard2.cardvalue;


                Console.WriteLine("Your cards are a " + playerscard1 + " and " + playerscard2 + " .");

                if (playertotal == 21)
                {
                    Console.WriteLine("You hit 21! You win!");
                    wallet += (win_rate * initial_bet);
                    game = false;
                    break;
                }
                Console.WriteLine();
                Console.WriteLine("Do you want to Hit or Stand? ('H' or 'S') ");
                string playermove = Console.ReadLine();

                //Loops through the players turn 
                while (playermove == "H")
                {
                    if (playermove == "S")
                    {
                        break;
                    }
                    Card newplayercard = new Card();
                    string newplayercardface = newplayercard.Generate_card();
                    playertotal += newplayercard.cardvalue;
                    Console.WriteLine("Your new card is a " + newplayercardface + ".");


                    if (playertotal == 21)
                    {
                        Console.WriteLine("You hit 21! You win!");
                        wallet += (win_rate * initial_bet);
                        game = false;
                        break;
                    }else if (playertotal > 21)
                    {
                        Console.WriteLine("You busted! You lose your bet!");
                        wallet -= initial_bet;
                        game = false;
                        break;
                    }
                    Console.WriteLine("Do you want to Hit or Stand? ('H' or 'S') ");
                    playermove = Console.ReadLine();
                };

                //Loops through the dealers turn until he hits 17 or above
                while(dealertotal <= 17)
                {
                    Card newdealercard = new Card();
                    string newdealercardface = newdealercard.Generate_card();
                    dealertotal += newdealercard.cardvalue;

                }
          
                Console.WriteLine();

                //The following collection of if statements compares the dealers count to the players count to see who won and adjusts the players wallet value accordingly
                if(playertotal != 21 & dealertotal == 21)
                {
                    Console.WriteLine("My count is " + dealertotal + " and your count is " + playertotal + ".");
                    Console.WriteLine();
                    Console.WriteLine("I win! You lose your bet.");
                    wallet -= initial_bet;
                }

                else if (playertotal == dealertotal)
                {
                    Console.WriteLine("My count is " + dealertotal + " and your count is " + playertotal + ".");
                    Console.WriteLine();
                    Console.WriteLine("We tied! Let's split the money.");
                    wallet += (initial_bet * 0.5);
                }
                else if (playertotal < 21 & dealertotal < 21)
                {
                    if (playertotal > dealertotal)
                    {
                        Console.WriteLine("My count is " + dealertotal + " and your count is " + playertotal + ".");
                        Console.WriteLine();
                        wallet += (win_rate * initial_bet);
                        Console.WriteLine("You won! You get 1.5 times your bet!");
                    }
                    else if (dealertotal > playertotal)
                    {
                        Console.WriteLine("My count is " + dealertotal + " and your count is " + playertotal + ".");
                        Console.WriteLine();
                        wallet -= initial_bet;
                        Console.WriteLine(("I beat you! You lose your bet!"));
                    }
                }
                else if (dealertotal > 21 & playertotal < 21)
                {
                    Console.WriteLine("My count is " + dealertotal + " and your count is " + playertotal + ".");
                    Console.WriteLine();
                    wallet += (win_rate * initial_bet);
                    Console.WriteLine(("I busted. You win!"));
                }

                Console.WriteLine();
                Console.WriteLine("Now you have $" + wallet + " in your wallet.");
                Console.WriteLine();
                Console.WriteLine("Do you want to keep betting? Type 'Y' or 'N'");
                string answer = Console.ReadLine();
                if (answer == "Y")
                {
                    game = true;
                }
                else if (answer == "N")
                {
                    game = false;
                    
                }
            }
            }
        }
    }

