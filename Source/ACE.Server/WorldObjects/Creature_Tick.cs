using System;
using System.Collections.Generic;
using System.Linq;
using ACE.Common;
using ACE.Entity.Enum;
using ACE.Server.Managers;
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

            if (this is CombatPet && Level >= 200 && !CombatPetUpgraded)
            {
                if (PetOwner != null)
                {
                    var player = PlayerManager.GetOnlinePlayer(PetOwner.Value);

                    if (player != null)
                    {
                        var end = player.Endurance.Base;
                        var self = player.Self.Base;

                        var total = end + self;

                        var byAmount = total * 0.007f;

                        var roundedAmount = Math.Round(byAmount);

                        var finalBonus = (uint)roundedAmount * (uint)(3 * roundedAmount);


                        Strength.StartingValue += finalBonus;
                        Endurance.StartingValue += finalBonus;
                        Coordination.StartingValue += finalBonus;
                        Quickness.StartingValue += finalBonus;
                        Focus.StartingValue += finalBonus;
                        Self.StartingValue += finalBonus;

                        Health.Current = Health.MaxValue;
                        Stamina.Current = Stamina.MaxValue;
                        CombatPetUpgraded = true;
                    }
                }
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

            if (Name.StartsWith("Invading") && CreatureType == ACE.Entity.Enum.CreatureType.GearKnight && !UpgradedUber1)
            {
                Strength.StartingValue += 500;
                Endurance.StartingValue += 500;
                Quickness.StartingValue += 600;
                Coordination.StartingValue += 600;
                Focus.StartingValue += 700;
                Self.StartingValue += 700;

                Health.StartingValue += 25000;

                Health.Current = Health.MaxValue;

                SetProperty(ACE.Entity.Enum.Properties.PropertyDataId.DeathTreasureType, 2000);

                var overpowerRng = ThreadSafeRandom.Next(0, 5);

                if (overpowerRng > 0)
                    SetProperty(ACE.Entity.Enum.Properties.PropertyInt.Overpower, overpowerRng);

                SetProperty(ACE.Entity.Enum.Properties.PropertyInt.LuminanceAward, 200);

                SetProperty(ACE.Entity.Enum.Properties.PropertyBool.UpgradedUber1, true);
            }
            else
            {

                //all monsters that reward luminance.
                if (LuminanceAward != null && !UpgradedUber1)
                {
                    Strength.StartingValue += 800;
                    Endurance.StartingValue += 300;
                    Quickness.StartingValue += 700;
                    Coordination.StartingValue += 800;
                    Focus.StartingValue += 600;
                    Self.StartingValue += 600;

                    Health.StartingValue += 25000;

                    Health.Current = Health.MaxValue;

                    var war = GetCreatureSkill(Skill.WarMagic, false);
                    var life = GetCreatureSkill(Skill.LifeMagic, false);
                    var resist = GetCreatureSkill(Skill.MagicDefense, false);
                    var missiled = GetCreatureSkill(Skill.MissileDefense, false);
                    var meleed = GetCreatureSkill(Skill.MeleeDefense, false);

                    if (war != null)
                        war.InitLevel += 200;
                    if (life != null)
                        life.InitLevel += 200;
                    if (resist != null)
                        resist.InitLevel += 100;
                    if (missiled != null)
                        missiled.InitLevel += 400;
                    if (meleed != null)
                        meleed.InitLevel += 100;

                    var overpowerRng = ThreadSafeRandom.Next(0, 5);

                    if (overpowerRng > 0)
                        SetProperty(ACE.Entity.Enum.Properties.PropertyInt.Overpower, overpowerRng);

                    SetProperty(ACE.Entity.Enum.Properties.PropertyInt.LuminanceAward, (int)LuminanceAward + 2500);

                    SetProperty(ACE.Entity.Enum.Properties.PropertyBool.UpgradedUber1, true);
                }
                else
                {
                    // DI bosses
                    if ((/* golem */ WeenieClassId == 40147 || WeenieClassId == 40149 || /* mukkir */ WeenieClassId == 33623 || WeenieClassId == 33626 || /* sleeches */ WeenieClassId == 33636 || WeenieClassId == 70331 || /* remoran */ WeenieClassId == 33629
                        || WeenieClassId == 70082 || /* shadow */ WeenieClassId == 33631 || WeenieClassId == 33633 || /* falatacot */ WeenieClassId == 38594 || /* ruschk */ WeenieClassId == 33639 || WeenieClassId == 33641) && !UpgradedUber1)
                    {
                        Strength.StartingValue += 1700;
                        Endurance.StartingValue += 800;
                        Quickness.StartingValue += 1500;
                        Coordination.StartingValue += 1500;

                        if (WeenieClassId == 33636 || WeenieClassId == 70331)
                        {
                            Focus.StartingValue += 800;
                            Self.StartingValue += 800;

                        }
                        else
                        {
                            Focus.StartingValue += 1100;
                            Self.StartingValue += 1100;
                        }

                        var war = GetCreatureSkill(Skill.WarMagic, false);
                        var life = GetCreatureSkill(Skill.LifeMagic, false);
                        var resist = GetCreatureSkill(Skill.MagicDefense, false);
                        var missiled = GetCreatureSkill(Skill.MissileDefense, false);
                        var meleed = GetCreatureSkill(Skill.MeleeDefense, false);

                        if (war != null)
                            war.InitLevel += 200;
                        if (life != null)
                            life.InitLevel += 400;
                        if (resist != null)
                            resist.InitLevel += 400;
                        if (missiled != null)
                            missiled.InitLevel += 500;
                        if (meleed != null)
                            meleed.InitLevel += 200;

                        Health.StartingValue += 20000;

                        Health.Current = Health.MaxValue;

                        SetProperty(ACE.Entity.Enum.Properties.PropertyDataId.DeathTreasureType, 2000);

                        Level = 450;

                        XpOverride += 160000000;

                        SetProperty(ACE.Entity.Enum.Properties.PropertyInt.Overpower, 25);
                        SetProperty(ACE.Entity.Enum.Properties.PropertyInt.LuminanceAward, 2500);

                        SetProperty(ACE.Entity.Enum.Properties.PropertyBool.UpgradedUber1, true);
                    }

                    // DI normal mobs
                    if (( /* golems */ WeenieClassId == 40149 || WeenieClassId == 40289 || WeenieClassId == 40153 || WeenieClassId == 40290 || /* mukkir */ WeenieClassId == 33732 || WeenieClassId == 33733 || WeenieClassId == 40281 || WeenieClassId == 40282
                        || /* sleeches */ WeenieClassId == 33739 || WeenieClassId == 40286 || WeenieClassId == 33738 || WeenieClassId == 40285 || /* remoran */ WeenieClassId == 33736 || WeenieClassId == 40283 || WeenieClassId == 33737 || WeenieClassId == 40284
                        || /* shadow */ WeenieClassId == 33730 || WeenieClassId == 40292 || WeenieClassId == 33731 || WeenieClassId == 40295 || /* undead */ WeenieClassId == 34973 || WeenieClassId == 38594 || WeenieClassId == 33642 || WeenieClassId == 33734
                        || WeenieClassId == 33735 || WeenieClassId == 40287) && !UpgradedUber1)
                    {
                        Strength.StartingValue += 1700;
                        Endurance.StartingValue += 700;
                        Quickness.StartingValue += 1500;
                        Coordination.StartingValue += 800;

                        if (WeenieClassId == 33636 || WeenieClassId == 70331)
                        {
                            Focus.StartingValue += 400;
                            Self.StartingValue += 400;

                        }
                        else
                        {
                            Focus.StartingValue += 1150;
                            Self.StartingValue += 1150;
                        }

                        var war = GetCreatureSkill(Skill.WarMagic, false);
                        var life = GetCreatureSkill(Skill.LifeMagic, false);
                        var resist = GetCreatureSkill(Skill.MagicDefense, false);
                        var missiled = GetCreatureSkill(Skill.MissileDefense, false);
                        var meleed = GetCreatureSkill(Skill.MeleeDefense, false);

                        if (war != null)
                            war.InitLevel += 200;
                        if (life != null)
                            life.InitLevel += 300;
                        if (resist != null)
                            resist.InitLevel += 200;
                        if (missiled != null)
                            missiled.InitLevel += 400;
                        if (meleed != null)
                            meleed.InitLevel += 200;

                        Health.StartingValue += 5500;

                        Health.Current = Health.MaxValue;

                        SetProperty(ACE.Entity.Enum.Properties.PropertyDataId.DeathTreasureType, 2000);

                        Level = 400;

                        XpOverride += 100000000;

                        SetProperty(ACE.Entity.Enum.Properties.PropertyInt.Overpower, 15);
                        SetProperty(ACE.Entity.Enum.Properties.PropertyInt.LuminanceAward, 1000);

                        SetProperty(ACE.Entity.Enum.Properties.PropertyBool.UpgradedUber1, true);
                    }
                }
            }
            base.Heartbeat(currentUnixTime);
        }
    }
}
