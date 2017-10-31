using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Artech.Genexus.Common.Objects;

namespace Sincrum.Packages.KBToExcel
{
   public partial class KBToExcelUI : Form
   {
      public KBToExcelUI()
      {
         InitializeComponent();

         CheckObjectList.DataSource = Enum.GetNames(typeof(ObjectTypes)).Where(str => str != "Other").ToArray();
         for (int i = 0; i < CheckObjectList.Items.Count; i++) {
            CheckObjectList.SetItemChecked(i,true);
         }
      }

      private void button1_Click(object sender, EventArgs e)
      {
         List<ObjectTypes> list = new List<ObjectTypes>();

         foreach (string s in CheckObjectList.CheckedItems) {
            Enum.TryParse( s, out ObjectTypes ot);
            list.Add(ot);
         }

         if (list.Count > 0)
         {
            KBToExcel kte = new KBToExcel();
            kte.ExportToCSV(list, checkOnlyDatePart.Checked);
            this.Close();
         }
         else
            MessageBox.Show("You must select at least one object type");
      }
   }
}
