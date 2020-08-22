using Discord;
using Discord.Gateway;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Windows.Forms;
using Console = Colorful.Console;

namespace Xtools
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists("data.json")) Environment.Exit(0);
            DataJson answer = JsonConvert.DeserializeObject<DataJson>(File.ReadAllText("data.json"));
            if (answer.startup == "true")
            {
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.SetValue("xTools", Application.ExecutablePath.ToString());
            }
            Console.Title = "Xtools";
            //-------------------------- / Start

            client.OnMessageReceived += client_OnMessageReceived;
            client.Login(answer.token);
            Console.Title = $"Xtools | v{Version()} | {client.User}";
            //-------------------------- / Starting
            Helper.Logo();
            Helper.Dots();
            Console.WriteLine();
            Console.ReadKey();
            //-------------------------- / Writing Text
        }

        private static string Version()
        {
            try
            {
                return new WebClient().DownloadString("https://pastebin.com/raw/71SYz2Hd");
            }
            catch { Environment.Exit(0); return string.Empty; }
        }

        private static void client_OnMessageReceived(DiscordSocketClient client, MessageEventArgs args)
		{
            var NewURL = "https://discord.gift/";
            var OLDUrl = "https://discordapp.com/gifts/";
            var URL    = "discord.gift/";
            var URL2   = "discordapp.com/gifts/";

            var Content = args.Message.Content.ToString();
            
            if ((Content.Contains(URL) && Content.Replace(NewURL, "").Length == 16) || (Content.Contains(URL2) && Content.Replace(OLDUrl, "").Length == 24) || Content.Contains(URL2) || Content.Contains(URL))
            {
                string Code = string.Empty;
                if (Content.Replace(NewURL, "").Length == 16)
                    Code = Content.Replace(NewURL, "");

                else if (Content.Replace(URL, "").Length == 16)
                    Code = Content.Replace(NewURL, "");

                else if (Content.Replace(NewURL, "").Length == 24)
                    Code = Content.Replace(NewURL, "");

                else if (Content.Replace(URL2, "").Length == 24)
                    Code = Content.Replace(URL2, "");

                try
                {
                    client.RedeemNitroGift(Code, args.Message.Channel.Id);
                    var NitroInfo = client.GetNitroGift(Code).SubscriptionPlan.Name;
                    if (NitroInfo.Contains("classic"))
                    {
                        ClassicMonth++;
                        Helper.WriteNitro(Helper.Nitros.ClassicMonth, client.GetGuild(args.Message.Guild.Id).Name);
                    }
                    else if (NitroInfo.Contains("Classic") && NitroInfo.Contains("Yearly"))
                    {
                        ClassicYearly++;
                        Helper.WriteNitro(Helper.Nitros.ClassicYearly, client.GetGuild(args.Message.Guild.Id).Name);
                    }
                    else if (NitroInfo == "Nitro Yearly")
                    {
                        NitroYearly++;
                        Helper.WriteNitro(Helper.Nitros.NitroYearly, client.GetGuild(args.Message.Guild.Id).Name);
                    }
                    else if (NitroInfo.Contains("Quarterly"))
                    {
                        Quarterly++;
                        Helper.WriteNitro(Helper.Nitros.Quarterly, client.GetGuild(args.Message.Guild.Id).Name);
                    }
                    else
                    {
                        NitroMonth++;
                        Helper.WriteNitro(Helper.Nitros.NitroMonth, client.GetGuild(args.Message.Guild.Id).Name);
                    }
                }
                catch (DiscordHttpException ex)
                {
                    DiscordError Ercode = ex.Code;
                    if (Ercode != DiscordError.UnknownGiftCode)
                    {
                        if (Ercode == DiscordError.NitroGiftRedeemed)
                        {
                            Invalid++;
                            Helper.WriteError(Helper.ErrorTypes.Redeemed, Code, client.GetGuild(args.Message.Guild.Id).Name);
                        }
                        else
                        {
                            Invalid++;
                            Helper.WriteError(Helper.ErrorTypes.Invalid, Code, client.GetGuild(args.Message.Guild.Id).Name);
                        }
                    }
                    else
                    {
                        Invalid++;
                        Helper.WriteError(Helper.ErrorTypes.Invalid, Code, client.GetGuild(args.Message.Guild.Id).Name);
                    }
                }
            }

            ulong GiveawayPartyID = 294882584201003009;
            string content = args.Message.Content;
            if (args.Message.Author.User.Id == GiveawayPartyID && !content.Contains("A winner could not be determined!") && !content.Contains("Giveaway time must not be shorter than") && !content.Contains("\ud83d\udca5") && !content.Contains("Please type the name of a channel in this server.") && !content.Contains("how long should the giveaway last?") && !content.Contains("Now, how many winners should there be?") && !content.Contains("Finally, what do you want to give away?") && !content.Contains("The new winner is") && !content.Contains("Done! The giveaway for the") && !content.Contains("Congratulations"))
            {
                client.AddMessageReaction(client.GetChannel(args.Message.Channel.Id).Id, args.Message.Id, "🎉");
                JoinedGiveaways++;
                Helper.WriteGiveaway(Helper.GiveawayTypes.Joined, client.GetGuild(args.Message.Guild.Id).Name, client.GetChannel(args.Message.Channel.Id).Name);
            }
            if (args.Message.Author.User.Id == GiveawayPartyID && args.Message.Content.Contains($"<@{Program.client.User.Id}>"))
            {
                WonGiveaways++;
                Helper.WriteGiveaway(Helper.GiveawayTypes.Won, client.GetGuild(args.Message.Guild.Id).Name, client.GetChannel(args.Message.Channel.Id).Name);
            }
        }
		public class DataJson
        {
            [JsonProperty("token")]
            public string token { get; set; }
            [JsonProperty("startup")]
            public string startup { get; set; }
        }
        public static DiscordSocketClient client = new DiscordSocketClient();

        public static int ClassicMonth    = 0;
        public static int ClassicYearly   = 0;
        public static int NitroMonth      = 0;
        public static int NitroYearly     = 0;
        public static int Quarterly       = 0;
        public static int Invalid         = 0;
        public static int WonGiveaways    = 0;
        public static int JoinedGiveaways = 0;
    }
}
