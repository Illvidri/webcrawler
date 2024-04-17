using HtmlAgilityPack;

namespace webcrawler {

    class Program {
        public static void Main(string[] args) {
            HtmlWeb hw = new HtmlWeb();
            Console.Write("Enter Root URL Here: ");
            string rooturl = Console.ReadLine()!;
            Console.Write("Enter Starting URL Here: ");
            string url = Console.ReadLine()!;

            List<string> listurl = new List<string>();
            listurl = urllist(hw,rooturl,url,listurl);
            
            Console.WriteLine("\n\nOutput:");
            foreach(string i in listurl) {
                Console.WriteLine($"Index {listurl.IndexOf(i)}: {i}");
            }
        }
        public static List<string> urllist(HtmlWeb hw, string root, string url, List<string> listurl) {
            HtmlDocument doc = hw.Load(url);
            List<string> foundlist = new List<string>();
            try {
                foreach(HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]")) {
                    string str = link.Attributes["href"].Value;
                    if(str.Contains(root) && !listurl.Contains(str) && !foundlist.Contains(str)) {
                        foundlist.Add(str);
                    }
                }
                listurl = listurl.Union(foundlist).ToList();
            }
            catch {
                Console.WriteLine("No Links on Page; Proceeding.");
            }
            if(foundlist.Any()) {
                foreach(string i in foundlist) {
                    Console.WriteLine(i);
                    try {
                        listurl = urllist(hw, root, i, listurl);
                    }
                    catch {
                        return listurl;
                    }
                }
            }
            return listurl;
        }
    }
}