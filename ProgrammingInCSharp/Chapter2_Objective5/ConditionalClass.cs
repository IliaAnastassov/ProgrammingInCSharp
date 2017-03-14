namespace Chapter2_Objective5
{
    using System.Diagnostics;

    public class ConditionalClass
    {
        [Conditional("FirstCondition")]
        public void ConditionalMethod()
        {
        }
    }
}
