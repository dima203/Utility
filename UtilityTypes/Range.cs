using System;


[Serializable]
public class Range<T>
{
    public T Min;
    public T Max;


    public Range(T min, T max)
    {
        Min = min; 
        Max = max;
    }
}
