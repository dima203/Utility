using System;


[Serializable]
public class Range<T>
{
    public T Min;
    public T Max;


    public Range()
    {
        Min = Math<T>.P.Zero; 
        Max = Math<T>.P.One;
    }


    public Range(T min, T max)
    {
        Min = min; 
        Max = max;
    }
}
