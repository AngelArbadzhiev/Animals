namespace Zadacha;

internal class Program
{
    private static void Main(string[] args)
    {
        Habitat habitat = new Habitat(1500,true);
        habitat.GenerateAnimals();
        Habitat.ReturnPopulation();
    }
}