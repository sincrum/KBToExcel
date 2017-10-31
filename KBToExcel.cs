using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Artech.Architecture.Common;
using Artech.Architecture.Common.Descriptors;
using Artech.Architecture.Common.Objects;
using Artech.Architecture.Common.Services;
using Artech.Architecture.Common.Packages;

using Artech.Architecture.UI.Framework.Services;
using Artech.Udm.Framework;
using Artech.Genexus.Common.Services;
using Artech.Architecture.BL.Framework.Services;

using System.Windows.Forms;
using System.IO;

namespace Sincrum.Packages.KBToExcel
{
   public enum ObjectTypes  { Transaction, Procedure, WorkPanel, WebPanel, DataProvider, DataSelector, DataView, Menu, SDT, Other }
   /*
   transaction TRN = 0
¾ procedure PRC = 1
¾ report RPT = 2
¾ menu MNU = 3
¾ work panel WKP = 4
¾ data view DV = 6
¾ table TBL = 7
¾ folder FLD = 8
¾ prompt PMT = 12
¾ web panel WBP = 13 
¾ external program XPG = 14
¾ menu bar MBR = 17
¾ transaction style STRN = 18
¾ procedure style SPRC = 19
¾ report style SRPT = 20
¾ work panel style SWKP = 21
¾ prompt style SPMT = 22
¾ web panel style SWBP = 23
¾ menu bar style SMBR = 24
¾ theme THM = 25
¾ structure data type SDT = 26
¾ Language LNG = 28
*/
   public class KBToExcel
   {

      private ObjectTypes GXTypeOf(KBObject o)
      {
         if (o is Artech.Genexus.Common.Objects.Transaction)
            return ObjectTypes.Transaction;
         else if (o is Artech.Genexus.Common.Objects.WebPanel)
            return ObjectTypes.WebPanel;
         else if (o is Artech.Genexus.Common.Objects.Procedure)
            return ObjectTypes.Procedure;
         else if (o is Artech.Genexus.Common.Objects.DataProvider)
            return ObjectTypes.DataProvider;
         else if (o is Artech.Genexus.Common.Objects.DataSelector)
            return ObjectTypes.DataSelector;
         else if (o is Artech.Genexus.Common.Objects.DataView)
            return ObjectTypes.DataView;
         else if (o is Artech.Genexus.Common.Objects.Menu)
            return ObjectTypes.Menu;
         else if (o is Artech.Genexus.Common.Objects.SDT)
            return ObjectTypes.SDT;
         else if (o is Artech.Genexus.Common.Objects.WorkPanel)
            return ObjectTypes.WorkPanel;
         else
            return ObjectTypes.Other;
      }

      private bool IsObjectType( KBObject o, ObjectTypes t)
      {
         if (GXTypeOf(o) == t)
            return true;
         return false;
      }

      private string Gx9Name(KBObject o) {
         if (o is Artech.Genexus.Common.Objects.SDT)
            return o.Name;
         else
            return o.Name.Substring(1, o.Name.Length-1);
      }

      public void ExportToCSV(List<ObjectTypes> list, bool OnlyDatePart) {
         IKBService kbService = UIServices.KB;
         if (kbService == null || kbService.CurrentModel == null)
         {
            MessageBox.Show("You must open a Genexus KB first");
            return;
         }

         SaveFileDialog saveFileDialog1 = new SaveFileDialog();

         saveFileDialog1.Filter = "CSV (comma delimited file)(*.csv)|*.csv|All files (*.*)|*.*";
         saveFileDialog1.FilterIndex = 2;
         saveFileDialog1.RestoreDirectory = true;

         if (saveFileDialog1.ShowDialog() == DialogResult.OK)
         {
            try
            {
               using (StreamWriter sw = new StreamWriter(saveFileDialog1.OpenFile(), Encoding.UTF8))
               {
                  string t = "Name;Module;Type;Created;Updated";
                  sw.WriteLine(t);

                  foreach (KBObject obj in kbService.CurrentModel.Objects)
                  {
                     // Se busca en la lista seleccionada
                     foreach (ObjectTypes ot in list)
                     {
                        if (GXTypeOf(obj) == ot)
                        {
                           if (OnlyDatePart)
                              t = obj.Name + ";" + obj.Module.QualifiedName + ";" + obj.GetType().ToString().Replace("Artech.Genexus.Common.Objects.", "") + ";" + obj.Timestamp.ToShortDateString() + ";" + obj.LastUpdate.ToShortDateString();
                           else
                              t = obj.Name + ";" + obj.Module.QualifiedName + ";" + obj.GetType().ToString().Replace("Artech.Genexus.Common.Objects.", "") + ";" + obj.Timestamp.ToString() + ";" + obj.LastUpdate.ToString();
                           sw.WriteLine(t);
                        }
                     }
                  }
               }
            }
            catch (Exception ex) {
               MessageBox.Show(ex.Message);
            }
         }
      }
   }
}
