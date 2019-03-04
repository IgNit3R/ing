﻿using System.Collections.Specialized;
using Newtonsoft.Json.Linq;
using Sakuno.ING.Game.Events.Combat;
using Sakuno.ING.Game.Models;
using Sakuno.ING.Game.Models.Combat;
using Sakuno.ING.Game.Models.MasterData;
using Sakuno.ING.Http;
using Sakuno.ING.Messaging;

namespace Sakuno.ING.Game
{
    public partial class GameProvider
    {
        public event TimedMessageHandler<EnemyDebuffConfirm> EnemyDebuffConfirmed;
        public event TimedMessageHandler<MapPartUnlock> MapPartUnlocked;
        public event TimedMessageHandler<SortieStart> SortieStarting;
        public event TimedMessageHandler<RawMapRouting> MapRouting;
        public event TimedMessageHandler<FleetId> PracticeStarted;
        public event TimedMessageHandler<BattleDetail> BattleStarted;
        public event TimedMessageHandler<BattleDetail> BattleAppended;
        public event TimedMessageHandler<RawBattleResult> BattleCompleted;

        private static SortieStart ParseSortieStart(NameValueCollection req)
            => new SortieStart
            (
                (FleetId)req.GetInt("api_deck_id"),
                (MapId)(req.GetInt("api_maparea_id") * 10 + req.GetInt("api_mapinfo_no"))
            );

        private static FleetId ParsePracticeStart(NameValueCollection req)
            => (FleetId)req.GetInt("api_deck_id");

        private BattleDetail ParseBattleDetail(HttpMessage message)
        {
            var detail = Response<JToken>(message);
            return new BattleDetail(detail.ToObject<RawBattle>(jSerializer), detail);
        }
    }
}
