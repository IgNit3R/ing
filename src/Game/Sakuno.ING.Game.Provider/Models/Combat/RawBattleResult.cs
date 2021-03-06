﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Sakuno.ING.Game.Models.MasterData;

namespace Sakuno.ING.Game.Models.Combat
{
    public sealed class RawBattleResult
    {
        [JsonProperty("api_win_rank")]
        public BattleRank Rank { get; internal set; }
        [JsonProperty("api_get_exp")]
        public int AdmiralExperience { get; internal set; }
        [JsonProperty("api_get_base_exp")]
        public int BaseExperience { get; internal set; }
        [JsonProperty("api_first_clear")]
        public bool MapCleared { get; internal set; }

        internal class EnemyInfo
        {
            public string api_deck_name;
        }
        internal EnemyInfo api_enemy_info;
        public string EnemyFleetName => api_enemy_info?.api_deck_name;

        [JsonProperty("api_mapcell_incentive")]
        public bool? AirReconnaissanceSuccessed { get; internal set; }

        internal class GetUseItem
        {
            public UseItemId api_useitem_id;
        }
        internal GetUseItem api_get_useitem;
        public UseItemId? UseItemAcquired => api_get_useitem?.api_useitem_id;

        internal class GetShip
        {
            public ShipInfoId api_ship_id;
        }
        internal GetShip api_get_ship;
        public ShipInfoId? ShipDropped => api_get_ship?.api_ship_id;

        [JsonProperty("api_get_exmap_rate")]
        public int? RankingPointAcquired { get; internal set; }

        internal bool api_m1;

        internal class LandingHP
        {
            public int api_sub_value;
        }
        internal LandingHP api_landing_hp;
        public int? TransportationPoint => api_landing_hp?.api_sub_value;

        internal class Escape
        {
            public int? api_escape_idx;
            public int? api_tow_idx;
        }
        internal Escape api_escape;
        public IReadOnlyCollection<int> EscapableShipIndices
        {
            get
            {
                if (api_escape == null)
                    return Array.Empty<int>();

                var l = new List<int>(2);
                if (api_escape.api_escape_idx is int escape)
                    l.Add(escape - 1);
                if (api_escape.api_tow_idx is int tow)
                    l.Add(tow - 1);
                return l;
            }
        }

        internal class GetEventItem : IRawReward
        {
            public int api_type;
            public int api_id;
            public int api_value;
            public int api_slot_level;

            public bool TryGetUseItem(out UseItemRecord useItem)
            {
                if (api_type == 1)
                {
                    useItem = new UseItemRecord
                    {
                        ItemId = (UseItemId)api_id,
                        Count = api_value
                    };
                    return true;
                }
                else
                {
                    useItem = default;
                    return false;
                }
            }
            public bool TryGetShip(out ShipInfoId ship)
            {
                if (api_type == 2)
                {
                    ship = (ShipInfoId)api_id;
                    return true;
                }
                else
                {
                    ship = default;
                    return false;
                }
            }
            public bool TryGetEquipment(out EquipmentRecord equipment)
            {
                if (api_type == 3)
                {
                    equipment = new EquipmentRecord
                    {
                        Id = (EquipmentInfoId)api_id,
                        ImprovementLevel = api_slot_level,
                        Count = api_value
                    };
                    return true;
                }
                else
                {
                    equipment = default;
                    return false;
                }
            }
            public bool TryGetFurniture(out FurnitureId furniture)
            {
                if (api_type == 5)
                {
                    furniture = (FurnitureId)api_id;
                    return true;
                }
                else
                {
                    furniture = default;
                    return false;
                }
            }
        }
        internal GetEventItem[] api_get_eventitem;
        public IReadOnlyCollection<IRawReward> Rewards => api_get_eventitem ?? Array.Empty<GetEventItem>();
    }
}
