//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Sakuno.ING.Game.Models.Quests
{
    public sealed partial class Quest : BindableObject, IComparable<Quest>, IUpdatable<QuestId, RawQuest>
    {

        [EditorBrowsable(EditorBrowsableState.Never)]
        private static readonly PropertyChangedEventArgs __eventArgs_category = new PropertyChangedEventArgs(nameof(Category));
        [EditorBrowsable(EditorBrowsableState.Never)]
        private QuestCategory _category;
        public QuestCategory Category
        {
            get => _category;
            private set => Set(ref _category, value, __eventArgs_category);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private static readonly PropertyChangedEventArgs __eventArgs_period = new PropertyChangedEventArgs(nameof(Period));
        [EditorBrowsable(EditorBrowsableState.Never)]
        private QuestPeriod _period;
        public QuestPeriod Period
        {
            get => _period;
            private set => Set(ref _period, value, __eventArgs_period);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private static readonly PropertyChangedEventArgs __eventArgs_state = new PropertyChangedEventArgs(nameof(State));
        [EditorBrowsable(EditorBrowsableState.Never)]
        private QuestState _state;
        public QuestState State
        {
            get => _state;
            private set => Set(ref _state, value, __eventArgs_state);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private static readonly PropertyChangedEventArgs __eventArgs_progress = new PropertyChangedEventArgs(nameof(Progress));
        [EditorBrowsable(EditorBrowsableState.Never)]
        private QuestProgress _progress;
        public QuestProgress Progress
        {
            get => _progress;
            private set => Set(ref _progress, value, __eventArgs_progress);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private static readonly PropertyChangedEventArgs __eventArgs_rewards = new PropertyChangedEventArgs(nameof(Rewards));
        [EditorBrowsable(EditorBrowsableState.Never)]
        private Materials _rewards;
        public Materials Rewards
        {
            get => _rewards;
            private set => Set(ref _rewards, value, __eventArgs_rewards);
        }

        public int CompareTo(Quest other) => Id.CompareTo(other?.Id ?? default);

        [EditorBrowsable(EditorBrowsableState.Never)]
        private static readonly PropertyChangedEventArgs __eventArgs_name = new PropertyChangedEventArgs(nameof(Name));
        [EditorBrowsable(EditorBrowsableState.Never)]
        private TextTranslationDescriptor _name;
        public TextTranslationDescriptor Name
        {
            get => _name;
            private set => Set(ref _name, value, __eventArgs_name);
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        private static readonly PropertyChangedEventArgs __eventArgs_description = new PropertyChangedEventArgs(nameof(Description));
        [EditorBrowsable(EditorBrowsableState.Never)]
        private TextTranslationDescriptor _description;
        public TextTranslationDescriptor Description
        {
            get => _description;
            private set => Set(ref _description, value, __eventArgs_description);
        }

        public QuestId Id { get; }
        private readonly QuestManager owner;
        public DateTimeOffset UpdationTime { get; private set; }

        public Quest(QuestId id, QuestManager owner)
        {
            Id = id;
            this.owner = owner;
            CreateCore();
        }

        public Quest(RawQuest raw, QuestManager owner, DateTimeOffset timeStamp) : this(raw.Id, owner) => UpdateProps(raw, timeStamp);

        public event Action<Quest, RawQuest, DateTimeOffset> Updating;
        public void Update(RawQuest raw, DateTimeOffset timeStamp)
        {
            Updating?.Invoke(this, raw, timeStamp);
            using (EnterBatchNotifyScope())
                UpdateProps(raw, timeStamp);
        }

        private void UpdateProps(RawQuest raw, DateTimeOffset timeStamp)
        {
            UpdationTime = timeStamp;

            if (raw.Name != Name?.Origin)
                Name = new TextTranslationDescriptor(Id, "QuestName", raw.Name, true);

            if (raw.Description != Description?.Origin)
                Description = new TextTranslationDescriptor(Id, "QuestDesc", raw.Description, true);

            Category = raw.Category;
            Period = raw.Period;
            State = raw.State;
            Progress = raw.Progress;
            Rewards = raw.Rewards;

            UpdateCore(raw, timeStamp);
        }

        partial void UpdateCore(RawQuest raw, DateTimeOffset timeStamp);

        partial void CreateCore();

        public override string ToString() => $"Quest {Id}: {Name.Origin}";
    }

}