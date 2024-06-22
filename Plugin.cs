using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;
using Microsoft.Xna.Framework;
using System.Text.Json;
using Terraria.GameContent.Creative;

namespace QuestFishUnlimited
{
    [ApiVersion(2, 1)]
    public class QuestFishUnlimited : TerrariaPlugin
    {

        public override string Author => "Onusai";
        public override string Description => "Recieve a new fishing quest immediately after turning one in";
        public override string Name => "Quest Fish Unlimited";
        public override Version Version => new Version(1, 0, 0, 0);


        public QuestFishUnlimited(Main game) : base(game) { }

        public override void Initialize()
        {
            ServerApi.Hooks.GameInitialize.Register(this, OnGameLoad);
        }

        void OnGameLoad(EventArgs e)
        {
            ServerApi.Hooks.NetGetData.Register(this, OnNetGetData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                ServerApi.Hooks.GameInitialize.Deregister(this, OnGameLoad);
                ServerApi.Hooks.NetGetData.Deregister(this, OnNetGetData);
            }
            base.Dispose(disposing);
        }

        void OnNetGetData(GetDataEventArgs args)
        {

            if (args.MsgID == PacketTypes.CompleteAnglerQuest)
            {
                TSPlayer player = TShock.Players[args.Msg.whoAmI];
                Random random = new Random();
                Main.anglerQuest = random.Next(41);
                Main.anglerWhoFinishedToday.Remove(player.Name);
                player.SendData(PacketTypes.AnglerQuest);
            }
        }

    }
}