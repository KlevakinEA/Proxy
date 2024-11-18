Proxy p = new Proxy();
Console.WriteLine(p.Request(1, 1));
Console.WriteLine(p.Request(12345678, 1));
Console.WriteLine(p.Request(12345678, 1));
Console.WriteLine(p.Request(12345678, 2));
Console.WriteLine(p.Request(12345678, 3));
Console.WriteLine(p.Request(12345678, 1));


interface ISubject
{
    int Request(int password, int seed);
}


class Real: ISubject
{
    public int Request(int password, int seed) => new Random(seed).Next();
}


class Proxy : ISubject
{
    Real real = new Real();
    List<int> seeds = new List<int>();
    Dictionary<int, int> hashes = new Dictionary<int, int>();
    public int Request(int password, int seed)
    {
        if (password == 12345678)
        {
            if (hashes.ContainsKey(seed)) return hashes[seed];
            else
            {
                int i = real.Request(0, seed);
                hashes[seed] = i;
                seeds.Add(i);
                if (hashes.Count > 2)
                {
                    hashes.Remove(seeds[0]);
                    seeds.RemoveAt(0);
                }
                return hashes[seed];
            }
        }
        else return 0;
    }
}