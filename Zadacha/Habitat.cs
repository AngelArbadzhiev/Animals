using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Zadacha;

public class Habitat
{
    private readonly List<Wolf> _listWolves = [];

    private readonly Random _rnd = new Random();
    public List<Deer> ListDeer = [];

    public Habitat(int maxGrassForPrey,int seasons, bool isSpeedCapped)
    {
        this.MaxGrassForPrey = maxGrassForPrey;
        this.IsSpeedCapped = isSpeedCapped;
        this.Seasons = seasons;
        GenerateGrass();
        Deer.OnDeerBorn += AddDeerKid;
        Wolf.OnWolfBorn += AddWolfKid;

    }

    private static int CurrentSeason { get; set; }

    public bool IsSpeedCapped { get; set; }
    private static int Grass { get; set; }
    private int MaxGrassForPrey { get; }
    private int Seasons {get;set;}

    private void GenerateGrass()
    {
        Grass = MaxGrassForPrey <= 200 ? 200 : _rnd.Next(200, MaxGrassForPrey);
    }

    public static void LowerGrass()
    {
        Random rnd = new Random();
        Grass -= rnd.Next(1, 3);
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
            var wolf = new Wolf(_rnd.Next(101, 105));
            _listWolves.Add(wolf);
        }
    }

    public void SeasonSimulator()
    {
        int pastSeason = CurrentSeason - 1;
        int nextSeason = CurrentSeason + 1;
        for (var i = CurrentSeason; i < Seasons; i++)
        {
            AnimalDoingThings();
            for (var j = 0; j < CurrentSeason; j += _rnd.Next(0, 2)) GenerateGrass();
            CurrentSeason++;
            if (CurrentSeason % (Seasons / 10) == 0) WritePopulation();
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
            else if(!ListDeer[index].IsFed && CurrentSeason != 0 && CurrentSeason % 2 == 0){
                    ListDeer.Remove(ListDeer[index]);
                }
            var mateIndex = 0;
            try
            {
                do
                {
                    mateIndex = _rnd.Next(0, ListDeer.Count);
                } while (mateIndex == index);
                
                ListDeer[index].Mate(ListDeer[mateIndex]);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                mateIndex = _rnd.Next(0, ListDeer.Count);
            }

            ListDeer[index].HasMated = true;
            ListDeer[mateIndex].HasMated = true;
        }
        for (int i = 0; i < ListDeer.Count; i++){
            if(!ListDeer[i].IsFed && CurrentSeason != 0 && CurrentSeason % 2 == 0){
                    ListDeer.Remove(ListDeer[i]);
                }
            }

        for (var index = 0; index < _listWolves.Count; index += _rnd.Next(1, 3))
        {
            if (ListDeer.Count <= 0) continue;
            else if(!_listWolves[index].IsFed && CurrentSeason != 0 && CurrentSeason % 2 == 0){
                    _listWolves.Remove(_listWolves[index]);
                }
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