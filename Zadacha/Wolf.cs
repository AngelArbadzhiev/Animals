namespace Zadacha;

public class Wolf : IPredator
{
    private static int wolvesPopulation = 20;

    public Wolf(double speed)
    {
        IsFed = false;
        var rnd = new Random();
        Speed = rnd.Next(102, (int)speed);
        CanMate = rnd.Next(0, 101) < 70 ? CanMate = true : CanMate = false;
    }

    private bool CanMate { get; set; }
    public bool HasMated { get; set; }
    public int Speed { get; set; }
    public bool IsFed { get; set; }

    public void Eat()
    {
        IsFed = true;
    }

    public void Chase(IPrey prey)
    {
        if (Speed <= prey.Speed) return;
        Deer.DeerPopulationMinus();
        Eat();
    }

    public static event Action<Wolf> OnWolfBorn;

    public void Mate(Wolf predator2)
    {
        if (!CanMate && predator2 is not { CanMate: true, HasMated: false } && !HasMated) return;
        if (!IsFed || !predator2.IsFed) return;
        var averageSpeedFromParents = (Speed + predator2.Speed) / 2;
        var _rnd = new Random();
        var doubleRnd = _rnd.NextDouble();
        double step = _rnd.Next(-4, 5);
        var kidSpeed = averageSpeedFromParents + step - doubleRnd;
        var kid = new Wolf(kidSpeed);
        HasMated = true;
        predator2.HasMated = true;
        kid.CanMate = false;
        wolvesPopulation++;
        OnWolfBorn?.Invoke(kid);
    }
}