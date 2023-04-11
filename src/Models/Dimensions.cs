namespace Cccat10RefactoringCode.Models;

public record Dimensions(double Width, double Height, double Length)
{
    public static Dimensions Square(int size) => new Dimensions(size, size, size);
    public double Volume => Width * Height * Length;
}