namespace Windtalker.Plumbing.Logging
{
    public class WrappedJObject
    {
        public dynamic Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}