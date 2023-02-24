namespace Eating_Philosophers
{
    enum ForkState { Taken, OnTheTable }

    class Fork
    {
        public int ForkID { get; set; }
        public ForkState State { get; set; }
        public string? TakenBy { get; set; }

        public bool Take(string takenBy)
        {
            lock (this)
            {
                if (this.State == ForkState.OnTheTable)
                {
                    State = ForkState.Taken;
                    TakenBy = takenBy;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Fork: {0} is taken by {1}", ForkID, TakenBy);
                    return true;
                }

                else
                {
                    State = ForkState.Taken;
                    return false;
                }
            }
        }

        public void Put()
        {
            State = ForkState.OnTheTable;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Fork: {0} is placed on the table by {1}", ForkID, TakenBy);
            TakenBy = "";
        }
    }

    class Table
    {
        internal static Fork Fork1 = new() { ForkID = 1, State = ForkState.OnTheTable };
        internal static Fork Fork2 = new() { ForkID = 2, State = ForkState.OnTheTable };
        internal static Fork Fork3 = new() { ForkID = 3, State = ForkState.OnTheTable };
        internal static Fork Fork4 = new() { ForkID = 4, State = ForkState.OnTheTable };
        internal static Fork Fork5 = new() { ForkID = 5, State = ForkState.OnTheTable };
    }
}
