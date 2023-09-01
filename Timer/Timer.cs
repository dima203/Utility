using System;


public class Timer
{
    public Action<float> TimeUpdated;

    protected float _seconds = 0f;
    protected Action _callback;
    protected float _currentSeconds = 0f;


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
        EventBus.Subscribe<TimeUpdatedSignal>(OnTick);
    }


    public void Tick(float deltaTime)
    { 
        if (_seconds == 0) 
            return;

        OnTick(new TimeUpdatedSignal(deltaTime));
    }


    public void Stop()
    {
        _currentSeconds = 0f;
        EventBus.Unsubscribe<TimeUpdatedSignal>(OnTick);
    }


    public void Reset()
    {
        _currentSeconds = 0f;
    }


    protected virtual void OnTick(TimeUpdatedSignal signal)
    {
        _currentSeconds += signal.DeltaTime; 
        TimeUpdated?.Invoke(_currentSeconds);

        if (_currentSeconds >= _seconds) {
            _currentSeconds -= _seconds;
            _callback?.Invoke();
        }
    }


    ~Timer()
    {
        Stop();
    }
}


public class ManualResetTimer: Timer
{
    public ManualResetTimer(float seconds, Action callback) : base(seconds, callback) { }


    protected override void OnTick(TimeUpdatedSignal signal)
    {
        _currentSeconds += signal.DeltaTime;

        if (_currentSeconds >= _seconds)
            _callback?.Invoke();
    }
}
