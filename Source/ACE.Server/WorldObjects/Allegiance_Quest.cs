using ACE.Common;
using ACE.Entity.Enum;
using ACE.Server.Entity;
using ACE.Server.Managers;
using ACE.Server.Network;
using ACE.Server.Network.GameEvent.Events;
using ACE.Server.Network.GameMessages.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ACE.Server.WorldObjects
{
    partial class Player
    {
        private static readonly List<string> EasyQuests = new List<string>()
        {



        };

        private static readonly List<string> MediumQuests = new List<string>()
        {



        };

        private static readonly List<string> HardQuests = new List<string>()
        {
            


        };


        public void QuestPointAdd(string questname, WorldObject questFrom)
        {
            // killtasks/things that count up dont count towards QP additions
            if (questname.Contains("Count", StringComparison.OrdinalIgnoreCase))
                return;

            //temple QP spam fix
            if (questname.Contains("TempleEnlightenment") || questname.Contains("TempleForgetfulness"))
                return;

            // portals dont give QP
            if (questFrom.WeenieType == WeenieType.Portal)
                return;

            //forges in starter towns no qp
            if (questFrom.Name == "Alchemy Forge" || questFrom.Name == "Cooking Forge" || questFrom.Name == "Lockpick Forge" || questFrom.Name == "Salvaging Forge" || questFrom.Name == "Fletching Forge")
                return;

            // tusker mask fix.. all masks do this?
            if (questname == "TuskerMask" && questname.Contains("Mask", StringComparison.OrdinalIgnoreCase))
                return;

            // GW fixes
            if (questname == "glendeninvaderskills" || questname.Contains("glendeninvadersblockade") || questname.Contains("CaptiveTalk", StringComparison.OrdinalIgnoreCase))
                return;

            // rare exchanger fix
            if (questname == "PaidRareExchanger")
                return;

            // FV glitch
            if (questname == "TuskerBloodCollectionStart")
                return;

            if (questname == "FrozenFortressTestingGroundsAccess_0513" || questname == "FrozenFortressLabAccess_0513")
                return;

            // vincadi fixes & issk
                if ((questname.Contains("raise", StringComparison.OrdinalIgnoreCase) && questname.Contains("harbinger", StringComparison.OrdinalIgnoreCase)) || questname.Contains("HarbingerCompletedWait", StringComparison.OrdinalIgnoreCase) ||
                questname.Contains("EnterHarbingerIssk", StringComparison.OrdinalIgnoreCase) || questname.Contains("EnterHarbingerVincadi", StringComparison.OrdinalIgnoreCase))
                return;

            // restless spirit fix
            if (questname.Contains("AssaultVaultAccessGranted", StringComparison.OrdinalIgnoreCase))
                return;

            // Ivory Crafter qp fix
            if (questname.Contains("HamudsDemiseStarted") || questname == "InsidiousMonougaHandIn")
                return;

            // nalicana fixes
            if (questname == "OracleLuminanceRewardsQuestStart_1110" || questname == "OraclePortalEntry")
                return;

            // sir durnstad fix
            if (questname == "AetheriumRaidsHighStarted_0210")
                return;

            // BM fixes
            if (questname.Contains("HizkRiEye", StringComparison.OrdinalIgnoreCase))
                return;

            // kiriel shadowborn fix
            if (questname == "GraelIslandTempleEastAccess0606")
                return;

            // society
            if (questname == "TaskDIBlackCoralStarted" || questname == "TaskMoarsmenArtifactsStarted" || questname == "TaskFreebooterJungleFlowerStarted" || questname == "TaskFreebooterJungleLilyStarted"
                || questname == "TaskDIReportStarted" || questname == "TaskFreebooterMoarGlandStarted")
                return;

            // chasing oswald
            if (questname == "ChasingOswaldRuschkIceHold" || questname == "ChasingOswaldViamontPrison" || questname == "OswaldJournal" || questname == "ChasingOswaldDungeonFlag")
                return;

            // skill/attribute reset npcs
            if (questFrom.WeenieClassId == 42818 || questFrom.WeenieClassId == 44950)
                return;

            // ancient tablet trophies
            if (questFrom.Name.Contains("Ancient Tablet of the Crystal", StringComparison.OrdinalIgnoreCase))
                return;

            //colo ticket
            if (questname == "ColoTicketPayment")
                return;

            if (questname == "ParadoxEggStarted")
                return;

            if (questname.StartsWith("TempleLiazkA") || questname.StartsWith("TempleLiazkB") || questname.StartsWith("TempleLiazkC"))
                return;

            if (Time.GetUnixTime() <= QuestPointTimer && QuestPointTimer.HasValue)
            {
                //EnqueueBroadcast(new GameMessageSystemChat($"You cannot gain a Quest Point for your allegiance while on cooldown.", ChatMessageType.System));
                return;
            }

            if (MonarchId == null)
                return;

            var monarchOnline = PlayerManager.GetOnlinePlayer(Allegiance.Monarch.Player.Guid.Full);
            var monarchOffline = PlayerManager.GetOfflinePlayer(Allegiance.Monarch.Player.Guid.Full);

            if (monarchOnline != null)
            {
                if (!monarchOnline.QuestPoints.HasValue)
                    monarchOnline.QuestPoints = 0;

                monarchOnline.QuestPoints += 1;
                SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.QuestPointTimer, Time.GetFutureUnixTime(5));

                // iterate through all allegiance members
                foreach (var member in Allegiance.Members.Keys)
                {
                    // is this allegiance member online?
                    var online = PlayerManager.GetOnlinePlayer(member);
                    if (online == null || online.SquelchManager.Squelches.Contains(Session.Player, ChatMessageType.Allegiance))
                        continue;

                    online.Session.Network.EnqueueSend(new GameEventChannelBroadcast(online.Session, Channel.AllegianceBroadcast, Name, $"[QuestPoint] {Name} Added a QP to your monarch {monarchOnline.Name}! (+1)({monarchOnline.QuestPoints})"));                    
                }

                //Session.Network.EnqueueSend(new GameMessageSystemChat($"{Name} Added a QP to your monarch {monarchOnline.Name}! (+1)({monarchOnline.QuestPoints})", ChatMessageType.System));
            }
            else
            {
                if (!monarchOffline.QuestPoints.HasValue)
                    monarchOffline.QuestPoints = 0;

                monarchOffline.QuestPoints += 1;
                SetProperty(ACE.Entity.Enum.Properties.PropertyFloat.QuestPointTimer, Time.GetFutureUnixTime(5));

                // iterate through all allegiance members
                foreach (var member in Allegiance.Members.Keys)
                {
                    // is this allegiance member online?
                    var online = PlayerManager.GetOnlinePlayer(member);
                    if (online == null || online.SquelchManager.Squelches.Contains(Session.Player, ChatMessageType.Allegiance))
                        continue;                    

                    online.Session.Network.EnqueueSend(new GameEventChannelBroadcast(online.Session, Channel.AllegianceBroadcast, Session.Player.Name, $"{Name} Added a QP to your monarch {monarchOffline.Name}! (+1)({monarchOffline.QuestPoints})"));
                }

                //Session.Network.EnqueueSend(new GameMessageSystemChat($"{Name} Added a QP to your monarch {monarchOffline.Name}! (+1)({monarchOffline.QuestPoints})", ChatMessageType.System));
            }                
        }
    }
}
