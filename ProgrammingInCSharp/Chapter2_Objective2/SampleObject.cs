namespace Chapter2_Objective2
{
    using System.Dynamic;

    public class SampleObject : DynamicObject
    {
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = binder.Name;
            return true;
        }
    }
}
