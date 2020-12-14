using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Time = SFML.System.Time;

namespace Saffron2D.Core
{

    public static class Run
    {
        private class IntervalAction
        {
            public IntervalAction(CancellationToken cancellationToken, Action action, Time interval)
            {
                CancellationToken = cancellationToken;
                Action = action;
                Interval = interval;
                Counter = new Time();
            }

            public CancellationToken CancellationToken { get; }

            public Action Action { get; }

            public Time Interval { get; }

            public Time Counter { get; set; }
        }

        private class LaterAction
        {
            public LaterAction(CancellationToken cancellationToken, Action action)
            {
                CancellationToken = cancellationToken;
                Action = action;
            }

            public CancellationToken CancellationToken { get; }

            public Action Action { get; }
        }

        private static readonly List<IntervalAction> intervalActions = new List<IntervalAction>();
        private static readonly List<LaterAction> laterActions = new List<LaterAction>();

        public static void OnUpdate(Time dt)
        {
            intervalActions.RemoveAll(synchronizedAction => synchronizedAction.CancellationToken.IsCancellationRequested);
            laterActions.RemoveAll(laterAction => laterAction.CancellationToken.IsCancellationRequested);

            foreach (var intervalAction in intervalActions)
            {
                intervalAction.Counter += dt;
            }
        }

        public static void Execute()
        {
            foreach (var intervalAction in intervalActions.Where(intervalAction => intervalAction.Counter > intervalAction.Interval))
            {
                intervalAction.Action();
                intervalAction.Counter = Time.Zero;
            }

            foreach (var laterAction in laterActions)
            {
                laterAction.Action();
            }
            laterActions.Clear();
        }

        public static CancellationTokenSource Later(Action action)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var laterAction = new LaterAction(cancellationTokenSource.Token, action);
            laterActions.Add(laterAction);
            return cancellationTokenSource;
        }

        public static CancellationTokenSource Interval(Action action, Time interval)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var intervalAction = new IntervalAction(cancellationTokenSource.Token, action, interval);
            intervalActions.Add(intervalAction);
            return cancellationTokenSource;
        }

        public static CancellationToken IntervalAsync(Action action, Time interval)
        {
            var cancellationToken = new CancellationToken();
            Task.Run(async () =>
            {
                while (true)
                {
                    action();
                    await Task.Delay(interval.AsMilliseconds(), cancellationToken);
                    if (cancellationToken.IsCancellationRequested)
                        break;
                }
            }, cancellationToken);
            return cancellationToken;
        }
    }
}