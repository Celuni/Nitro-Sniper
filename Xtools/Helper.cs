using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xtools
{
    public class Helper
    {
        public static void Write(string value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
        }

        private static bool Login(bool check = false)
        {
            if (check)
            {
                var values = new NameValueCollection()
                {
                    ["hwid"] = "",
                    ["ip"]   = "",
                    [""]     = ""

                };

                
            }

            return true;
        }

        public static void Dots()
        {
            for (int i = 0; i < 50; i++)
                Console.Write("-");
        }
        public static void Logo()
        {
            Helper.Write(@"       ___________           .__          ", ConsoleColor.Magenta);
            Helper.Write(@"___  __\__    ___/___   ____ |  |   ______", ConsoleColor.Magenta);
            Helper.Write(@"\  \/  / |    | /  _ \ /  _ \|  |  /  ___/", ConsoleColor.Magenta);
            Helper.Write(@" >    <  |    |(  <_> |  <_> )  |__\___ \ ", ConsoleColor.Magenta);
            Helper.Write(@"/__/\_ \ |____| \____/ \____/|____/____  >", ConsoleColor.Magenta);
            Helper.Write(@"      \/                               \/ ", ConsoleColor.Magenta);
        }

        public enum Nitros
        {
            ClassicMonth,
            ClassicYearly,
            NitroMonth,
            NitroYearly,
            Quarterly
        }

        public enum ErrorTypes
        {
            Invalid,
            Redeemed
        }

        public enum GiveawayTypes
        {
            Won,
            Joined
        }
        public static void WriteNitro(Nitros nitro, string Server)
        {
            Console.WriteLine(nitro switch
            {
                Nitros.ClassicMonth  => $"[{Program.ClassicMonth}] Classic Nitro | One Month | {Server}",
                Nitros.ClassicYearly => $"[{Program.ClassicYearly}] Classic Nitro | One Year | {Server}", 
                Nitros.NitroMonth    => $"[{Program.NitroMonth}] Normal Nitro | One Month | {Server}",
                Nitros.NitroYearly   => $"[{Program.NitroYearly}] Normal Nitro | One Month | {Server}",
                Nitros.Quarterly     => $"[{Program.Quarterly}] Normal Nitro | Three Months | {Server}"
            }, Console.ForegroundColor = ConsoleColor.Green);
        }

        public static void WriteError(ErrorTypes type, string code, string server)
        {
            Console.WriteLine(type switch
            {
                ErrorTypes.Invalid  => $"[Invalid] Code: {code} | Server: {server} | Time: {DateTime.Now} | Count: {Program.Invalid}",
                ErrorTypes.Redeemed => $"[Already Redeemed] Code: {code} | Server: {server} | Time: {DateTime.Now} | Count: {Program.Invalid}" 
            }, Console.ForegroundColor = ConsoleColor.Red);
        }

        public static void WriteGiveaway(GiveawayTypes type, string server, string channel)
        {
            Console.WriteLine(type switch
            {
                GiveawayTypes.Won => $"[Won Giveaway] Server: {server} | Channel: {channel} | Count: {Program.WonGiveaways} | Time: {DateTime.Now}",
                GiveawayTypes.Joined => $"[Joined Giveaway] Server: {server} | Channel: {channel} | Count: {Program.JoinedGiveaways} | Time: {DateTime.Now}"
            }, Console.ForegroundColor = ConsoleColor.Magenta);
        }
    }
}
