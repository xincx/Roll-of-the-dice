using Tomino;

public class StubTargetOutlineProvider : ITargetOutlineProvider
{
    private TargetOutline targetOutline;

    public StubTargetOutlineProvider()
    {
        this.targetOutline = new TargetOutline();
    }

    public StubTargetOutlineProvider(TargetOutline targetOutline)
    {
        this.targetOutline = targetOutline;
    }

    public TargetOutline GetTargetOutline()
    {
        return targetOutline;
    }
}
