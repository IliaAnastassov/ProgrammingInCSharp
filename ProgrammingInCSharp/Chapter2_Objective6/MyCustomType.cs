namespace Chapter2_Objective6
{
    using System;

    public class MyCustomType
    {
        public MyCustomType(int id)
        {
            Id = id;
            Console.WriteLine($"Creating...{Id}");
        }

        ~MyCustomType()
        {
            Console.WriteLine($"Finalizing...{Id}");
        }

        public int Id { get; set; }
    }
}
