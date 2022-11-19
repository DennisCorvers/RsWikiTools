using System.Collections.Generic;

namespace RsWiki
{
    public interface IWikiModule
    {
        string Process(string inputLine);

        IEnumerable<string> GetInfo();
    }
}
