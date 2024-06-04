namespace Zadacha;

public interface IPredator
{
    public int Speed { get; set; }
    public bool IsFed { get; set; }
    public void Eat();
    public void Chase(IPrey prey);
}