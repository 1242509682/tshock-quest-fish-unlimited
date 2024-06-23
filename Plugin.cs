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
using TShockAPI.Net;

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
                AnglerQuestSwap();

                Main.anglerWhoFinishedToday.Remove(TShock.Players[args.Msg.whoAmI].Name);

                NetMessage.SendAnglerQuest(args.Msg.whoAmI);
            }
        }

        void AnglerQuestSwap()
        {
            bool flag = NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3 || Main.hardMode || NPC.downedSlimeKing || NPC.downedQueenBee;
            bool flag2 = true;
            while (flag2)
            {
                flag2 = false;
                Main.anglerQuest = Main.rand.Next(Main.anglerQuestItemNetIDs.Length);
                int num = Main.anglerQuestItemNetIDs[Main.anglerQuest];
                if (num == 2454 && (!Main.hardMode || WorldGen.crimson))
                {
                    flag2 = true;
                }
                if (num == 2457 && WorldGen.crimson)
                {
                    flag2 = true;
                }
                if (num == 2462 && !Main.hardMode)
                {
                    flag2 = true;
                }
                if (num == 2463 && (!Main.hardMode || !WorldGen.crimson))
                {
                    flag2 = true;
                }
                if (num == 2465 && !Main.hardMode)
                {
                    flag2 = true;
                }
                if (num == 2468 && !Main.hardMode)
                {
                    flag2 = true;
                }
                if (num == 2471 && !Main.hardMode)
                {
                    flag2 = true;
                }
                if (num == 2473 && !Main.hardMode)
                {
                    flag2 = true;
                }
                if (num == 2477 && !WorldGen.crimson)
                {
                    flag2 = true;
                }
                if (num == 2480 && !Main.hardMode)
                {
                    flag2 = true;
                }
                if (num == 2483 && !Main.hardMode)
                {
                    flag2 = true;
                }
                if (num == 2484 && !Main.hardMode)
                {
                    flag2 = true;
                }
                if (num == 2485 && WorldGen.crimson)
                {
                    flag2 = true;
                }
                if ((num == 2476 || num == 2453 || num == 2473) && !flag)
                {
                    flag2 = true;
                }
            }
        }

    }
}

/*
public static int[] anglerQuestItemNetIDs = new int[]
{
	2450,
	2451,
	2452,
	2453,
	2454,
	2455,
	2456,
	2457,
	2458,
	2459,
	2460,
	2461,
	2462,
	2463,
	2464,
	2465,
	2466,
	2467,
	2468,
	2469,
	2470,
	2471,
	2472,
	2473,
	2474,
	2475,
	2476,
	2477,
	2478,
	2479,
	2480,
	2481,
	2482,
	2483,
	2484,
	2485,
	2486,
	2487,
	2488,
	4393,
	4394
};
*/