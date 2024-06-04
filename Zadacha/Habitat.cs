namespace Zadacha;

public class Habitat
{
    private int _predatorPopulation;
    private int _preyPopulation;
    private int _maxSpeed;
    private readonly int _maxGrassForPrey;
    public static int _grass;

    public Habitat(int maxGrassForPrey)
    {
        _maxGrassForPrey = maxGrassForPrey;
        GenerateGrass();
    }

    public int PredatorPopulation
    {
        get => _predatorPopulation;
        set => _predatorPopulation = value;
    }

    public int PreyPopulation
    {
        get => _preyPopulation;
        set => _preyPopulation = value;
    }

    private static int Grass
    {
        get => _grass;
        set => _grass = value;
    }

    public int MaxSpeed
    {
        get => _maxSpeed;
        set => _maxSpeed = value;
    }

    private int MaxGrassForPrey => _maxGrassForPrey;

    public void GenerateGrass()
    {
        var rnd = new Random();
        var grass = rnd.Next(200, MaxGrassForPrey);
        Grass = grass;
    }

    public static void GrassMinus()
    {
        _grass--;
    }

    public void GenerateAnimals()
    {
        Random rnd = new Random();
        List<Deer> listdeer = new List<Deer>();
        List<Wolf> listwolf= new List<Wolf>();

        for (int i = 0; i < 50; i++)
        {
            Deer deer = new Deer(rnd.Next(100, 105));
            listdeer.Add(deer); 
        }
        for (int j = 0; j < listdeer.Count; j++)
        {
            listdeer[j].Eat();
        }
        
        for (int k = 0; k < 20; k++)
        {
            Wolf wolf = new Wolf(rnd.Next(100, 105));
            listwolf.Add(wolf);
        }

        for (int p = 0; p < listwolf.Count; p+=1)
        {
            listwolf[p].Chase(listdeer[rnd.Next(0, listdeer.Count)]);
        }
    }

    public void ReturnPopulation()
    {
        Console.WriteLine($"Deer population {Deer.ReturnDeerPopulation()}");
        Console.WriteLine($"Wolf population {Wolf.ReturnWolfPopulation()}");
    }
    
    
}