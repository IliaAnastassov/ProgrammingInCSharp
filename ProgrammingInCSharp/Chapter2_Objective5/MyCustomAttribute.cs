namespace Chapter2_Objective5
{
    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class MyCustomAttribute : Attribute
    {
        public MyCustomAttribute(string description)
        {
            Description = description;
        }

        public string Description { get; set; }
    }
}
