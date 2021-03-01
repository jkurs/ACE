using System.Collections.Generic;
using System.Linq;

using ACE.Entity.Enum;
using ACE.Server.Network.GameMessages.Messages;

namespace ACE.Server.WorldObjects
{
    partial class Creature
    {
        /// <summary>
        /// Called every ~5 seconds for Creatures
        /// </summary>
        public override void Heartbeat(double currentUnixTime)
        {
            var expireItems = new List<WorldObject>();

            // added where clause
            foreach (var wo in EquippedObjects.Values.Where(i => i.EnchantmentManager.HasEnchantments || i.Lifespan.HasValue))
            {
                // FIXME: wo.NextHeartbeatTime is double.MaxValue here
                //if (wo.NextHeartbeatTime <= currentUnixTime)
                    //wo.Heartbeat(currentUnixTime);

                // just go by parent heartbeats, only for enchantments?
                // TODO: handle players dropping / picking up items
                wo.EnchantmentManager.HeartBeat(CachedHeartbeatInterval);

                if (wo.IsLifespanSpent)
                    expireItems.Add(wo);
            }

            VitalHeartBeat();

            EmoteManager.HeartBeat();

            DamageHistory.TryPrune();

            // delete items when RemainingLifespan <= 0
            foreach (var expireItem in expireItems)
            {
                expireItem.DeleteObject(this);

                if (this is Player player)
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"Its lifespan finished, your {expireItem.Name} crumbles to dust.", ChatMessageType.Broadcast));
            }

            // summons level 15-100 will get a nice stat increase whenever their creature tick hits.
            if (this is CombatPet && Level < 200 && !CombatPetUpgraded)
            {

                switch (Level)
                {
                    case 15:
                        Strength.StartingValue += 30;
                        Endurance.StartingValue += 15;
                        Coordination.StartingValue += 30;
                        Quickness.StartingValue += 75;
                        Focus.StartingValue += 50;
                        Self.StartingValue += 50;
                        break;
                    case 30:
                        Strength.StartingValue += 50;
                        Endurance.StartingValue += 30;
                        Coordination.StartingValue += 50;
                        Quickness.StartingValue += 90;
                        Focus.StartingValue += 70;
                        Self.StartingValue += 70;
                        break;
                    case 50:
                        Strength.StartingValue += 75;
                        Endurance.StartingValue += 45;
                        Coordination.StartingValue += 75;
                        Quickness.StartingValue += 120;
                        Focus.StartingValue += 90;
                        Self.StartingValue += 90;
                        break;
                    case 80:
                        Strength.StartingValue += 100;
                        Endurance.StartingValue += 60;
                        Coordination.StartingValue += 100;
                        Quickness.StartingValue += 150;
                        Focus.StartingValue += 100;
                        Self.StartingValue += 100;
                        break;
                    case 100:
                        Strength.StartingValue += 120;
                        Endurance.StartingValue += 80;
                        Coordination.StartingValue += 120;
                        Quickness.StartingValue += 170;
                        Focus.StartingValue += 110;
                        Self.StartingValue += 110;
                        break;
                }

                Health.Current = Health.MaxValue;
                CombatPetUpgraded = true;
            }

            base.Heartbeat(currentUnixTime);
        }
    }
}
