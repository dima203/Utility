using System;
using UnityEngine;


public interface IMath<T>
{
    T Zero { get; }
    T One { get; }


    T Add(T value1, T value2);
    T Multiple(T value1, float value2);
    bool Equal(T value1, T value2);
    bool Less(T value1, T value2);
    bool Greater(T value1, T value2);
    T Clamp(T value1, T min, T max);
}


public class Math<T> : IMath<T>
{
    public static readonly IMath<T> P = Math.P as IMath<T> ?? new Math<T>();

    T IMath<T>.Zero { get; }
    T IMath<T>.One { get; }

    T IMath<T>.Add(T value1, T value2) { throw new NotImplementedException(); }
    T IMath<T>.Multiple(T value1, float value2) { throw new NotImplementedException(); }
    bool IMath<T>.Equal(T value1, T value2) { throw new NotImplementedException(); }
    bool IMath<T>.Less(T value1, T value2) { throw new NotImplementedException(); }
    bool IMath<T>.Greater(T value1, T value2) { throw new NotImplementedException(); }
    T IMath<T>.Clamp(T value1, T min, T max) { throw new NotImplementedException(); }
}


class Math : IMath<int>, IMath<float>
{
    public static Math P = new Math();

    int IMath<int>.Zero { get; } = 0;
    float IMath<float>.Zero { get; } = 0f;
    int IMath<int>.One { get; } = 1;
    float IMath<float>.One { get; } = 1f;


    int IMath<int>.Add(int value1, int value2) => value1 + value2;
    float IMath<float>.Add(float value1, float value2) => value1 + value2;
    int IMath<int>.Multiple(int value1, float value2) => (int)(value1 * value2);
    float IMath<float>.Multiple(float value1, float value2) => (float)(value1 * value2);
    bool IMath<int>.Equal(int value1, int value2) => value1 == value2;
    bool IMath<float>.Equal(float value1, float value2) => value1 == value2;
    bool IMath<int>.Less(int value1, int value2) => value1 < value2;
    bool IMath<float>.Less(float value1, float value2) => value1 < value2;
    bool IMath<int>.Greater(int value1, int value2) => value1 > value2;
    bool IMath<float>.Greater(float value1, float value2) => value1 > value2;
    int IMath<int>.Clamp(int value1, int min, int max) => Mathf.Clamp(value1, min, max);
    float IMath<float>.Clamp(float value1, float min, float max) => Mathf.Clamp(value1, min, max);
}