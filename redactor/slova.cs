using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redactor
{
    internal class slova
    {
        public string slovo { get; set; }
        public string Sentencelength { get; set; }
        public string width { get; set; }
        private slova() { }
        public slova(string slovos, string Sentencelengths, string widths)
        {
            slovo = slovos;
            Sentencelength = Sentencelengths;
            width = widths;
        }
    }
}
