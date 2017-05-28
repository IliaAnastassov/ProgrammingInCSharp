namespace Chapter4_Objective2
{
    using System.ServiceModel;

    [ServiceContract]
    public class MyService
    {
        [OperationContract]
        public string DoWork(string left, string right)
        {
            return left + right;
        }
    }
}
