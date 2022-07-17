namespace Tomino
{
    public class EmptyTargetOutlineProvider : ITargetOutlineProvider
    {
        public TargetOutline GetTargetOutline()
        {
            return new TargetOutline();
        }
    }
}