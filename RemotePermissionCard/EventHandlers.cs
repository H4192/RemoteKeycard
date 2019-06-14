using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System;
using System.Collections.Generic;

namespace RemotePermissionCard
{
    internal class EventHandlers : IEventHandler, IEventHandlerDoorAccess, IEventHandlerWaitingForPlayers
    {
        private RemotePermissionCard plugin;
        public EventHandlers(RemotePermissionCard plugin) => this.plugin = plugin;

        private readonly List<ItemType> keypads = new List<ItemType>()
        {
            ItemType.O5_LEVEL_KEYCARD,
            ItemType.CHAOS_INSURGENCY_DEVICE,
            ItemType.FACILITY_MANAGER_KEYCARD,
            ItemType.MTF_COMMANDER_KEYCARD,
            ItemType.MTF_LIEUTENANT_KEYCARD,
            ItemType.CONTAINMENT_ENGINEER_KEYCARD,
            ItemType.SENIOR_GUARD_KEYCARD,
            ItemType.GUARD_KEYCARD,
            ItemType.ZONE_MANAGER_KEYCARD,
            ItemType.MAJOR_SCIENTIST_KEYCARD,
            ItemType.SCIENTIST_KEYCARD,
            ItemType.JANITOR_KEYCARD
        };


        private readonly Dictionary<int, string> keypadsperm = new Dictionary<int, string>()
        {
            { 0,    "CONT_LVL_1,CONT_LVL_2,CONT_LVL_3,ARMORY_LVL_1,ARMORY_LVL_2,ARMORY_LVL_3,CHCKPOINT_ACC,EXIT_ACC,INCOM_ACC"          },
            { 1,    "CONT_LVL_1,CONT_LVL_2,ARMORY_LVL_1,ARMORY_LVL_2,ARMORY_LVL_3,CHCKPOINT_ACC,EXIT_ACC,INCOM_ACC"                     },
            { 2,    "CONT_LVL_1,CONT_LVL_2,CONT_LVL_3,CHCKPOINT_ACC,EXIT_ACC,INCOM_ACC"                                                 },
            { 3,    "CONT_LVL_1,CONT_LVL_2,ARMORY_LVL_1,ARMORY_LVL_2,ARMORY_LVL_3,CHCKPOINT_ACC,EXIT_ACC,INCOM_ACC"                     },
            { 4,    "CONT_LVL_1,CONT_LVL_2,ARMORY_LVL_1,ARMORY_LVL_2,CHCKPOINT_ACC,EXIT_ACC"                                            },
            { 5,    "CONT_LVL_1,CONT_LVL_2,CONT_LVL_3,CHCKPOINT_ACC,INCOM_ACC"                                                          },
            { 6,    "CONT_LVL_1,CONT_LVL_2,ARMORY_LVL_1,ARMORY_LVL_2,CHCKPOINT_ACC"                                                     },
            { 7,    "CONT_LVL_1,ARMORY_LVL_1,CHCKPOINT_ACC"                                                                             },
            { 8,    "CHCKPOINT_ACC"                                                                                                     },
            { 9,    "CONT_LVL_1,CONT_LVL_2,CHCKPOINT_ACC"                                                                               },
            { 10,   "CONT_LVL_1,CONT_LVL_2"                                                                                             },
            { 11,   "CONT_LVL_1"                                                                                                        },
        };


        public void OnDoorAccess(PlayerDoorAccessEvent ev)
        {
            if (ev.Door.Destroyed == false && ev.Door.BlockAfterWarheadDetonation == false && ev.Player.GetInventory().Count > 0 && ev.Door.Locked == false && ev.Door.Permission != String.Empty && ev.Player.TeamRole.Team != Team.SCP)
            {
                if (ConfigManagers.RPCDebug) plugin.Info($"Door perm: {ev.Door.Permission}");
                int z = 0;
                foreach (ItemType item in keypads)
                {
                    if (ConfigManagers.RPCDebug) plugin.Info($"Check has item: {item}...");
                    if (ConfigManagers.cardlist.Contains(z))
                    {
                        if (ev.Player.HasItem(item))
                        {
                            if (ConfigManagers.RPCDebug) plugin.Info($"Successfully found item: {item}");
                            if (ConfigManagers.RPCDebug) plugin.Info($"Z = {z}");
                            string[] perms = keypadsperm[z].Split(',');
                            foreach (string perm in perms)
                            {
                                if (ConfigManagers.RPCDebug) plugin.Info($"Check {perm} permission...");
                                if (ev.Door.Permission == perm)
                                {
                                    ev.Allow = true;
                                    if (ConfigManagers.RPCDebug) plugin.Info($"Successfully on {perm} permission!");
                                    if (ConfigManagers.RPCInfo) plugin.Info($"Player {ev.Player.Name} open the door '{ev.Door.Name}' with the help '{item}' thanks to permission '{perm}'.");
                                    break;
                                }
                            }
                        }
                    }
                    else if (ConfigManagers.RPCDebug) plugin.Info($"The card {item} is locked for remote use");
                    z++;
                }
            }
        }

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            if (plugin.GetConfigBool("rpc_disable")) PluginManager.Manager.DisablePlugin(plugin);
        }
    }
}