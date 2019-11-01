using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using System.Collections.Generic;
using UnityEngine;

namespace Edsparr.Houseplugin
{
    public class CommandAddHouse : IRocketCommand
    {
        #region Declarations

        public bool AllowFromConsole
        {
            get
            {
                return false;
            }
        }

        public List<string> Permissions
        {
            get
            {
                return new List<string>()
                    {
                        "addhouse"
                    };
            }
        }

        public AllowedCaller AllowedCaller
        {
            get
            {
                return AllowedCaller.Player;
            }
        }

        public bool RunFromConsole
        {
            get
            {
                return false;
            }
        }

        public string Name
        {
            get
            {
                return "addhouse";
            }
        }

        public string Syntax
        {
            get
            {
                return "apartment (create/select)";
            }
        }

        public string Help
        {
            get

            {
                return "You assign the sell barricade.";
            }
        }

        public List<string> Aliases
        {
            get
            {
                return new List<string>();
            }
        }

        #endregion

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            if(command.Length < 1)
            {
                UnturnedChat.Say(player, "Invalid syntax: /addhouse <price>", Color.red);
                return;
            }
            bool IsNumber = decimal.TryParse(command[0], out decimal cost);
            var house = Plugin.Instance.getHouseFromObjects(player.Position);
            if (!IsNumber)
            {
                UnturnedChat.Say(player, command[0] + " is not a number!", Color.red);
                return;
            }
            if(house == null)
            {
                UnturnedChat.Say(player, "Couden't manage to find a level object where you are standing!", Color.red);
                return;
            }
            var incase = (Plugin.Instance.Configuration.Instance.Houses.Find(c => (c.id == house.asset.id)));
            if(incase != null)
            {
                incase.cost = cost;
            }
            else
            {
                
                Plugin.Instance.Configuration.Instance.Houses.Add(new HouseItem(house.asset.id, cost));
            }
            Plugin.Instance.Configuration.Save();
            UnturnedChat.Say(player, "You succesfully added this house to the houselist!", Color.red);
        }
    }
}