using System;
using System.Collections.Generic;

using log4net;

using ACE.Common;
using ACE.Database;
using ACE.Entity;
using ACE.Entity.Enum;
using ACE.Server.Entity;
using ACE.Server.Entity.Actions;
using ACE.Server.Managers;
using ACE.Server.Network;
using ACE.Server.Network.GameEvent.Events;
using ACE.Server.Network.GameMessages.Messages;
using ACE.Server.WorldObjects;
using ACE.Entity.Enum.Properties;
using ACE.DatLoader;
using ACE.Server.Factories;

namespace ACE.Server.Command.Handlers
{
    public static class PlayerCommands
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // pop
        [CommandHandler("pop", AccessLevel.Player, CommandHandlerFlag.None, 0,
            "Show current world population",
            "")]
        public static void HandlePop(Session session, params string[] parameters)
        {
            CommandHandlerHelper.WriteOutputInfo(session, $"Current world population: {PlayerManager.GetOnlineCount():N0}", ChatMessageType.Broadcast);
        }

        // hardmode
        [CommandHandler("hardmode", AccessLevel.Player, CommandHandlerFlag.None, 0,
            "Enables Hardmode",
            "")]
        public static void HandleHardmode(Session session, params string[] parameters)
        {
            if (!session.Player.HardMode)
                HandleHardMode(session, false);
            else
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"You're already in Hardmode", ChatMessageType.Broadcast));
                return;
            }
        }

        public static void HandleHardMode(Session session, bool confirmed)
        {
            if (!confirmed)
            {
                var msg = $"WARNING!!!!!! This action is irreversible!! You will be returned to level 1, lose ALL your enlightenment levels (you will keep your achievements), and earn 75% less XP/Lum from ALL sources, this includes allegiance passup AND fellowships. Monsters will deal 6.5x Physical damage to you and 6.2x magic damage to you. This is intended to provide an even harsher and difficult experience! !!DO NOT DO THIS UNLESS YOU ARE ABSOLUTELY SURE!!";
                session.Player.ConfirmationManager.EnqueueSend(new Confirmation_Custom(session.Player.Guid, () => HandleHardMode(session, true)), msg);
                return;
            }
            session.Player.SetProperty(PropertyBool.HardModeFirst, true);
            session.Player.SetProperty(PropertyBool.HardMode, true);
            Enlightenment.HandleEnlightenment(session.Player, session.Player);
            session.Player.Enlightenment = 0;
            session.Network.EnqueueSend(new GameMessageSystemChat($"[HARDMODE] You have entered Hardmode. Goodluck, don't expect it to be easy!", ChatMessageType.Broadcast));
        }

        [CommandHandler("bank", AccessLevel.Player, CommandHandlerFlag.None,
            "Handles all Bank operations.",
            "")]
        public static void HandleBank(Session session, params string[] parameters)
        {

            if (parameters.Length == 0)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] To use The Bank of Dereth you must input one of the commands listed below into the chatbox. When you first use any command correctly, you will receive a bank account number.", ChatMessageType.System));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You may access your account number at any time to give to others so that they may send you pyreals or luminance.", ChatMessageType.System));
                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] How to use The Bank of Dereth!", ChatMessageType.System));
                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank account - Shows your account number and account balances.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank send ACCOUNT# pyreals ### - Attempts to send an amount of pyreals to another account number.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank send ACCOUNT# luminance ### - Attempts to send an amount of luminance to another account number.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank deposit all - Attempts to deposit all pyreals, MMD's(converts to pyreals), and luminance from your character into your bank.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank deposit pyreals ### - Attempts to deposit the specified amount of pyreals into your pyreal bank.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank deposit luminance ### - Attempts to deposit the specified amount of luminance into your luminance bank.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank withdraw luminance ### - Attempts to withdraw the specified amount of luminance from your bank to your character.", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] /bank withdraw pyreals ### - Attempts to withdraw the specified amount of pyreals from your bank to your inventory. ", ChatMessageType.x1B));
                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
            }
            else
            {
                if (session.Player.BankAccountNumber == null)
                {
                    session.Player.BankedLuminance = 0;
                    session.Player.BankedPyreals = 0;

                    var bankAccountCreation = new ActionChain();
                    bankAccountCreation.AddDelaySeconds(2);

                    bankAccountCreation.AddAction(WorldManager.ActionQueue, () =>
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Looks like you don't have an account, don't worry here in Dereth we give everyone a free checking account for all your needs!", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Creating your personal bank account number...", ChatMessageType.Broadcast));
                        Player_Bank.GenerateAccountNumber(session.Player);
                    });

                    bankAccountCreation.EnqueueChain();
                }
                else
                {
                    if (parameters[0].Equals("account", StringComparison.OrdinalIgnoreCase))
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Account Number: {session.Player.BankAccountNumber}", ChatMessageType.x1B));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Account Balances: {session.Player.BankedPyreals:N0} Pyreals || {session.Player.BankedLuminance:N0} Luminance", ChatMessageType.x1B));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                        return;
                    }

                    if (parameters[0].Equals("send", StringComparison.OrdinalIgnoreCase))
                    {
                        bool accountFound = false;
                        long amountSent = 0;
                        int.TryParse(parameters[1], out int account);
                        Int64.TryParse(parameters[3], out long amt);

                        if (parameters.Length < 3)
                        {
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] ERROR: Expected more parameters. Please make sure you have all the required fields for the /bank send command.", ChatMessageType.Help));
                            return;
                        }

                        if (parameters[2].Equals("pyreals", StringComparison.OrdinalIgnoreCase))
                        {

                            var players = PlayerManager.GetAllPlayers();

                            foreach (var player in players)
                            {
                                if (account == session.Player.BankAccountNumber)
                                    continue;

                                if (account == player.BankAccountNumber)
                                {
                                    if (amt > session.Player.BankedPyreals)
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pyreals in your bank to send {player.Name} that amount.", ChatMessageType.Help));
                                        return;
                                    }
                                    else
                                    {
                                        amountSent += amt;
                                        accountFound = true;
                                        session.Player.BankedPyreals -= amt;
                                        player.BankedPyreals += amt;
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You sent {player.Name} {amt:N0} Pyreals.", ChatMessageType.x1B));
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Your New Account Balance: {session.Player.BankedPyreals:N0} Pyreals", ChatMessageType.x1B));
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));

                                        var isOnline = PlayerManager.GetOnlinePlayer(player.Guid.Full);

                                        if (isOnline != null)
                                            isOnline.Session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK TRANSACTION] {session.Player.Name} sent you {amountSent:N0} Pyreals", ChatMessageType.x1B));

                                        break;
                                    }
                                }
                                else
                                    accountFound = false;
                            }

                            if (!accountFound)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] ERROR: Account number {account} does not exist.", ChatMessageType.Help));
                                return;
                            }
                        }
                        else if (parameters[2].Equals("luminance", StringComparison.OrdinalIgnoreCase))
                        {
                            var players = PlayerManager.GetAllPlayers();

                            foreach (var player in players)
                            {
                                if (account == session.Player.BankAccountNumber)
                                    continue;

                                if (account == player.BankAccountNumber)
                                {
                                    if (amt > session.Player.BankedLuminance)
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough luminance in your bank to send {player.Name} that amount.", ChatMessageType.Help));
                                        return;
                                    }
                                    else
                                    {
                                        amountSent += amt;
                                        accountFound = true;
                                        session.Player.BankedLuminance -= amt;
                                        player.BankedLuminance += amt;
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You sent {player.Name} {amt:N0} Luminance.", ChatMessageType.x1B));
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Your New Account Balance: {session.Player.BankedLuminance:N0} Luminanace", ChatMessageType.x1B));
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));

                                        var isOnline = PlayerManager.GetOnlinePlayer(player.Guid.Full);

                                        if (isOnline != null)
                                            isOnline.Session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK TRANSACTION] {session.Player.Name} sent you {amountSent:N0} Luminance", ChatMessageType.x1B));

                                        break;
                                    }
                                }
                                else
                                    accountFound = false;
                            }

                            if (!accountFound)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] ERROR: Account Number {account} does not exist.", ChatMessageType.Help));
                                return;
                            }
                        }
                        else
                        {
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] ERROR: Please specify whether you are sending luminance or pyreals.", ChatMessageType.x1B));
                            return;
                        }
                    }


                    if (parameters[0].Equals("deposit", StringComparison.OrdinalIgnoreCase))
                    {

                        if (parameters[1].Equals("all", StringComparison.OrdinalIgnoreCase))
                        {
                            if (session.Player.BankCommandTimer.HasValue)
                            {
                                if (Time.GetUnixTime() >= session.Player.BankCommandTimer)
                                {
                                    session.Player.RemoveProperty(PropertyFloat.BankCommandTimer);
                                }
                                else
                                {
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You have used this command too recently.", ChatMessageType.Help));
                                    return;
                                }
                            }

                            session.Player.SetProperty(PropertyFloat.BankCommandTimer, Time.GetFutureUnixTime(10));
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Attempting to Deposit your Pyreals and Luminance into your bank...", ChatMessageType.Broadcast));

                            var bankAccountDeposit = new ActionChain();
                            bankAccountDeposit.AddDelaySeconds(1);

                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Contacting your local Bank of Dereth representative...", ChatMessageType.Broadcast));
                            });
                            bankAccountDeposit.AddDelaySeconds(1);
                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Giving security details...", ChatMessageType.Broadcast));
                            });
                            bankAccountDeposit.AddDelaySeconds(1);
                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Processing done!", ChatMessageType.Broadcast));
                                Player_Bank.Deposit(session.Player, 0, true);
                            });

                            bankAccountDeposit.EnqueueChain();
                            return;
                        }

                        if (parameters[1].Equals("pyreals", StringComparison.OrdinalIgnoreCase))
                        {
                            if (session.Player.BankCommandTimer.HasValue)
                            {
                                if (Time.GetUnixTime() >= session.Player.BankCommandTimer)
                                {
                                    session.Player.RemoveProperty(PropertyFloat.BankCommandTimer);
                                }
                                else
                                {
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You have used this command too recently.", ChatMessageType.Help));
                                    return;
                                }
                            }

                            Int64.TryParse(parameters[2], out long amt);

                            if (amt <= 0)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You did not enter a valid amount to deposit.", ChatMessageType.Help));
                                return;
                            }

                            var pyreals = session.Player.GetInventoryItemsOfWCID(273);
                            long availablePyreals = 0;

                            foreach (var item in pyreals)
                                availablePyreals += (long)item.StackSize;

                            if (amt > availablePyreals)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pyreals in your inventory to deposit that amount.", ChatMessageType.Help));
                                return;
                            }

                            session.Player.SetProperty(PropertyFloat.BankCommandTimer, Time.GetFutureUnixTime(10));
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Attempting to Deposit {amt:N0} Pyreals into your bank...", ChatMessageType.Broadcast));

                            var bankAccountDeposit = new ActionChain();
                            bankAccountDeposit.AddDelaySeconds(1);

                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Contacting your local Bank of Dereth representative...", ChatMessageType.Broadcast));
                            });
                            bankAccountDeposit.AddDelaySeconds(1);
                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Giving security details...", ChatMessageType.Broadcast));
                            });
                            bankAccountDeposit.AddDelaySeconds(1);
                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Processing done!", ChatMessageType.Broadcast));
                                Player_Bank.Deposit(session.Player, amt, false);
                            });

                            bankAccountDeposit.EnqueueChain();
                            return;
                        }

                        if (parameters[1].Equals("luminance", StringComparison.OrdinalIgnoreCase))
                        {
                            if (session.Player.BankCommandTimer.HasValue)
                            {
                                if (Time.GetUnixTime() >= session.Player.BankCommandTimer)
                                {
                                    session.Player.RemoveProperty(PropertyFloat.BankCommandTimer);
                                }
                                else
                                {
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You have used this command too recently.", ChatMessageType.Help));
                                    return;
                                }
                            }

                            Int64.TryParse(parameters[2], out long amt);

                            if (amt <= 0)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You did not enter a valid amount to deposit.", ChatMessageType.Help));
                                return;
                            }

                            var available = session.Player.AvailableLuminance ?? 0;
                            var maximum = session.Player.MaximumLuminance ?? 0;
                            var remaining = maximum - available;

                            if (amt > available)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough Luminance to deposit that amount.", ChatMessageType.Help));
                                return;
                            }

                            session.Player.SetProperty(PropertyFloat.BankCommandTimer, Time.GetFutureUnixTime(10));
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Attempting to Deposit {amt:N0} Luminance into your bank...", ChatMessageType.Broadcast));

                            var bankAccountDeposit = new ActionChain();
                            bankAccountDeposit.AddDelaySeconds(1);

                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Contacting your local Bank of Dereth representative...", ChatMessageType.Broadcast));
                            });
                            bankAccountDeposit.AddDelaySeconds(1);
                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Giving security details...", ChatMessageType.Broadcast));
                            });
                            bankAccountDeposit.AddDelaySeconds(1);
                            bankAccountDeposit.AddAction(WorldManager.ActionQueue, () =>
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] Processing done!", ChatMessageType.Broadcast));
                                Player_Bank.Deposit(session.Player, amt, false, false);
                            });

                            bankAccountDeposit.EnqueueChain();
                            return;
                        }
                    }

                    if (parameters[0].Equals("withdraw", StringComparison.OrdinalIgnoreCase) && (parameters[1].Equals("luminance", StringComparison.OrdinalIgnoreCase) || parameters[1].Equals("pyreals", StringComparison.OrdinalIgnoreCase)))
                    {
                        if (session.Player.BankCommandTimer.HasValue)
                        {
                            if (Time.GetUnixTime() >= session.Player.BankCommandTimer)
                            {
                                session.Player.RemoveProperty(PropertyFloat.BankCommandTimer);
                            }
                            else
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You have used this command too recently.", ChatMessageType.Help));
                                return;
                            }
                        }

                        Int64.TryParse(parameters[2], out long amt);

                        if (amt <= 0)
                        {
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You did not enter a valid amount to withdraw.", ChatMessageType.Broadcast));
                            return;
                        }

                        if (parameters[1].Equals("pyreals", StringComparison.OrdinalIgnoreCase))
                        {
                            if (amt > session.Player.BankedPyreals)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pyreals to withdraw that amount from your bank. You have {session.Player.BankedPyreals:N0} Pyreals in your bank.", ChatMessageType.Help));
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You requested {amt:N0}.", ChatMessageType.Broadcast));
                                return;
                            }
                            else
                            {
                                session.Player.SetProperty(PropertyFloat.BankCommandTimer, Time.GetFutureUnixTime(10));
                                long amountWithdrawn = 0;


                                if (amt >= 250000)
                                {
                                    var mmd = WorldObjectFactory.CreateNewWorldObject(20630);
                                    var mmds = amt / 250000f;
                                    mmd.SetStackSize((int)mmds);

                                    if (session.Player.GetFreeInventorySlots(true) < 10 || !session.Player.HasEnoughBurdenToAddToInventory(mmd))
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pack space or you are overburdened.", ChatMessageType.Broadcast));
                                        return;
                                    }

                                    amt -= (long)mmds * 250000;
                                    amountWithdrawn += (long)mmds * 250000;
                                    session.Player.TryCreateInInventoryWithNetworking(mmd);
                                    session.Player.BankedPyreals -= (long)mmds * 250000;
                                    session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You withdrew {Math.Floor(mmds)} MMDS.", ChatMessageType.Broadcast));
                                }

                                for (var i = amt; i >= 25000; i -= 25000)
                                {
                                    var pyreals = WorldObjectFactory.CreateNewWorldObject(273);
                                    pyreals.SetStackSize(25000);

                                    if (session.Player.GetFreeInventorySlots(true) < 10 || !session.Player.HasEnoughBurdenToAddToInventory(pyreals))
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pack space or you are overburdened.", ChatMessageType.Broadcast));
                                        break;
                                    }

                                    amt -= 25000;

                                    
                                    session.Player.TryCreateInInventoryWithNetworking(pyreals);

                                    session.Player.BankedPyreals -= pyreals.StackSize;
                                    amountWithdrawn += 25000;
                                }

                                if (amt < 25000 && amt > 0)
                                {
                                    var pyreals = WorldObjectFactory.CreateNewWorldObject(273);
                                    pyreals.SetStackSize((int)amt);

                                    if (session.Player.GetFreeInventorySlots(true) < 10 || !session.Player.HasEnoughBurdenToAddToInventory(pyreals))
                                    {
                                        session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough pack space or you are overburdened.", ChatMessageType.Broadcast));
                                        return;
                                    }
                                    
                                    session.Player.TryCreateInInventoryWithNetworking(pyreals);

                                    session.Player.BankedPyreals -= amt;
                                    amountWithdrawn += amt;
                                }                                

                                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You withdrew some pyreals from your bank account. (-{amountWithdrawn:N0})", ChatMessageType.x1B));
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] New Account Balances: {session.Player.BankedPyreals:N0} Pyreals || {session.Player.BankedLuminance:N0} Luminance", ChatMessageType.x1B));
                                session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                            }
                        }

                        if (parameters[1].Equals("luminance", StringComparison.OrdinalIgnoreCase))
                        {
                            
                            Int64.TryParse(parameters[2], out long amt2);

                            if (amt2 <= 0)
                                return;

                            if (amt2 > session.Player.BankedLuminance)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You do not have enough Luminance to withdraw that amount from your bank. You have {session.Player.BankedLuminance:N0} Luminance in your bank.", ChatMessageType.Help));
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You requested {amt2:N0}.", ChatMessageType.Broadcast));
                                return;
                            }

                            var available = session.Player.AvailableLuminance ?? 0;
                            var maximum = session.Player.MaximumLuminance ?? 0;
                            var remaining = maximum - available;

                            if (available == maximum)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You cannot withdraw that much Luminance because you cannot hold that much.", ChatMessageType.Help));
                                return;
                            }

                            if (amt2 > remaining)
                            {
                                session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You cannot withdraw that much Luminance because you cannot hold that much.", ChatMessageType.Help));
                                return;
                            }
                            session.Player.SetProperty(PropertyFloat.BankCommandTimer, Time.GetFutureUnixTime(10));
                            session.Player.GrantLuminance(amt2, XpType.Admin, ShareType.None);
                            session.Player.BankedLuminance -= amt2;
                            session.Player.Session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt64(session.Player, PropertyInt64.AvailableLuminance, session.Player.AvailableLuminance ?? 0));

                            session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] You withdrew some Luminance from your bank account. (-{amt2:N0})", ChatMessageType.x1B));
                            session.Network.EnqueueSend(new GameMessageSystemChat($"[BANK] New Account Balances: {session.Player.BankedPyreals:N0} Pyreals || {session.Player.BankedLuminance:N0} Luminance", ChatMessageType.x1B));
                            session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
                        }
                    }
                }
            }
        }

        [CommandHandler("checkxp", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 0)]
        public static void HandleCheckXp(Session session, params string[] parameters)
        {
            if (session.Player.Level >= 275)
            {
                var currentxp = session.Player.TotalXpBeyond;

                var currentremaining = currentxp - session.Player.TotalExperience;

                session.Network.EnqueueSend(new GameMessageSystemChat($"You need {currentremaining:N0}xp to reach level {session.Player.Level + 1}. Required total xp is {currentxp:N0}", ChatMessageType.Broadcast));
            }
            else
                return;         
        }

        [CommandHandler("achev", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 0)]
        public static void HandleCheckAchievement(Session session, params string[] parameters)
        {

            session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));
            session.Network.EnqueueSend(new GameMessageSystemChat($"The list of possible Achievements and their tiers are as follows: *Off White -> Earned, *Red -> Not Earned", ChatMessageType.Broadcast));
            
            session.Network.EnqueueSend(new GameMessageSystemChat($"Health Total:", ChatMessageType.System));

            if (session.Player.HP1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 500 Health", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 500 Health", ChatMessageType.Help));

            if (session.Player.HP2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 1,000 Health", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 1,000 Health", ChatMessageType.Help));

            if (session.Player.HP3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 3,000 Health", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 3,000 Health", ChatMessageType.Help));

            if (session.Player.HP4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 6,000 Health", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 6,000 Health", ChatMessageType.Help));

            if (session.Player.HP5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 8,000 Health", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 8,000 Health", ChatMessageType.Help));

            if (session.Player.HP6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 10,000 Health", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 10,000 Health", ChatMessageType.Help));

            session.Network.EnqueueSend(new GameMessageSystemChat($"Stamina Total:", ChatMessageType.System));

            if (session.Player.ST1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 1,000 Stamina", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 1,000 Stamina", ChatMessageType.Help));

            if (session.Player.ST2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 2,000 Stamina", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 2,000 Stamina", ChatMessageType.Help));

            if (session.Player.ST3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 4,000 Stamina", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 4,000 Stamina", ChatMessageType.Help));

            if (session.Player.ST4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 6,000 Stamina", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 6,000 Stamina", ChatMessageType.Help));

            if (session.Player.ST5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 12,000 Stamina", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 12,000 Stamina", ChatMessageType.Help));

            if (session.Player.ST6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 24,300 Stamina", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 24,300 Stamina", ChatMessageType.Help));

            session.Network.EnqueueSend(new GameMessageSystemChat($"Mana Total:", ChatMessageType.System));

            if (session.Player.MA1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 1,000 Mana", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 1,000 Mana", ChatMessageType.Help));

            if (session.Player.MA2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 2,000 Mana", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 2,000 Mana", ChatMessageType.Help));

            if (session.Player.MA3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 4,000 Mana", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 4,000 Mana", ChatMessageType.Help));

            if (session.Player.MA4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 6,000 Mana", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 6,000 Mana", ChatMessageType.Help));

            if (session.Player.MA5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 12,000 Mana", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 12,000 Mana", ChatMessageType.Help));

            if (session.Player.MA6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 24,300 Mana", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 24,300 Mana", ChatMessageType.Help));

            session.Network.EnqueueSend(new GameMessageSystemChat($"Enlightenment Total:", ChatMessageType.System));

            if (session.Player.ENL1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 50 Enlightenments", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 50 Enlightenments", ChatMessageType.Help));

            if (session.Player.ENL2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 100 Enlightenments", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 100 Enlightenments", ChatMessageType.Help));

            if (session.Player.ENL3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 200 Enlightenments", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 200 Enlightenments", ChatMessageType.Help));

            if (session.Player.ENL4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 400 Enlightenments", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 400 Enlightenments", ChatMessageType.Help));

            if (session.Player.ENL5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 600 Enlightenments", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 600 Enlightenments", ChatMessageType.Help));

            if (session.Player.ENL6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 800 Enlightenments", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 800 Enlightenments", ChatMessageType.Help));

            session.Network.EnqueueSend(new GameMessageSystemChat($"Attribute Combined Total:", ChatMessageType.System));

            if (session.Player.AT1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 1,000 combined total base attributes", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 1,000 combined total base attributes", ChatMessageType.Help));

            if (session.Player.AT2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 4,000 combined total base attributes", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 4,000 combined total base attributes", ChatMessageType.Help));

            if (session.Player.AT3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 7,000 combined total base attributes", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 7,000 combined total base attributes", ChatMessageType.Help));

            if (session.Player.AT4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 12,000 combined total base attributes", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 12,000 combined total base attributes", ChatMessageType.Help));

            if (session.Player.AT5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 15,000 combined total base attributes", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 15,000 combined total base attributes", ChatMessageType.Help));

            if (session.Player.AT6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 20,000 combined total base attributes", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 20,000 combined total base attributes", ChatMessageType.Help));

            session.Network.EnqueueSend(new GameMessageSystemChat($"Player Level Total:", ChatMessageType.System));

            if (session.Player.LV1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach Level 275", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach Level 275", ChatMessageType.Help));

            if (session.Player.LV2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach Level 1,000", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach Level 1,000", ChatMessageType.Help));

            if (session.Player.LV3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach Level 4,000", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach Level 4,000", ChatMessageType.Help));

            if (session.Player.LV4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach Level 8,000", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach Level 8,000", ChatMessageType.Help));

            if (session.Player.LV5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach Level 10,000", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach Level 10,000", ChatMessageType.Help));

            if (session.Player.LV6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach Level 20,000", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach Level 20,000", ChatMessageType.Help));


            session.Network.EnqueueSend(new GameMessageSystemChat($"Total Any Level Creature Kills:", ChatMessageType.System));

            if (session.Player.MC1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Kill a total of 1,000 creatures of any level", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Kill a total of 1,000 creatures of any level", ChatMessageType.Help));

            if (session.Player.MC2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Kill a total of 10,000 creatures of any level", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Kill a total of 10,000 creatures of any level", ChatMessageType.Help));

            if (session.Player.MC3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Kill a total of 100,000 creatures of any level", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Kill a total of 100,000 creatures of any level", ChatMessageType.Help));

            if (session.Player.MC4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Kill a total of 1,000,000 creatures of any level", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Kill a total of 1,000,000 creatures of any level", ChatMessageType.Help));

            if (session.Player.MC5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Kill a total of 10,000,000 creatures of any level", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Kill a total of 10,000,000 creatures of any level", ChatMessageType.Help));

            if (session.Player.MC6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Kill a total of 100,000,000 creatures of any level", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Kill a total of 100,000,000 creatures of any level", ChatMessageType.Help));


            session.Network.EnqueueSend(new GameMessageSystemChat($"Total 100+ Level Creature Kills:", ChatMessageType.System));


            if (session.Player.HMC1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Kill a total of 100 creatures level 100+", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Kill a total of 100 creatures level 100+", ChatMessageType.Help));

            if (session.Player.HMC2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Kill a total of 1,000 creatures level 100+", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Kill a total of 1,000 creatures level 100+", ChatMessageType.Help));

            if (session.Player.HMC3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Kill a total of 10,000 creatures level 100+", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Kill a total of 10,000 creatures level 100+", ChatMessageType.Help));

            if (session.Player.HMC4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Kill a total of 100,000 creatures level 100+", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Kill a total of 100,000 creatures level 100+", ChatMessageType.Help));

            if (session.Player.HMC5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Kill a total of 1,000,000 creatures level 100+", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Kill a total of 1,000,000 creatures level 100+", ChatMessageType.Help));

            if (session.Player.HMC6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Kill a total of 10,000,000 creatures level 100+", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Kill a total of 10,000,000 creatures level 100+", ChatMessageType.Help));


            session.Network.EnqueueSend(new GameMessageSystemChat($"Total Combined Skill Levels:", ChatMessageType.System));

            if (session.Player.TS1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 5,000 combined total base Skill Levels", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 5,000 combined total base Skill Levels", ChatMessageType.Help));

            if (session.Player.TS2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 10,000 combined total base Skill Levels", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 10,000 combined total base Skill Levels", ChatMessageType.Help));

            if (session.Player.TS3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 25,000 combined total base Skill Levels", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 25,000 combined total base Skill Levels", ChatMessageType.Help));

            if (session.Player.TS4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 35,000 combined total base Skill Levels", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 35,000 combined total base Skill Levels", ChatMessageType.Help));

            if (session.Player.TS5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 40,000 combined total base Skill Levels", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 40,000 combined total base Skill Levels", ChatMessageType.Help));

            if (session.Player.TS6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 60,000 combined total base Skill Levels", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 60,000 combined total base Skill Levels", ChatMessageType.Help));

            session.Network.EnqueueSend(new GameMessageSystemChat($"Total Critical Strike Augmentations:", ChatMessageType.System));

            if (session.Player.CS1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 10 combined total Critical Strike Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 10 combined total Critical Strike Augmentations", ChatMessageType.Help));

            if (session.Player.CS2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 25 combined total Critical Strike Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 25 combined total Critical Strike Augmentations", ChatMessageType.Help));

            if (session.Player.CS3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 40 combined total Critical Strike Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 40 combined total Critical Strike Augmentations", ChatMessageType.Help));

            if (session.Player.CS4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 55 combined total Critical Strike Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 55 combined total Critical Strike Augmentations", ChatMessageType.Help));

            if (session.Player.CS5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 70 combined total Critical Strike Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 70 combined total Critical Strike Augmentations", ChatMessageType.Help));

            if (session.Player.CS6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 100 combined total Critical Strike Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 100 combined total Critical Strike Augmentations", ChatMessageType.Help));

            session.Network.EnqueueSend(new GameMessageSystemChat($"Total Critical Strike Damage Augmentations:", ChatMessageType.System));

            if (session.Player.CSD1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 10 combined total Critical Strike Damage Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 10 combined total Critical Strike Damage Augmentations", ChatMessageType.Help));

            if (session.Player.CSD2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 25 combined total Critical Strike Damage Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 25 combined total Critical Strike Damage Augmentations", ChatMessageType.Help));

            if (session.Player.CSD3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 40 combined total Critical Strike Damage Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 40 combined total Critical Strike Damage Augmentations", ChatMessageType.Help));

            if (session.Player.CSD4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 55 combined total Critical Strike Damage Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 55 combined total Critical Strike Damage Augmentations", ChatMessageType.Help));

            if (session.Player.CSD5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 70 combined total Critical Strike Damage Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 70 combined total Critical Strike Damage Augmentations", ChatMessageType.Help));

            if (session.Player.CSD6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 100 combined total Critical Strike Damage Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 100 combined total Critical Strike Damage Augmentations", ChatMessageType.Help));

            session.Network.EnqueueSend(new GameMessageSystemChat($"Total Spell Component Burn Augmentations:", ChatMessageType.System));

            if (session.Player.SCB1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 10 combined total Spell Component Burn Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 10 combined total Spell Component Burn Augmentations", ChatMessageType.Help));

            if (session.Player.SCB2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 25 combined total Spell Component Burn Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 25 combined total Spell Component Burn Augmentations", ChatMessageType.Help));

            if (session.Player.SCB3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 40 combined total Spell Component Burn Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 40 combined total Spell Component Burn Augmentations", ChatMessageType.Help));

            if (session.Player.SCB4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 55 combined total Spell Component Burn Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 55 combined total Spell Component Burn Augmentations", ChatMessageType.Help));

            if (session.Player.SCB5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 70 combined total Spell Component Burn Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 70 combined total Spell Component Burn Augmentations", ChatMessageType.Help));

            if (session.Player.SCB6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 100 combined total Spell Component Burn Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 100 combined total Spell Component Burn Augmentations", ChatMessageType.Help));

            session.Network.EnqueueSend(new GameMessageSystemChat($"Total Missile Ammunition Consume Augmentations:", ChatMessageType.System));

            if (session.Player.MAC1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 10 combined total Missile Ammunition Consume Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 10 combined total Missile Ammunition Consume Augmentations", ChatMessageType.Help));

            if (session.Player.MAC2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 25 combined total Missile Ammunition Consume Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 25 combined total Missile Ammunition Consume Augmentations", ChatMessageType.Help));

            if (session.Player.MAC3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 40 combined total Missile Ammunition Consume Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 40 combined total Missile Ammunition Consume Augmentations", ChatMessageType.Help));

            if (session.Player.MAC4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 55 combined total Missile Ammunition Consume Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 55 combined total Missile Ammunition Consume Augmentations", ChatMessageType.Help));

            if (session.Player.MAC5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 70 combined total Missile Ammunition Consume Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 70 combined total Missile Ammunition Consume Augmentations", ChatMessageType.Help));

            if (session.Player.MAC6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 100 combined total Missile Ammunition Consume Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 100 combined total Missile Ammunition Consume Augmentations", ChatMessageType.Help));

            session.Network.EnqueueSend(new GameMessageSystemChat($"Total Spell Duration Augmentations:", ChatMessageType.System));

            if (session.Player.SD1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 10 combined total Spell Duration Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 10 combined total Spell Duration Augmentations", ChatMessageType.Help));

            if (session.Player.SD2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 25 combined total Spell Duration Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 25 combined total Spell Duration Augmentations", ChatMessageType.Help));

            if (session.Player.SD3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 40 combined total Spell Duration Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 40 combined total Spell Duration Augmentations", ChatMessageType.Help));

            if (session.Player.SD4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 55 combined total Spell Duration Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 55 combined total Spell Duration Augmentations", ChatMessageType.Help));

            if (session.Player.SD5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 70 combined total Spell Duration Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 70 combined total Spell Duration Augmentations", ChatMessageType.Help));

            if (session.Player.SD6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 100 combined total Spell Duration Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 100 combined total Spell Duration Augmentations", ChatMessageType.Help));

            session.Network.EnqueueSend(new GameMessageSystemChat($"Total Vitality Augmentations:", ChatMessageType.System));

            if (session.Player.V1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 10 combined total Vitality Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 10 combined total Vitality Augmentations", ChatMessageType.Help));

            if (session.Player.V2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 25 combined total Vitality Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 25 combined total Vitality Augmentations", ChatMessageType.Help));

            if (session.Player.V3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 40 combined total Vitality Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 40 combined total Vitality Augmentations", ChatMessageType.Help));

            if (session.Player.V4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 55 combined total Vitality Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 55 combined total Vitality Augmentations", ChatMessageType.Help));

            if (session.Player.V5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 70 combined total Vitality Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 70 combined total Vitality Augmentations", ChatMessageType.Help));

            if (session.Player.V6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 100 combined total Vitality Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 100 combined total Vitality Augmentations", ChatMessageType.Help));

            session.Network.EnqueueSend(new GameMessageSystemChat($"Total Armor Mana Augmentations:", ChatMessageType.System));

            if (session.Player.AM1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 10 combined total Armor Mana Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 10 combined total Armor Mana Augmentations", ChatMessageType.Help));

            if (session.Player.AM2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 25 combined total Armor Mana Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 25 combined total Armor Mana Augmentations", ChatMessageType.Help));

            if (session.Player.AM3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 40 combined total Armor Mana Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 40 combined total Armor Mana Augmentations", ChatMessageType.Help));

            if (session.Player.AM4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 55 combined total Armor Mana Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 55 combined total Armor Mana Augmentations", ChatMessageType.Help));

            if (session.Player.AM5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 70 combined total Armor Mana Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 70 combined total Armor Mana Augmentations", ChatMessageType.Help));

            if (session.Player.AM6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 100 combined total Armor Mana Augmentations", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 100 combined total Armor Mana Augmentations", ChatMessageType.Help));

            session.Network.EnqueueSend(new GameMessageSystemChat($"Total Achievements Completed:", ChatMessageType.System));

            if (session.Player.TAC1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 17 total Achievements", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 17 total Achievements", ChatMessageType.Help));

            if (session.Player.TAC2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 34 total Achievements", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 34 total Achievements", ChatMessageType.Help));

            if (session.Player.TAC3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 51 total Achievements", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 51 total Achievements", ChatMessageType.Help));

            if (session.Player.TAC4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 68 total Achievements", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 68 total Achievements", ChatMessageType.Help));

            if (session.Player.TAC5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 85 total Achievements", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 85 total Achievements", ChatMessageType.Help));

            if (session.Player.TAC6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 102 total Achievements", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 102 total Achievements", ChatMessageType.Help));

            session.Network.EnqueueSend(new GameMessageSystemChat($"Hardmode Player Level Total:", ChatMessageType.System));

            if (session.Player.HMLV1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach Level 50 in Hardmode", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach Level 50 in Hardmode", ChatMessageType.Help));

            if (session.Player.HMLV2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach Level 150 in Hardmode", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach Level 150 in Hardmode", ChatMessageType.Help));

            if (session.Player.HMLV3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach Level 275 in Hardmode", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach Level 275 in Hardmode", ChatMessageType.Help));

            if (session.Player.HMLV4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach Level 400 in Hardmode", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach Level 400 in Hardmode", ChatMessageType.Help));

            if (session.Player.HMLV5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach Level 700 in Hardmode", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach Level 700 in Hardmode", ChatMessageType.Help));

            if (session.Player.HMLV6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach Level 1,000 in Hardmode", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach Level 1,000 in Hardmode", ChatMessageType.Help));

            session.Network.EnqueueSend(new GameMessageSystemChat($"Hardmode Enlightenment Total:", ChatMessageType.System));

            if (session.Player.HMENL1)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 10 Enlightenments in Hardmode", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 1: Reach 10 Enlightenments in Hardmode", ChatMessageType.Help));

            if (session.Player.HMENL2)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 50 Enlightenments in Hardmode", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 2: Reach 50 Enlightenments in Hardmode", ChatMessageType.Help));

            if (session.Player.HMENL3)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 100 Enlightenments in Hardmode", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 3: Reach 100 Enlightenments in Hardmode", ChatMessageType.Help));

            if (session.Player.HMENL4)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 150 Enlightenments in Hardmode", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 4: Reach 150 Enlightenments in Hardmode", ChatMessageType.Help));

            if (session.Player.HMENL5)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 250 Enlightenments in Hardmode", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 5: Reach 250 Enlightenments in Hardmode", ChatMessageType.Help));

            if (session.Player.HMENL6)
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 350 Enlightenments in Hardmode", ChatMessageType.x1B));
            else
                session.Network.EnqueueSend(new GameMessageSystemChat($"Tier 6: Reach 350 Enlightenments in Hardmode", ChatMessageType.Help));

            session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));

            var achievementBonusTotal = session.Player.AchievementCount * 3;

            if (session.Player.HardMode)
                achievementBonusTotal = session.Player.AchievementCount * 5;

            session.Network.EnqueueSend(new GameMessageSystemChat($"You have completed {session.Player.AchievementCount} out of 114 achievements", ChatMessageType.Broadcast));
            session.Network.EnqueueSend(new GameMessageSystemChat($"Your achievements are earning you {achievementBonusTotal}% bonus XP/Lum! (Multiplicative)", ChatMessageType.Broadcast));
            session.Network.EnqueueSend(new GameMessageSystemChat($"---------------------------", ChatMessageType.Broadcast));

        }

        [CommandHandler("qp", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "add quest point allocations")]
        public static void HandleQuestPoint(Session session, params string[] parameters)
        {           

            if (!session.Player.HasAllegiance)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"Only Monarchs may use this command.", ChatMessageType.System));
                return;
            }
            else
            {
                if (session.Player.Guid.Full != session.Player.Allegiance.Monarch.PlayerGuid.Full)
                {
                    if (parameters.Length == 0)
                        return;

                    if (parameters[0].Equals("show", StringComparison.OrdinalIgnoreCase))
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Current Quest Point Allocations", ChatMessageType.System));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"#-------------------------------------#", ChatMessageType.Broadcast));

                        session.Network.EnqueueSend(new GameMessageSystemChat($"Available QP: {session.Player.Allegiance.Monarch.Player.QuestPoints}.", ChatMessageType.Broadcast));

                        if (session.Player.Allegiance.Monarch.Player.XPBonus.HasValue)
                            session.Network.EnqueueSend(new GameMessageSystemChat($"%XP has been upgraded {session.Player.Allegiance.Monarch.Player.XPBonus}x Times. Allegiance Members are gaining {session.Player.Allegiance.Monarch.Player.XPBonus * 5}% XP from all sources.", ChatMessageType.Allegiance));
                        else
                            session.Network.EnqueueSend(new GameMessageSystemChat($"%XP has been upgraded 0x Times. Allegiance Members are gaining 0% XP from all sources.", ChatMessageType.Allegiance));

                        if (session.Player.Allegiance.Monarch.Player.XPBonusTick.HasValue)
                            session.Network.EnqueueSend(new GameMessageSystemChat($"TXP has been upgraded {session.Player.Allegiance.Monarch.Player.XPBonusTick}x Times. Allegiance Members are gaining up to {5000000 * session.Player.Allegiance.Monarch.Player.XPBonusTick:N0} XP every 5 seconds.", ChatMessageType.Allegiance));
                        else
                            session.Network.EnqueueSend(new GameMessageSystemChat($"TXP has been upgraded 0x Times. Allegiance Members are gaining 0 XP every 5 seconds.", ChatMessageType.Allegiance));

                        if (session.Player.Allegiance.Monarch.Player.LXPBonus.HasValue)
                            session.Network.EnqueueSend(new GameMessageSystemChat($"%LXP has been upgraded {session.Player.Allegiance.Monarch.Player.LXPBonus}x Times. Allegiance Members are gaining {session.Player.Allegiance.Monarch.Player.LXPBonus * 5}% Luminance XP from all sources.", ChatMessageType.Allegiance));
                        else
                            session.Network.EnqueueSend(new GameMessageSystemChat($"%LXP has been upgraded 0x Times. Allegiance Members are gaining 0% Luminace XP from all sources.", ChatMessageType.Allegiance));

                        if (session.Player.Allegiance.Monarch.Player.LXPBonusTick.HasValue)
                            session.Network.EnqueueSend(new GameMessageSystemChat($"TLXP has been upgraded {session.Player.Allegiance.Monarch.Player.LXPBonusTick}x Times. Allegiance Members are gaining {session.Player.Allegiance.Monarch.Player.LXPBonusTick * 5} Luminance XP every 5 seconds.", ChatMessageType.Allegiance));
                        else
                            session.Network.EnqueueSend(new GameMessageSystemChat($"TLXP has been upgraded 0x Times. Allegiance Members are gaining 0 Luminance XP every 5 seconds.", ChatMessageType.Allegiance));

                        if (session.Player.Allegiance.Monarch.Player.AllegianceBonusDamageRating.HasValue)
                            session.Network.EnqueueSend(new GameMessageSystemChat($"DR has been upgraded {session.Player.Allegiance.Monarch.Player.AllegianceBonusDamageRating}x Times. Allegiance Members are gaining +{session.Player.Allegiance.Monarch.Player.AllegianceBonusDamageRating} Damage Rating", ChatMessageType.Allegiance));
                        else
                            session.Network.EnqueueSend(new GameMessageSystemChat($"DR has been upgraded 0x Times. Allegiance Members are gaining 0 Damage Rating.", ChatMessageType.Allegiance));

                        if (session.Player.Allegiance.Monarch.Player.AllegianceBonusDamageResistRating.HasValue)
                            session.Network.EnqueueSend(new GameMessageSystemChat($"DRR has been upgraded {session.Player.Allegiance.Monarch.Player.AllegianceBonusDamageResistRating}x Times. Allegiance Members are gaining +{session.Player.Allegiance.Monarch.Player.AllegianceBonusDamageResistRating} Damage Resist Rating.", ChatMessageType.Allegiance));
                        else
                            session.Network.EnqueueSend(new GameMessageSystemChat($"DRR has been upgraded 0x Times. Allegiance Members are gaining 0 Damage Resist Rating", ChatMessageType.Allegiance));

                        if (session.Player.Allegiance.Monarch.Player.LKey.HasValue)
                            session.Network.EnqueueSend(new GameMessageSystemChat($"LKEY has been upgraded {session.Player.Allegiance.Monarch.Player.LKey}x Times. Allegiance Members are allowed to claim {session.Player.Allegiance.Monarch.Player.LKey} 2-use Legendary keys every hour using /Lkey.", ChatMessageType.Allegiance));
                        else
                            session.Network.EnqueueSend(new GameMessageSystemChat($"LKEY has been upgraded 0x Times. Allegiance Members cannot claim any Aged Legendary Keys.", ChatMessageType.Allegiance));

                        session.Network.EnqueueSend(new GameMessageSystemChat($"#-------------------------------------#", ChatMessageType.Broadcast));
                        return;
                    }
                }
                else
                {

                    if (parameters.Length == 0)
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Please include which bonus you would like for your allegiance.", ChatMessageType.System));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"#-------------------------------------#", ChatMessageType.Broadcast));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"%XP- Each point raises the amount of xp your allegiance members get by 5%", ChatMessageType.Allegiance));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"TXP- Each point grants a small amount of XP automatically to allegiance members every 5 seconds.", ChatMessageType.Allegiance));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"%LXP- Each point raises the amount of luminance your allegiance members get by 5%", ChatMessageType.Allegiance));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"TLXP- Each point grants a small amount of luminance automatically to allegiance members every 5 seconds.", ChatMessageType.Allegiance));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"DR - Each point raises each allegiance members damage rating by 1.", ChatMessageType.Allegiance));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"DRR - Each point raises each allegiance members Damage Resistance rating by 1", ChatMessageType.Allegiance));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"LKey - Each point grants allegiance members the ability to generate a legendary key every 1 hour.", ChatMessageType.Allegiance));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"#-------------------------------------#", ChatMessageType.Broadcast));
                        return;
                    }

                    if (parameters[0].Equals("%xp", StringComparison.OrdinalIgnoreCase))
                    {
                        // cost base 250 + 50 for each level
                        var cost = 250 + ((session.Player.XPBonus ?? 0) * 50);

                        if (session.Player.QuestPoints < cost)
                        {
                            session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough quest points to add this point. You need {cost} but you only have {session.Player.QuestPoints}", ChatMessageType.Broadcast));
                            return;
                        }

                        if (session.Player.XPBonus == null)
                            session.Player.XPBonus = 0;

                        session.Player.QuestPoints -= cost;
                        session.Player.XPBonus++;

                        if (!session.Player.QuestPointsSpent.HasValue)
                            session.Player.QuestPointsSpent = 0;

                        session.Player.QuestPointsSpent += cost;

                        // iterate through all allegiance members
                        foreach (var member in session.Player.Allegiance.Members.Keys)
                        {
                            // is this allegiance member online?
                            var online = PlayerManager.GetOnlinePlayer(member);
                            if (online == null || online.SquelchManager.Squelches.Contains(session.Player, ChatMessageType.Allegiance))
                                continue;

                            online.Session.Network.EnqueueSend(new GameEventChannelBroadcast(online.Session, Channel.AllegianceBroadcast, session.Player.Name, $"Has added quest points into %XP. All allegiance members will now gain {session.Player.XPBonus * 5}% XP from all sources."));
                        }

                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have added quest points into %XP. Your allegiance members will now gain {session.Player.XPBonus * 5}% XP from all sources.", ChatMessageType.Broadcast));
                        return;
                    }

                    if (parameters[0].Equals("%lxp", StringComparison.OrdinalIgnoreCase))
                    {
                        // 1000 quest points per point.
                        var cost = 250 + ((session.Player.LXPBonus ?? 0) * 50);

                        if (session.Player.QuestPoints < cost)
                        {
                            session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough quest points to add this point. You need {cost} but you only have {session.Player.QuestPoints}", ChatMessageType.Broadcast));
                            return;
                        }

                        if (session.Player.LXPBonus == null)
                            session.Player.LXPBonus = 0;

                        session.Player.QuestPoints -= cost;
                        session.Player.LXPBonus++;

                        if (!session.Player.QuestPointsSpent.HasValue)
                            session.Player.QuestPointsSpent = 0;

                        session.Player.QuestPointsSpent += cost;

                        // iterate through all allegiance members
                        foreach (var member in session.Player.Allegiance.Members.Keys)
                        {
                            // is this allegiance member online?
                            var online = PlayerManager.GetOnlinePlayer(member);
                            if (online == null || online.SquelchManager.Squelches.Contains(session.Player, ChatMessageType.Allegiance))
                                continue;

                            online.Session.Network.EnqueueSend(new GameEventChannelBroadcast(online.Session, Channel.AllegianceBroadcast, session.Player.Name, $"Has added quest points into %LXP. All allegiance members will now gain {session.Player.LXPBonus * 5}% Luminance XP from all sources."));
                        }

                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have added quest points into %LXP. Your allegiance members will now gain {session.Player.LXPBonus * 5}% Luminance XP from all sources.", ChatMessageType.Broadcast));
                        return;
                    }

                    if (parameters[0].Equals("txp", StringComparison.OrdinalIgnoreCase))
                    {
                        // 1000 quest points per point.
                        var cost = 250 + ((session.Player.XPBonusTick ?? 0) * 50);

                        if (session.Player.QuestPoints < cost)
                        {
                            session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough quest points to add this point. You need {cost} but you only have {session.Player.QuestPoints}", ChatMessageType.Broadcast));
                            return;
                        }

                        if (session.Player.XPBonusTick == null)
                            session.Player.XPBonusTick = 0;

                        session.Player.QuestPoints -= cost;
                        session.Player.XPBonusTick++;

                        if (!session.Player.QuestPointsSpent.HasValue)
                            session.Player.QuestPointsSpent = 0;

                        session.Player.QuestPointsSpent += cost;

                        // iterate through all allegiance members
                        foreach (var member in session.Player.Allegiance.Members.Keys)
                        {
                            // is this allegiance member online?
                            var online = PlayerManager.GetOnlinePlayer(member);
                            if (online == null || online.SquelchManager.Squelches.Contains(session.Player, ChatMessageType.Allegiance))
                                continue;

                            online.Session.Network.EnqueueSend(new GameEventChannelBroadcast(online.Session, Channel.AllegianceBroadcast, session.Player.Name, $"Has added quest points into TXP. All allegiance members will now gain up to {5000000 * session.Player.XPBonusTick:N0} XP every 5 seconds. ({3750 * session.Player.XPBonusTick:N0}xp per level until 275)"));
                        }

                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have added quest points into TXP. Your allegiance members will now gain up to 5,000,000 XP every 5 seconds. (750xp per level until 275)", ChatMessageType.Broadcast));
                        return;
                    }

                    if (parameters[0].Equals("dr", StringComparison.OrdinalIgnoreCase))
                    {
                        // 1000 quest points per point.
                        var cost = 250 + ((session.Player.AllegianceBonusDamageRating ?? 0) * 100);

                        if (session.Player.QuestPoints < cost)
                        {
                            session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough quest points to add this point. You need {cost} but you only have {session.Player.QuestPoints}", ChatMessageType.Broadcast));
                            return;
                        }

                        if (session.Player.AllegianceBonusDamageRating == null)
                            session.Player.AllegianceBonusDamageRating = 0;

                        session.Player.QuestPoints -= cost;
                        session.Player.AllegianceBonusDamageRating++;

                        if (!session.Player.QuestPointsSpent.HasValue)
                            session.Player.QuestPointsSpent = 0;

                        session.Player.QuestPointsSpent += cost;

                        // iterate through all allegiance members
                        foreach (var member in session.Player.Allegiance.Members.Keys)
                        {
                            // is this allegiance member online?
                            var online = PlayerManager.GetOnlinePlayer(member);
                            if (online == null || online.SquelchManager.Squelches.Contains(session.Player, ChatMessageType.Allegiance))
                                continue;

                            online.Session.Network.EnqueueSend(new GameEventChannelBroadcast(online.Session, Channel.AllegianceBroadcast, session.Player.Name, $"Has added quest points into Damage Rating. All allegiance members will now gain +{session.Player.AllegianceBonusDamageRating:N0} Damage Rating."));
                        }

                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have added quest points into Damage Rating. Your allegiance members will now gain +{session.Player.AllegianceBonusDamageRating:N0} Damage Rating.", ChatMessageType.Broadcast));
                        return;
                    }

                    if (parameters[0].Equals("drr", StringComparison.OrdinalIgnoreCase))
                    {
                        // 1000 quest points per point.
                        var cost = 250 + ((session.Player.AllegianceBonusDamageResistRating ?? 0) * 100);

                        if (session.Player.QuestPoints < cost)
                        {
                            session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough quest points to add this point. You need {cost} but you only have {session.Player.QuestPoints}", ChatMessageType.Broadcast));
                            return;
                        }

                        if (session.Player.AllegianceBonusDamageResistRating == null)
                            session.Player.AllegianceBonusDamageResistRating = 0;

                        session.Player.QuestPoints -= cost;
                        session.Player.AllegianceBonusDamageResistRating++;

                        if (!session.Player.QuestPointsSpent.HasValue)
                            session.Player.QuestPointsSpent = 0;

                        session.Player.QuestPointsSpent += cost;

                        // iterate through all allegiance members
                        foreach (var member in session.Player.Allegiance.Members.Keys)
                        {
                            // is this allegiance member online?
                            var online = PlayerManager.GetOnlinePlayer(member);
                            if (online == null || online.SquelchManager.Squelches.Contains(session.Player, ChatMessageType.Allegiance))
                                continue;

                            online.Session.Network.EnqueueSend(new GameEventChannelBroadcast(online.Session, Channel.AllegianceBroadcast, session.Player.Name, $"Has added quest points into Damage Resist Rating. All allegiance members will now gain +{session.Player.AllegianceBonusDamageResistRating:N0} Damage Resist Rating."));
                        }

                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have added quest points into Damage Resist Rating. Your allegiance members will now gain +{session.Player.AllegianceBonusDamageResistRating:N0} Damage Resist Rating.", ChatMessageType.Broadcast));
                        return;
                    }

                    if (parameters[0].Equals("tlxp", StringComparison.OrdinalIgnoreCase))
                    {
                        // 1000 quest points per point.
                        var cost = 250 + ((session.Player.LXPBonusTick ?? 0) * 50);

                        var tickamount = 5;

                        if (session.Player.QuestPoints < cost)
                        {
                            session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough quest points to add this point. You need {cost} but you only have {session.Player.QuestPoints}", ChatMessageType.Broadcast));
                            return;
                        }

                        if (session.Player.LXPBonusTick == null)
                            session.Player.LXPBonusTick = 0;

                        session.Player.QuestPoints -= cost;
                        session.Player.LXPBonusTick++;

                        if (!session.Player.QuestPointsSpent.HasValue)
                            session.Player.QuestPointsSpent = 0;

                        session.Player.QuestPointsSpent += cost;

                        // iterate through all allegiance members
                        foreach (var member in session.Player.Allegiance.Members.Keys)
                        {
                            // is this allegiance member online?
                            var online = PlayerManager.GetOnlinePlayer(member);
                            if (online == null || online.SquelchManager.Squelches.Contains(session.Player, ChatMessageType.Allegiance))
                                continue;

                            online.Session.Network.EnqueueSend(new GameEventChannelBroadcast(online.Session, Channel.AllegianceBroadcast, session.Player.Name, $"Has added quest points into TLXP. All allegiance members will now gain {tickamount * session.Player.LXPBonusTick:N0} Luminance XP every 5 seconds"));
                        }

                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have added quest points into TLXP. Your allegiance members will now gain {tickamount * session.Player.LXPBonusTick:N0} Luminance XP every 5 seconds", ChatMessageType.Broadcast));
                        return;
                    }

                    if (parameters[0].Equals("lkey", StringComparison.OrdinalIgnoreCase))
                    {
                        // 1000 quest points per point.
                        var cost = 250 + ((session.Player.LKey ?? 0) * 50);

                        if (session.Player.QuestPoints < cost)
                        {
                            session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough quest points to add this point. You need {cost} but you only have {session.Player.QuestPoints}", ChatMessageType.Broadcast));
                            return;
                        }

                        if (session.Player.LKey == null)
                            session.Player.LKey = 0;

                        session.Player.QuestPoints -= cost;
                        session.Player.LKey++;

                        if (!session.Player.QuestPointsSpent.HasValue)
                            session.Player.QuestPointsSpent = 0;

                        session.Player.QuestPointsSpent += cost;

                        // iterate through all allegiance members
                        foreach (var member in session.Player.Allegiance.Members.Keys)
                        {
                            // is this allegiance member online?
                            var online = PlayerManager.GetOnlinePlayer(member);
                            if (online == null || online.SquelchManager.Squelches.Contains(session.Player, ChatMessageType.Allegiance))
                                continue;

                            online.Session.Network.EnqueueSend(new GameEventChannelBroadcast(online.Session, Channel.AllegianceBroadcast, session.Player.Name, $"Has added quest points into LKEY. All allegiance members can claim legendary keys using the command /Lkey {session.Player.LKey} times an hour"));
                        }

                        session.Network.EnqueueSend(new GameMessageSystemChat($"You have added quest points into LKEY. Your allegiance members can claim legendary keys using the command /Lkey {session.Player.LKey} times an hour", ChatMessageType.Broadcast));
                        return;
                    }

                    if (parameters[0].Equals("show", StringComparison.OrdinalIgnoreCase))
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Current Quest Point Allocations", ChatMessageType.System));
                        session.Network.EnqueueSend(new GameMessageSystemChat($"#-------------------------------------#", ChatMessageType.Broadcast));

                        session.Network.EnqueueSend(new GameMessageSystemChat($"Available QP: {session.Player.QuestPoints}. Total QP spent: {session.Player.QuestPointsSpent}", ChatMessageType.Broadcast));

                        if (session.Player.XPBonus.HasValue)
                            session.Network.EnqueueSend(new GameMessageSystemChat($"%XP has been upgraded {session.Player.XPBonus}x Times. Allegiance Members are gaining {session.Player.XPBonus * 5}% XP from all sources.", ChatMessageType.Allegiance));
                        else
                            session.Network.EnqueueSend(new GameMessageSystemChat($"%XP has been upgraded 0x Times. Allegiance Members are gaining 0% XP from all sources.", ChatMessageType.Allegiance));

                        if (session.Player.XPBonusTick.HasValue)
                            session.Network.EnqueueSend(new GameMessageSystemChat($"TXP has been upgraded {session.Player.XPBonusTick}x Times. Allegiance Members are gaining up to {1000000 * session.Player.XPBonusTick:N0} XP every 5 seconds.", ChatMessageType.Allegiance));
                        else
                            session.Network.EnqueueSend(new GameMessageSystemChat($"TXP has been upgraded 0x Times. Allegiance Members are gaining 0 XP every 5 seconds.", ChatMessageType.Allegiance));

                        if (session.Player.LXPBonus.HasValue)
                            session.Network.EnqueueSend(new GameMessageSystemChat($"%LXP has been upgraded {session.Player.LXPBonus}x Times. Allegiance Members are gaining {session.Player.LXPBonus * 5}% Luminance XP from all sources.", ChatMessageType.Allegiance));
                        else
                            session.Network.EnqueueSend(new GameMessageSystemChat($"%LXP has been upgraded 0x Times. Allegiance Members are gaining 0% Luminace XP from all sources.", ChatMessageType.Allegiance));

                        if (session.Player.LXPBonusTick.HasValue)
                            session.Network.EnqueueSend(new GameMessageSystemChat($"TLXP has been upgraded {session.Player.LXPBonusTick}x Times. Allegiance Members are gaining {session.Player.LXPBonusTick * 5} Luminance XP every 5 seconds.", ChatMessageType.Allegiance));
                        else
                            session.Network.EnqueueSend(new GameMessageSystemChat($"TLXP has been upgraded 0x Times. Allegiance Members are gaining 0 Luminance XP every 5 seconds.", ChatMessageType.Allegiance));

                        if (session.Player.AllegianceBonusDamageRating.HasValue)
                            session.Network.EnqueueSend(new GameMessageSystemChat($"DR has been upgraded {session.Player.AllegianceBonusDamageRating}x Times. Allegiance Members are gaining +{session.Player.AllegianceBonusDamageRating} Damage Rating", ChatMessageType.Allegiance));
                        else
                            session.Network.EnqueueSend(new GameMessageSystemChat($"DR has been upgraded 0x Times. Allegiance Members are gaining 0 Damage Rating.", ChatMessageType.Allegiance));

                        if (session.Player.AllegianceBonusDamageResistRating.HasValue)
                            session.Network.EnqueueSend(new GameMessageSystemChat($"DRR has been upgraded {session.Player.AllegianceBonusDamageResistRating}x Times. Allegiance Members are gaining +{session.Player.AllegianceBonusDamageResistRating} Damage Resist Rating.", ChatMessageType.Allegiance));
                        else
                            session.Network.EnqueueSend(new GameMessageSystemChat($"DRR has been upgraded 0x Times. Allegiance Members are gaining 0 Damage Resist Rating", ChatMessageType.Allegiance));

                        if (session.Player.LKey.HasValue)
                            session.Network.EnqueueSend(new GameMessageSystemChat($"LKEY has been upgraded {session.Player.LKey}x Times. Allegiance Members are allowed to claim {session.Player.LKey} 2-use Legendary keys every hour using /Lkey.", ChatMessageType.Allegiance));
                        else
                            session.Network.EnqueueSend(new GameMessageSystemChat($"LKEY has been upgraded 0x Times. Allegiance Members cannot claim any Aged Legendary Keys.", ChatMessageType.Allegiance));

                        session.Network.EnqueueSend(new GameMessageSystemChat($"#-------------------------------------#", ChatMessageType.Broadcast));
                        return;
                    }


                    if (!parameters[0].Equals("%xp", StringComparison.OrdinalIgnoreCase) || !parameters[0].Equals("txp", StringComparison.OrdinalIgnoreCase) || !parameters[0].Equals("dr", StringComparison.OrdinalIgnoreCase)
                        || !parameters[0].Equals("drr", StringComparison.OrdinalIgnoreCase) || !parameters[0].Equals("dr", StringComparison.OrdinalIgnoreCase) || !parameters[0].Equals("show", StringComparison.OrdinalIgnoreCase)
                        || parameters.Length > 0 || !parameters[0].Equals("lkey", StringComparison.OrdinalIgnoreCase))
                    {
                        return;
                    }
                }
            }
        }

        [CommandHandler("Lkey", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 0,
            "claim legendary keys from allegiance Lkey QP bonus",
            "")]
        public static void HandleLKey(Session session, params string[] parameters)
        {
            var characters = Player.GetAccountPlayers(session.Player.Account.AccountId);

            bool lkeyclaimed = false;
            bool keysclaim = false;
            int counttotal = 0;
            foreach (var character in characters)
            {
                if (character.LKeyClaims.HasValue)
                    counttotal++;

                if (character.Name != session.Player.Name)
                {
                    if (character.LKeyClaims.HasValue)
                        keysclaim = true;

                    if (character.AllegianceLKeyTimer.HasValue)
                    {
                        if (character.AllegianceLKeyTimer > 0)
                            lkeyclaimed = true;

                        if (Time.GetUnixTime() >= character.AllegianceLKeyTimer)
                        {
                                character.RemoveProperty(PropertyFloat.AllegianceLKeyTimer);
                                character.RemoveProperty(PropertyInt.LKeyClaims);
                                session.Network.EnqueueSend(new GameMessageSystemChat($"A character on your account Lkey timer has expired.", ChatMessageType.System));
                        }
                    }
                }
            }

            if (session.Player.AllegianceLKeyTimer.HasValue)
            {
                var remaining = Math.Abs((double)Time.GetUnixTime() - (double)session.Player.AllegianceLKeyTimer);

                var minutes = remaining / 60;
                var hours = minutes / 60;    

                var twodec = string.Format("{0:0.00}", minutes);      // "123.46"
                session.Network.EnqueueSend(new GameMessageSystemChat($"You cannot claim more keys for another {twodec} Minutes", ChatMessageType.System));
            }

            // if the count of characters that has lkey is greater than 1 reset all characters.
            if (counttotal > 1)
            {
                foreach (var character in characters)
                {
                    character.RemoveProperty(PropertyFloat.AllegianceLKeyTimer);
                    character.RemoveProperty(PropertyInt.LKeyClaims);
                }

                session.Network.EnqueueSend(new GameMessageSystemChat($"[INFORMATION] Your account may have been in a bugged state. Because of this you may claim your keys again, however please note that from here on out, once you start the claim process" +
                    $" you will not be able to claim the remainder on another character. You must finish claiming them on one single character and once the timer expires on that character you may use it on another if you wish. " +
                    $" Please reissue /lkey to start claiming your keys on this character!", ChatMessageType.System));
                return;
            }            

            if (lkeyclaimed)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"You have a character who has already claimed legendary key(s) on your account.", ChatMessageType.System));
                return;
            }

            if (keysclaim)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"You must claim your keys all on one character.", ChatMessageType.System));
                return;
            }

            if (!session.Player.HasAllegiance)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"You are not in an allegiance.", ChatMessageType.System));
                return;
            }
            else
            {
                var monarch = session.Player.Allegiance.Monarch.Player;

                if (monarch.LKey.HasValue)
                {
                    var claims = monarch.LKey;

                    if (!session.Player.LKeyClaims.HasValue)
                        session.Player.LKeyClaims = 0;

                    if (session.Player.LKeyClaims < claims)
                    {
                        var legkey = WorldObjectFactory.CreateNewWorldObject(5000004);

                        if (!session.Player.TryCreateInInventoryWithNetworking(legkey))
                        {
                            session.Network.EnqueueSend(new GameMessageSystemChat($"Failed to create your legendary key, please make space or make sure you have enough burden!", ChatMessageType.System));
                            return;
                        }

                        session.Player.TryCreateInInventoryWithNetworking(legkey);

                        session.Player.LKeyClaims++;

                        if (session.Player.LKeyClaims >= claims)
                            session.Network.EnqueueSend(new GameMessageSystemChat($"You have claimed a Legendary Key. You do not have any more claims and are now on cooldown. You may claim another if your monarch allocates QP into LKey which bypasses the timer.", ChatMessageType.System));
                        else
                            session.Network.EnqueueSend(new GameMessageSystemChat($"You have claimed a Legendary Key. You have {claims - session.Player.LKeyClaims} claims remaining.", ChatMessageType.System));


                        if (session.Player.LKeyClaims >= claims && !session.Player.AllegianceLKeyTimer.HasValue)
                            session.Player.SetProperty(PropertyFloat.AllegianceLKeyTimer, Time.GetFutureUnixTime(3600)); // sets timer for key reset. Does not reset if a point is allocated during cooldown.
                    }
                    else
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"Your timer has not expired, or you have claimed your maximum number of keys that your allegiance QP allocation allows.", ChatMessageType.System));
                        return;
                    }
                }
            }
        }

        [CommandHandler("attribute", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Raise attributes over max.")]
        public static void HandleAttribute(Session session, params string[] parameters)
        {
            var amt = 1;
            if (parameters.Length > 1)
                int.TryParse(parameters[1], out amt);

            if (parameters[0].Equals("str", StringComparison.OrdinalIgnoreCase))
            {
                var str = session.Player.Strength;

                if (!str.IsMaxRank)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"Your Strength is not max level yet. Please raise strength until it is maxxed out. ", ChatMessageType.Broadcast));
                    return;
                }

                double strcost = 0;
                var multiamount = 0UL;
                var strCostEthereal = session.Player.RaisedStr;

                if (amt > 1)
                {
                    for (var i = 0; i < amt; i++)
                    {                        
                        strcost = (ulong)Math.Round(10UL * (ulong)strCostEthereal / (2.995D - (0.001D * strCostEthereal)) * 329220194D);
                        multiamount += (ulong)strcost;
                        strCostEthereal++;
                    }
                }
                else
                    strcost = (ulong)Math.Round(10UL * (ulong)session.Player.RaisedStr / (2.995D - (0.001D * session.Player.RaisedStr)) * 329220194D);

                if (session.Player.AvailableExperience < (long?)multiamount && amt > 1)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough available experience to level your Strength up {amt} times. ", ChatMessageType.Broadcast));
                    return;
                }
                else if (session.Player.AvailableExperience < strcost)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough available experience to level your Strength up. ", ChatMessageType.Broadcast));
                    return;
                }

                if (amt > 1)
                {
                    session.Player.RaisedStr += amt;
                    str.StartingValue += (uint)amt;
                    session.Player.AvailableExperience = session.Player.AvailableExperience - (long?)multiamount;
                }
                else
                {
                    session.Player.RaisedStr++;
                    str.StartingValue += 1;
                    session.Player.AvailableExperience = session.Player.AvailableExperience - (long?)strcost;
                }

                
                session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt64(session.Player, PropertyInt64.AvailableExperience, session.Player.AvailableExperience ?? 0));
                session.Network.EnqueueSend(new GameMessagePrivateUpdateAttribute(session.Player, session.Player.Strength));

                if (amt > 1)
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You have increased your Strength to {str.Base}! XP spent {multiamount:N0}", ChatMessageType.Advancement));
                else
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You have increased your Strength to {str.Base}! XP spent {strcost:N0}", ChatMessageType.Advancement));                
            }

            if (parameters[0].Equals("end", StringComparison.OrdinalIgnoreCase))
            {
                var end = session.Player.Endurance;

                if (!end.IsMaxRank)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"Your Endurance is not max level yet. Please raise Endurance until it is maxxed out. ", ChatMessageType.Broadcast));
                    return;
                }

                double endcost = 0;
                var multiamount = 0UL;
                var endCostEthereal = session.Player.RaisedEnd;

                if (amt > 1)
                {
                    for (var i = 0; i < amt; i++)
                    {                        
                        endcost = (ulong)Math.Round(10UL * (ulong)endCostEthereal / (2.995D - (0.001D * endCostEthereal)) * 329220194D);
                        multiamount += (ulong)endcost;
                        endCostEthereal++;
                    }
                }
                else
                    endcost = (ulong)Math.Round(10UL * (ulong)session.Player.RaisedEnd / (2.995D - (0.001D * session.Player.RaisedEnd)) * 329220194D);

                if (session.Player.AvailableExperience < (long?)multiamount && amt > 1)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough available experience to level your Endurance up {amt} times. ", ChatMessageType.Broadcast));
                    return;
                }
                else if (session.Player.AvailableExperience < endcost)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough available experience to level your Endurance up. ", ChatMessageType.Broadcast));
                    return;
                }

                if (amt > 1)
                {
                    session.Player.RaisedEnd += amt;
                    end.StartingValue += (uint)amt;
                    session.Player.AvailableExperience = session.Player.AvailableExperience - (long?)multiamount;
                }
                else
                {
                    session.Player.RaisedEnd++;
                    end.StartingValue += 1;
                    session.Player.AvailableExperience = session.Player.AvailableExperience - (long?)endcost;
                }


                session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt64(session.Player, PropertyInt64.AvailableExperience, session.Player.AvailableExperience ?? 0));
                session.Network.EnqueueSend(new GameMessagePrivateUpdateAttribute(session.Player, session.Player.Endurance));

                if (amt > 1)
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You have increased your Endurance to {end.Base}! XP spent {multiamount:N0}", ChatMessageType.Advancement));
                else
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You have increased your Endurance to {end.Base}! XP spent {endcost:N0}", ChatMessageType.Advancement));
            }

            if (parameters[0].Equals("coord", StringComparison.OrdinalIgnoreCase))
            {
                var coord = session.Player.Coordination;

                if (!coord.IsMaxRank)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"Your Coordination is not max level yet. Please raise Coordination until it is maxxed out. ", ChatMessageType.Broadcast));
                    return;
                }

                double coordcost = 0;
                var multiamount = 0UL;
                var coordCostEthereal = session.Player.RaisedCoord;

                if (amt > 1)
                {
                    for (var i = 0; i < amt; i++)
                    {                        
                        coordcost = (ulong)Math.Round(10UL * (ulong)coordCostEthereal / (2.995D - (0.001D * coordCostEthereal)) * 329220194D);
                        multiamount += (ulong)coordcost;
                        coordCostEthereal++;
                    }
                }
                else
                    coordcost = (ulong)Math.Round(10UL * (ulong)session.Player.RaisedCoord / (2.995D - (0.001D * session.Player.RaisedCoord)) * 329220194D);

                if (session.Player.AvailableExperience < (long?)multiamount && amt > 1)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough available experience to level your Coordination up {amt} times. ", ChatMessageType.Broadcast));
                    return;
                }
                else if (session.Player.AvailableExperience < coordcost)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough available experience to level your Coordination up. ", ChatMessageType.Broadcast));
                    return;
                }

                if (amt > 1)
                {
                    session.Player.RaisedCoord += amt;
                    coord.StartingValue += (uint)amt;
                    session.Player.AvailableExperience = session.Player.AvailableExperience - (long?)multiamount;
                }
                else
                {
                    session.Player.RaisedCoord++;
                    coord.StartingValue += 1;
                    session.Player.AvailableExperience = session.Player.AvailableExperience - (long?)coordcost;
                }


                session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt64(session.Player, PropertyInt64.AvailableExperience, session.Player.AvailableExperience ?? 0));
                session.Network.EnqueueSend(new GameMessagePrivateUpdateAttribute(session.Player, session.Player.Coordination));

                if (amt > 1)
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You have increased your Coordination to {coord.Base}! XP spent {multiamount:N0}", ChatMessageType.Advancement));
                else
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You have increased your Coordination to {coord.Base}! XP spent {coordcost:N0}", ChatMessageType.Advancement));
            }

            if (parameters[0].Equals("quick", StringComparison.OrdinalIgnoreCase))
            {
                var quick = session.Player.Quickness;

                if (!quick.IsMaxRank)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"Your Quickness is not max level yet. Please raise Quickness until it is maxxed out. ", ChatMessageType.Broadcast));
                    return;
                }

                double quickcost = 0;
                var multiamount = 0UL;
                var quickCostEthereal = session.Player.RaisedQuick;

                if (amt > 1)
                {
                    for (var i = 0; i < amt; i++)
                    {                        
                        quickcost = (ulong)Math.Round(10UL * (ulong)quickCostEthereal / (2.995D - (0.001D * quickCostEthereal)) * 329220194D);
                        multiamount += (ulong)quickcost;
                        quickCostEthereal++;
                    }
                }
                else
                    quickcost = (ulong)Math.Round(10UL * (ulong)session.Player.RaisedQuick / (2.995D - (0.001D * session.Player.RaisedQuick)) * 329220194D);

                if (session.Player.AvailableExperience < (long?)multiamount && amt > 1)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough available experience to level your Quickness up {amt} times. ", ChatMessageType.Broadcast));
                    return;
                }
                else if (session.Player.AvailableExperience < quickcost)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough available experience to level your Quickness up. ", ChatMessageType.Broadcast));
                    return;
                }

                if (amt > 1)
                {
                    session.Player.RaisedQuick += amt;
                    quick.StartingValue += (uint)amt;
                    session.Player.AvailableExperience = session.Player.AvailableExperience - (long?)multiamount;
                }
                else
                {
                    session.Player.RaisedQuick++;
                    quick.StartingValue += 1;
                    session.Player.AvailableExperience = session.Player.AvailableExperience - (long?)quickcost;
                }


                session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt64(session.Player, PropertyInt64.AvailableExperience, session.Player.AvailableExperience ?? 0));
                session.Network.EnqueueSend(new GameMessagePrivateUpdateAttribute(session.Player, session.Player.Quickness));

                if (amt > 1)
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You have increased your Quickness to {quick.Base}! XP spent {multiamount:N0}", ChatMessageType.Advancement));
                else
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You have increased your Quickness to {quick.Base}! XP spent {quickcost:N0}", ChatMessageType.Advancement));
            }

            if (parameters[0].Equals("focus", StringComparison.OrdinalIgnoreCase))
            {
                var focus = session.Player.Focus;

                if (!focus.IsMaxRank)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"Your Focus is not max level yet. Please raise Focus until it is maxxed out. ", ChatMessageType.Broadcast));
                    return;
                }

                double focuscost = 0;
                var multiamount = 0UL;
                var focusCostEthereal = session.Player.RaisedFocus;

                if (amt > 1)
                {
                    for (var i = 0; i < amt; i++)
                    {                        
                        focuscost = (ulong)Math.Round(10UL * (ulong)focusCostEthereal / (2.995D - (0.001D * focusCostEthereal)) * 329220194D);
                        multiamount += (ulong)focuscost;
                        focusCostEthereal++;
                    }
                }
                else
                    focuscost = (ulong)Math.Round(10UL * (ulong)session.Player.RaisedFocus / (2.995D - (0.001D * session.Player.RaisedFocus)) * 329220194D);

                if (session.Player.AvailableExperience < (long?)multiamount && amt > 1)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough available experience to level your Focus up {amt} times. ", ChatMessageType.Broadcast));
                    return;
                }
                else if (session.Player.AvailableExperience < focuscost)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough available experience to level your Focus up. ", ChatMessageType.Broadcast));
                    return;
                }

                if (amt > 1)
                {
                    session.Player.RaisedFocus += amt;
                    focus.StartingValue += (uint)amt;
                    session.Player.AvailableExperience = session.Player.AvailableExperience - (long?)multiamount;
                }
                else
                {
                    session.Player.RaisedFocus++;
                    focus.StartingValue += 1;
                    session.Player.AvailableExperience = session.Player.AvailableExperience - (long?)focuscost;
                }


                session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt64(session.Player, PropertyInt64.AvailableExperience, session.Player.AvailableExperience ?? 0));
                session.Network.EnqueueSend(new GameMessagePrivateUpdateAttribute(session.Player, session.Player.Focus));

                if (amt > 1)
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You have increased your Focus to {focus.Base}! XP spent {multiamount:N0}", ChatMessageType.Advancement));
                else
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You have increased your Focus to {focus.Base}! XP spent {focuscost:N0}", ChatMessageType.Advancement));
            }


            if (parameters[0].Equals("self", StringComparison.OrdinalIgnoreCase))
            {
                var self = session.Player.Self;

                if (!self.IsMaxRank)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"Your Self is not max level yet. Please raise Self until it is maxxed out. ", ChatMessageType.Broadcast));
                    return;
                }

                double selfcost = 0;
                var multiamount = 0UL;
                var selfCostEthereal = session.Player.RaisedSelf;

                if (amt > 1)
                {
                    for (var i = 0; i < amt; i++)
                    {                        
                        selfcost = (ulong)Math.Round(10UL * (ulong)selfCostEthereal / (2.995D - (0.001D * selfCostEthereal)) * 329220194D);
                        multiamount += (ulong)selfcost;
                        selfCostEthereal++;
                    }
                }
                else
                    selfcost = (ulong)Math.Round(10UL * (ulong)session.Player.RaisedSelf / (2.995D - (0.001D * session.Player.RaisedSelf)) * 329220194D);

                if (session.Player.AvailableExperience < (long?)multiamount && amt > 1)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough available experience to level your Self up {amt} times. ", ChatMessageType.Broadcast));
                    return;
                }
                else if (session.Player.AvailableExperience < selfcost)
                {
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You do not have enough available experience to level your Self up. ", ChatMessageType.Broadcast));
                    return;
                }

                if (amt > 1)
                {
                    session.Player.RaisedSelf += amt;
                    self.StartingValue += (uint)amt;
                    session.Player.AvailableExperience = session.Player.AvailableExperience - (long?)multiamount;
                }
                else
                {
                    session.Player.RaisedSelf++;
                    self.StartingValue += 1;
                    session.Player.AvailableExperience = session.Player.AvailableExperience - (long?)selfcost;
                }


                session.Network.EnqueueSend(new GameMessagePrivateUpdatePropertyInt64(session.Player, PropertyInt64.AvailableExperience, session.Player.AvailableExperience ?? 0));
                session.Network.EnqueueSend(new GameMessagePrivateUpdateAttribute(session.Player, session.Player.Self));

                if (amt > 1)
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You have increased your Self to {self.Base}! XP spent {multiamount:N0}", ChatMessageType.Advancement));
                else
                    session.Network.EnqueueSend(new GameMessageSystemChat($"You have increased your Self to {self.Base}! XP spent {selfcost:N0}", ChatMessageType.Advancement));
            }

            if (parameters[0].Equals("str", StringComparison.OrdinalIgnoreCase) && parameters[0].Equals("end", StringComparison.OrdinalIgnoreCase) && parameters[0].Equals("coord", StringComparison.OrdinalIgnoreCase) &&
                parameters[0].Equals("quick", StringComparison.OrdinalIgnoreCase) && parameters[0].Equals("focus", StringComparison.OrdinalIgnoreCase) && parameters[0].Equals("self", StringComparison.OrdinalIgnoreCase))
            {

                session.Network.EnqueueSend(new GameMessageSystemChat($"must specify which attribute you wish to raise. ex. /attribute STR or /attribute END or /attribute COORD or /attribute QUICK or /attribute FOCUS or /attribute SELF", ChatMessageType.Broadcast));

            }

        }

        [CommandHandler("repair", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 0,
            "Choose an armor piece to repair by Identifying it and using this command.",
            "")]
        public static void HandleRepair(Session session, params string[] parameters)
        {
            /*var value = 0;
            if (parameters.Length > 0)
                int.TryParse(parameters[0], out value);
*/

            var obj = CommandHandlerHelper.GetLastAppraisedObject(session);
            if (obj == null) return;

            // ensure in inventory
            var Inventory = session.Player.Inventory;
            if  (!Inventory.ContainsKey(obj.Guid))
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"[Failed] You do not own that item or it is an invalid object.", ChatMessageType.System));
                return;
            }

            if (!obj.ArmorMana.HasValue)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"[Failed] Item does not have Armor Mana.", ChatMessageType.System));
                return;
            }

            if (session.Player.GetInventoryItemsOfWCID(5000000).Count <= 0)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"[Failed] You do not have an Armor Mana Repair Kit in your inventory.", ChatMessageType.System));
                return;
            }

            if (obj.ArmorMana >= 50)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"[Failed] Armor is not under 50% Armor Mana.", ChatMessageType.System));
                return;
            }            

            // repair the armor and consume kit
            if (obj.ArmorMana < 50)
            {
                if (obj.ArmorMana <= 0)
                {
                    if (!session.Player.TryConsumeFromInventoryWithNetworking(5000000, 10))
                    {
                        session.Network.EnqueueSend(new GameMessageSystemChat($"[Failed] You could not repair {obj.NameWithMaterial} because you didn't have 10 Armor Repair Kits available.", ChatMessageType.System));
                        return;
                    }
                    else
                    {
                        session.Player.TryConsumeFromInventoryWithNetworking(5000000, 10);
                        obj.ArmorMana = 100;
                        obj.LongDesc = $"Armor Mana: {obj.ArmorMana}/100";
                        session.Network.EnqueueSend(new GameMessageSystemChat($"[Success] Your {obj.NameWithMaterial} has been repaired, consuming 10 Armor Mana Repair Kits", ChatMessageType.System));
                        return;
                    }
                }
                else
                {
                    obj.ArmorMana = 100;
                    obj.LongDesc = $"Armor Mana: {obj.ArmorMana}/100";
                    session.Player.TryConsumeFromInventoryWithNetworking(5000000, 1);
                    session.Network.EnqueueSend(new GameMessageSystemChat($"[Success] Your {obj.NameWithMaterial} has been repaired.", ChatMessageType.System));
                }
            }
        }

            // quest info (uses GDLe formatting to match plugin expectations)
            [CommandHandler("myquests", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Shows your quest log")]
        public static void HandleQuests(Session session, params string[] parameters)
        {
            if (!PropertyManager.GetBool("quest_info_enabled").Item)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat("The command \"myquests\" is not currently enabled on this server.", ChatMessageType.Broadcast));
                return;
            }

            var quests = session.Player.QuestManager.GetQuests();

            if (quests.Count == 0)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat("Quest list is empty.", ChatMessageType.Broadcast));
                return;
            }

            foreach (var playerQuest in quests)
            {
                var text = "";
                var questName = QuestManager.GetQuestName(playerQuest.QuestName);
                var quest = DatabaseManager.World.GetCachedQuest(questName);
                if (quest == null)
                {
                    Console.WriteLine($"Couldn't find quest {playerQuest.QuestName}");
                    continue;
                }

                var minDelta = quest.MinDelta;
                if (QuestManager.CanScaleQuestMinDelta(quest))
                    minDelta = (uint)(quest.MinDelta * PropertyManager.GetDouble("quest_mindelta_rate").Item);

                text += $"{playerQuest.QuestName.ToLower()} - {playerQuest.NumTimesCompleted} solves ({playerQuest.LastTimeCompleted})";
                text += $"\"{quest.Message}\" {quest.MaxSolves} {minDelta}";

                session.Network.EnqueueSend(new GameMessageSystemChat(text, ChatMessageType.Broadcast));
            }
        }

        /// <summary>
        /// For characters/accounts who currently own multiple houses, used to select which house they want to keep
        /// </summary>
        [CommandHandler("house-select", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 1, "For characters/accounts who currently own multiple houses, used to select which house they want to keep")]
        public static void HandleHouseSelect(Session session, params string[] parameters)
        {
            HandleHouseSelect(session, false, parameters);
        }

        public static void HandleHouseSelect(Session session, bool confirmed, params string[] parameters)
        {
            if (!int.TryParse(parameters[0], out var houseIdx))
                return;

            // ensure current multihouse owner
            if (!session.Player.IsMultiHouseOwner(false))
            {
                log.Warn($"{session.Player.Name} tried to /house-select {houseIdx}, but they are not currently a multi-house owner!");
                return;
            }

            // get house info for this index
            var multihouses = session.Player.GetMultiHouses();

            if (houseIdx < 1 || houseIdx > multihouses.Count)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"Please enter a number between 1 and {multihouses.Count}.", ChatMessageType.Broadcast));
                return;
            }

            var keepHouse = multihouses[houseIdx - 1];

            // show confirmation popup
            if (!confirmed)
            {
                var houseType = $"{keepHouse.HouseType}".ToLower();
                var loc = HouseManager.GetCoords(keepHouse.SlumLord.Location);

                var msg = $"Are you sure you want to keep the {houseType} at\n{loc}?";
                session.Player.ConfirmationManager.EnqueueSend(new Confirmation_Custom(session.Player.Guid, () => HandleHouseSelect(session, true, parameters)), msg);
                return;
            }

            // house to keep confirmed, abandon the other houses
            var abandonHouses = new List<House>(multihouses);
            abandonHouses.RemoveAt(houseIdx - 1);

            foreach (var abandonHouse in abandonHouses)
            {
                var house = session.Player.GetHouse(abandonHouse.Guid.Full);

                HouseManager.HandleEviction(house, house.HouseOwner ?? 0, true);
            }

            // set player properties for house to keep
            var player = PlayerManager.FindByGuid(keepHouse.HouseOwner ?? 0, out bool isOnline);
            if (player == null)
            {
                log.Error($"{session.Player.Name}.HandleHouseSelect({houseIdx}) - couldn't find HouseOwner {keepHouse.HouseOwner} for {keepHouse.Name} ({keepHouse.Guid})");
                return;
            }

            player.HouseId = keepHouse.HouseId;
            player.HouseInstance = keepHouse.Guid.Full;

            player.SaveBiotaToDatabase();

            // update house panel for current player
            var actionChain = new ActionChain();
            actionChain.AddDelaySeconds(3.0f);  // wait for slumlord inventory biotas above to save
            actionChain.AddAction(session.Player, session.Player.HandleActionQueryHouse);
            actionChain.EnqueueChain();

            Console.WriteLine("OK");
        }

        [CommandHandler("debugcast", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Shows debug information about the current magic casting state")]
        public static void HandleDebugCast(Session session, params string[] parameters)
        {
            var physicsObj = session.Player.PhysicsObj;

            var pendingActions = physicsObj.MovementManager.MoveToManager.PendingActions;
            var currAnim = physicsObj.PartArray.Sequence.CurrAnim;

            session.Network.EnqueueSend(new GameMessageSystemChat(session.Player.MagicState.ToString(), ChatMessageType.Broadcast));
            session.Network.EnqueueSend(new GameMessageSystemChat($"IsMovingOrAnimating: {physicsObj.IsMovingOrAnimating}", ChatMessageType.Broadcast));
            session.Network.EnqueueSend(new GameMessageSystemChat($"PendingActions: {pendingActions.Count}", ChatMessageType.Broadcast));
            session.Network.EnqueueSend(new GameMessageSystemChat($"CurrAnim: {currAnim?.Value.Anim.ID:X8}", ChatMessageType.Broadcast));
        }

        [CommandHandler("fixcast", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Fixes magic casting if locked up for an extended time")]
        public static void HandleFixCast(Session session, params string[] parameters)
        {
            var magicState = session.Player.MagicState;

            if (magicState.IsCasting && DateTime.UtcNow - magicState.StartTime > TimeSpan.FromSeconds(5))
            {
                session.Network.EnqueueSend(new GameEventCommunicationTransientString(session, "Fixed casting state"));
                session.Player.SendUseDoneEvent();
                magicState.OnCastDone();
            }
        }

        [CommandHandler("castmeter", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Shows the fast casting efficiency meter")]
        public static void HandleCastMeter(Session session, params string[] parameters)
        {
            if (parameters.Length == 0)
            {
                session.Player.MagicState.CastMeter = !session.Player.MagicState.CastMeter;
            }
            else
            {
                if (parameters[0].Equals("on", StringComparison.OrdinalIgnoreCase))
                    session.Player.MagicState.CastMeter = true;
                else
                    session.Player.MagicState.CastMeter = false;
            }
            session.Network.EnqueueSend(new GameMessageSystemChat($"Cast efficiency meter {(session.Player.MagicState.CastMeter ? "enabled" : "disabled")}", ChatMessageType.Broadcast));
        }

        private static List<string> configList = new List<string>()
        {
            "Common settings:\nConfirmVolatileRareUse, MainPackPreferred, SalvageMultiple, SideBySideVitals, UseCraftSuccessDialog",
            "Interaction settings:\nAcceptLootPermits, AllowGive, AppearOffline, AutoAcceptFellowRequest, DragItemOnPlayerOpensSecureTrade, FellowshipShareLoot, FellowshipShareXP, IgnoreAllegianceRequests, IgnoreFellowshipRequests, IgnoreTradeRequests, UseDeception",
            "UI settings:\nCoordinatesOnRadar, DisableDistanceFog, DisableHouseRestrictionEffects, DisableMostWeatherEffects, FilterLanguage, LockUI, PersistentAtDay, ShowCloak, ShowHelm, ShowTooltips, SpellDuration, TimeStamp, ToggleRun, UseMouseTurning",
            "Chat settings:\nHearAllegianceChat, HearGeneralChat, HearLFGChat, HearRoleplayChat, HearSocietyChat, HearTradeChat, HearPKDeaths, StayInChatMode",
            "Combat settings:\nAdvancedCombatUI, AutoRepeatAttack, AutoTarget, LeadMissileTargets, UseChargeAttack, UseFastMissiles, ViewCombatTarget, VividTargetingIndicator",
            "Character display settings:\nDisplayAge, DisplayAllegianceLogonNotifications, DisplayChessRank, DisplayDateOfBirth, DisplayFishingSkill, DisplayNumberCharacterTitles, DisplayNumberDeaths"
        };

        /// <summary>
        /// Mapping of GDLE -> ACE CharacterOptions
        /// </summary>
        private static Dictionary<string, string> translateOptions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            // Common
            { "ConfirmVolatileRareUse", "ConfirmUseOfRareGems" },
            { "MainPackPreferred", "UseMainPackAsDefaultForPickingUpItems" },
            { "SalvageMultiple", "SalvageMultipleMaterialsAtOnce" },
            { "SideBySideVitals", "SideBySideVitals" },
            { "UseCraftSuccessDialog", "UseCraftingChanceOfSuccessDialog" },

            // Interaction
            { "AcceptLootPermits", "AcceptCorpseLootingPermissions" },
            { "AllowGive", "LetOtherPlayersGiveYouItems" },
            { "AppearOffline", "AppearOffline" },
            { "AutoAcceptFellowRequest", "AutomaticallyAcceptFellowshipRequests" },
            { "DragItemOnPlayerOpensSecureTrade", "DragItemToPlayerOpensTrade" },
            { "FellowshipShareLoot", "ShareFellowshipLoot" },
            { "FellowshipShareXP", "ShareFellowshipExpAndLuminance" },
            { "IgnoreAllegianceRequests", "IgnoreAllegianceRequests" },
            { "IgnoreFellowshipRequests", "IgnoreFellowshipRequests" },
            { "IgnoreTradeRequests", "IgnoreAllTradeRequests" },
            { "UseDeception", "AttemptToDeceiveOtherPlayers" },

            // UI
            { "CoordinatesOnRadar", "ShowCoordinatesByTheRadar" },
            { "DisableDistanceFog", "DisableDistanceFog" },
            { "DisableHouseRestrictionEffects", "DisableHouseRestrictionEffects" },
            { "DisableMostWeatherEffects", "DisableMostWeatherEffects" },
            { "FilterLanguage", "FilterLanguage" },
            { "LockUI", "LockUI" },
            { "PersistentAtDay", "AlwaysDaylightOutdoors" },
            { "ShowCloak", "ShowYourCloak" },
            { "ShowHelm", "ShowYourHelmOrHeadGear" },
            { "ShowTooltips", "Display3dTooltips" },
            { "SpellDuration", "DisplaySpellDurations" },
            { "TimeStamp", "DisplayTimestamps" },
            { "ToggleRun", "RunAsDefaultMovement" },
            { "UseMouseTurning", "UseMouseTurning" },

            // Chat
            { "HearAllegianceChat", "ListenToAllegianceChat" },
            { "HearGeneralChat", "ListenToGeneralChat" },
            { "HearLFGChat", "ListenToLFGChat" },
            { "HearRoleplayChat", "ListentoRoleplayChat" },
            { "HearSocietyChat", "ListenToSocietyChat" },
            { "HearTradeChat", "ListenToTradeChat" },
            { "HearPKDeaths", "ListenToPKDeathMessages" },
            { "StayInChatMode", "StayInChatModeAfterSendingMessage" },

            // Combat
            { "AdvancedCombatUI", "AdvancedCombatInterface" },
            { "AutoRepeatAttack", "AutoRepeatAttacks" },
            { "AutoTarget", "AutoTarget" },
            { "LeadMissileTargets", "LeadMissileTargets" },
            { "UseChargeAttack", "UseChargeAttack" },
            { "UseFastMissiles", "UseFastMissiles" },
            { "ViewCombatTarget", "KeepCombatTargetsInView" },
            { "VividTargetingIndicator", "VividTargetingIndicator" },

            // Character Display
            { "DisplayAge", "AllowOthersToSeeYourAge" },
            { "DisplayAllegianceLogonNotifications", "ShowAllegianceLogons" },
            { "DisplayChessRank", "AllowOthersToSeeYourChessRank" },
            { "DisplayDateOfBirth", "AllowOthersToSeeYourDateOfBirth" },
            { "DisplayFishingSkill", "AllowOthersToSeeYourFishingSkill" },
            { "DisplayNumberCharacterTitles", "AllowOthersToSeeYourNumberOfTitles" },
            { "DisplayNumberDeaths", "AllowOthersToSeeYourNumberOfDeaths" },
        };

        /// <summary>
        /// Manually sets a character option on the server. Use /config list to see a list of settings.
        /// </summary>
        [CommandHandler("config", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 1, "Manually sets a character option on the server.\nUse /config list to see a list of settings.", "<setting> <on/off>")]
        public static void HandleConfig(Session session, params string[] parameters)
        {
            if (!PropertyManager.GetBool("player_config_command").Item)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat("The command \"config\" is not currently enabled on this server.", ChatMessageType.Broadcast));
                return;
            }

            // /config list - show character options
            if (parameters[0].Equals("list", StringComparison.OrdinalIgnoreCase))
            {
                foreach (var line in configList)
                    session.Network.EnqueueSend(new GameMessageSystemChat(line, ChatMessageType.Broadcast));

                return;
            }

            // translate GDLE CharacterOptions for existing plugins
            if (!translateOptions.TryGetValue(parameters[0], out var param) || !Enum.TryParse(param, out CharacterOption characterOption))
            {
                session.Network.EnqueueSend(new GameMessageSystemChat($"Unknown character option: {parameters[0]}", ChatMessageType.Broadcast));
                return;
            }

            var option = session.Player.GetCharacterOption(characterOption);

            // modes of operation:
            // on / off / toggle

            // - if none specified, default to toggle
            var mode = "toggle";

            if (parameters.Length > 1)
            {
                if (parameters[1].Equals("on", StringComparison.OrdinalIgnoreCase))
                    mode = "on";
                else if (parameters[1].Equals("off", StringComparison.OrdinalIgnoreCase))
                    mode = "off";
            }

            // set character option
            if (mode.Equals("on"))
                option = true;
            else if (mode.Equals("off"))
                option = false;
            else
                option = !option;

            session.Player.SetCharacterOption(characterOption, option);

            session.Network.EnqueueSend(new GameMessageSystemChat($"Character option {parameters[0]} is now {(option ? "on" : "off")}.", ChatMessageType.Broadcast));

            // update client
            session.Network.EnqueueSend(new GameEventPlayerDescription(session));
        }

        /// <summary>
        /// Force resend of all visible objects known to this player. Can fix rare cases of invisible object bugs.
        /// Can only be used once every 5 mins max.
        /// </summary>
        [CommandHandler("objsend", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Force resend of all visible objects known to this player. Can fix rare cases of invisible object bugs. Can only be used once every 5 mins max.")]
        public static void HandleObjSend(Session session, params string[] parameters)
        {
            // a good repro spot for this is the first room after the door in facility hub
            // in the portal drop / staircase room, the VisibleCells do not have the room after the door
            // however, the room after the door *does* have the portal drop / staircase room in its VisibleCells (the inverse relationship is imbalanced)
            // not sure how to fix this atm, seems like it triggers a client bug..

            if (DateTime.UtcNow - session.Player.PrevObjSend < TimeSpan.FromMinutes(5))
            {
                session.Player.SendTransientError("You have used this command too recently!");
                return;
            }

            var knownObjs = session.Player.GetKnownObjects();

            foreach (var knownObj in knownObjs)
            {
                session.Player.RemoveTrackedObject(knownObj, false);
                session.Player.TrackObject(knownObj);
            }
            session.Player.PrevObjSend = DateTime.UtcNow;
        }

        // show player ace server versions
        [CommandHandler("aceversion", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, "Shows this server's version data")]
        public static void HandleACEversion(Session session, params string[] parameters)
        {
            if (!PropertyManager.GetBool("version_info_enabled").Item)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat("The command \"aceversion\" is not currently enabled on this server.", ChatMessageType.Broadcast));
                return;
            }

            var msg = ServerBuildInfo.GetVersionInfo();

            session.Network.EnqueueSend(new GameMessageSystemChat(msg, ChatMessageType.WorldBroadcast));
        }

        // reportbug < code | content > < description >
        [CommandHandler("reportbug", AccessLevel.Player, CommandHandlerFlag.RequiresWorld, 2,
            "Generate a Bug Report",
            "<category> <description>\n" +
            "This command generates a URL for you to copy and paste into your web browser to submit for review by server operators and developers.\n" +
            "Category can be the following:\n" +
            "Creature\n" +
            "NPC\n" +
            "Item\n" +
            "Quest\n" +
            "Recipe\n" +
            "Landblock\n" +
            "Mechanic\n" +
            "Code\n" +
            "Other\n" +
            "For the first three options, the bug report will include identifiers for what you currently have selected/targeted.\n" +
            "After category, please include a brief description of the issue, which you can further detail in the report on the website.\n" +
            "Examples:\n" +
            "/reportbug creature Drudge Prowler is over powered\n" +
            "/reportbug npc Ulgrim doesn't know what to do with Sake\n" +
            "/reportbug quest I can't enter the portal to the Lost City of Frore\n" +
            "/reportbug recipe I cannot combine Bundle of Arrowheads with Bundle of Arrowshafts\n" +
            "/reportbug code I was killed by a Non-Player Killer\n"
            )]
        public static void HandleReportbug(Session session, params string[] parameters)
        {
            if (!PropertyManager.GetBool("reportbug_enabled").Item)
            {
                session.Network.EnqueueSend(new GameMessageSystemChat("The command \"reportbug\" is not currently enabled on this server.", ChatMessageType.Broadcast));
                return;
            }

            var category = parameters[0];
            var description = "";

            for (var i = 1; i < parameters.Length; i++)
                description += parameters[i] + " ";

            description.Trim();

            switch (category.ToLower())
            {
                case "creature":
                case "npc":
                case "quest":
                case "item":
                case "recipe":
                case "landblock":
                case "mechanic":
                case "code":
                case "other":
                    break;
                default:
                    category = "Other";
                    break;
            }

            var sn = ConfigManager.Config.Server.WorldName;
            var c = session.Player.Name;

            var st = "ACE";

            //var versions = ServerBuildInfo.GetVersionInfo();
            var databaseVersion = DatabaseManager.World.GetVersion();
            var sv = ServerBuildInfo.FullVersion;
            var pv = databaseVersion.PatchVersion;

            //var ct = PropertyManager.GetString("reportbug_content_type").Item;
            var cg = category.ToLower();

            var w = "";
            var g = "";

            if (cg == "creature" || cg == "npc"|| cg == "item" || cg == "item")
            {
                var objectId = new ObjectGuid();
                if (session.Player.HealthQueryTarget.HasValue || session.Player.ManaQueryTarget.HasValue || session.Player.CurrentAppraisalTarget.HasValue)
                {
                    if (session.Player.HealthQueryTarget.HasValue)
                        objectId = new ObjectGuid((uint)session.Player.HealthQueryTarget);
                    else if (session.Player.ManaQueryTarget.HasValue)
                        objectId = new ObjectGuid((uint)session.Player.ManaQueryTarget);
                    else
                        objectId = new ObjectGuid((uint)session.Player.CurrentAppraisalTarget);

                    //var wo = session.Player.CurrentLandblock?.GetObject(objectId);

                    var wo = session.Player.FindObject(objectId.Full, Player.SearchLocations.Everywhere);

                    if (wo != null)
                    {
                        w = $"{wo.WeenieClassId}";
                        g = $"0x{wo.Guid:X8}";
                    }
                }
            }

            var l = session.Player.Location.ToLOCString();

            var issue = description;

            var urlbase = $"https://www.accpp.net/bug?";

            var url = urlbase;
            if (sn.Length > 0)
                url += $"sn={Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(sn))}";
            if (c.Length > 0)
                url += $"&c={Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(c))}";
            if (st.Length > 0)
                url += $"&st={Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(st))}";
            if (sv.Length > 0)
                url += $"&sv={Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(sv))}";
            if (pv.Length > 0)
                url += $"&pv={Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(pv))}";
            //if (ct.Length > 0)
            //    url += $"&ct={Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(ct))}";
            if (cg.Length > 0)
            {
                if (cg == "npc")
                    cg = cg.ToUpper();
                else
                    cg = char.ToUpper(cg[0]) + cg.Substring(1);
                url += $"&cg={Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(cg))}";
            }
            if (w.Length > 0)
                url += $"&w={Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(w))}";
            if (g.Length > 0)
                url += $"&g={Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(g))}";
            if (l.Length > 0)
                url += $"&l={Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(l))}";
            if (issue.Length > 0)
                url += $"&i={Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(issue))}";

            var msg = "\n\n\n\n";
            msg += "Bug Report - Copy and Paste the following URL into your browser to submit a bug report\n";
            msg += "-=-\n";
            msg += $"{url}\n";
            msg += "-=-\n";
            msg += "\n\n\n\n";

            session.Network.EnqueueSend(new GameMessageSystemChat(msg, ChatMessageType.AdminTell));
        }
    }
}
