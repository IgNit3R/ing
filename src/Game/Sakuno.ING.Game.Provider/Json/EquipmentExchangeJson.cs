﻿using Sakuno.ING.Game.Events;
using Sakuno.ING.Game.Models;

namespace Sakuno.ING.Game.Json
{
    internal class EquipmentExchangeJson : IMaterialsUpdate
    {
        public RawShip api_ship_data;
        public int? api_bauxite;

        public MaterialsChangeReason Reason => MaterialsChangeReason.ShipEquip;

        public void Apply(ref Materials materials)
        {
            if (api_bauxite is int bauxite)
                materials.Bauxite = bauxite;
        }
    }
}
