namespace Zadacha;

public class Wolf : IPredator
{
    private static int wolvesPopulation = 20;
    Random rnd = new Random();

    public Wolf(double speed)
    {
        IsFed = false;
        Speed = rnd.Next(101, (int)speed);
        CanMate = rnd.Next(0, 101) < 70 ? CanMate = true : CanMate = false;
    }

    private bool CanMate { get; set; }
    public bool HasMated { get; set; }
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
        Eat();
    }

    public static event Action<Wolf> OnWolfBorn;
    public static event Action<Wolf> OnWolfDead;

    public void Mate(Wolf predator2)
    {
        if (!CanMate && predator2 is not { CanMate: true, HasMated: false } && !HasMated) return;
        
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
    public void CheckIsFed(){
        if(!this.IsFed){
            OnWolfDead?.Invoke(this);
        }
    }
}