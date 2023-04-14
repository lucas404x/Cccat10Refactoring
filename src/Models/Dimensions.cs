namespace Cccat10RefactoringCode.Models;

public record Dimensions
{
    private const int CUBIC_DIVISOR = 1000000;

    public double Width { get; private set; }
    public double Height { get; private set; }
    public double Length { get; private set; }

    /// <summary>
    /// Value provided in cubic meter.
    /// </summary>
    public double Volume => (Width * Height * Length ) / CUBIC_DIVISOR;
    
    private Dimensions(double width, double height, double length) 
    {
        if (width < 0 || height < 0 || length < 0) 
        {
            throw new ArgumentOutOfRangeException("the dimension must have positive numbers only");
        }

        Width = width;
        Height = height;
        Length = length;
    }

    public static Dimensions FromCentimers(double width, double height, double length) => new(width, height, length);
}