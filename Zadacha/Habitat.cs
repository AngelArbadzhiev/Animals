namespace Zadacha;

public class Habitat
{
    public Habitat(int maxGrassForPrey, bool isSpeedCapped)
    {
        this.MaxGrassForPrey = maxGrassForPrey;
        this.IsSpeedCapped = isSpeedCapped;
        GenerateGrass();
    }
    private Random rnd = new Random();
    List<Deer> listDeer = [];
    List<Wolf> listWolves= [];
    public bool IsSpeedCapped { get; set; }
    public int PredatorPopulation { get; set; }
    public int PreyPopulation { get; set; }
    private static int Grass { get; set; }
    public int MaxSpeed { get; set; }
    private int MaxGrassForPrey { get; }
    
    private void GenerateGrass()
    {
        int grass = rnd.Next(200, MaxGrassForPrey);
        Grass = grass;
    }

    public static void LowerGrass()
    {
        Random rnd = new Random();
        Grass= Grass - rnd.Next(1,3);
    }

    public void GenerateAnimals()
    {
        for (int i = 0; i < 50; i++)
        {
            Deer deer = new Deer(rnd.Next(100, 105));
            listDeer.Add(deer); 
        }
        
        for (int k = 0; k < 20; k++)
        {
            Wolf wolf = new Wolf(rnd.Next(100, 105));
            listWolves.Add(wolf);
        }
    }

    public void SeasonSimulator() //TODO: Implement a dynamic way for seasons
    {
        for (int i = 0; i < 15; i++)
        {
            AnimalDoingThings();
            for (int j = 0; j < 15; j+=rnd.Next(0,2)) GenerateGrass();
            
        }
    }

    private void AnimalDoingThings()     //TODO: Make the wolves chase the deer and mate and do shit
    {
        if (Grass > 0 && Grass > listDeer.Count)
        {
            foreach (var deer in listDeer) deer.Eat();
        }
        for (int p = 0; p < listWolves.Count; p+=1) 
        {
            listWolves[p].Chase(listDeer[rnd.Next(0, listDeer.Count)]);
        }
        
    }

    public static void ReturnPopulation()
    {
        Console.WriteLine($"Deer population {Deer.ReturnDeerPopulation()}");
        Console.WriteLine($"Wolf population {Wolf.ReturnWolfPopulation()}");
    }
    
    
}