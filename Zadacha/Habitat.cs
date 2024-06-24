using System.Diagnostics.CodeAnalysis;

namespace Zadacha;

public class Habitat
{
    private readonly List<Wolf> _listWolves = [];

    private readonly Random _rnd = new Random();
    public List<Deer> ListDeer = [];

    public Habitat(int maxGrassForPrey, bool isSpeedCapped)
    {
        MaxGrassForPrey = maxGrassForPrey;
        IsSpeedCapped = isSpeedCapped;
        GenerateGrass();
        Deer.OnDeerBorn += AddDeerKid;
        Wolf.OnWolfBorn += AddWolfKid;
    }

    private static int CurrentSeason { get; set; }

    public bool IsSpeedCapped { get; set; }
    private static int Grass { get; set; }
    public int MaxSpeed { get; set; }
    private int MaxGrassForPrey { get; }

    private void GenerateGrass()
    {
        Grass = MaxGrassForPrey <= 200 ? 200 : _rnd.Next(200, MaxGrassForPrey);
    }

    public static void LowerGrass()
    {
        var rnd = new Random();
        Grass = Grass - rnd.Next(1, 3);
    }

    public void GenerateAnimals()
    {
        for (var i = 0; i < 50; i++)
        {
            var deer = new Deer(_rnd.Next(102, 105));
            ListDeer.Add(deer);
        }

        for (var k = 0; k < 20; k++)
        {
            var wolf = new Wolf(_rnd.Next(102, 105));
            _listWolves.Add(wolf);
        }
    }

    public void SeasonSimulator()
    {
        for (var i = CurrentSeason; i < 1000; i++)
        {
            AnimalDoingThings();
            for (var j = 0; j < CurrentSeason; j += _rnd.Next(0, 2)) GenerateGrass();
            CurrentSeason++;
            if (CurrentSeason % 100 == 0) WritePopulation();
        }

        if (CurrentSeason % 2 != 0) return;
    }

    private void WritePopulation()
    {
        Console.WriteLine($"Population at season {CurrentSeason}:");
        Console.WriteLine($"Deer population: {ListDeer.Count}");
        Console.WriteLine($"Wolf population: {_listWolves.Count}");
        Console.WriteLine();
    }
    
    private void AnimalDoingThings()
    {
        if (Grass > 0 && Grass > ListDeer.Count)
            foreach (var deer in ListDeer)
                deer.Eat();

        for (var index = 0; index < ListDeer.Count; index++)
        {
            if (ListDeer.Count <= 1) continue;
            var mateIndex = 0;
            try
            {
                do
                {
                    mateIndex = _rnd.Next(0, ListDeer.Count);
                } while (mateIndex == index);
                
                ListDeer[index].Mate(ListDeer[mateIndex]);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                mateIndex = _rnd.Next(0, ListDeer.Count);
            }

            ListDeer[index].HasMated = true;
            ListDeer[mateIndex].HasMated = true;
        }

        for (var index = 0; index < _listWolves.Count; index += _rnd.Next(1, 3))
        {
            if (ListDeer.Count <= 0) continue;
            var deerToChaseIndex = _rnd.Next(0, ListDeer.Count);
            var deerToChase = ListDeer[deerToChaseIndex];
            _listWolves[index].Chase(deerToChase);
            if (_listWolves[index].Speed <= deerToChase.Speed) continue;
            ListDeer.Remove(deerToChase);
        }

        for (var i = 0; i < _listWolves.Count; i++)
        {
            if (_listWolves.Count <= 1) continue;
            var mateIndex = 0;
            try
            {
                do
                {
                    mateIndex = _rnd.Next(0, _listWolves.Count);
                } while (mateIndex == i);
                
                _listWolves[i].Mate(_listWolves[mateIndex]);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                mateIndex = _rnd.Next(0, _listWolves.Count);
            }

            _listWolves[i].HasMated = true;
            _listWolves[mateIndex].HasMated = true;
        }
    }

    private void AddDeerKid(Deer kid)
    {
        ListDeer.Add(kid);
    }

    private void AddWolfKid(Wolf kid)
    {
        _listWolves.Add(kid);
    }
}