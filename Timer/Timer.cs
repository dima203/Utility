using System;


public class Timer
{
    public Action<float> TimeUpdated;

    protected float _seconds = 0f;
    protected Action _callback;
    protected float _currentSeconds = 0f;
    protected bool _isRunning = false;


    public Timer(float seconds, Action callback)
    {
        _seconds = seconds;
        _callback = callback;
    }


    public float GetTime() 
    {
        return _currentSeconds;
    }


    public void Start()
    {
        if (_seconds == 0)
            return;

        _currentSeconds = 0f;
        _isRunning = true;
        EventBus.Subscribe<TimeUpdatedSignal>(OnTick);
    }


    public void Tick(float deltaTime)
    { 
        if (_seconds == 0) 
            return;

        OnTick(deltaTime);
    }


    public void Stop()
    {
        _currentSeconds = 0f;
        _isRunning = false;
    }


    public void Reset()
    {
        _currentSeconds = 0f;
    }


    protected virtual void OnTick(TimeUpdatedSignal signal)
    {
        if (!_isRunning)
            return;

        OnTick(signal.DeltaTime);
    }


    protected virtual void OnTick(float deltaTime)
    {
        _currentSeconds += deltaTime; 
        TimeUpdated?.Invoke(_currentSeconds);

        if (_currentSeconds >= _seconds) {
            _currentSeconds -= _seconds;
            _callback?.Invoke();
        }
    }


    ~Timer()
    {
        EventBus.Unsubscribe<TimeUpdatedSignal>(OnTick);
    }
}


public class ManualResetTimer: Timer
{
    public ManualResetTimer(float seconds, Action callback) : base(seconds, callback) { }


    protected override void OnTick(TimeUpdatedSignal signal)
    {
        if (!_isRunning)
            return;

        _currentSeconds += signal.DeltaTime;

        if (_currentSeconds >= _seconds)
            _callback?.Invoke();
    }
}
