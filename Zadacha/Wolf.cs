namespace Zadacha;

public class Wolf : IPredator
{
    private  static int wolvesPopulation = 20;

    public Wolf(int speed)
    {
        IsFed = false;
        var rnd = new Random();
        Speed = rnd.Next(100, speed);
    }

    public bool CanMate { get; set; }
    public bool HasMated { get; set; }
    public int Speed { get; set; }
    public bool IsFed { get; set; }

    public void Eat()
    {
        IsFed = true;
    }

    public void Chase(IPrey prey)
    {
        if (this.Speed > prey.Speed)
        {
            Deer.DeerPoplationMinus();
            this.Eat();
        }
    }
    public void Mate(Wolf predator2)
    {
        var rnd = new Random();
        if (rnd.Next(0, 101) < 85)
        {
            CanMate = true;
            predator2.CanMate = rnd.Next(0, 101) < 85 ? CanMate = true : CanMate = false;
        }
        else
        {
            CanMate = false;
        }

        if ((CanMate && predator2.CanMate) && (!HasMated && !predator2.HasMated))
        {
            if (IsFed && predator2.IsFed)
            {
                var averageSpeedFromParents = (Speed + predator2.Speed) / 2;
                var _rnd = new Random();
                var step = _rnd.Next(-5, 5);
                var kidSpeed = averageSpeedFromParents + step;
                var kid = new Wolf(kidSpeed);
                wolvesPopulation++;
            }
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