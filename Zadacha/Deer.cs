namespace Zadacha;

public class Deer : IPrey
{
    private static int deerPopulation = 50;
   
    public Deer(int speed)
    {
        this.IsFed = false;
        var rnd = new Random();
        Speed = rnd.Next(100, speed);
        this.CanMate = rnd.Next(0, 101) < 70? CanMate = true : CanMate = false;
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
    public static void DeerPopulationMinus()
    {
        deerPopulation--;
    }

    public static int ReturnDeerPopulation()
    {
        return deerPopulation;
    }
   

    public void Mate(Deer parent2)
    {
        var rnd = new Random();

        if ((CanMate && parent2.CanMate) && (!parent2.HasMated && !HasMated))
        {
            if (!IsFed || !parent2.IsFed) return;
            var averageSpeedFromParents = (Speed + parent2.Speed) / 2;
            var _rnd = new Random();
            var step = _rnd.Next(-5, 5);
            var kidSpeed = averageSpeedFromParents + step;
            var kid = new Deer(kidSpeed);
            deerPopulation++;
            HasMated = true;
            parent2.HasMated = true;
            kid.CanMate = false;
            kid.
        }
        else if (CanMate == false || parent2.CanMate == false)
        {
        }
    }

    public void CheckIsFed()
    {
        if (!IsFed)
        {
            DeerPopulationMinus();
        }
    }
}