using Microsoft.Xna.Framework;
using StardewValley.Menus;

#pragma warning disable 108,114,626,649
// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace StardewValley.Menus
{
    public class patch_ChatBox : ChatBox
    {
        private extern void orig_runCommand(string command);
        protected override void runCommand(string command)
        {
            var parts = command.Split(' ');
            var head = parts[0];

            switch (head)
            {
                case "gemfruit":
                    addMessage("CalmBit: ;)", Color.Purple);
                    enableCheats = true;
                    return;
                default:
                    orig_runCommand(command);
                    return;
            }
            
            
        }
    }
}