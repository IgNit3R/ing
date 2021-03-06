﻿using System;

namespace Sakuno.ING.Game
{
    public sealed class CountDown : BindableObject
    {
        private DateTimeOffset? _completion;
        public DateTimeOffset? Completion
        {
            get => _completion;
            private set => Set(ref _completion, value);
        }

        private TimeSpan? _remaining;
        public TimeSpan? Remaining
        {
            get => _remaining;
            private set
            {
                Set(ref _remaining, value);
                RemainingDisplayString = value switch
                {
                    null => null,
                    TimeSpan t => $"{(int)t.TotalHours:00}:{t.Minutes:00}:{t.Seconds:00}"
                };
            }
        }

        private string _remainingDisplayString;
        public string RemainingDisplayString
        {
            get => _remainingDisplayString;
            private set => Set(ref _remainingDisplayString, value);
        }

        internal bool SetCompletionTime(DateTimeOffset? completion, DateTimeOffset timeStamp)
        {
            if (Completion != completion)
                using (EnterBatchNotifyScope())
                {
                    Completion = completion;
                    Update(timeStamp);
                    return true;
                }
            else
                return false;
        }

        internal void Clear()
        {
            using (EnterBatchNotifyScope())
            {
                Completion = null;
                Remaining = null;
            }
        }

        internal void Update(DateTimeOffset timeStamp)
        {
            if (Completion == null)
                Remaining = null;
            else if (timeStamp > Completion)
                Remaining = default(TimeSpan);
            else
                Remaining = Completion - timeStamp;
        }
    }
}
