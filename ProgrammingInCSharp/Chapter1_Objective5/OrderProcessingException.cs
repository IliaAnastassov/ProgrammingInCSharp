namespace Chapter1_Objective5
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class OrderProcessingException : Exception, ISerializable
    {
        public OrderProcessingException(int orderId)
        {
            OrderId = orderId;
            HelpLink = "http://www.mydomain.com/OrderProcessingExceptionInfo";
        }

        public OrderProcessingException(int orderId, string message) : base(message)
        {
            OrderId = orderId;
            HelpLink = "http://www.mydomain.com/OrderProcessingExceptionInfo";
        }

        public OrderProcessingException(int orderId, string message, Exception innerExcepion) : base(message, innerExcepion)
        {
            OrderId = orderId;
            HelpLink = "http://www.mydomain.com/OrderProcessingExceptionInfo";
        }

        protected OrderProcessingException(SerializationInfo info, StreamingContext context)
        {
            OrderId = (int)info.GetValue("OrderId", typeof(int));
        }

        public int OrderId { get; set; }
    }
}
