using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sincrum.Packages.KBToExcel;
using System.Windows.Forms;

namespace ConsoleApp1
{
   class Program
   {
      static void Main(string[] args)
      {
         KBToExcelUI frm = new KBToExcelUI();
         frm.ShowDialog();
      }
   }
}
