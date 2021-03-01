using ACE.Common;
using ACE.Entity.Enum;
using ACE.Entity.Enum.Properties;
using ACE.Server.Entity;
using ACE.Server.Managers;
using ACE.Server.Network.GameMessages.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACE.Server.WorldObjects
{
    partial class Player
    {
        public WorldObject ModifyQuestItem(WorldObject item, int modSelectMin, int modSelectMax, bool extrachance)
        {
            var selectMod = ThreadSafeRandom.Next(modSelectMin, modSelectMax);
            var selectSlayerType = ThreadSafeRandom.Next(1, 24);
            var selectArmorMod = ThreadSafeRandom.Next(1, 1);
            var extraChance = ThreadSafeRandom.Next(1, 100);
            var splitChance = ThreadSafeRandom.Next(1, 2);
            var splitChance2 = ThreadSafeRandom.Next(1, 2);

            // if an extra chance occurred, we only want it to choose from cantrips and ratings.
            //This means if you want a rend, armor set, or slayer you gotta get lucky on the very first roll from Player_Inventory.cs
            if (extrachance) // 2 , 4 , 5
            {
                var chooseExtraMod = ThreadSafeRandom.Next(1, 3);

                switch (chooseExtraMod)
                {
                    case 1:
                        selectMod = 2;
                        break;
                    case 2:
                        selectMod = 4;
                        break;
                    case 3:
                        selectMod = 5;
                        break;
                }
            }

            switch (selectMod)
            {
                case 1: // Slayer Weapons & Steel Tinks
                    if (item.ItemType == ItemType.MeleeWeapon || item.ItemType == ItemType.MissileWeapon || item.ItemType == ItemType.Caster)
                    {

                        switch (selectSlayerType)
                        {
                            case 1:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Banderling;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 2:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Drudge;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 3:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Eater;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 4:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Fiun;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 5:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Gromnie;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 6:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Lugian;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 7:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Grievver;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 8:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Human;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 9:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Mattekar;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 10:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Mite;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 11:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Mosswart;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 12:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Mumiyah;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 13:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Olthoi;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 14:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.PhyntosWasp;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 15:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Shadow;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 16:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Shreth;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 17:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Skeleton;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 18:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Sleech;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 19:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Tumerok;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 20:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Tusker;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 21:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.ViamontianKnight;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 22:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Virindi;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 23:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Wisp;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                            case 24:
                                item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Zefir;
                                item.SlayerDamageBonus = 1.20f;
                                item.SlayerAdded = 1;
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;
                        }
                    }
                    else if (item.ItemType == ItemType.Armor || item.ItemType == ItemType.Clothing)
                    {
                        switch (selectArmorMod)
                        {
                            case 1: // Steel Tinker

                                var tinkamount = ThreadSafeRandom.Next(1, 10);

                                item.ArmorLevel += 20 * tinkamount;
                                item.NumTimesTinkered += tinkamount;

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {tinkamount} Steel tinks! (+{20 * tinkamount}AL)", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 2, 3, true);

                                return item;

                            default:
                                return item;
                        }
                    }
                    break;
                case 2: // Weapon Attribute Cantrips & Armor Skill Cantrips
                    if (item.ItemType == ItemType.MeleeWeapon || item.ItemType == ItemType.MissileWeapon || item.ItemType == ItemType.Caster)
                    {
                        var selectAttribute = ThreadSafeRandom.Next(1, 6);

                        switch (selectAttribute)
                        {
                            case 1: // strength

                                RecipeManager.AddSpell(this, item, SpellId.CantripStrength4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Strength to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 2: // endurance

                                RecipeManager.AddSpell(this, item, SpellId.CantripEndurance4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Endurance to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 3:// coordination

                                RecipeManager.AddSpell(this, item, SpellId.CantripCoordination4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Coordination to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 4: // quickness

                                RecipeManager.AddSpell(this, item, SpellId.CantripQuickness4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Quickness to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 5: // focus

                                RecipeManager.AddSpell(this, item, SpellId.CantripFocus4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Focus to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 6: // self

                                RecipeManager.AddSpell(this, item, SpellId.CantripWillpower4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Willpower to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                        }
                    }
                    else if (item.ItemType == ItemType.Armor || item.ItemType == ItemType.Clothing)
                    {
                        var selectSkill = ThreadSafeRandom.Next(1, 21);

                        switch (selectSkill)
                        {
                            case 1:

                                RecipeManager.AddSpell(this, item, SpellId.CantripInvulnerability4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Invulnerability to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 2:

                                RecipeManager.AddSpell(this, item, SpellId.CantripMagicResistance4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Magic Resistance to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 3:

                                RecipeManager.AddSpell(this, item, SpellId.CantripCreatureEnchantmentAptitude4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Creature Enchantment to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 4:

                                RecipeManager.AddSpell(this, item, SpellId.CantripLifeMagicAptitude4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Life Magic to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 5:

                                RecipeManager.AddSpell(this, item, SpellId.CantripItemEnchantmentAptitude4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Item Enchantment to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 6:

                                RecipeManager.AddSpell(this, item, SpellId.CantripArcaneProwess4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Arcane Lore to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 7:

                                RecipeManager.AddSpell(this, item, SpellId.CantripManaConversionProwess4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Mana Conversion to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 8:

                                RecipeManager.AddSpell(this, item, SpellId.CantripSummoningProwess4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Summoning to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 9:

                                RecipeManager.AddSpell(this, item, SpellId.CantripVoidMagicAptitude4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Void Magic to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 10:

                                RecipeManager.AddSpell(this, item, SpellId.CantripWarMagicAptitude4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary War Magic to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 11:

                                RecipeManager.AddSpell(this, item, SpellId.CantripHealingProwess4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Healing to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 12:

                                RecipeManager.AddSpell(this, item, SpellId.CantripJumpingProwess4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Jump to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 13:

                                RecipeManager.AddSpell(this, item, SpellId.CantripSprint4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Run to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 14:

                                RecipeManager.AddSpell(this, item, SpellId.CantripDualWieldAptitude4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Dual Wield to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 15:

                                RecipeManager.AddSpell(this, item, SpellId.CantripDirtyFightingProwess4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Dirty Fighting to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 16:

                                RecipeManager.AddSpell(this, item, SpellId.CantripTwoHandedAptitude4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Two Handed Combat to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 17:

                                RecipeManager.AddSpell(this, item, SpellId.CantripRecklessnessProwess4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Recklessness to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 18:

                                RecipeManager.AddSpell(this, item, SpellId.CantripDeceptionProwess4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Deception to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 19:

                                RecipeManager.AddSpell(this, item, SpellId.CantripLeadership4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Leadership to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 20:

                                RecipeManager.AddSpell(this, item, SpellId.CantripSneakAttackProwess4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Sneak Attack to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                            case 21:

                                RecipeManager.AddSpell(this, item, SpellId.CantripFletchingProwess4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Willpower to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 3, 3, true);

                                return item;
                        }
                    }
                    break;
                case 3: // Weapon Rends & Armor Sets
                    if (item.ItemType == ItemType.MeleeWeapon || item.ItemType == ItemType.MissileWeapon || item.ItemType == ItemType.Caster)
                    {
                        var rendSelect = ThreadSafeRandom.Next(1, 7);

                        switch (rendSelect)
                        {
                            case 1: // acid

                                RecipeManager.AddImbuedEffect(this, item, ImbuedEffectType.AcidRending);
                                item.W_DamageType = DamageType.Acid;
                                item.SetProperty(PropertyDataId.IconUnderlay, 0x06003355);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Acid Rending to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 2: // Cold

                                RecipeManager.AddImbuedEffect(this, item, ImbuedEffectType.ColdRending);
                                item.W_DamageType = DamageType.Cold;
                                item.SetProperty(PropertyDataId.IconUnderlay, 0x06003353);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Cold Rending to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 3: // Electric

                                RecipeManager.AddImbuedEffect(this, item, ImbuedEffectType.ElectricRending);
                                item.W_DamageType = DamageType.Electric;
                                item.SetProperty(PropertyDataId.IconUnderlay, 0x06003354);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Lightning Rending to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 4: // Fire

                                RecipeManager.AddImbuedEffect(this, item, ImbuedEffectType.FireRending);
                                item.W_DamageType = DamageType.Fire;
                                item.SetProperty(PropertyDataId.IconUnderlay, 0x06003359);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Fire Rending to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 5: // Pierce

                                RecipeManager.AddImbuedEffect(this, item, ImbuedEffectType.PierceRending);
                                item.W_DamageType = DamageType.Pierce;
                                item.SetProperty(PropertyDataId.IconUnderlay, 0x0600335b);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Pierce Rending to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 6: // Slash

                                RecipeManager.AddImbuedEffect(this, item, ImbuedEffectType.SlashRending);
                                item.W_DamageType = DamageType.Slash;
                                item.SetProperty(PropertyDataId.IconUnderlay, 0x0600335c);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Slash Rending to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 7: // Bludgeon

                                RecipeManager.AddImbuedEffect(this, item, ImbuedEffectType.BludgeonRending);
                                item.W_DamageType = DamageType.Bludgeon;
                                item.SetProperty(PropertyDataId.IconUnderlay, 0x0600335a);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Bludgeon Rending to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                        }

                    }
                    else if (item.ItemType == ItemType.Armor || item.ItemType == ItemType.Clothing)
                    {
                        var armorSetSelect = ThreadSafeRandom.Next(1, 9);

                        switch (armorSetSelect)
                        {
                            case 1:
                                item.EquipmentSetId = EquipmentSet.Adepts;

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Adept Set to quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 2:
                                item.EquipmentSetId = EquipmentSet.Archers;

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Archer Set to quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 3:
                                item.EquipmentSetId = EquipmentSet.Crafters;

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Crafters Set to quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 4:
                                item.EquipmentSetId = EquipmentSet.Defenders;

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Defender Set to quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 5:
                                item.EquipmentSetId = EquipmentSet.Dexterous;

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Dexterous Set to quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 6:
                                item.EquipmentSetId = EquipmentSet.Hardened;

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Hardened Set to quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 7:
                                item.EquipmentSetId = EquipmentSet.Hearty;

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Hearty Set to quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 8:
                                item.EquipmentSetId = EquipmentSet.Interlocking;

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Interlocking Set to quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 9:
                                item.EquipmentSetId = EquipmentSet.Reinforced;

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Reinforced Set to quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                        }
                    }

                    break;
                case 4: // Weapon Item Cantrips & Armor Attribute Cantrips
                    if (item.ItemType == ItemType.MeleeWeapon || item.ItemType == ItemType.MissileWeapon || item.ItemType == ItemType.Caster)
                    {
                        var selectItemSpell = ThreadSafeRandom.Next(1, 6);

                        switch (selectItemSpell)
                        {
                            case 1: // Blood Drinker / Spirit Drinker

                                if (item.ItemType != ItemType.Caster)
                                {
                                    RecipeManager.AddSpell(this, item, SpellId.CantripBloodThirst4);
                                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Blood Drinker to the quest item!", ChatMessageType.System));

                                    if (extraChance <= 15)
                                        return ModifyQuestItem(item, 1, 2, true);

                                    return item;
                                }
                                else
                                {
                                    RecipeManager.AddSpell(this, item, SpellId.CantripSpiritThirst4);
                                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Spirit Drinker to the quest item!", ChatMessageType.System));

                                    if (extraChance <= 15)
                                        return ModifyQuestItem(item, 1, 2, true);

                                    return item;
                                }
                            case 2: // Defender / HeartSeeker                                

                                if (splitChance == 1)
                                {
                                    RecipeManager.AddSpell(this, item, SpellId.CantripDefender4);
                                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Defender to the quest item!", ChatMessageType.System));

                                    if (extraChance <= 15)
                                        return ModifyQuestItem(item, 1, 2, true);

                                    return item;
                                }
                                else
                                {
                                    if (item.ItemType == ItemType.Caster)
                                    {
                                        if (splitChance2 == 1)
                                        {
                                            RecipeManager.AddSpell(this, item, SpellId.CantripSpiritThirst4);
                                            Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Spirit Drinker to the quest item!", ChatMessageType.System));

                                            if (extraChance <= 15)
                                                return ModifyQuestItem(item, 1, 2, true);

                                            return item;
                                        }
                                        else
                                        {
                                            RecipeManager.AddSpell(this, item, SpellId.CantripDefender4);
                                            Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Defender to the quest item!", ChatMessageType.System));

                                            if (extraChance <= 15)
                                                return ModifyQuestItem(item, 1, 2, true);

                                            return item;
                                        }
                                    }
                                    else
                                    {
                                        RecipeManager.AddSpell(this, item, SpellId.CantripHeartThirst4);
                                        Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Heart Thirst to the quest item!", ChatMessageType.System));

                                        if (extraChance <= 15)
                                            return ModifyQuestItem(item, 1, 2, true);

                                        return item;
                                    }
                                }
                            case 3:// Swift Killer / Hermetic Link

                                if (item.ItemType == ItemType.Caster)
                                {

                                    RecipeManager.AddSpell(this, item, SpellId.CantripHermeticLink4);
                                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Hermetic Link to the quest item!", ChatMessageType.System));

                                    if (extraChance <= 15)
                                        return ModifyQuestItem(item, 1, 2, true);

                                    return item;
                                }
                                else
                                {
                                    RecipeManager.AddSpell(this, item, SpellId.CantripSwiftHunter4);
                                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Swift Hunter to the quest item!", ChatMessageType.System));

                                    if (extraChance <= 15)
                                        return ModifyQuestItem(item, 1, 2, true);

                                    return item;
                                }
                        }
                    }
                    else if (item.ItemType == ItemType.Armor || item.ItemType == ItemType.Clothing)
                    {
                        var selectAttribute = ThreadSafeRandom.Next(1, 6);

                        switch (selectAttribute)
                        {
                            case 1: // strength

                                RecipeManager.AddSpell(this, item, SpellId.CantripStrength4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Strength to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 2: // endurance

                                RecipeManager.AddSpell(this, item, SpellId.CantripEndurance4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Endurance to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 3:// coordination

                                RecipeManager.AddSpell(this, item, SpellId.CantripCoordination4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Coordination to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 4: // quickness

                                RecipeManager.AddSpell(this, item, SpellId.CantripQuickness4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Quickness to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 5: // focus

                                RecipeManager.AddSpell(this, item, SpellId.CantripFocus4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Focus to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                            case 6: // self

                                RecipeManager.AddSpell(this, item, SpellId.CantripWillpower4);

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Willpower to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 2, true);

                                return item;
                        }
                    }
                    break;
                case 5: // Weapon Skill Cantrips & Armor Ratings
                    if (item.ItemType == ItemType.MeleeWeapon || item.ItemType == ItemType.MissileWeapon || item.ItemType == ItemType.Caster)
                    {
                        var selectSkill = ThreadSafeRandom.Next(1, 21);

                        switch (selectSkill)
                        {
                            case 1:

                                RecipeManager.AddSpell(this, item, SpellId.CantripInvulnerability4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Invulnerability to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 2:

                                RecipeManager.AddSpell(this, item, SpellId.CantripMagicResistance4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Magic Resistance to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 3:

                                RecipeManager.AddSpell(this, item, SpellId.CantripCreatureEnchantmentAptitude4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Creature Enchantment to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 4:

                                RecipeManager.AddSpell(this, item, SpellId.CantripLifeMagicAptitude4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Life Magic to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 5:

                                RecipeManager.AddSpell(this, item, SpellId.CantripItemEnchantmentAptitude4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Item Enchantment to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 6:

                                RecipeManager.AddSpell(this, item, SpellId.CantripArcaneProwess4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Arcane Lore to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 7:

                                RecipeManager.AddSpell(this, item, SpellId.CantripManaConversionProwess4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Mana Conversion to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 8:

                                RecipeManager.AddSpell(this, item, SpellId.CantripSummoningProwess4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Summoning to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 9:

                                RecipeManager.AddSpell(this, item, SpellId.CantripVoidMagicAptitude4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Void Magic to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 10:

                                RecipeManager.AddSpell(this, item, SpellId.CantripWarMagicAptitude4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary War Magic to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 11:

                                RecipeManager.AddSpell(this, item, SpellId.CantripHealingProwess4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Healing to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 12:

                                RecipeManager.AddSpell(this, item, SpellId.CantripJumpingProwess4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Jump to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 13:

                                RecipeManager.AddSpell(this, item, SpellId.CantripSprint4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Run to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 14:

                                RecipeManager.AddSpell(this, item, SpellId.CantripDualWieldAptitude4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Dual Wield to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 15:

                                RecipeManager.AddSpell(this, item, SpellId.CantripDirtyFightingProwess4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Dirty Fighting to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 16:

                                RecipeManager.AddSpell(this, item, SpellId.CantripTwoHandedAptitude4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Two Handed Combat to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 17:

                                RecipeManager.AddSpell(this, item, SpellId.CantripRecklessnessProwess4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Recklessness to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 18:

                                RecipeManager.AddSpell(this, item, SpellId.CantripDeceptionProwess4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Deception to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 19:

                                RecipeManager.AddSpell(this, item, SpellId.CantripLeadership4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Leadership to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 20:

                                RecipeManager.AddSpell(this, item, SpellId.CantripSneakAttackProwess4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Sneak Attack to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 21:

                                RecipeManager.AddSpell(this, item, SpellId.CantripFletchingProwess4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Willpower to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 22:

                                RecipeManager.AddSpell(this, item, SpellId.CantripLightWeaponsAptitude4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Light Weapons Aptitude to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 23:

                                RecipeManager.AddSpell(this, item, SpellId.CantripHeavyWeaponsAptitude4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Heavy Weapons Aptitude to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 24:

                                RecipeManager.AddSpell(this, item, SpellId.CantripFinesseWeaponsAptitude4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Finesse Weapons Aptitude to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 25:

                                RecipeManager.AddSpell(this, item, SpellId.CantripMissileWeaponsAptitude4);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Legendary Missile Weapons Aptitude to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                        }
                    }
                    else if (item.ItemType == ItemType.Armor || item.ItemType == ItemType.Clothing)
                    {
                        var selectRating = ThreadSafeRandom.Next(1, 6);
                        var ratingAmount = ThreadSafeRandom.Next(1, 3);

                        switch (selectRating)
                        {
                            case 1:

                                if (item.GearDamage == null)
                                    item.GearDamage = 0;

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                item.SetProperty(PropertyInt.GearDamage, (int)item.GearDamage + ratingAmount);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added {ratingAmount} Damage Rating to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 2:

                                if (item.GearDamageResist == null)
                                    item.GearDamageResist = 0;

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                item.SetProperty(PropertyInt.GearDamageResist, (int)item.GearDamageResist + ratingAmount);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added {ratingAmount} Damage Resist Rating to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 3:

                                if (item.GearCritDamageResist == null)
                                    item.GearCritDamageResist = 0;

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                item.SetProperty(PropertyInt.GearCritDamageResist, (int)item.GearCritDamageResist + ratingAmount);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added {ratingAmount} Crit Damage Resist Rating to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 4:

                                if (item.GearCritDamage == null)
                                    item.GearCritDamage = 0;

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                item.SetProperty(PropertyInt.GearCritDamage, (int)item.GearCritDamage + ratingAmount);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added {ratingAmount} Crit Damage Rating to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 5:

                                if (item.GearCritResist == null)
                                    item.GearCritResist = 0;

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                item.SetProperty(PropertyInt.GearCritResist, (int)item.GearCritResist + ratingAmount);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added {ratingAmount} Crit Resist Rating to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                            case 6:

                                if (item.GearCrit == null)
                                    item.GearCrit = 0;

                                item.SetProperty(PropertyInt.ArmorMana, 100);
                                item.LongDesc = $"Armor Mana: {item.ArmorMana}/100";

                                item.SetProperty(PropertyInt.GearCrit, (int)item.GearCrit + ratingAmount);
                                Session.Network.EnqueueSend(new GameMessageSystemChat($"Added {ratingAmount} Crit Rating to the quest item!", ChatMessageType.System));

                                if (extraChance <= 15)
                                    return ModifyQuestItem(item, 1, 5, true);

                                return item;
                        }
                    }
                    break;
                default:
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"{selectMod}", ChatMessageType.System));
                    return item;
            }
            return item;
        }


        public bool RerollForRend(WorldObject item)
        {
            var rendcount = 0;

            if (item.HasImbuedEffect(ImbuedEffectType.AcidRending))
                rendcount++;
            if (item.HasImbuedEffect(ImbuedEffectType.ColdRending))
                rendcount++;
            if (item.HasImbuedEffect(ImbuedEffectType.ElectricRending))
                rendcount++;
            if (item.HasImbuedEffect(ImbuedEffectType.FireRending))
                rendcount++;
            if (item.HasImbuedEffect(ImbuedEffectType.SlashRending))
                rendcount++;
            if (item.HasImbuedEffect(ImbuedEffectType.PierceRending))
                rendcount++;
            if (item.HasImbuedEffect(ImbuedEffectType.BludgeonRending))
                rendcount++;

            if (rendcount > 0)
                return false;
            else
                return true;
        }

        public bool RerollForSlayer(WorldObject item)
        {
            if (item.SlayerAdded == 0 || item.SlayerAdded == null)
                return true;
            else
                return false;
        }

        public bool RerollForSet(WorldObject item)
        {
            if (item.EquipmentSetId == null)
                return true;
            else
                return false;
        }

        public WorldObject GiveSlayer(WorldObject item)
        {
            var selectSlayerType = ThreadSafeRandom.Next(1, 24);

            switch (selectSlayerType)
            {
                case 1:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Banderling;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 2:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Drudge;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 3:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Eater;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 4:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Fiun;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 5:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Gromnie;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 6:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Lugian;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 7:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Grievver;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 8:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Human;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 9:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Mattekar;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 10:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Mite;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 11:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Mosswart;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 12:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Mumiyah;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 13:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Olthoi;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 14:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.PhyntosWasp;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 15:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Shadow;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 16:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Shreth;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 17:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Skeleton;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 18:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Sleech;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 19:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Tumerok;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 20:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Tusker;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 21:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.ViamontianKnight;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 22:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Virindi;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 23:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Wisp;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
                case 24:
                    item.SlayerCreatureType = ACE.Entity.Enum.CreatureType.Zefir;
                    item.SlayerDamageBonus = 1.20f;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"This quest item was granted {item.SlayerCreatureType} slayer!", ChatMessageType.System));

                    return item;
            }
            return item;
        }

        public WorldObject GiveRend(WorldObject item)
        {
            var rendSelect = ThreadSafeRandom.Next(1, 7);

            switch (rendSelect)
            {
                case 1: // acid

                    RecipeManager.AddImbuedEffect(this, item, ImbuedEffectType.AcidRending);
                    item.W_DamageType = DamageType.Acid;
                    item.SetProperty(PropertyDataId.IconUnderlay, 0x06003355);
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Acid Rending to the quest item!", ChatMessageType.System));

                    return item;
                case 2: // Cold

                    RecipeManager.AddImbuedEffect(this, item, ImbuedEffectType.ColdRending);
                    item.W_DamageType = DamageType.Cold;
                    item.SetProperty(PropertyDataId.IconUnderlay, 0x06003353);
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Cold Rending to the quest item!", ChatMessageType.System));

                    return item;
                case 3: // Electric

                    RecipeManager.AddImbuedEffect(this, item, ImbuedEffectType.ElectricRending);
                    item.W_DamageType = DamageType.Electric;
                    item.SetProperty(PropertyDataId.IconUnderlay, 0x06003354);
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Lightning Rending to the quest item!", ChatMessageType.System));

                    return item;
                case 4: // Fire

                    RecipeManager.AddImbuedEffect(this, item, ImbuedEffectType.FireRending);
                    item.W_DamageType = DamageType.Fire;
                    item.SetProperty(PropertyDataId.IconUnderlay, 0x06003359);
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Fire Rending to the quest item!", ChatMessageType.System));

                    return item;
                case 5: // Pierce

                    RecipeManager.AddImbuedEffect(this, item, ImbuedEffectType.PierceRending);
                    item.W_DamageType = DamageType.Pierce;
                    item.SetProperty(PropertyDataId.IconUnderlay, 0x0600335b);
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Pierce Rend to the quest item!", ChatMessageType.System));

                    return item;
                case 6: // Slash

                    RecipeManager.AddImbuedEffect(this, item, ImbuedEffectType.SlashRending);
                    item.W_DamageType = DamageType.Slash;
                    item.SetProperty(PropertyDataId.IconUnderlay, 0x0600335c);
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Slash Rending to the quest item!", ChatMessageType.System));

                    return item;
                case 7: // Bludgeon

                    RecipeManager.AddImbuedEffect(this, item, ImbuedEffectType.BludgeonRending);
                    item.W_DamageType = DamageType.Bludgeon;
                    item.SetProperty(PropertyDataId.IconUnderlay, 0x0600335a);
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Bludgeon Rending to the quest item!", ChatMessageType.System));

                    return item;
            }

            return item;
        }
        public WorldObject GiveSet(WorldObject item)
        {
            var armorSetSelect = ThreadSafeRandom.Next(1, 9);

            switch (armorSetSelect)
            {
                case 1:
                    item.EquipmentSetId = EquipmentSet.Adepts;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Adept Set to quest item!", ChatMessageType.System));         

                    return item;
                case 2:
                    item.EquipmentSetId = EquipmentSet.Archers;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Archer Set to quest item!", ChatMessageType.System));

                    return item;
                case 3:
                    item.EquipmentSetId = EquipmentSet.Crafters;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Crafters Set to quest item!", ChatMessageType.System));

                    return item;
                case 4:
                    item.EquipmentSetId = EquipmentSet.Defenders;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Defender Set to quest item!", ChatMessageType.System));

                    return item;
                case 5:
                    item.EquipmentSetId = EquipmentSet.Dexterous;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Dexterous Set to quest item!", ChatMessageType.System));

                    return item;
                case 6:
                    item.EquipmentSetId = EquipmentSet.Hardened;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Hardened Set to quest item!", ChatMessageType.System));

                    return item;
                case 7:
                    item.EquipmentSetId = EquipmentSet.Hearty;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Hearty Set to quest item!", ChatMessageType.System));

                    return item;
                case 8:
                    item.EquipmentSetId = EquipmentSet.Interlocking;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Interlocking Set to quest item!", ChatMessageType.System));

                    return item;
                case 9:
                    item.EquipmentSetId = EquipmentSet.Reinforced;
                    Session.Network.EnqueueSend(new GameMessageSystemChat($"Added Reinforced Set to quest item!", ChatMessageType.System));

                    return item;
            }

            return item;
        }
    }
}
