namespace Zadacha;

public class Wolf : IPredator
{
    private  static int wolvesPopulation = 20;

    public Wolf(int speed)
    {
        IsFed = false;
        var rnd = new Random();
        Speed = rnd.Next(101, speed);
        this.CanMate = rnd.Next(0, 101) < 85 ? CanMate = true : CanMate = false;
    }

    private bool CanMate { get; set; }
    private bool HasMated { get; set; }
    public int Speed { get; set; }
    public bool IsFed { get; set; }

    public void Eat()
    {
        this.IsFed = true;
    }

    public void Chase(IPrey prey)
    {
        if (this.Speed <= prey.Speed) return;
        Deer.DeerPopulationMinus();
        this.Eat();
    }
    public void Mate(Wolf predator2)
    {
        if ((CanMate && predator2.CanMate) && (!HasMated && !predator2.HasMated))
        {
            if (!IsFed || !predator2.IsFed) return;
            int averageSpeedFromParents = (Speed + predator2.Speed) / 2;
            Random _rnd = new Random();
            int step = _rnd.Next(-5, 5);
            int kidSpeed = averageSpeedFromParents + step;
            Wolf kid = new Wolf(kidSpeed);
            wolvesPopulation++;
        }
        else if (CanMate == false || predator2.CanMate == false)
        {
            
        }
    }
    public static int ReturnWolfPopulation()
    {
        return wolvesPopulation;
    }
}