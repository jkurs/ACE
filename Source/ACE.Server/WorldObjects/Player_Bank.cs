using ACE.Common;
using ACE.Entity.Enum;
using ACE.Entity.Enum.Properties;
using ACE.Server.Factories;
using ACE.Server.Managers;
using ACE.Server.Network.GameMessages.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACE.Server.WorldObjects
{
    class Player_Bank
    {
        public static void GenerateAccountNumber(Player player)
        {
           var generatedNumber = ThreadSafeRandom.Next(000000000, 999999999);

            if (VerifyNumber(player, generatedNumber))
            {
                player.BankAccountNumber = generatedNumber;
                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Your account number is {generatedNumber}", ChatMessageType.x1B));
            }
            else
                player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Failed to create your account, please reissue the command.", ChatMessageType.x1B));
        }

        public static bool VerifyNumber(Player player, int generatedNumber)
        {
            var allplayers = PlayerManager.GetAllPlayers();

            foreach(var character in allplayers)
            {
                if (character.BankAccountNumber != null)
                {
                    if (character.BankAccountNumber == generatedNumber)
                        return false;
                }
            }

            return true;
        }


        public static void Deposit(Player player, long amount, bool all, bool pyreal = true)
        {
            if (player != null)
            {

                var mmd = player.GetInventoryItemsOfWCID(20630);
                var pyreals = player.GetInventoryItemsOfWCID(273);
                long totalValue = 0;
                long inheritedValue = 0;
                long lumInheritedValue = 0;
                long oldBalanceP = (long)player.BankedPyreals;
                long oldBalanceL = (long)player.BankedLuminance;

                if (all)
                {
                    if (mmd == null)
                        return;

                    foreach (var item in mmd)
                    {
                        if (item == null)
                            continue;

                        if (item.StackSize > 0)
                            totalValue = (long)item.StackSize * 250000;
                        else
                            totalValue = 250000;

                        player.TryConsumeFromInventoryWithNetworking(20630);

                        if (!player.BankedPyreals.HasValue)
                            player.BankedPyreals = 0;

                        player.BankedPyreals += totalValue;

                        inheritedValue += totalValue;
                    }

                    foreach (var item in pyreals)
                    {
                        if (item != null)
                        {
                            totalValue = (long)item.StackSize;

                            player.TryConsumeFromInventoryWithNetworking(273);

                            if (!player.BankedPyreals.HasValue)
                                player.BankedPyreals = 0;

                            player.BankedPyreals += totalValue;

                            inheritedValue += totalValue;
                        }
                    }

                    if (player.AvailableLuminance > 0)
                    {
                        player.BankedLuminance += player.AvailableLuminance;
                        lumInheritedValue += (long)player.AvailableLuminance;
                        player.AvailableLuminance = 0;
                        player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt64(player, PropertyInt64.AvailableLuminance, player.AvailableLuminance ?? 0));
                    }

                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You banked a total of {inheritedValue:N0} Pyreals and {lumInheritedValue:N0} Luminance", ChatMessageType.x1D));
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Old Account Balances: {oldBalanceP:N0} Pyreals || {oldBalanceL:N0} Luminance", ChatMessageType.Help));
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] New Account Balances: {player.BankedPyreals:N0} Pyreals || {player.BankedLuminance:N0} Luminance", ChatMessageType.x1B));
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                }

                if (!all && pyreal)
                {
                    long amountDeposited = 0;

                    for (var i = amount; i >= 25000; i -= 25000)
                    {
                        amount -= 25000;
                        player.TryConsumeFromInventoryWithNetworking(273, 25000);
                        player.BankedPyreals += 25000;
                        amountDeposited += 25000;
                    }

                    if (amount < 25000)
                    {
                        player.TryConsumeFromInventoryWithNetworking(273, (int)amount);
                        player.BankedPyreals += amount;
                        amountDeposited += amount;
                    }

                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You banked {amountDeposited:N0} Pyreals", ChatMessageType.x1D));
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Old Account Balance: {oldBalanceP:N0} Pyreals", ChatMessageType.Help));
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] New Account Balance: {player.BankedPyreals:N0} Pyreals", ChatMessageType.x1B));
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                }

                if (!all && !pyreal)
                {
                    long amountDeposited = 0;

                    player.BankedLuminance += amount;
                    amountDeposited += amount;
                    player.AvailableLuminance -= amount;
                    player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt64(player, PropertyInt64.AvailableLuminance, player.AvailableLuminance ?? 0));

                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You banked {amountDeposited:N0} Luminance", ChatMessageType.x1D));
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Old Account Balance: {oldBalanceL:N0} Luminance", ChatMessageType.Help));
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] New Account Balance: {player.BankedLuminance:N0} Luminance", ChatMessageType.x1B));
                    player.Session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                }
            }
            else
                return;
        }

        public static void Send(Player player, int bankAccountNumber)
        {





        }


    }
}
