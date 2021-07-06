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

            var targetPlayer = AttackTarget as Player;

            if (SlingShot && PlayersInRange(10) && IsAwake && targetPlayer != null)
            {
                //select element
                var element = ThreadSafeRandom.Next(1, 7);                

                var maxChance =  100.00f - SlingShotChance;
                var chance = ThreadSafeRandom.Next(0.00f, 100.00f);

                if (chance >= maxChance)
                {
                    switch (element)
                    {
                        case 1:

                            if (Level < 25)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.FlameArc1), this);

                            if (Level >= 25 && Level <= 39)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.FlameArc2), this);

                            if (Level >= 40 && Level <= 69)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.FlameArc3), this);

                            if (Level >= 70 && Level <= 99)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.FlameArc4), this);

                            if (Level >= 100 && Level <= 129)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.FlameArc5), this);

                            if (Level >= 130 && Level <= 149)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.FlameArc6), this);

                            if (Level >= 150 && Level <= 199)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.FlameArc7), this);

                            if (Level >= 200)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.FlameArc8), this);

                            break;
                        case 2:
                            if (Level < 25)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.FrostArc1), this);

                            if (Level >= 25 && Level <= 39)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.FrostArc2), this);

                            if (Level >= 40 && Level <= 69)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.FrostArc3), this);

                            if (Level >= 70 && Level <= 99)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.FrostArc4), this);

                            if (Level >= 100 && Level <= 129)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.FrostArc5), this);

                            if (Level >= 130 && Level <= 149)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.FrostArc6), this);

                            if (Level >= 150 && Level <= 199)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.FrostArc7), this);

                            if (Level >= 200)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.FrostArc8), this);
                            break;
                        case 3:
                            if (Level < 25)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.LightningArc1), this);

                            if (Level >= 25 && Level <= 39)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.LightningArc2), this);

                            if (Level >= 40 && Level <= 69)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.LightningArc3), this);

                            if (Level >= 70 && Level <= 99)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.LightningArc4), this);

                            if (Level >= 100 && Level <= 129)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.LightningArc5), this);

                            if (Level >= 130 && Level <= 149)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.LightningArc6), this);

                            if (Level >= 150 && Level <= 199)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.LightningArc7), this);

                            if (Level >= 200)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.LightningArc8), this);
                            break;
                        case 4:
                            if (Level < 25)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.AcidArc1), this);

                            if (Level >= 25 && Level <= 39)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.AcidArc2), this);

                            if (Level >= 40 && Level <= 69)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.AcidArc3), this);

                            if (Level >= 70 && Level <= 99)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.AcidArc4), this);

                            if (Level >= 100 && Level <= 129)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.AcidArc5), this);

                            if (Level >= 130 && Level <= 149)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.AcidArc6), this);

                            if (Level >= 150 && Level <= 199)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.AcidArc7), this);

                            if (Level >= 200)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.AcidArc8), this);
                            break;
                        case 5:
                            if (Level < 25)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.ShockArc1), this);

                            if (Level >= 25 && Level <= 39)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.ShockArc2), this);

                            if (Level >= 40 && Level <= 69)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.ShockArc3), this);

                            if (Level >= 70 && Level <= 99)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.ShockArc4), this);

                            if (Level >= 100 && Level <= 129)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.ShockArc5), this);

                            if (Level >= 130 && Level <= 149)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.ShockArc6), this);

                            if (Level >= 150 && Level <= 199)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.ShockArc7), this);

                            if (Level >= 200)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.ShockArc8), this);
                            break;
                        case 6:
                            if (Level < 25)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.ForceArc1), this);

                            if (Level >= 25 && Level <= 39)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.ForceArc2), this);

                            if (Level >= 40 && Level <= 69)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.ForceArc3), this);

                            if (Level >= 70 && Level <= 99)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.ForceArc4), this);

                            if (Level >= 100 && Level <= 129)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.ForceArc5), this);

                            if (Level >= 130 && Level <= 149)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.ForceArc6), this);

                            if (Level >= 150 && Level <= 199)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.ForceArc7), this);

                            if (Level >= 200)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.ForceArc8), this);
                            break;
                        case 7:
                            if (Level < 25)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.BladeArc1), this);

                            if (Level >= 25 && Level <= 39)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.BladeArc2), this);

                            if (Level >= 40 && Level <= 69)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.BladeArc3), this);

                            if (Level >= 70 && Level <= 99)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.BladeArc4), this);

                            if (Level >= 100 && Level <= 129)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.BladeArc5), this);

                            if (Level >= 130 && Level <= 149)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.BladeArc6), this);

                            if (Level >= 150 && Level <= 199)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.BladeArc7), this);

                            if (Level >= 200)
                                WarMagic(targetPlayer, new Server.Entity.Spell(SpellId.BladeArc8), this);
                            break;
                    }
                }
            }


            if (Cleveled)
            {
                bool leveled = false;

                for (var i = CreatureExperience; i >= CreatureRequiredXpToLevel; i -= CreatureRequiredXpToLevel)
                {
                    //indicates whether a mob has leveled up or not.

                    if (!Name.Contains("[L]"))
                    {
                        string levelTag = $"[L] {Name}";
                        Name = levelTag;
                    }

                    // grabs the remainder leftover from the previous levels XP gained to passover to the creatures next experience pool.
                    var remainder = CreatureExperience - CreatureRequiredXpToLevel;
                    CreatureExperience = remainder;

                    //ensure no negatives here.
                    if (CreatureExperience <= 0)
                        CreatureExperience = 0;

                    // sets the new xp requirement for a level for the creature.
                    CreatureRequiredXpToLevel = ((int)Level * 100) * CreatureOriginalLevel;

                    PlayParticleEffect(PlayScript.LevelUp, Guid);
                    Level++;

                    leveled = true;

                    if (!Location.Indoors)
                    {
                        if (ObjScale != null)
                            ObjScale += 0.005f;
                    }

                    var statbonusfromlevel1 = 0;
                    var statbonusfromlevel2 = 0;
                    var statbonusfromlevel3 = 0;
                    var statbonusfromlevel4 = 0;
                    var statbonusfromlevel5 = 0;
                    var statbonusfromlevel6 = 0;
                    var statmax = 0;

                    // Max tiered stat increases based on level of creature
                    if (Level < 25)
                        statmax = 2;

                    if (Level >= 25 && Level <= 39 )
                        statmax = 4;

                    if (Level >= 40 && Level <= 69)                    
                        statmax = 6;

                    if (Level >= 70 && Level <= 99)
                        statmax = 10;

                    if (Level >= 100)
                        statmax = 15;

                    statbonusfromlevel1 = ThreadSafeRandom.Next(0, statmax);
                    statbonusfromlevel2 = ThreadSafeRandom.Next(0, statmax);
                    statbonusfromlevel3 = ThreadSafeRandom.Next(0, statmax);
                    statbonusfromlevel4 = ThreadSafeRandom.Next(0, statmax);
                    statbonusfromlevel5 = ThreadSafeRandom.Next(0, statmax);
                    statbonusfromlevel6 = ThreadSafeRandom.Next(0, statmax);

                    Strength.StartingValue += (uint)statbonusfromlevel1;
                    Endurance.StartingValue += (uint)statbonusfromlevel2;
                    Coordination.StartingValue += (uint)statbonusfromlevel3;
                    Quickness.StartingValue += (uint)statbonusfromlevel4;
                    Focus.StartingValue += (uint)statbonusfromlevel5;
                    Self.StartingValue += (uint)statbonusfromlevel6;

                    Health.Current = Health.MaxValue;
                    Stamina.Current = Stamina.MaxValue;
                    Mana.Current = Mana.MaxValue;

                    var abilitychance = ThreadSafeRandom.Next(0.00f, 100.00f);

                    //25% on leveling to gain a buff/ability
                    if (abilitychance <= 25.00f)
                    {
                        var abilityselect = ThreadSafeRandom.Next(1, 4);

                        switch (abilityselect)
                        {
                            case 1:
                                //slingshot Enables and adds 5% chance to launch a war projectile.

                                var warMagic = GetCreatureSkill(Skill.WarMagic);

                                warMagic.AdvancementClass = SkillAdvancementClass.Trained;                               

                                if (warMagic.InitLevel <= 319)
                                    warMagic.InitLevel = 320;

                                if (!SlingShot)
                                    SetProperty(ACE.Entity.Enum.Properties.PropertyBool.SlingShot, true);

                                if (SlingShotChance <= 0 || SlingShotChance == null)
                                    SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.SlingShotChance, 0);

                                //cap at 50% chance
                                if (SlingShotChance < 50.00f)
                                    SlingShotChance += 0.05f;

                                if (SlingShotChance >= 50.00f)
                                    SlingShotChance = 50.00f;

                                string ability1 = $"[CREATURE] {Name} has gained the ability to Sling war spells at enemies by 5% from leveling up!";
                                PlayerManager.BroadcastToAll(new GameMessageSystemChat(ability1, ChatMessageType.Broadcast));

                                break;

                            case 2:
                                //resistancegain 5% resistance gain to all resistances
                                if (ResistFire == null)
                                    SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.ResistFire, 0);
                                if (ResistCold == null)
                                    SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.ResistCold, 0);
                                if (ResistElectric == null)
                                    SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.ResistElectric, 0);
                                if (ResistAcid == null)
                                    SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.ResistAcid, 0);
                                if (ResistBludgeon == null)
                                    SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.ResistBludgeon, 0);
                                if (ResistPierce == null)
                                    SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.ResistPierce, 0);
                                if (ResistSlash == null)
                                    SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.ResistSlash, 0);
                                if (ResistNether == null)
                                    SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.ResistNether, 0);

                                if (ResistFire <= 49.95f)
                                    ResistFire += 0.05f;
                                if (ResistCold <= 49.95f)
                                    ResistCold += 0.05f;
                                if (ResistElectric <= 49.95f)
                                    ResistElectric += 0.05f;
                                if (ResistBludgeon <= 49.95f)
                                    ResistBludgeon += 0.05f;
                                if (ResistSlash <= 49.95f)
                                    ResistSlash += 0.05f;
                                if (ResistPierce <= 49.95f)
                                    ResistPierce += 0.05f;
                                if (ResistAcid <= 49.95f)
                                    ResistAcid += 0.05f;
                                if (ResistNether <= 49.95f)
                                    ResistNether += 0.05f;

                                string ability2 = $"[CREATURE] {Name} has raised its natural resistances by 5% from leveling up!";
                                PlayerManager.BroadcastToAll(new GameMessageSystemChat(ability2, ChatMessageType.Broadcast));

                                break;

                            case 3:
                                //damagebuff 5% damage increase.

                                if (DamageBuff == null)
                                    SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.DamageBuff, 0);

                                DamageBuff += 0.05f;

                                string ability3 = $"[CREATURE] {Name} had its damage increased by 5% from leveling up!";
                                PlayerManager.BroadcastToAll(new GameMessageSystemChat(ability3, ChatMessageType.Broadcast));

                                break;

                            case 4:
                                //xpmodifier 25% increase of Base creature XP.
                                var bonusXP = Math.Round((double)XpOverride * 0.25f);

                                XpOverride += (int)bonusXP;

                                string ability4 = $"[CREATURE] XP received from killing {Name} monster has increased by 25%";
                                PlayerManager.BroadcastToAll(new GameMessageSystemChat(ability4, ChatMessageType.Broadcast));

                                break;
                        }
                    }

                    string msg = $"[CREATURE] {Name} has leveled up to {Level} from {Level - 1} New stats: STR +{statbonusfromlevel1}({Strength.StartingValue}) | END +{statbonusfromlevel2}({Endurance.StartingValue}) | COORD +{statbonusfromlevel3}({Coordination.StartingValue}) | QUICK +{statbonusfromlevel4}({Quickness.StartingValue}) | FOCUS +{statbonusfromlevel5}({Focus.StartingValue}) | SELF +{statbonusfromlevel6}({Self.StartingValue})";
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat(msg, ChatMessageType.Broadcast));
                    string msg2 = $"[CREATURE] XP required to level: {CreatureExperience}/{CreatureRequiredXpToLevel}";
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat(msg2, ChatMessageType.Broadcast));
                }

                if (leveled)
                {
                    EnqueueBroadcastUpdateObject();
                    leveled = false;
                }
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
