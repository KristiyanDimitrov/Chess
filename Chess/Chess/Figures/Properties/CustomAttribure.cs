[System.AttributeUsage(System.AttributeTargets.Class)]
public class FigureInfoAttribute : System.Attribute
{
    public string fullName;
    private string shortName;

    public FigureInfoAttribute(string fullName, string shortName)
    {
        this.fullName = fullName;
        this.shortName = shortName;
    }

    public string GetShortName()
    {
        return shortName;
    }
}