using System;
using System.Xml;
using System.Xml.XPath;
using System.Globalization;

namespace XmlTB1_2415053122127
{
    class Program
    {
        static void Main(string[] args)
        {
            string lenhFull = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(lenhFull)) return;

            string noiDung = Console.In.ReadToEnd();

            int spaceIdx = lenhFull.IndexOf(' ');
            string cmd = spaceIdx != -1 ? lenhFull.Substring(0, spaceIdx) : lenhFull;
            string arg = spaceIdx != -1 ? lenhFull.Substring(spaceIdx + 1) : "";
            XmlDocument doc = new XmlDocument();
            try { doc.LoadXml(noiDung); } catch { return; }

            XPathNavigator nav = doc.CreateNavigator();

            switch (cmd)
            {
                case "LUONG_TREN":
                    long x = long.Parse(arg, CultureInfo.InvariantCulture);
                    XmlNodeList nodes = doc.SelectNodes($"//nhanvien[luong > {x}]");
                    if (nodes.Count == 0)
                    {
                        Console.WriteLine("Khong co nhan vien");
                    }
                    else
                    {
                        foreach (XmlNode n in nodes)
                            Console.WriteLine($"{n.Attributes["ma"].Value} - {n["hoten"].InnerText} - {n["luong"].InnerText}");
                    }
                    break;