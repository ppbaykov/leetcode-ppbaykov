namespace hashset1;

public class MyHashSet
{
    static T[] Add<T>(T[] target, params T[] items)
    {
        // Validate the parameters
        if (target == null) {
            target = new T[] { };
        }
        if (items== null) {
            items = new T[] { };
        }

        // Join the arrays
        T[] result = new T[target.Length + items.Length];
        target.CopyTo(result, 0);
        items.CopyTo(result, target.Length);
        return result;
    }
    private int HashFunction(int x) => x % 1000;
    private int[][] bucket;
    
    public MyHashSet()
    {
        bucket = new int[1000][];
    }

    public void Add(int key)
    {
        var hashFunctionValue = HashFunction(key);
        var bucketCell = bucket[hashFunctionValue];
        if (bucketCell == null)
        {
            bucket[hashFunctionValue] = new[] { key };
            return;
        }
        
        if (bucketCell.Any(value => key == value))
            return; // do nothing cause its hashset

        var newBucketCell = Add(bucketCell, new[] { key });
        bucket[hashFunctionValue] = newBucketCell;
    }

    public void Remove(int key)
    {
        var hashFunctionValue = HashFunction(key);
        var bucketCell = bucket[hashFunctionValue];
        if (bucketCell == null)
            return;
        if (bucketCell.All(value => key != value))
            return; // do nothing

        var newBucketCell = bucketCell.Where(x => x != key).ToArray();
        bucket[hashFunctionValue] = newBucketCell;
    }

    public bool Contains(int key)
    {
        var hashFunctionValue = HashFunction(key);
        var bucketCell = bucket[hashFunctionValue];

        if (bucketCell == null)
            return false;
        return bucketCell.Any(value => key == value);
    }
}