namespace Cccat10RefactoringCode.Models;

public record Dimensions(double Width, double Height, double Length)
{
    public double Volume => Width * Height * Length;
}