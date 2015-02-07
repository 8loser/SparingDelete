using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using CommandLine;
using CommandLine.Text;

namespace SparingDelete
{
    class Options
    {
        [Option('f', "folder", Required = true,
           HelpText = "起始目錄路徑")]
        public string folder { get; set; }

        [Option('p', "pattern", DefaultValue = "*.bak",
          HelpText = "搜尋名稱樣本")]
        public string pattern { get; set; }

        [Option('k', "keep", DefaultValue = 5,
        HelpText = "保留檔案數量")]
        public int keep { get; set; }

        [Option('r', "recycling", DefaultValue = false,
        HelpText = "是否移至資源回收筒")]
        public Boolean recycling { get; set; }

        [Option('l', "log", DefaultValue = false,
        HelpText = "是否產生紀錄檔")]
        public Boolean log { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
