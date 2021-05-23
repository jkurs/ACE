using ACE.Entity.Enum;
using ACE.Server.Entity;
using ACE.Server.Network.GameMessages.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACE.Server.WorldObjects
{
    class Player_GreaterLuminanceAug
    {

        public static void ConfirmAug(bool confirmed, Player player, WorldObject augDevice)
        {
            if (!confirmed && augDevice.WeenieClassId == 6000001)
            {
                var critAugAmount = player.CriticalStrikeAug ?? 0;
                long critcost = 5000000 + (critAugAmount * 1000000);

                if (!player.BankedLuminance.HasValue)
                {
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You need {critcost:N0} Luminance", ChatMessageType.Help));
                    return;
                }

                if (player.BankedLuminance.HasValue)
                {
                    if (player.BankedLuminance < critcost)
                    {
                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You have {player.BankedLuminance:N0} Luminance. You need {critcost:N0}", ChatMessageType.Help));
                        return;
                    }
                    else
                    {
                        var msg = $"Using this will increase your Critical Strike Chance by 0.2%. It costs {critcost:N0} Luminance from your bank. Are you sure?";
                        player.ConfirmationManager.EnqueueSend(new Confirmation_Custom(player.Guid, () => CriticalStrikeAug(player, augDevice)), msg);
                        return;
                    }
                }
            }

            if (!confirmed && augDevice.WeenieClassId == 6000002)
            {
                var critDamageAugAmount = player.CriticalStrikeDamageAug ?? 0;
                long cost = 3000000 + (critDamageAugAmount * 500000);

                if (!player.BankedLuminance.HasValue)
                {
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You need {cost:N0} Luminance", ChatMessageType.Help));
                    return;
                }

                if (player.BankedLuminance.HasValue)
                {
                    if (player.BankedLuminance < cost)
                    {
                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You have {player.BankedLuminance:N0} Luminance. You need {cost:N0}", ChatMessageType.Help));
                        return;
                    }
                    else
                    {
                        var msg = $"Using this will increase your Critical Strike Damage Multiplier by 0.5%. It costs {cost:N0} Luminance from your bank. Are you sure?";
                        player.ConfirmationManager.EnqueueSend(new Confirmation_Custom(player.Guid, () => CriticalStrikeDamageAug(player, augDevice)), msg);
                        return;
                    }
                }
            }            

            if (!confirmed && augDevice.WeenieClassId == 6000003)
            {
                var compBurnAugAmount = player.CompBurnAug ?? 0;
                long cost = 500000 + (compBurnAugAmount * 250000);

                if (player.CompBurnAug.HasValue)
                {
                    if (player.CompBurnAug >= 100)
                    {
                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You have reached the maximum amount of times you may use this augmentation.", ChatMessageType.Help));
                        return;
                    }
                }

                if (!player.BankedLuminance.HasValue)
                {
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You need {cost:N0} Luminance", ChatMessageType.Help));
                    return;
                }

                if (player.BankedLuminance.HasValue)
                {
                    if (player.BankedLuminance < cost)
                    {
                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You have {player.BankedLuminance:N0} Luminance. You need {cost:N0}", ChatMessageType.Help));
                        return;
                    }
                    else
                    {
                        var msg = $"Using this will increase your chance to avoid burning spell components by 1%. It costs {cost:N0} Luminance from your bank. Are you sure?";
                        player.ConfirmationManager.EnqueueSend(new Confirmation_Custom(player.Guid, () => CompBurnAug(player, augDevice)), msg);
                        return;
                    }
                }
            }

            if (!confirmed && augDevice.WeenieClassId == 6000004)
            {
                var missileConsumeAugAmount = player.MissileConsumeAug ?? 0;
                long cost = 500000 + (missileConsumeAugAmount * 250000);

                if (!player.BankedLuminance.HasValue)
                {
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You need {cost:N0} Luminance", ChatMessageType.Help));
                    return;
                }

                if (player.BankedLuminance.HasValue)
                {
                    if (player.BankedLuminance < cost)
                    {
                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You have {player.BankedLuminance:N0} Luminance. You need {cost:N0}", ChatMessageType.Help));
                        return;
                    }
                    else
                    {
                        var msg = $"Using this will increase your chance to avoid consuming missile ammunition by 1%. It costs {cost:N0} Luminance from your bank. Are you sure?";
                        player.ConfirmationManager.EnqueueSend(new Confirmation_Custom(player.Guid, () => MissileConsumeAug(player, augDevice)), msg);
                        return;
                    }
                }
            }

            if (!confirmed && augDevice.WeenieClassId == 6000005)
            {
                var spellDurationAugAmount = player.SpellDurationAug ?? 0;
                long cost = 100000 + (spellDurationAugAmount * 250000);

                if (!player.BankedLuminance.HasValue)
                {
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You need {cost:N0} Luminance", ChatMessageType.Help));
                    return;
                }

                if (player.BankedLuminance.HasValue)
                {
                    if (player.BankedLuminance < cost)
                    {
                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You have {player.BankedLuminance:N0} Luminance. You need {cost:N0}", ChatMessageType.Help));
                        return;
                    }
                    else
                    {
                        var msg = $"Using this will increase your spell durations by 20%. It costs {cost:N0} Luminance from your bank. Are you sure?";
                        player.ConfirmationManager.EnqueueSend(new Confirmation_Custom(player.Guid, () => SpellDurationAug(player, augDevice)), msg);
                        return;
                    }
                }
            }

            if (!confirmed && augDevice.WeenieClassId == 6000006)
            {
                var vitalityAugAmount = player.VitalityAug ?? 0;
                long cost = 100000 + (vitalityAugAmount * 250000);

                if (!player.BankedLuminance.HasValue)
                {
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You need {cost:N0} Luminance", ChatMessageType.Help));
                    return;
                }

                if (player.BankedLuminance.HasValue)
                {
                    if (player.BankedLuminance < cost)
                    {
                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You have {player.BankedLuminance:N0} Luminance. You need {cost:N0}", ChatMessageType.Help));
                        return;
                    }
                    else
                    {
                        var msg = $"Using this will increase your vitality by 2. It costs {cost:N0} Luminance from your bank. Are you sure?";
                        player.ConfirmationManager.EnqueueSend(new Confirmation_Custom(player.Guid, () => VitalityAug(player, augDevice)), msg);
                        return;
                    }
                }
            }

            if (!confirmed && augDevice.WeenieClassId == 6000007)
            {
                var armorManaAugAmount = player.ArmorManaAug ?? 0;
                long cost = 300000 + (armorManaAugAmount * 300000);

                if (!player.BankedLuminance.HasValue)
                {
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You need {cost:N0} Luminance", ChatMessageType.Help));
                    return;
                }

                if (player.BankedLuminance.HasValue)
                {
                    if (player.BankedLuminance < cost)
                    {
                        player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You have {player.BankedLuminance:N0} Luminance. You need {cost:N0}", ChatMessageType.Help));
                        return;
                    }
                    else
                    {
                        var msg = $"Using this will reduce the chance your armor reduces its Armor Mana from combat by 1%. It costs {cost:N0} Luminance from your bank. Are you sure?";
                        player.ConfirmationManager.EnqueueSend(new Confirmation_Custom(player.Guid, () => ArmorManaAug(player, augDevice)), msg);
                        return;
                    }
                }
            }
        }

        public static void CriticalStrikeAug(Player player, WorldObject augDevice)
        {
            var critAugAmount = player.CriticalStrikeAug ?? 0;
            long cost = 3000000 + (critAugAmount * 800000);

            if (player.BankedLuminance < cost)
            {
                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You have {player.BankedLuminance:N0} Luminance. You need {cost:N0}", ChatMessageType.Help));
                return;
            }

            player.TryConsumeFromInventoryWithNetworking(augDevice, 1);
            player.BankedLuminance -= cost;

            if (!player.CriticalStrikeAug.HasValue)
                player.CriticalStrikeAug = 0;

            player.CriticalStrikeAug++;

            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You have augmented your ability to critical strike!", ChatMessageType.Broadcast));
            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] {player.BankedLuminance:N0} Luminance. (-{cost:N0})", ChatMessageType.x1B));
        }

        public static void CriticalStrikeDamageAug(Player player, WorldObject augDevice)
        {
            var critDamageAugAmount = player.CriticalStrikeDamageAug ?? 0;
            long cost = 2000000 + (critDamageAugAmount * 750000);

            if (player.BankedLuminance < cost)
            {
                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You have {player.BankedLuminance:N0} Luminance. You need {cost:N0}", ChatMessageType.Help));
                return;
            }

            player.TryConsumeFromInventoryWithNetworking(augDevice, 1);
            player.BankedLuminance -= cost;

            if (!player.CriticalStrikeDamageAug.HasValue)
                player.CriticalStrikeDamageAug = 0;

            player.CriticalStrikeDamageAug++;

            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You have augmented your ability to deal more damage with critical strikes!", ChatMessageType.Broadcast));
            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] {player.BankedLuminance:N0} Luminance. (-{cost:N0})", ChatMessageType.x1B));
        }

        public static void CompBurnAug(Player player, WorldObject augDevice)
        {
            var compBurnAugAmount = player.CompBurnAug ?? 0;
            long cost = 500000 + (compBurnAugAmount * 250000);

            if (player.CompBurnAug.HasValue)
            {
                if (player.CompBurnAug >= 100)
                {
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You have reached the maximum amount of times you may use this augmentation.", ChatMessageType.Help));
                    return;
                }
            }

            if (player.BankedLuminance < cost)
            {
                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You have {player.BankedLuminance:N0} Luminance. You need {cost:N0}", ChatMessageType.Help));
                return;
            }

            player.TryConsumeFromInventoryWithNetworking(augDevice, 1);
            player.BankedLuminance -= cost;

            if (!player.CompBurnAug.HasValue)
                player.CompBurnAug = 0;

            player.CompBurnAug++;

            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You have augmented your ability to avoid burning spell components!", ChatMessageType.Broadcast));
            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] {player.BankedLuminance:N0} Luminance. (-{cost:N0})", ChatMessageType.x1B));
        }

        public static void MissileConsumeAug(Player player, WorldObject augDevice)
        {
            var missileConsumeAugAmount = player.MissileConsumeAug ?? 0;
            long cost = 500000 + (missileConsumeAugAmount * 250000);

            if (player.MissileConsumeAug.HasValue)
            {
                if (player.MissileConsumeAug >= 100)
                {
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You have reached the maximum amount of times you may use this augmentation.", ChatMessageType.Help));
                    return;
                }
            }

            if (player.BankedLuminance < cost)
            {
                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You have {player.BankedLuminance:N0} Luminance. You need {cost:N0}", ChatMessageType.Help));
                return;
            }

            player.TryConsumeFromInventoryWithNetworking(augDevice, 1);
            player.BankedLuminance -= cost;

            if (!player.MissileConsumeAug.HasValue)
                player.MissileConsumeAug = 0;

            player.MissileConsumeAug++;

            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You have augmented your ability to avoid consuming projectile ammunition!", ChatMessageType.Broadcast));
            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] {player.BankedLuminance:N0} Luminance. (-{cost:N0})", ChatMessageType.x1B));
        }

        public static void SpellDurationAug(Player player, WorldObject augDevice)
        {
            var spellDurationAugAmount = player.SpellDurationAug ?? 0;
            long cost = 100000 + (spellDurationAugAmount * 250000);

            if (player.BankedLuminance < cost)
            {
                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You have {player.BankedLuminance:N0} Luminance. You need {cost:N0}", ChatMessageType.Help));
                return;
            }

            player.TryConsumeFromInventoryWithNetworking(augDevice, 1);
            player.BankedLuminance -= cost;

            if (!player.SpellDurationAug.HasValue)
                player.SpellDurationAug = 0;

            player.SpellDurationAug++;

            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You have augmented your spell durations!", ChatMessageType.Broadcast));
            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] {player.BankedLuminance:N0} Luminance. (-{cost:N0})", ChatMessageType.x1B));
        }

        public static void VitalityAug(Player player, WorldObject augDevice)
        {
            var vitaltiyAugAmount = player.VitalityAug ?? 0;
            long cost = 500000 + (vitaltiyAugAmount * 250000);

            if (player.BankedLuminance < cost)
            {
                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You have {player.BankedLuminance:N0} Luminance. You need {cost:N0}", ChatMessageType.Help));
                return;
            }

            player.TryConsumeFromInventoryWithNetworking(augDevice, 1);
            player.BankedLuminance -= cost;

            if (!player.VitalityAug.HasValue)
                player.VitalityAug = 0;

            player.VitalityAug++;            

            player.HandleMaxHealthUpdate();

            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You have augmented your vitality!", ChatMessageType.Broadcast));
            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] {player.BankedLuminance:N0} Luminance. (-{cost:N0})", ChatMessageType.x1B));
        }

        public static void ArmorManaAug(Player player, WorldObject augDevice)
        {
            var armorManaAugAmount = player.ArmorManaAug ?? 0;
            long cost = 300000 + (armorManaAugAmount * 300000);

            if (player.BankedLuminance < cost)
            {
                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You do not have enough Luminance in your bank. You have {player.BankedLuminance:N0} Luminance. You need {cost:N0}", ChatMessageType.Help));
                return;
            }

            player.TryConsumeFromInventoryWithNetworking(augDevice, 1);
            player.BankedLuminance -= cost;

            if (!player.ArmorManaAug.HasValue)
                player.ArmorManaAug = 0;

            player.ArmorManaAug++;

            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] You have augmented your ability to avoid consuming Armor Mana from items.", ChatMessageType.Broadcast));
            player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[AUG] {player.BankedLuminance:N0} Luminance. (-{cost:N0})", ChatMessageType.x1B));
        }



    }
}
