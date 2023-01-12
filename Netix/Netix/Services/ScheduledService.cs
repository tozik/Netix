using System;
using System.Threading.Tasks;
using System.Timers;

namespace Netix.Services
{
    public class ScheduledService : IDisposable
    {
        private Func<Task> _func;
        private Timer _timer;
        private int _interval = 1000;

        public static ScheduledService Instance = new ScheduledService();
        public bool IsInitialised => _timer != null;

        public void Dispose()
        {
            _timer?.Dispose();
            _timer = null;
        }

        public void Pause()
        {
            _timer?.Stop();
        }

        public void Continue()
        {
            _timer?.Start();
        }

        public void ScheduleTimer(Func<Task> func)
        {
            _func = func;
            // this is System.Threading.Timer, of course
            _timer = new Timer
            {
                AutoReset = true,
                Interval = _interval
            };
            _timer.Elapsed += (o, e) => Tick();
            _timer.Start();
        }

        private async void Tick()
        {
            try
            {
                if (App.Current is App app && app.IsAppForeground)
                {
                    await _func();
                }
            }
            finally
            {
            }
        }
    }
}

