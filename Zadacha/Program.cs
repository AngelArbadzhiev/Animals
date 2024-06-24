namespace Zadacha;

internal class Program
{
    private static void Main(string[] args)
    {
        var habitat = new Habitat(1500, true);
        habitat.GenerateAnimals();
        habitat.SeasonSimulator();
        var listDeerCount = habitat.ListDeer.Count;
        ;
    }
}