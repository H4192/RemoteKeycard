using Smod2.API;
using Smod2.Config;
using System.Collections.Generic;
using System.Linq;

namespace RemotePermissionCard
{
    public class ConfigManagers
    {
        public bool RPCInfo { get; private set; }
        public bool RPCPermissionMode { get; private set; }
        public bool RPCRemote { get; private set; }
        public bool RPCDefaultIfNone { get; private set; }

        public int RPCMode;

        private static ConfigManagers singleton;
        public static ConfigManagers Manager
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new ConfigManagers();
                }
                return singleton;
            }
        }

        // Readonly  
        private readonly List<string> DoorPerms = new List<string>()
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
        private readonly List<string> NameDoors = new List<string>()
        {
            "HCZ_ARMORY",
            "914",
            "012_BOTTOM",
            "106_BOTTOM",
            "LCZ_ARMORY",
            "GATE_A",
            "106_SECONDARY",
            "GATE_B",
            "012",
            "079_SECOND",
            "106_PRIMARY",
            "049_ARMORY",
            "NUKE_SURFACE",
            "NUKE_ARMORY",
            "CHECKPOINT_ENT",
            "CHECKPOINT_LCZ_B",
            "HID_RIGHT",
            "173_ARMORY",
            "CHECKPOINT_LCZ_A",
            "173",
            "ESCAPE",
            "HID_LEFT",
            "HID",
            "096",
            "372",
            "ESCAPE_INNER",
            "SURFACE_GATE",
            "INTERCOM",
            "079_FIRST"
        };
        private readonly List<string> RPCModes = new List<string>()
        {
            { "Remote Door Controls" },
            { "Custom Access Card" },
            { "Door List" },
            { "Door Access" },
            { "Door List + Door Access" },
            { "Custom Access Card + Door Access" }
        };

        internal readonly Dictionary<int, PList> DefaultCardAccess = new Dictionary<int, PList>()
        {
            { 0,    new PList(new List<string>(new string[] { "CONT_LVL_1", "CONT_LVL_2", "CONT_LVL_3", "ARMORY_LVL_1", "ARMORY_LVL_2", "ARMORY_LVL_3", "CHCKPOINT_ACC", "EXIT_ACC", "INCOM_ACC" }))    },
            { 1,    new PList(new List<string>(new string[] { "CONT_LVL_1", "CONT_LVL_2", "ARMORY_LVL_1", "ARMORY_LVL_2", "ARMORY_LVL_3", "CHCKPOINT_ACC", "EXIT_ACC", "INCOM_ACC" }))                  },
            { 2,    new PList(new List<string>(new string[] { "CONT_LVL_1", "CONT_LVL_2", "CONT_LVL_3", "CHCKPOINT_ACC", "EXIT_ACC", "INCOM_ACC" } ))                                                   },
            { 3,    new PList(new List<string>(new string[] { "CONT_LVL_1", "CONT_LVL_2", "ARMORY_LVL_1", "ARMORY_LVL_2", "ARMORY_LVL_3", "CHCKPOINT_ACC", "EXIT_ACC", "INCOM_ACC" }))                  },
            { 4,    new PList(new List<string>(new string[] { "CONT_LVL_1", "CONT_LVL_2", "ARMORY_LVL_1", "ARMORY_LVL_2", "CHCKPOINT_ACC", "EXIT_ACC" }))                                               },
            { 5,    new PList(new List<string>(new string[] { "CONT_LVL_1", "CONT_LVL_2", "CONT_LVL_3", "CHCKPOINT_ACC", "INCOM_ACC" }))                                                                },
            { 6,    new PList(new List<string>(new string[] { "CONT_LVL_1", "CONT_LVL_2", "ARMORY_LVL_1", "ARMORY_LVL_2", "CHCKPOINT_ACC" }))                                                           },
            { 7,    new PList(new List<string>(new string[] { "CONT_LVL_1", "ARMORY_LVL_1", "CHCKPOINT_ACC" }))                                                                                         },
            { 8,    new PList(new List<string>(new string[] { "CHCKPOINT_ACC" }))                                                                                                                       },
            { 9,    new PList(new List<string>(new string[] { "CONT_LVL_1", "CONT_LVL_2", "CHCKPOINT_ACC" }))                                                                                           },
            { 10,   new PList(new List<string>(new string[] { "CONT_LVL_1", "CONT_LVL_2" }))                                                                                                            },
            { 11,   new PList(new List<string>(new string[] { "CONT_LVL_1" }))                                                                                                                          },
        };
        internal readonly Dictionary<string, string> DefaultDoorAccess = new Dictionary<string, string>()
        {
            { "HCZ_ARMORY",                     "ARMORY_LVL_1"  },
            { "106_SECONDARY",                  "CONT_LVL_3"    },
            { "914",                            "CONT_LVL_1"    },
            { "LCZ_ARMORY",                     "ARMORY_LVL_1"  },
            { "079_SECOND",                     "CONT_LVL_3"    },
            { "GATE_A",                         "EXIT_ACC"      },
            { "GATE_B",                         "EXIT_ACC"      },
            { "106_BOTTOM",                     "CONT_LVL_3"    },
            { "106_PRIMARY",                    "CONT_LVL_3"    },
            { "NUKE_ARMORY",                    "ARMORY_LVL_2"  },
            { "012",                            "CONT_LVL_2"    },
            { "049_ARMORY",                     "ARMORY_LVL_2"  },
            { "CHECKPOINT_ENT",                 "CHCKPOINT_ACC" },
            { "NUKE_SURFACE",                   "CONT_LVL_3"    },
            { "CHECKPOINT_LCZ_A",               "CHCKPOINT_ACC" },
            { "CHECKPOINT_LCZ_B",               "CHCKPOINT_ACC" },
            { "HID",                            "ARMORY_LVL_3"  },
            { "079_FIRST",                      "CONT_LVL_3"    },
            { "096",                            "CONT_LVL_2"    },
            { "INTERCOM",                       "INCOM_ACC"     }
        };
        internal readonly Dictionary<string, CList> DefaultDoorList = new Dictionary<string, CList>()
        {
            { "HCZ_ARMORY",                     new CList(new List<int>(new int[] { 0,1,3,4,6,7 }))                 },
            { "106_SECONDARY",                  new CList(new List<int>(new int[] { 0,2,5 }))                       },
            { "914",                            new CList(new List<int>(new int[] { 0,1,2,3,4,5,6,7,8,9,10,11 }))   },
            { "LCZ_ARMORY",                     new CList(new List<int>(new int[] { 0,1,3,4,6,7 }))                 },
            { "079_SECOND",                     new CList(new List<int>(new int[] { 0,2,5 }))                       },
            { "GATE_A",                         new CList(new List<int>(new int[] { 0,1,2,3,4 }))                   },
            { "GATE_B",                         new CList(new List<int>(new int[] { 0,1,2,3,4 }))                   },
            { "106_BOTTOM",                     new CList(new List<int>(new int[] { 0,2,5 }))                       },
            { "106_PRIMARY",                    new CList(new List<int>(new int[] { 0,2,5 }))                       },
            { "NUKE_ARMORY",                    new CList(new List<int>(new int[] { 0,1,3,4,6 }))                   },
            { "012",                            new CList(new List<int>(new int[] { 0,1,2,3,4,5,6,9,10 }))          },
            { "049_ARMORY",                     new CList(new List<int>(new int[] { 0,1,3,4,6 }))                   },
            { "CHECKPOINT_ENT",                 new CList(new List<int>(new int[] { 0,1,2,3,4,5,6,7,8,9 }))         },
            { "NUKE_SURFACE",                   new CList(new List<int>(new int[] { 0,2,5 }))                       },
            { "CHECKPOINT_LCZ_A",               new CList(new List<int>(new int[] { 0,1,2,3,4,5,6,7,8,9 }))         },
            { "CHECKPOINT_LCZ_B",               new CList(new List<int>(new int[] { 0,1,2,3,4,5,6,7,8,9 }))         },
            { "HID",                            new CList(new List<int>(new int[] { 0,1,3 }))                       },
            { "079_FIRST",                      new CList(new List<int>(new int[] { 0,2 }))                         },
            { "096",                            new CList(new List<int>(new int[] { 0,1,2,3,4,5,6,9,10 }))          },
            { "INTERCOM",                       new CList(new List<int>(new int[] { 0,1,2,3,5 }))                   }
        };
        internal readonly List<ItemType> DCard = new List<ItemType>()
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

        // Customs
        internal readonly List<int> CardsList = new List<int>();
        internal readonly Dictionary<int, PList> CustomCardAccess = new Dictionary<int, PList>();
        internal readonly Dictionary<string, string> CustomDoorAccess = new Dictionary<string, string>();
        internal readonly Dictionary<string, CList> CustomDoorList = new Dictionary<string, CList>();

        internal void ReloadConfig()
        {
            int CONFIG_MODE = ConfigFile.GetInt("rpc_mode", 1);

            RPCInfo = ConfigFile.GetBool("rpc_info", true);
            RPCPermissionMode = ConfigFile.GetBool("rpc_permission", false);
            RPCRemote = ConfigFile.GetBool("rpc_remote", true);
            RPCDefaultIfNone = ConfigFile.GetBool("rpc_default_if_none", false);

            ClearingData();

            if (CONFIG_MODE > 0 && CONFIG_MODE < 7)
            {
                RPCMode = CONFIG_MODE;
                RemotePermissionCard.plugin.Info($"Successfully loaded mode {RPCModes[RPCMode]}");
            }
            else RemotePermissionCard.plugin.Warn("MODE1: The current mode is not recognized, the default value is '1'");

            if (RPCMode == 1) LoadRemote();
            else if (RPCMode == 2) LoadConfigModeOne();
            else if (RPCMode == 3) LoadConfigModeTwo();
            else if (RPCMode == 4) LoadConfigModeTree();
            else if (RPCMode == 5) LoadConfigModeFour();
            else if (RPCMode == 6) LoadConfigModeFive();
            else RemotePermissionCard.plugin.Warn("MODE2: Error recognizing the current work mode, contact the developer");
        }

        internal void LoadRemote()
        {
            if (RPCRemote)
            {
                string CardList = ConfigFile.GetString("rpc_card_list", "0,1,2,3,4,5,6,7,8,9,10,11");
                string[] Cards = CardList.Split(',');

                if (Cards[0].Trim() != string.Empty)
                {
                    for (int x = 0; x < Cards.Length; x++)
                    {
                        int CardID = int.TryParse(Cards[x], out int z) ? z : -1;

                        if (CardID >= 0 && CardID <= 11)
                        {
                            if (!CardsList.Contains(CardID)) CardsList.Add(CardID);
                            else RemotePermissionCard.plugin.Warn($"REMOTE4: Duplicate value '{CardID}' in CardsList");
                        }
                        else RemotePermissionCard.plugin.Warn($"REMOTE3: Incorrect value '{Cards[x]}'");
                    }
                }
                else RemotePermissionCard.plugin.Warn("REMOTE2: Incorrect format");
            }
            else if (RPCMode == 1) RemotePermissionCard.plugin.Warn("REMOTE1: Value 'rpc_remote' installed on 'false'. I don't know why he's working now ¯\\_(ツ)_/¯");
        }

        // Custom Access Card
        internal void LoadConfigModeOne()
        {
            LoadRemote();
            string ConfigCCA = ConfigFile.GetString("rpc_card_access", string.Empty);

            if (ConfigCCA != string.Empty)
            {
                string[] CardsAndPerms = ConfigCCA.Split(',');

                foreach (string CardAndPerm in CardsAndPerms)
                {
                    if (CardAndPerm.Contains(':'))
                    {
                        string[] CardAndPermDict = CardAndPerm.Split(':');
                        string Perm = CardAndPermDict[0].Trim().ToUpper();

                        if (Perm != string.Empty)
                        {
                            if (DoorPerms.Contains(Perm))
                            {
                                if (CardAndPermDict[1].Trim() != string.Empty)
                                {
                                    string[] Cards = CardAndPermDict[1].Split('&');

                                    foreach (string Card in Cards)
                                    {
                                        int CardID = int.TryParse(Card, out int z) ? z : -1;

                                        if (CardID >= 0 && CardID <= 11)
                                        {
                                            if (!CustomCardAccess.ContainsKey(CardID)) CustomCardAccess.Add(CardID, new PList(new List<string>(new string[] { Perm })));
                                            else
                                            {
                                                if (!CustomCardAccess[CardID].perms.Contains(Perm)) CustomCardAccess[CardID].perms.Add(Perm);
                                                else RemotePermissionCard.plugin.Warn($"CA7: Duplicate permission '{Perm}' in CardID '{CardID}'");
                                            }
                                        }
                                        else RemotePermissionCard.plugin.Warn($"CA6: Incorrect value '{Card.Trim()}'");
                                    }
                                }
                                else RemotePermissionCard.plugin.Warn($"CA5: CList value not set in permission '{Perm}'");
                            }
                            else RemotePermissionCard.plugin.Warn($"CA4: Wrong permission '{Perm}'");
                        }
                        else RemotePermissionCard.plugin.Warn("CA3: Permission value not set");

                    }
                    else RemotePermissionCard.plugin.Warn($"CA2: Incorrect format in the line '{CardAndPerm}'");
                }
            }
            else RemotePermissionCard.plugin.Warn("CA1: Incorrect format");
        }

        // Door List
        internal void LoadConfigModeTwo()
        {
            LoadRemote();
            string ConfigDL = ConfigFile.GetString("rpc_door_list", string.Empty);

            if (ConfigDL.Trim() != string.Empty)
            {
                string[] DoorsAndCards = ConfigDL.Split(',');

                if (DoorsAndCards[0].Trim() != string.Empty)
                {
                    foreach (string DoorAndCards in DoorsAndCards)
                    {
                        if (DoorAndCards.Contains(':'))
                        {
                            string[] DoorAndCardsDict = DoorAndCards.Split(':');
                            string DoorName = DoorAndCardsDict[0].Trim().ToUpper();

                            if (DoorName != string.Empty)
                            {
                                if (NameDoors.Contains(DoorName))
                                {
                                    string[] DoorCards = DoorAndCardsDict[1].Split('&');

                                    if (DoorCards[0].Trim() != string.Empty)
                                    {
                                        for (int z = 0; z < DoorCards.Length; z++)
                                        {
                                            int CardID = int.TryParse(DoorCards[z], out int t) ? t : -1;

                                            if (CardID >= 0 && CardID <= 11)
                                            {
                                                if (!CustomDoorList.ContainsKey(DoorName)) CustomDoorList.Add(DoorName, new CList(new List<int>(new int[] { CardID })));
                                                else
                                                {
                                                    if (!CustomDoorList[DoorName].ints.Contains(CardID)) CustomDoorList[DoorName].ints.Add(CardID);
                                                    else RemotePermissionCard.plugin.Warn($"DL7: Duplicate CardID '{CardID}' in DoorName '{DoorName}'");
                                                }
                                            }
                                        }
                                    }
                                    else RemotePermissionCard.plugin.Warn($"DL6: Incorrect format CList in DoorName '{DoorName}'");
                                }
                                else RemotePermissionCard.plugin.Warn($"DL5: Incorrect name of door '{DoorName}'");
                            }
                            else RemotePermissionCard.plugin.Warn("DL4: DoorName value not set");
                        }
                        else RemotePermissionCard.plugin.Warn($"DL3: Incorrect format in the line '{DoorAndCards}'");
                    }
                }
                else RemotePermissionCard.plugin.Warn("DL2: Incorrect format");
            }
            else RemotePermissionCard.plugin.Warn("DL1: Incorrect format");
        }

        // Door Access
        internal void LoadConfigModeTree()
        {
            LoadRemote();
            string ConfigDA = ConfigFile.GetString("rpc_door_access", string.Empty);

            if (ConfigDA.Trim() != string.Empty)
            {
                string[] DoorsAndPerms = ConfigDA.Split(',');

                if (DoorsAndPerms[0].Trim() != string.Empty)
                {
                    foreach (string DoorAndPerms in DoorsAndPerms)
                    {
                        if (DoorAndPerms.Contains(':'))
                        {
                            string[] DoorAndPermsDict = DoorAndPerms.Split(':');
                            string DoorName = DoorAndPermsDict[0].Trim().ToUpper();

                            if (DoorName != string.Empty)
                            {
                                if (NameDoors.Contains(DoorName))
                                {
                                    string Perm = DoorAndPermsDict[1].Trim().ToUpper();

                                    if (Perm != string.Empty)
                                    {
                                        if (DoorPerms.Contains(Perm))
                                        {
                                            if (!CustomDoorAccess.ContainsKey(DoorName)) CustomDoorAccess.Add(DoorName, Perm);
                                            else RemotePermissionCard.plugin.Warn($"DA8: Duplicate permission '{Perm}' in DoorName '{DoorName}'");
                                        }
                                        else RemotePermissionCard.plugin.Warn($"DA7: Wrong permission '{Perm}'");
                                    }
                                    else RemotePermissionCard.plugin.Warn($"DA6: Permission value not set in door '{DoorName}'");
                                }
                                else RemotePermissionCard.plugin.Warn($"DA5: Incorrect name of door '{DoorName}'");
                            }
                            else RemotePermissionCard.plugin.Warn("DA4: DoorName value not set");
                        }
                        else RemotePermissionCard.plugin.Warn($"DA3: Incorrect format in the line '{DoorAndPerms}'");
                    }
                }
                else RemotePermissionCard.plugin.Warn("DA2: Incorrect format");
            }
            else RemotePermissionCard.plugin.Warn("DA1: Incorrect format");
        }

        // Door List + Door Access
        internal void LoadConfigModeFour()
        {
            LoadRemote();
            LoadConfigModeTwo();
            LoadConfigModeFour();
            // ¯\_(ツ)_/¯
        }

        // Custom Access Card + Door Access
        internal void LoadConfigModeFive()
        {
            LoadRemote();
            LoadConfigModeOne();
            LoadConfigModeTree();
            // ¯\_(ツ)_/¯ x2
        }

        internal void ClearingData()
        {
            CardsList.Clear();
            CustomCardAccess.Clear();
            CustomDoorAccess.Clear();
            CustomDoorList.Clear();
        }

        internal class PList
        {
            public List<string> perms;
            public PList(List<string> perms)
            {
                this.perms = perms;
            }
        }

        internal class CList
        {
            public List<int> ints;
            public CList(List<int> ints)
            {
                this.ints = ints;
            }
        }
    }
}
