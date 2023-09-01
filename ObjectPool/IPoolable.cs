using System;

public interface IPoolable
{
    void Get();
    void Release();
}
