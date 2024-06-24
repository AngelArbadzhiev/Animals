namespace Zadacha
{
    public class Habitat
    {
        public Habitat(int maxGrassForPrey, bool isSpeedCapped)
        {
            if (maxGrassForPrey < 200)
            {
                throw new ArgumentOutOfRangeException(nameof(maxGrassForPrey), "maxGrassForPrey must be at least 200.");
            }

            this.MaxGrassForPrey = maxGrassForPrey;
            this.IsSpeedCapped = isSpeedCapped;
            GenerateGrass();
        }

        private Random _rnd = new Random();
        public static List<Deer> ListDeer = new List<Deer>();
        private List<Wolf> _listWolves = new List<Wolf>();
        public bool IsSpeedCapped { get; set; }
        public int PredatorPopulation { get; set; }
        public int PreyPopulation { get; set; }
        private static int Grass { get; set; }
        public int MaxSpeed { get; set; }
        private int MaxGrassForPrey { get; }

        private void GenerateGrass()
        {
            Grass = MaxGrassForPrey <= 200 ? 200 : _rnd.Next(200, MaxGrassForPrey);
        }

        public static void LowerGrass()
        {
            Random rnd = new Random();
            Grass = Grass - rnd.Next(1, 3);
        }

        public void GenerateAnimals()
        {
            for (int i = 0; i < 50; i++)
            {
                Deer deer = new Deer(_rnd.Next(101, 105));
                ListDeer.Add(deer);
            }

            for (int k = 0; k < 20; k++)
            {
                Wolf wolf = new Wolf(_rnd.Next(101, 105));
                _listWolves.Add(wolf);
            }
        }

        public void SeasonSimulator()
        {
            for (int i = 0; i < 3; i++)
            {
                AnimalDoingThings();
                for (int j = 0; j < 15; j += _rnd.Next(0, 2)) GenerateGrass();
            }
        }

        private void AnimalDoingThings()
        {
            if (Grass > 0 && Grass > ListDeer.Count)
            {
                foreach (var deer in ListDeer) deer.Eat();
            }

            for (int index = 0; index < ListDeer.Count; index++)
            {
                if (ListDeer.Count <= 1) continue;
                int mateIndex = 0;
                try
                {
                    do
                    {
                        mateIndex = _rnd.Next(0, ListDeer.Count);
                    }
                    while (mateIndex == index);

                    Console.WriteLine($"Deer[{index}] mating with Deer[{mateIndex}]"); // Debug statement
                    ListDeer[index].Mate(ListDeer[mateIndex]);
                    
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    mateIndex = _rnd.Next(0, ListDeer.Count);
                }
                ListDeer[index].HasMated = true;
                ListDeer[mateIndex].HasMated = true;
            }

            for (int index = 0; index < _listWolves.Count; index += _rnd.Next(1, 3)) // Ensure step is at least 1
            {
                if (ListDeer.Count <= 0) continue;
                int deerToChaseIndex = _rnd.Next(0, ListDeer.Count);
                Deer deerToChase = ListDeer[deerToChaseIndex];
                Console.WriteLine($"Wolf[{index}] chasing Deer[{deerToChaseIndex}]"); // Debug statement
                _listWolves[index].Chase(deerToChase);
                if (_listWolves[index].Speed <= deerToChase.Speed) continue;
                ListDeer.Remove(deerToChase);
                Console.WriteLine($"Deer[{deerToChaseIndex}] caught and removed"); // Debug statement
            }
        }

        public static void AddDeerKid(Deer kid)
        {
            ListDeer.Add(kid);
        }
        
        public static void ReturnPopulation()
        {
            Console.WriteLine($"Deer population: ");
            Console.WriteLine($"Wolf population: {Wolf.ReturnWolfPopulation()}");
        }
    }
}