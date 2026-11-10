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
                case "PHONG":
                    XmlNode phong = doc.SelectSingleNode($"//phongban[@ma='{arg}']");
                    if (phong == null)
                    {
                        Console.WriteLine($"Khong co phong {arg}");
                    }
                    else
                    {
                        XmlNodeList nvs = phong.SelectNodes("nhanvien");
                        if (nvs.Count == 0)
                            Console.WriteLine($"Phong {arg} chua co nhan vien");
                        else
                            foreach (XmlNode n in nvs)
                                Console.WriteLine($"{n.Attributes["ma"].Value} - {n["hoten"].InnerText}");
                    }
                    break;
                case "DEM":
                    double soPhong = (double)nav.Evaluate("count(//phongban)");
                    double soNhanVien = (double)nav.Evaluate("count(//nhanvien)");
                    Console.WriteLine($"So phong ban: {soPhong}");
                    Console.WriteLine($"So nhan vien: {soNhanVien}");
                    break;
                default:
                    Console.WriteLine("Lenh khong hop le");
                    break;
                case "XPATH":
                    try
                    {
                        object result = nav.Evaluate(arg);
                        if (result is XPathNodeIterator iter)
                        {
                            if (iter.Count == 0)
                                Console.WriteLine("Khong co ket qua");
                            else
                                while (iter.MoveNext())
                                    Console.WriteLine($"{iter.Current.Name}: {iter.Current.Value}");
                        }
                        else if (result is double d)
                            Console.WriteLine(d.ToString(CultureInfo.InvariantCulture));
                        else if (result is bool b)
                            Console.WriteLine(b ? "true" : "false");
                        else
                            Console.WriteLine(result.ToString());
                    }
                    catch
                    {
                        Console.WriteLine("Bieu thuc XPath khong hop le");
                    }
                    break;
            }
        }
    }
}