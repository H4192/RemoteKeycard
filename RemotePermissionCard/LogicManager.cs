using Smod2.API;
using Smod2.Events;
using System;

namespace RemotePermissionCard
{
    public enum ItemInt
    {
        NULL = -1,
        O5_LEVEL_KEYCARD = 0,
        CHAOS_INSURGENCY_DEVICE = 1,
        FACILITY_MANAGER_KEYCARD = 2,
        MTF_COMMANDER_KEYCARD = 3,
        MTF_LIEUTENANT_KEYCARD = 4,
        CONTAINMENT_ENGINEER_KEYCARD = 5,
        SENIOR_GUARD_KEYCARD = 6,
        GUARD_KEYCARD = 7,
        ZONE_MANAGER_KEYCARD = 8,
        MAJOR_SCIENTIST_KEYCARD = 9,
        SCIENTIST_KEYCARD = 10,
        JANITOR_KEYCARD = 11
    }

    public class LogicManager
    {
        // Remote
        public static void LogicOneRemote(RemotePermissionCard plugin, PlayerDoorAccessEvent ev)
        {
            if (ev.Door.Permission != string.Empty && ev.Allow == false && ev.Destroy == false && ev.Player.GetInventory().Count > 0 && ev.Door.Locked == false)
            {
                for (int z = 0; z < 12; z++)
                {
                    if (ConfigManagers.CardsList.Contains(z))
                    {
                        ItemType item = ConfigManagers.DCard[z];
                        if (ev.Player.HasItem(item))
                        {
                            if (ConfigManagers.RPCDebug) plugin.Info($"Successfully found item: '{item}' on number: '{z}'");
                            if (ConfigManagers.DefaultCardAccess[z].perms.Contains(ev.Door.Permission)) ev.Allow = true;
                            if (ConfigManagers.RPCInfo) plugin.Info($"Player {ev.Player.Name} open the door '{ev.Door.Name}' with the help '{item}' thanks to permission '{ev.Door.Permission}'.");
                            break;
                        }
                    }
                }
            }
        }

        // Custom Card Access +- Remote and Default
        public static void LogicTwo(RemotePermissionCard plugin, PlayerDoorAccessEvent ev)
        {
            if (ev.Door.Permission != string.Empty && ev.Door.Destroyed == false && ev.Door.Locked == false)
            {
                ItemInt CurrentCard = Enum.TryParse(ev.Player.GetCurrentItem().ItemType.ToString(), out ItemInt z) ? z : ItemInt.NULL;

                int CardID = (int)CurrentCard;
                if (ConfigManagers.CustomCardAccess.ContainsKey(CardID))
                {
                    if (ConfigManagers.CustomCardAccess[CardID].perms.Contains(ev.Door.Permission))
                    {
                        ev.Allow = true;
                    }
                    else
                    {
                        if (ConfigManagers.RPCRemote)
                        {
                            bool Really = false;
                            for (int x = 0; x < 12; z++)
                            {
                                if (ConfigManagers.CardsList.Contains(x))
                                {
                                    ItemInt CurrentItem = (ItemInt)x;
                                    ItemType ResultItem = (ItemType)Enum.Parse(typeof(ItemType), CurrentItem.ToString());

                                    if (ev.Player.HasItem(ResultItem))
                                    {
                                        if (ConfigManagers.CustomCardAccess.ContainsKey(x))
                                        {
                                            if (ConfigManagers.CustomCardAccess[x].perms.Contains(ev.Door.Permission))
                                            {
                                                Really = true;
                                                ev.Allow = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            if (!Really) ev.Allow = false;
                        }
                        else ev.Allow = false;
                    }
                }
                else if (ConfigManagers.RPCDefaultIfNone)
                {
                    if (ConfigManagers.DefaultCardAccess.ContainsKey(CardID))
                    {
                        if (ConfigManagers.DefaultCardAccess[CardID].perms.Contains(ev.Door.Permission)) ev.Allow = true;
                        else
                        {
                            if (ConfigManagers.RPCRemote)
                            {
                                bool Really = false;
                                for (int n = 0; n < 12; n++)
                                {
                                    if (ConfigManagers.CardsList.Contains(n))
                                    {
                                        ItemInt CurrentItem = (ItemInt)n;
                                        ItemType ResultItem = (ItemType)Enum.Parse(typeof(ItemType), CurrentItem.ToString());

                                        if (ev.Player.HasItem(ResultItem))
                                        {
                                            if (ConfigManagers.DefaultCardAccess[n].perms.Contains(ev.Door.Permission))
                                            {
                                                Really = true;
                                                ev.Allow = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (!Really) ev.Allow = false;
                            }
                        }
                    }
                    else ev.Allow = false;
                }
                else ev.Allow = false;
            }
        }

        // Door List +- Remote and Default
        public static void LogicTree(RemotePermissionCard plugin, PlayerDoorAccessEvent ev)
        {
            if (ev.Door.Name != string.Empty && ev.Door.Destroyed == false && ev.Door.Locked == false)
            {
                ItemInt CurrentCard = Enum.TryParse<ItemInt>(ev.Player.GetCurrentItem().ItemType.ToString(), out ItemInt z) ? z : ItemInt.NULL;

                int CardID = (int)CurrentCard;
                if (ConfigManagers.CustomDoorList.ContainsKey(ev.Door.Name))
                {
                    if (ConfigManagers.CustomDoorList[ev.Door.Name].ints.Contains(CardID)) ev.Allow = true;
                    else
                    {
                        if (ConfigManagers.RPCRemote)
                        {
                            bool Really = false;
                            for (int v = 0; v < 12; v++)
                            {
                                if (ConfigManagers.CardsList.Contains(v))
                                {
                                    ItemInt CurrentItem = (ItemInt)v;
                                    ItemType ResultItem = (ItemType)Enum.Parse(typeof(ItemType), CurrentItem.ToString());

                                    if (ev.Player.HasItem(ResultItem))
                                    {
                                        if (ConfigManagers.CustomDoorList[ev.Door.Name].ints.Contains(v))
                                        {
                                            Really = true;
                                            ev.Allow = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (!Really) ev.Allow = false;
                        }
                        else ev.Allow = false;
                    }
                }
                else if (ConfigManagers.RPCDefaultIfNone)
                {
                    if (ConfigManagers.DefaultDoorList.ContainsKey(ev.Door.Name))
                    {
                        if (ConfigManagers.DefaultDoorList[ev.Door.Name].ints.Contains(CardID)) ev.Allow = true;
                        else
                        {
                            if (ConfigManagers.RPCRemote)
                            {
                                bool Really = false;
                                for (int k = 0; k < 12; k++)
                                {
                                    if (ConfigManagers.CardsList.Contains(k))
                                    {
                                        ItemInt CurrentItem = (ItemInt)k;
                                        ItemType ResultItem = (ItemType)Enum.Parse(typeof(ItemType), CurrentItem.ToString());

                                        if (ev.Player.HasItem(ResultItem))
                                        {
                                            if (ConfigManagers.DefaultDoorList[ev.Door.Name].ints.Contains(k))
                                            {
                                                Really = true;
                                                ev.Allow = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (!Really) ev.Allow = false;
                            }
                            else ev.Allow = false;
                        }
                    }
                    else ev.Allow = true;
                }
                else ev.Allow = false;
            }
        }

        // Door Access +- Remote and Default
        public static void LogicFour(RemotePermissionCard plugin, PlayerDoorAccessEvent ev)
        {
            if (ev.Door.Name != string.Empty && ev.Door.Locked == false && ev.Door.Destroyed == false)
            {
                ItemInt CurrentCard = Enum.TryParse<ItemInt>(ev.Player.GetCurrentItem().ItemType.ToString(), out ItemInt z) ? z : ItemInt.NULL;

                int CardID = (int)CurrentCard;
                if (ConfigManagers.CustomDoorAccess.ContainsKey(ev.Door.Name))
                {
                    if (ConfigManagers.DefaultCardAccess[CardID].perms.Contains(ConfigManagers.CustomDoorAccess[ev.Door.Name])) ev.Allow = true;
                    else
                    {
                        if (ConfigManagers.RPCRemote)
                        {
                            bool Really = false;
                            for (int b = 0; b < 12; b++)
                            {
                                if (ConfigManagers.CardsList.Contains(b))
                                {
                                    ItemInt CurrentItem = (ItemInt)b;
                                    ItemType ResultItem = (ItemType)Enum.Parse(typeof(ItemType), CurrentItem.ToString());

                                    if (ev.Player.HasItem(ResultItem))
                                    {
                                        if (ConfigManagers.DefaultCardAccess[b].perms.Contains(ConfigManagers.CustomDoorAccess[ev.Door.Name]))
                                        {
                                            Really = true;
                                            ev.Allow = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (!Really) ev.Allow = false;
                        }
                        else ev.Allow = false;
                    }
                }
                else if (ConfigManagers.RPCDefaultIfNone)
                {
                    if (ConfigManagers.DefaultDoorAccess.ContainsKey(ev.Door.Name))
                    {
                        if (ConfigManagers.DefaultCardAccess[CardID].perms.Contains(ConfigManagers.DefaultDoorAccess[ev.Door.Name])) ev.Allow = true;
                        else
                        {
                            if (ConfigManagers.RPCRemote)
                            {
                                bool Really = false;
                                for (int f = 0; f < 12; f++)
                                {
                                    if (ConfigManagers.CardsList.Contains(f))
                                    {
                                        ItemInt CurrentItem = (ItemInt)f;
                                        ItemType ResultItem = (ItemType)Enum.Parse(typeof(ItemType), CurrentItem.ToString());

                                        if (ev.Player.HasItem(ResultItem))
                                        {
                                            if (ConfigManagers.DefaultCardAccess[f].perms.Contains(ConfigManagers.DefaultDoorAccess[ev.Door.Name]))
                                            {
                                                Really = true;
                                                ev.Allow = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                                if (!Really) ev.Allow = false;
                            }
                            else ev.Allow = false;
                        }
                    }
                    else ev.Allow = true;
                }
                else ev.Allow = false;
            }
        }

        // Door List + Door Access +- Remote and Default
        public static void LogicFive(RemotePermissionCard pluign, PlayerDoorAccessEvent ev)
        {
            if (ev.Door.Name != string.Empty && ev.Door.Locked == false && ev.Door.Destroyed == false)
            {
                ItemInt CurrentCard = Enum.TryParse<ItemInt>(ev.Player.GetCurrentItem().ItemType.ToString(), out ItemInt z) ? z : ItemInt.NULL;

                int CardID = (int)CurrentCard;
                if (ConfigManagers.CustomDoorList.ContainsKey(ev.Door.Name) && ConfigManagers.CustomDoorAccess.ContainsKey(ev.Door.Name))
                {
                    if (ConfigManagers.CustomDoorList[ev.Door.Name].ints.Contains(CardID) && ConfigManagers.DefaultCardAccess[CardID].perms.Contains(ConfigManagers.CustomDoorAccess[ev.Door.Name])) ev.Allow = true;
                    else
                    {
                        if (ConfigManagers.RPCRemote)
                        {
                            bool Really = false;
                            for (int g = 0; g < 12; g++)
                            {
                                ItemInt CurrentItem = (ItemInt)g;
                                ItemType ResultItem = (ItemType)Enum.Parse(typeof(ItemType), CurrentItem.ToString());
                                if (ev.Player.HasItem(ResultItem))
                                {
                                    if (ConfigManagers.CustomDoorList[ev.Door.Name].ints.Contains(g) && ConfigManagers.DefaultCardAccess[g].perms.Contains(ConfigManagers.CustomDoorAccess[ev.Door.Name]))
                                    {
                                        Really = true;
                                        ev.Allow = true;
                                        break;
                                    }
                                }
                            }
                            if (!Really) ev.Allow = false;
                        }
                        else ev.Allow = false;
                    }
                }
                else if (ConfigManagers.RPCDefaultIfNone)
                {
                    if (ConfigManagers.DefaultDoorAccess.ContainsKey(ev.Door.Name) && ConfigManagers.DefaultDoorList.ContainsKey(ev.Door.Name))
                    {
                        if (ConfigManagers.DefaultDoorList[ev.Door.Name].ints.Contains(CardID) && ConfigManagers.DefaultCardAccess[CardID].perms.Contains(ConfigManagers.DefaultDoorAccess[ev.Door.Name])) ev.Allow = true;
                        else ev.Allow = false;
                    }
                    else
                    {
                        if (ConfigManagers.RPCRemote)
                        {
                            bool Really = false;
                            for (int h = 0; h < 12; h++)
                            {
                                if (ConfigManagers.DefaultDoorList[ev.Door.Name].ints.Contains(h) && ConfigManagers.DefaultCardAccess[h].perms.Contains(ConfigManagers.DefaultDoorAccess[ev.Door.Name]))
                                {
                                    Really = true;
                                    ev.Allow = true;
                                    break;
                                }
                            }
                            if (!Really) ev.Allow = false;
                        }
                        else ev.Allow = false;
                    }
                }
                else ev.Allow = false;
            }
        }

        // Custom Card Access + Door Access +- Remote and Default
        public static void LogicSix(RemotePermissionCard pluign, PlayerDoorAccessEvent ev)
        {
            if (ev.Door.Name != string.Empty && ev.Door.Locked == false && ev.Door.Destroyed == false)
            {
                ItemInt CurrentCard = Enum.TryParse<ItemInt>(ev.Player.GetCurrentItem().ItemType.ToString(), out ItemInt z) ? z : ItemInt.NULL;

                int CardID = (int)CurrentCard;
                if (ConfigManagers.CustomCardAccess.ContainsKey(CardID) && ConfigManagers.CustomDoorAccess.ContainsKey(ev.Door.Name))
                {
                    if (ConfigManagers.CustomCardAccess[CardID].perms.Contains(ConfigManagers.CustomDoorAccess[ev.Door.Name])) ev.Allow = true;
                    else
                    {
                        if (ConfigManagers.RPCRemote)
                        {
                            bool Really = false;
                            for (int j = 0; j < 12; j++)
                            {
                                ItemInt CurrentItem = (ItemInt)j;
                                ItemType ResultItem = (ItemType)Enum.Parse(typeof(ItemType), CurrentItem.ToString());

                                if (ev.Player.HasItem(ResultItem))
                                {
                                    if (ConfigManagers.CustomCardAccess.ContainsKey(j))
                                    {
                                        if (ConfigManagers.CustomCardAccess[j].perms.Contains(ConfigManagers.CustomDoorAccess[ev.Door.Name]))
                                        {
                                            Really = true;
                                            ev.Allow = true;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (!Really) ev.Allow = false;
                        }
                        else ev.Allow = false;
                    }
                }
                else if (ConfigManagers.RPCDefaultIfNone)
                {
                    if (ConfigManagers.DefaultDoorAccess.ContainsKey(ev.Door.Name) && ConfigManagers.DefaultCardAccess.ContainsKey(CardID))
                    {
                        if (ConfigManagers.DefaultCardAccess[CardID].perms.Contains(ConfigManagers.DefaultDoorAccess[ev.Door.Name])) ev.Allow = true;
                        else
                        {
                            if (ConfigManagers.RPCRemote)
                            {
                                bool Really = false;
                                for (int t = 0; t < 12; t++)
                                {
                                    ItemInt CurrentItem = (ItemInt)t;
                                    ItemType ResultItem = (ItemType)Enum.Parse(typeof(ItemType), CurrentItem.ToString());

                                    if (ev.Player.HasItem(ResultItem))
                                    {
                                        if (ConfigManagers.DefaultCardAccess[t].perms.Contains(ConfigManagers.DefaultDoorAccess[ev.Door.Name]))
                                        {
                                            Really = true;
                                            ev.Allow = true;
                                            break;
                                        }
                                    }
                                }
                                if (!Really) ev.Allow = false;
                            }
                            else ev.Allow = false;
                        }
                    }
                    else ev.Allow = false;
                }
                else ev.Allow = false;
            }
        }
    }
}
