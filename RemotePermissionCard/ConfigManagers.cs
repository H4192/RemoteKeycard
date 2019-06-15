using Smod2.API;
using System.Collections.Generic;
using System.Linq;

namespace RemotePermissionCard
{
    public class ConfigManagers
    {
        public static List<int> cardlist = new List<int>();
        public static bool RPCDebug;
        public static bool RPCInfo;
        public static bool RPCPermissionMode;
        public static Dictionary<int, string> Keycardperm;
        public static readonly List<ItemType> keycards = new List<ItemType>()
        {
            {  ItemType.O5_LEVEL_KEYCARD                },
            {  ItemType.CHAOS_INSURGENCY_DEVICE         },
            {  ItemType.FACILITY_MANAGER_KEYCARD        },
            {  ItemType.MTF_COMMANDER_KEYCARD           },
            {  ItemType.MTF_LIEUTENANT_KEYCARD          },
            {  ItemType.CONTAINMENT_ENGINEER_KEYCARD    },
            {  ItemType.SENIOR_GUARD_KEYCARD            },
            {  ItemType.GUARD_KEYCARD                   },
            {  ItemType.ZONE_MANAGER_KEYCARD            },
            {  ItemType.MAJOR_SCIENTIST_KEYCARD         },
            {  ItemType.SCIENTIST_KEYCARD               },
            {  ItemType.JANITOR_KEYCARD                 }
        };

        private static readonly List<string> permissions = new List<string>()
        {
            "CONT_LVL_1",
            "CONT_LVL_2",
            "CONT_LVL_3",
            "ARMORY_LVL_1",
            "ARMORY_LVL_2",
            "ARMORY_LVL_3",
            "CHCKPOINT_ACC",
            "EXIT_ACC",
            "INCOM_ACC"
        };

        public static Dictionary<int, string> customKeycardperm = new Dictionary<int, string>();
        public static readonly Dictionary<int, string> defaultKeycardsperm = new Dictionary<int, string>()
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

        public static void ReloadConfig(RemotePermissionCard plugin)
        {
            RPCDebug = plugin.GetConfigBool("rpc_debug");
            RPCInfo = plugin.GetConfigBool("rpc_info");
            RPCPermissionMode = plugin.GetConfigBool("rpc_permission");

            string cardsPerm = plugin.GetConfigString("rpc_door_access");
            string cards = plugin.GetConfigString("rpc_cards");
            foreach (string card in cards.Split(','))
            {
                int cardid = int.TryParse(card.Trim(), out int z) ? z : -1;
                if ((cardid < 0) || (cardid > 12)) // Thanks Laserman
                    plugin.Warn($"Incorrect value '{card}', fix this value!");
                else
                {
                    if (!cardlist.Contains(cardid)) cardlist.Add(cardid);
                    else plugin.Warn($"Duplicate value '{cardid}', fix this duplicate!");
                }
            }
            if (cardsPerm != string.Empty)
            {
                foreach (string cardPerm in cardsPerm.Split(','))
                {
                    if (cardPerm.Contains(':'))
                    {
                        string[] cardsDict = cardPerm.Split(':');
                        string[] cardsList = cardsDict[1].Split('&');

                        string currentperm = cardsDict[0].ToUpper().Trim();

                        if (permissions.Contains(currentperm))
                        {
                            if (cardsList.Length >= 1)
                            {
                                for (int z = 0; z < cardsList.Length; z++)
                                {
                                    int cardID = int.TryParse(cardsList[z], out int t) ? t : -1;
                                    if ((cardID >= 0) && (cardID <= 11))
                                    {
                                        if (!customKeycardperm.ContainsKey(cardID)) customKeycardperm.Add(cardID, currentperm);
                                        else
                                        {
                                            string[] currentPerms = customKeycardperm[cardID].Split(',');
                                            string updatedPerms = "";
                                            if (!currentPerms.Contains(currentperm))
                                            {
                                                foreach (string perm in currentPerms)
                                                {
                                                    updatedPerms += $"{perm},";
                                                }
                                                updatedPerms += currentperm;
                                                customKeycardperm.Remove(cardID);
                                                customKeycardperm.Add(cardID, updatedPerms);
                                            }
                                            else plugin.Warn($"Duplicate value '{currentperm}' in '{cardID}' ID item, fix this duplicate!");
                                        }
                                    }
                                    else plugin.Warn($"Incorrect value '{cardsList[z]}', fix this value!");
                                }
                            }
                            else if (cardsList.Length == 0)
                            {
                                int cardID = int.TryParse(cardsDict[1], out int t) ? t : -1;
                                if (cardID > -1 || cardID < 12)
                                {
                                    if (!customKeycardperm.ContainsKey(cardID)) customKeycardperm.Add(cardID, currentperm);
                                    else
                                    {
                                        string[] currentPerms = customKeycardperm[cardID].Split(',');
                                        string updatedPerms = "";
                                        if (!currentPerms.Contains(currentperm))
                                        {
                                            foreach (string perm in currentPerms)
                                            {
                                                updatedPerms += $"{perm},";
                                            }
                                            updatedPerms += currentperm;
                                            customKeycardperm.Remove(cardID);
                                            customKeycardperm.Add(cardID, updatedPerms);
                                        }
                                        else plugin.Warn($"Duplicate value '{currentperm}' in '{cardID}' ID item, fix this duplicate!");
                                    }
                                }
                                else plugin.Warn($"Incorrect value '{cardsDict[1]}', fix this value!");
                            }
                            else plugin.Warn($"Permission '{currentperm}' has no value");
                        }
                        else plugin.Warn($"Wrong permission: '{currentperm}'");
                    }
                    else plugin.Warn($"Incorrect format in the line: '{cardPerm}'");
                }
            }
            if (cardsPerm == string.Empty) Keycardperm = defaultKeycardsperm;
            else Keycardperm = customKeycardperm;
        }
    }
}
