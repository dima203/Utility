using System;
using UnityEngine;


[Serializable]
public class Counter<T>
{
    public T Value { get; private set; }
    public T MaxValue;


    public Counter(T maxValue)
    {
        Value = Math<T>.P.Zero;
        MaxValue = maxValue;
    }


    public bool Add(T value)
    {
        if (Math<T>.P.Less(value, Math<T>.P.Zero))
            return false;

        Value = Math<T>.P.Add(Value, value);
        return CheckEndCount();
    }


    public void SetPercent(float percent)
    {
        if (percent < 0f || percent > 1f)
            return;

        Value = Math<T>.P.Multiple(Value, percent);
    }


    public bool CheckEndCount()
    {
        if (Math<T>.P.Less(Value, MaxValue)) 
            return false;

        Value = Math<T>.P.Zero;
        return true;
    }


    public void Reset()
    {
        Value = Math<T>.P.Zero;
    }
}


[Serializable]
public class BorderedValue<T>
{
    public T MinValue = Math<T>.P.Zero;
    public T MaxValue = Math<T>.P.One;
    public T Value { get => _value; private set => _value = value; }

    [SerializeField] private T _value = Math<T>.P.Zero;


    public BorderedValue(T value, T minValue, T maxValue)
    { 
        Value = value;
        MinValue = minValue;
        MaxValue = maxValue; 
        CheckOutBorder();
    }


    public void Add(T value)
    {
        Value = Math<T>.P.Add(Value, value);
        CheckOutBorder();
    }


    public void SetMin()
    {
        Value = MinValue;
    }


    public void SetMax()
    {
        Value = MaxValue;
    }


    public void SetPercent(float percent)
    {
        if (percent < 0f || percent > 1f)
            return;

        Value = Math<T>.P.Multiple(Value, percent);
    }


    public void SetValue(T value)
    {
        if (Math<T>.P.Less(value, MinValue) || Math<T>.P.Greater(value, MaxValue))
            return;
        
        Value = value;
    }


    public bool CheckOutBorder()
    {
        if ((Math<T>.P.Greater(Value, MinValue) || Math<T>.P.Equal(Value, MinValue)) && Math<T>.P.Less(Value, MaxValue))
            return false;

        Value = Math<T>.P.Clamp(Value, MinValue, MaxValue);
        return true;
    }
}
