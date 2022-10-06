class PriorityQueue
{
    public List<int> list;
    public int Count { get { return list.Count; } }

    public PriorityQueue()
    {
        list = new List<int>();
    }

    public PriorityQueue(int count)
    {
        list = new List<int>(count);
    }


    public void Enqueue(int x)
    {
        list.Add(x);
        int i = Count - 1;

        while (i > 0)
        {
            int p = (i - 1) / 2;
            if (list[p] <= x) break;

            list[i] = list[p];
            i = p;
        }

        if (Count > 0) list[i] = x;
    }

    public int Dequeue()
    {
        int min = Peek();
        int root = list[Count - 1];
        list.RemoveAt(Count - 1);

        int i = 0;
        while (i * 2 + 1 < Count)
        {
            int a = i * 2 + 1;
            int b = i * 2 + 2;
            int c = b < Count && list[b] < list[a] ? b : a;

            if (list[c] >= root) break;
            list[i] = list[c];
            i = c;
        }

        if (Count > 0) list[i] = root;
        return min;
    }

    public int Peek()
    {
        if (Count == 0) throw new InvalidOperationException("Queue is empty.");
        return list[0];
    }

    public void Clear()
    {
        list.Clear();
    }
}
