﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memorys;

namespace LFOverlay.Cheat.SDK
{
    internal class Game
    {
        public static int Client, Engine;
        public static bool Initalized = false;

        public static bool Initalize()
        {
            var result = Memory.Initalize("csgo");
            if(result == Memory.Enums.InitalizeResult.OK)
            {
                Client = Memory.GetModuleAddress("client.dll");
                Engine = Memory.GetModuleAddress("engine.dll");
                Initalized = true;
                return true;
            }
            else
            {
                Console.WriteLine("Game cannot initalized! -> " + result.ToString());
                Initalized = false;
                return false;
            }
        }
    }
}
