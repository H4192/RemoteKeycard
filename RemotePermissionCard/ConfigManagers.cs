using System.Collections.Generic;

namespace RemotePermissionCard
{
    public class ConfigManagers
    {
        public static List<int> cardlist = new List<int>();
        public static bool RPCDebug;
        public static bool RPCInfo;
        public static void ReloadConfig(RemotePermissionCard plugin)
        {
            RPCDebug = plugin.GetConfigBool("rpc_debug");
            RPCInfo = plugin.GetConfigBool("rpc_info");

            string cards = plugin.GetConfigString("rpc_cards");
            foreach (string card in cards.Split(','))
            {
                int cardid = int.TryParse(card.Trim(), out int z) ? z : -1;
                if (!(cardid >= 0) && !(cardid <= 12))
                    plugin.Warn($"Incorrect value {card}, fix this value!");
                else
                {
                    if (!cardlist.Contains(cardid)) cardlist.Add(cardid);
                    else plugin.Warn($"Duplicate value {cardid}, fix this duplicate!");
                }
            }
        }
    }
}
