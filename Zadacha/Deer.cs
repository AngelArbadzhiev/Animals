namespace Zadacha;

public class Deer : IPrey
{
    private static int deerPopulation = 50;
    
    public Deer(int speed)
    {
        this.IsFed = false;
        var rnd = new Random();
        Speed = rnd.Next(100, speed);
        
    }
    private bool CanMate { get; set; }
    private bool HasMated { get; set; }
    public int Speed { get; set; }
    public bool IsFed { get; set; }

    public void Eat()
    {
        IsFed = true;
        Habitat.GrassMinus();
    }
    public static void DeerPoplationMinus()
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
        this.CanMate = rnd.Next(0, 101) < 85? CanMate = true : CanMate = false;
        parent2.CanMate = rnd.Next(0, 101) < 85? CanMate = true : CanMate = false;

        if ((CanMate && parent2.CanMate) && (!parent2.HasMated && !HasMated))
        {
            if (IsFed && parent2.IsFed)
            {
                var averageSpeedFromParents = (Speed + parent2.Speed) / 2;
                var _rnd = new Random();
                var step = _rnd.Next(-5, 5);
                var kidSpeed = averageSpeedFromParents + step;
                var kid = new Deer(kidSpeed);
                deerPopulation++;
                HasMated = true;
                parent2.HasMated = true;
            }
        }
        else if (CanMate == false || parent2.CanMate == false)
        {
        }
    }

    public void CheckIsFed()
    {
        if (!IsFed)
        {
            DeerPoplationMinus();
        }
    }
}