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
