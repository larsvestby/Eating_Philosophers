namespace Eating_Philosophers
{
    enum PhilosopherState { Eating, Thinking }

    /// <summary>
    /// Filosof objekt.
    /// </summary>
    internal class Philosophers
    {
        public string Name { get; set; }

        public PhilosopherState State { get; set; }

        //Bruges til at bestemme hvor mange "ture" i træk en filosof kan gå uden at spise.
        readonly int IsStarving;

        //Højre og venstre gaffel.
        public readonly Fork RightFork;
        public readonly Fork LeftFork;

        readonly Random randy = new();

        int TurnsThinking = 0;

        /// <summary>
        /// Filosof.
        /// </summary>
        /// <param name="rightFork">Højre gaffel.</param>
        /// <param name="leftFork">Venstre gaffel.</param>
        /// <param name="name">Navn.</param>
        /// <param name="isStarving">Status på hvor mange ture de har ventet.</param>
        public Philosophers(Fork rightFork, Fork leftFork, string name, int isStarving)
        {
            RightFork = rightFork;
            LeftFork = leftFork;
            Name = name;
            State = PhilosopherState.Thinking;
            IsStarving = isStarving;
        }

        /// <summary>
        /// Metode til at prøve at spise.
        /// </summary>
        public void TryEating()
        {
            //Saml højre gaffel op.
            if (TakeRightFork())
            {
                //Hvis det lykkedes at samle højre gaffel op, skal de med de samme prøve at tage venstre gaffel.
                if (TakeLeftFork())
                {
                    Eating();
                }
                //Hvis det lykkedes at få højre gaffel men ikke den venstre. Så ventes der i et øjeblik, før der prøves igen.
                else
                {
                    Thread.Sleep(randy.Next(100, 400));
                    if (TakeLeftFork())
                    {
                        //Hvis venstre gaffel nu er samlet op kan der spises.
                        Eating();
                    }
                    //Hvis det igen ikke lykkes at få venstre gaffel, lægges højre gaffel ned.
                    else
                    {
                        RightFork.Put();
                    }
                }
            }
            //Hvis højre gaffel ikke kan samles op, så prøves der på venstre.
            else
            {
                //Saml venstre gaffel op.
                if (TakeLeftFork())
                {
                    //Der ventes et øjeblik før vi prøver at samle højre gaffel op.
                    Thread.Sleep(randy.Next(100, 400));
                    if (TakeRightFork())
                    {
                        //Hvis højre gaffel samles op, kan der spises.
                        Eating();
                    }
                    else
                    {
                        //Hvis højre gaffel ikke samles op, lægger vi venstre gaffel på bordet igen.
                        LeftFork.Put();
                    }
                }
            }
            //Efter filosoffen har prøvet at spsie, så tænker han.
            Think();
        }

        /// <summary>
        /// Metode til at tænke
        /// </summary>
        public void Think()
        {
            this.State = PhilosopherState.Thinking;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("{0} is thinking...", Name);

            Thread.Sleep(randy.Next(2500, 20000));
            TurnsThinking++;

            //Hvis filosoffen har tænkt i længere tid end han kan gå uden mad prioriteres han.
            if (TurnsThinking > IsStarving)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Oh no! {0} is starving!", Name);
            }

            //Når filosoffen er færdig med at tænke, prøver han at spise.
            TryEating();
        }

        /// <summary>
        /// Metode til at spise.
        /// </summary>
        public void Eating()
        {
            //Hvis det lykkes at få begge gafler, spises der.
            this.State = PhilosopherState.Eating;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("{0} is eating! Om nom nom", Name);

            Thread.Sleep(randy.Next(5000, 10000));

            TurnsThinking = 0;

            //Gafler lægges ned.
            RightFork.Put();
            LeftFork.Put();
        }

        /// <summary>
        /// Tag venstre gaffel.
        /// </summary>
        /// <returns>Venstre gaffel, hvis tilgængelig.</returns>
        private bool TakeLeftFork()
        {
            return LeftFork.Take(Name);
        }

        /// <summary>
        /// Tag højre gaffel.
        /// </summary>
        /// <returns>Højre gaffel, hvis tilgængelig.</returns>
        private bool TakeRightFork()
        {
            return RightFork.Take(Name);
        }
    }
}
