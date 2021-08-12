using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryManager2
{
    /// <summary>
    /// Interaction logic for ItemDisplayControl.xaml
    /// </summary>
    public partial class ItemDisplayControl : UserControl
    {
        private Engine displayedEngine = null;

        public ItemDisplayControl(Engine engineIn)
        {
            InitializeComponent();
            displayedEngine = engineIn;
            tbName.Text = engineIn.Name;
            tbMake.Text = engineIn.Make;
            tbModel.Text = engineIn.Model;
            tbYearRange.Text = engineIn.StartYearsOfUse.ToString() + " - " + engineIn.EndYearsOfUse.ToString();
            tbStock.Text = engineIn.Stock.ToString();
            tbBinNumber.Text = engineIn.BinNumber;
            tbDescription.Text = engineIn.Description;
        }

        private void EditItemBtn_Click(object sender, RoutedEventArgs e)
        {
            (this.Parent as ContentControl).Content = new ItemEditControl(displayedEngine);
        }
    }
}
