namespace Zadacha;

public class Deer : IPrey
{
    private static int deerPopulation = 50;


    public Deer(int speed)
    {
        IsFed = false;
        var rnd = new Random();
        Speed = rnd.Next(102, speed);
        CanMate = rnd.Next(0, 101) < 70 ? CanMate = true : CanMate = false;
    }

    private bool CanMate { get; set; }
    public bool HasMated { get; set; }
    
    public int Speed { get; set; }
    public bool IsFed { get; set; }


    public void Eat()
    {
        IsFed = true;
        Habitat.LowerGrass();
    }

    public static event Action<Deer> OnDeerBorn;

    public static void DeerPopulationMinus()
    {
        deerPopulation--;
    }


    public void Mate(Deer parent2)
    {
        var rnd = new Random();

        if (!CanMate && parent2 is not { CanMate: true, HasMated: false } && !HasMated) return;
        if (!IsFed || !parent2.IsFed) return;
        var averageSpeedFromParents = (Speed + parent2.Speed) / 2;
        var _rnd = new Random();
        var step = _rnd.Next(-4, 5);
        var kidSpeed = averageSpeedFromParents + step;
        var kid = new Deer(kidSpeed);
        deerPopulation++;
        HasMated = true;
        parent2.HasMated = true;
        kid.CanMate = false;
 
        OnDeerBorn?.Invoke(kid);
    }
    
}