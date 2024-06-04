namespace Zadacha;

public interface IPrey
{
    public int Speed { get; set; }
    public bool IsFed { get; set; }
    public void Eat();
}