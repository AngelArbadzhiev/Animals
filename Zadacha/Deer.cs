namespace Zadacha;

public class Deer : IPrey
{
    private Random _rnd = new Random();

    public Deer(int speed)
    {
        IsFed = false;
        Speed = _rnd.Next(102, speed);
        CanMate = _rnd.Next(0, 101) < 70 ? CanMate = true : CanMate = false;
    }

    private bool CanMate { get; set; }
    public bool HasMated { get; set; }
    public static event Action<Deer> OnDeerBorn;
    public int Speed { get; set; }
    public bool IsFed { get; set; }


    public void Eat()
    {
        IsFed = true;
        Habitat.LowerGrass();
    }

    public void Mate(Deer partner)
    {
        if (!CanMate && partner is not { CanMate: true, HasMated: false } && !HasMated) return;
        if (!IsFed || !partner.IsFed) return;
        double averageSpeedFromParents = (Speed + partner.Speed) / 2;
        int step = _rnd.Next(-4, 5);
        int kidSpeed = (int)(averageSpeedFromParents + step);
        var kid = new Deer(kidSpeed);
        this.HasMated = true;
        partner.HasMated = true;
        kid.CanMate = false;
        OnDeerBorn?.Invoke(kid);
    }
    
}