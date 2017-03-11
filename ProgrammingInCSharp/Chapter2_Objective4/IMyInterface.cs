namespace Chapter2_Objective4
{
    using System;

    public interface IMyInterface
    {
        string GetResult();

        int Value { get; }

        event EventHandler ResultRetreived;

        int this[string index] { get; }
    }
}
