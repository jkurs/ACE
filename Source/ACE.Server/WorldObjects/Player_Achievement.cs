using ACE.Entity.Enum;
using ACE.Server.Managers;
using ACE.Server.Network.GameMessages.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACE.Server.WorldObjects
{
    class Player_Achievement
    {
        public List<string> MaleRanks = new List<string>()
        {
            // Peasants
            "Slave",   // 0
            "Freemen", // 1
            "Serf",    // 2

            // Clergy
            "Monk",    // 3
            "Priest",  // 4
            "Bishop",  // 5
            "Pope",    // 6

            // Nobility
            "Knight",  // 7
            "Baron",   // 8
            "Duke",    // 9
            "Prince",  // 10
            "King"     // 11

        };

        public List<string> FemaleRanks = new List<string>()
        {
            // Peasants
            "Slave",      // 0
            "Freemen",    // 1
            "Serf",       // 2

            // Clergy
            "Nun",        // 3
            "Priestess",  // 4
            "Bishop",     // 5
            "Pope",       // 6

            // Nobility
            "Dame",       // 7
            "Baroness",   // 8
            "Duchess",    // 9
            "Princess",   // 10
            "Queen"       // 11

        };

        public static void CheckAchievements(Player player)
        {
            // HP INCREMENT ACHEV 
            CheckHP(player);

            // STAM INCREMENT ACHEV
            CheckStam(player);

            // MANA INCREMENT ACHEV
            CheckMana(player);

            //ENLIGHTENS ACHEV
            CheckEnl(player);

            //ATTRIBUTE TOTAL ACHEV
            CheckAttributeTotal(player);

            //PLAYER LEVEL ACHEV
            CheckPlayerLevel(player);

            //TOTAL CREATURE KILLS ACHEV
            CheckMonsterKills(player);

            //HIGH LEVEL CREATURE KILLS ACHEV
            CheckHighMonsterKills(player);

            //TOTAL SKILL LEVEL ACHEV
            CheckTotalSkillLevel(player);

            //TOTAL CRITICAL STRIKE AUG
            CheckCriticalStrikeAug(player);

            //TOTAL CRITICAL STRIKE DAMAGE AUG
            CheckCriticalStrikeDamageAug(player);

            //TOTAL SPELL COMP BURN AUG
            CheckSpellCompBurnAug(player);

            //TOTAL SPELL DURATION AUG
            CheckSpellDurationAug(player);

            //TOTAL MISSILE AMMO CONSUME AUG
            CheckMissileAmmunitionConsumeAug(player);

            //TOTAL VITALITY AUG
            CheckVitalityAug(player);

            //TOTAL ARMOR MANA AUG
            CheckArmorManaAug(player);

            //TOTAL ACHIEVEMENTS
            CheckTotalAchievements(player);

            //CHECK PLAYER LEVELS IN HARDMODE
            CheckHardModeLevels(player);

            //CHECK ENLIGHTENMENTS IN HARDMODE
            CheckHMEnl(player);
        }

        public static void CheckHP(Player player)
        {
            if (!player.HP1)
            {
                if (player.Health.MaxValue >= 500)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HP1, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 500 Health!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.HP2)
            {
                if (player.Health.MaxValue >= 1000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HP2, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 1,000 Health!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.HP3)
            {
                if (player.Health.MaxValue >= 3000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HP3, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 3,000 Health!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.HP4)
            {
                if (player.Health.MaxValue >= 6000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HP4, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 6,000 Health!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.HP5)
            {
                if (player.Health.MaxValue >= 8000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HP5, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 8,000 Health!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.HP6)
            {
                if (player.Health.MaxValue >= 10000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HP6, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 10,000 Health!!!", ChatMessageType.WorldBroadcast));
                }
            }
        }

        public static void CheckStam(Player player)
        {
            if (!player.ST1)
            {
                if (player.Stamina.MaxValue >= 1000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.ST1, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 1,000 Stamina!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.ST2)
            {
                if (player.Stamina.MaxValue >= 2000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.ST2, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 2,000 Stamina!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.ST3)
            {
                if (player.Stamina.MaxValue >= 4000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.ST3, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 4,000 Stamina!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.ST4)
            {
                if (player.Stamina.MaxValue >= 6000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.ST4, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 6,000 Stamina!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.ST5)
            {
                if (player.Stamina.MaxValue >= 12000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.ST5, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 12,000 Stamina!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.ST6)
            {
                if (player.Stamina.MaxValue >= 24300)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.ST6, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 24,300 Stamina!!!", ChatMessageType.WorldBroadcast));
                }
            }
        }

        public static void CheckMana(Player player)
        {
            if (!player.MA1)
            {
                if (player.Mana.MaxValue >= 1000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.MA1, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 1,000 Mana!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.MA2)
            {
                if (player.Mana.MaxValue >= 2000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.MA2, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 2,000 Mana!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.MA3)
            {
                if (player.Mana.MaxValue >= 4000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.MA3, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 4,000 Mana!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.MA4)
            {
                if (player.Mana.MaxValue >= 6000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.MA4, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 6,000 Mana!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.MA5)
            {
                if (player.Mana.MaxValue >= 12000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.MA5, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 12,000 Mana!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.MA6)
            {
                if (player.Mana.MaxValue >= 24300)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.MA6, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 24,300 Mana!!!", ChatMessageType.WorldBroadcast));
                }
            }
        }


        public static void CheckEnl(Player player)
        {
            if (!player.ENL1)
            {
                if (player.Enlightenment >= 50)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.ENL1, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 50 Enlightenments!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.ENL2)
            {
                if (player.Enlightenment >= 100)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.ENL2, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 100 Enlightenments!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.ENL3)
            {
                if (player.Enlightenment >= 200)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.ENL3, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 200 Enlightenments!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.ENL4)
            {
                if (player.Enlightenment >= 400)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.ENL4, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 400 Enlightenments!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.ENL5)
            {
                if (player.Enlightenment >= 600)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.ENL5, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 600 Enlightenments!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.ENL6)
            {
                if (player.Enlightenment >= 800)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.ENL6, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 800 Enlightenments!!!", ChatMessageType.WorldBroadcast));
                }
            }
        }


        public static void CheckAttributeTotal(Player player)
        {
            var str = player.Strength.Base;
            var end = player.Endurance.Base;
            var quick = player.Quickness.Base;
            var coord = player.Coordination.Base;
            var focus = player.Focus.Base;
            var self = player.Self.Base;

            var attributetotal = str + end + quick + coord + focus + self;

            if (!player.AT1)
            {
                if (attributetotal >= 1000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.AT1, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained a Total Base Attribute value of 1,000!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.AT2)
            {
                if (attributetotal >= 4000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.AT2, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained a Total Base Attribute value of 4,000!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.AT3)
            {
                if (attributetotal >= 7000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.AT3, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained a Total Base Attribute value of 7,000!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.AT4)
            {
                if (attributetotal >= 12000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.AT4, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained a Total Base Attribute value of 12,000!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.AT5)
            {
                if (attributetotal >= 15000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.AT5, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained a Total Base Attribute value of 15,000!!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.AT6)
            {
                if (attributetotal >= 20000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.AT2, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained a Total Base Attribute value of 20,000!!!", ChatMessageType.WorldBroadcast));
                }
            }
        }

        public static void CheckPlayerLevel(Player player)
        {
            if (!player.LV1)
            {
                if (player.Level >= 275)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.LV1, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached level 275!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.LV2)
            {
                if (player.Level >= 1000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.LV2, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached level 1,000!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.LV3)
            {
                if (player.Level >= 4000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.LV3, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached level 4,000!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.LV4)
            {
                if (player.Level >= 8000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.LV4, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached level 8,000!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.LV5)
            {
                if (player.Level >= 10000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.LV5, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached level 10,000!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.LV6)
            {
                if (player.Level >= 20000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.LV6, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached level 20,000!!", ChatMessageType.WorldBroadcast));
                }
            }
        }

        public static void CheckMonsterKills(Player player)
        {
            if (!player.MC1)
            {
                if (player.CreatureKills >= 1000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.MC1, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has killed a total of 1,000 creatures!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.MC2)
            {
                if (player.CreatureKills >= 10000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.MC2, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has killed a total of 10,000 creatures!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.MC3)
            {
                if (player.CreatureKills >= 100000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.MC3, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has killed a total of 100,000 creatures!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.MC4)
            {
                if (player.CreatureKills >= 1000000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.MC4, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has killed a total of 1,000,000 creatures!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.MC5)
            {
                if (player.CreatureKills >= 10000000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.MC5, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has killed a total of 10,000,000 creatures!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.MC6)
            {
                if (player.CreatureKills >= 100000000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.MC6, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has killed a total of 100,000,000 creatures!!", ChatMessageType.WorldBroadcast));
                }
            }
        }

        public static void CheckHighMonsterKills(Player player)
        {
            if (!player.HMC1)
            {
                if (player.MonsterKillCount >= 100)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HMC1, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has killed a total of 100 high level creatures!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.HMC2)
            {
                if (player.MonsterKillCount >= 1000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HMC2, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has killed a total of 1,000 high level creatures!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.HMC3)
            {
                if (player.MonsterKillCount >= 10000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HMC3, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has killed a total of 10,000 high level creatures!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.HMC4)
            {
                if (player.MonsterKillCount >= 100000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HMC4, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has killed a total of 100,000 high level creatures!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.HMC5)
            {
                if (player.MonsterKillCount >= 1000000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HMC5, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has killed a total of 1,000,000 high level creatures!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.HMC6)
            {
                if (player.MonsterKillCount >= 10000000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HMC6, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has killed a total of 10,000,000 high level creatures!!", ChatMessageType.WorldBroadcast));
                }
            }
        }

        public static void CheckTotalSkillLevel(Player player)
        {
            var skillTotal = 0;

            foreach(var skill in player.Skills)
            {
                if (skill.Value.AdvancementClass >= SkillAdvancementClass.Trained)
                {
                    skillTotal += (int)skill.Value.Base;
                }
            }

            if (!player.TS1)
            {
                if (skillTotal >= 5000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.TS1, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained a total skill value of 5,000!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.TS2)
            {
                if (skillTotal >= 10000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.TS2, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained a total skill value of 10,000!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.TS3)
            {
                if (skillTotal >= 25000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.TS3, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained a total skill value of 25,000!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.TS4)
            {
                if (skillTotal >= 35000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.TS4, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained a total skill value of 35,000!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.TS5)
            {
                if (skillTotal >= 40000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.TS5, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained a total skill value of 40,000!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.TS6)
            {
                if (skillTotal >= 60000)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.TS6, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained a total skill value of 60,000!!", ChatMessageType.WorldBroadcast));
                }
            }
        }

        public static void CheckCriticalStrikeAug(Player player)
        {
            if (!player.CS1)
            {
                if (player.CriticalStrikeAug >= 10)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.CS1, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 10 Critical Strike Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.CS2)
            {
                if (player.CriticalStrikeAug >= 25)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.CS2, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 25 Critical Strike Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.CS3)
            {
                if (player.CriticalStrikeAug >= 40)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.CS3, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 40 Critical Strike Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.CS4)
            {
                if (player.CriticalStrikeAug >= 55)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.CS4, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 55 Critical Strike Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.CS5)
            {
                if (player.CriticalStrikeAug >= 70)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.CS5, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 70 Critical Strike Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.CS6)
            {
                if (player.CriticalStrikeAug >= 100)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.CS6, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 100 Critical Strike Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }
        }

        public static void CheckCriticalStrikeDamageAug(Player player)
        {
            if (!player.CSD1)
            {
                if (player.CriticalStrikeDamageAug >= 10)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.CSD1, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 10 Critical Strike Damage Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.CSD2)
            {
                if (player.CriticalStrikeDamageAug >= 25)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.CSD2, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 25 Critical Strike Damage Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.CSD3)
            {
                if (player.CriticalStrikeDamageAug >= 40)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.CSD3, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 40 Critical Strike Damage Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.CSD4)
            {
                if (player.CriticalStrikeDamageAug >= 55)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.CSD4, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 55 Critical Strike Damage Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.CSD5)
            {
                if (player.CriticalStrikeDamageAug >= 70)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.CSD5, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 70 Critical Strike Damage Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.CSD6)
            {
                if (player.CriticalStrikeDamageAug >= 100)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.CSD6, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 100 Critical Strike Damage Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }
        }

        public static void CheckSpellCompBurnAug(Player player)
        {
            if (!player.SCB1)
            {
                if (player.CompBurnAug >= 10)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.SCB1, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 10 Spell Component Burn Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.SCB2)
            {
                if (player.CompBurnAug >= 25)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.SCB2, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 25 Spell Component Burn Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.SCB3)
            {
                if (player.CompBurnAug >= 40)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.SCB3, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 40 Spell Component Burn Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.SCB4)
            {
                if (player.CompBurnAug >= 55)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.SCB4, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 55 Spell Component Burn Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.SCB5)
            {
                if (player.CompBurnAug >= 70)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.SCB5, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 70 Spell Component Burn Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.SCB6)
            {
                if (player.CompBurnAug >= 100)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.SCB6, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 100 Spell Component Burn Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }
        }

        public static void CheckMissileAmmunitionConsumeAug(Player player)
        {
            if (!player.MAC1)
            {
                if (player.MissileConsumeAug >= 10)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.MAC1, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 10 Missile Consume Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.MAC2)
            {
                if (player.MissileConsumeAug >= 25)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.MAC2, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 25 Missile Consume Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.MAC3)
            {
                if (player.MissileConsumeAug >= 40)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.MAC3, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 40 Missile Consume Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.MAC4)
            {
                if (player.MissileConsumeAug >= 55)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.MAC4, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 55 Missile Consume Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.MAC5)
            {
                if (player.MissileConsumeAug >= 70)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.MAC5, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 70 Missile Consume Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.MAC6)
            {
                if (player.MissileConsumeAug >= 100)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.MAC6, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 100 Missile Consume Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }
        }

        public static void CheckSpellDurationAug(Player player)
        {
            if (!player.SD1)
            {
                if (player.SpellDurationAug >= 10)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.SD1, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 10 Spell Duration Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.SD2)
            {
                if (player.SpellDurationAug >= 25)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.SD2, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 25 Spell Duration Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.SD3)
            {
                if (player.SpellDurationAug >= 40)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.SD3, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 40 Spell Duration Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.SD4)
            {
                if (player.SpellDurationAug >= 55)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.SD4, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 55 Spell Duration Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.SD5)
            {
                if (player.SpellDurationAug >= 70)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.SD5, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 70 Spell Duration Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.SD6)
            {
                if (player.SpellDurationAug >= 100)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.SD6, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 100 Spell Duration Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }
        }

        public static void CheckVitalityAug(Player player)
        {
            if (!player.V1)
            {
                if (player.VitalityAug >= 10)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.V1, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 10 Vitality Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.V2)
            {
                if (player.VitalityAug >= 25)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.V2, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 25 Vitality Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.V3)
            {
                if (player.VitalityAug >= 40)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.V3, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 40 Vitality Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.V4)
            {
                if (player.VitalityAug >= 55)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.V4, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 55 Vitality Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.V5)
            {
                if (player.VitalityAug >= 70)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.V5, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 70 Vitality Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.V6)
            {
                if (player.VitalityAug >= 100)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.V6, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 100 Vitality Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }
        }

        public static void CheckArmorManaAug(Player player)
        {
            if (!player.AM1)
            {
                if (player.ArmorManaAug >= 10)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.AM1, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 10 Armor Mana Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.AM2)
            {
                if (player.ArmorManaAug >= 25)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.AM2, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 25 Armor Mana Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.AM3)
            {
                if (player.ArmorManaAug >= 40)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.AM3, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 40 Armor Mana Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.AM4)
            {
                if (player.ArmorManaAug >= 55)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.AM4, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 55 Armor Mana Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.AM5)
            {
                if (player.ArmorManaAug >= 70)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.AM5, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 70 Armor Mana Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.AM6)
            {
                if (player.ArmorManaAug >= 100)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.AM6, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 100 Armor Mana Augmentations!!", ChatMessageType.WorldBroadcast));
                }
            }
        }

        public static void CheckTotalAchievements(Player player)
        {
            if (!player.TAC1)
            {
                if (player.AchievementCount >= 17)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.TAC1, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 17 total Achievements!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.TAC2)
            {
                if (player.AchievementCount >= 34)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.TAC2, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 34 total Achievements!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.TAC3)
            {
                if (player.AchievementCount >= 51)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.TAC3, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 51 total Achievements!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.TAC4)
            {
                if (player.AchievementCount >= 68)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.TAC4, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 68 total Achievements!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.TAC5)
            {
                if (player.AchievementCount >= 85)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.TAC5, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 85 total Achievements!!", ChatMessageType.WorldBroadcast));
                }
            }

            if (!player.TAC6)
            {
                if (player.AchievementCount >= 101)
                {
                    player.AchievementCount++;
                    player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.TAC6, true);
                    PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has obtained 102 total Achievements!!", ChatMessageType.WorldBroadcast));
                }
            }
        }

        public static void CheckHardModeLevels(Player player)
        {
            if (player.HardMode && !player.HardModeFirst)
            {
                if (!player.HMLV1)
                {
                    if (player.Level >= 50)
                    {
                        player.AchievementCount++;
                        player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HMLV1, true);
                        PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached level 50 in Hardmode!!", ChatMessageType.WorldBroadcast));
                    }
                }

                if (!player.HMLV2)
                {
                    if (player.Level >= 150)
                    {
                        player.AchievementCount++;
                        player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HMLV2, true);
                        PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached level 150 in Hardmode!!", ChatMessageType.WorldBroadcast));
                    }
                }

                if (!player.HMLV3)
                {
                    if (player.Level >= 275)
                    {
                        player.AchievementCount++;
                        player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HMLV3, true);
                        PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached level 275 in Hardmode!!", ChatMessageType.WorldBroadcast));
                    }
                }

                if (!player.HMLV4)
                {
                    if (player.Level >= 400)
                    {
                        player.AchievementCount++;
                        player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HMLV4, true);
                        PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached level 400 in Hardmode!!", ChatMessageType.WorldBroadcast));
                    }
                }

                if (!player.HMLV5)
                {
                    if (player.Level >= 700)
                    {
                        player.AchievementCount++;
                        player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HMLV5, true);
                        PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached level 700 in Hardmode!!", ChatMessageType.WorldBroadcast));
                    }
                }

                if (!player.HMLV6)
                {
                    if (player.Level >= 1000)
                    {
                        player.AchievementCount++;
                        player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HMLV6, true);
                        PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached level 1,000 in Hardmode!!", ChatMessageType.WorldBroadcast));
                    }
                }
            }
        }

        public static void CheckHMEnl(Player player)
        {
            if (player.HardMode && !player.HardModeFirst)
            {
                if (!player.HMENL1)
                {
                    if (player.Enlightenment >= 10)
                    {
                        player.AchievementCount++;
                        player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HMENL1, true);
                        PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 10 Enlightenments in Hardmode!!!", ChatMessageType.WorldBroadcast));
                    }
                }

                if (!player.HMENL2)
                {
                    if (player.Enlightenment >= 50)
                    {
                        player.AchievementCount++;
                        player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HMENL2, true);
                        PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 50 Enlightenments in Hardmode!!!", ChatMessageType.WorldBroadcast));
                    }
                }

                if (!player.HMENL3)
                {
                    if (player.Enlightenment >= 100)
                    {
                        player.AchievementCount++;
                        player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HMENL3, true);
                        PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 100 Enlightenments in Hardmode!!!", ChatMessageType.WorldBroadcast));
                    }
                }

                if (!player.HMENL4)
                {
                    if (player.Enlightenment >= 150)
                    {
                        player.AchievementCount++;
                        player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HMENL4, true);
                        PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 150 Enlightenments in Hardmode!!!", ChatMessageType.WorldBroadcast));
                    }
                }

                if (!player.HMENL5)
                {
                    if (player.Enlightenment >= 250)
                    {
                        player.AchievementCount++;
                        player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HMENL5, true);
                        PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 250 Enlightenments in Hardmode!!!", ChatMessageType.WorldBroadcast));
                    }
                }

                if (!player.HMENL6)
                {
                    if (player.Enlightenment >= 350)
                    {
                        player.AchievementCount++;
                        player.SetProperty(ACE.Entity.Enum.Properties.PropertyBool.HMENL6, true);
                        PlayerManager.BroadcastToAll(new GameMessageSystemChat($"[ACHIEVEMENT] {player.Name} has reached a total of 350 Enlightenments in Hardmode!!!", ChatMessageType.WorldBroadcast));
                    }
                }
            }
        }
    }
}
