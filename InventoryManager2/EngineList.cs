using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
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
    class EngineList
    {
        public static List<Engine> Engines = new List<Engine>();
        public static List<Engine> SearchedEngines = new List<Engine>();
        public static int? SelectedIndex = null;
        public static int? SearchedSelectedIndex = null;
        public static StackPanel Parent = null;
        public static SearchParameters SearchParams = new SearchParameters("","","",-1);

        public static void UpdatePartsDisplay()
        {
            //remove all children so it can be redrawn
            Parent.Children.RemoveRange(0, Parent.Children.Count);

            //update the serched engine list to have the correct engines in it
            UpdateSerchedList();

            //add the serched engines list back in
            foreach (Engine e in SearchedEngines)
            {
                e.AddToParent(Parent);
            }

            //change the selected index to selected colors
            if (SearchedSelectedIndex != null)
            {
                foreach (UIElement element in SearchedEngines[SearchedSelectedIndex.Value].wpfRepresentationGrid.Children)
                {
                    (element as Border).Background = Brushes.PowderBlue;
                }
            }

            WriteContentsToXML();
        }

        //used to change which engine is currently selected
        public static void UpdateSelected(Guid selectedID)
        {
            //revert the previous selected object to normal colors
            if(SelectedIndex == null)
            {
                //intentionally empty
            }
            else
            {
                for (int i = 0; i < Engines[SelectedIndex.Value].wpfRepresentationGrid.Children.Count; i++)
                {
                    if(i%2 == 0)
                    {
                        (Engines[SelectedIndex.Value].wpfRepresentationGrid.Children[i] as Border).Background = Brushes.White;
                    }
                    else
                    {
                        (Engines[SelectedIndex.Value].wpfRepresentationGrid.Children[i] as Border).Background = Brushes.LightYellow;
                    }
                }
            }

            //find the new selected serched index
            for(int i = 0; i < Engines.Count; i++)
            {
                if (Engines[i].ID == selectedID)
                {
                    SelectedIndex = i;
                    break;
                }
            }

            //change the new selected object to selected colors
            foreach (UIElement element in Engines[SelectedIndex.Value].wpfRepresentationGrid.Children)
            {
                (element as Border).Background = Brushes.PowderBlue;
            }

        }

        public static void DeleteSelected()
        {
            if (SelectedIndex != null)
            {
                //display message box to confirm deletion
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this item?", "Delete Item", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                switch(result)
                {
                    case MessageBoxResult.Yes:
                        Engines.RemoveAt(SelectedIndex.Value);
                        SelectedIndex = null;
                        UpdatePartsDisplay();
                        break;
                    case MessageBoxResult.No:
                        //intentionally empty
                        break;
                }
            }
        }

        public static void DisplaySelected(Window win)
        {
            if (SelectedIndex != null)
            {
                ItemViewer displayWindow = new ItemViewer(Engines[SelectedIndex.Value]);
                displayWindow.Owner = win;
                displayWindow.Show();
            }
        }

        public static void WriteContentsToXML()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            using(XmlWriter w = XmlWriter.Create("Inventory.xml", settings))
            {
                w.WriteStartElement("Inventory");
                foreach(Engine e in Engines)
                {
                    e.WriteToFileXml(w);
                }
                w.WriteEndElement();
            }
        }

        public static void LoadContents(string uri)
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreWhitespace = true;

                using(XmlReader r = XmlReader.Create("Inventory.xml", settings))
                {
                    string tempName = "";
                    string tempMake = "";
                    string tempModel = "";
                    string tempStartYear = "";
                    string tempEndYear = "";
                    string tempStock = "";
                    string tempBinNumber = "";
                    string tempDescription = "";

                    while(r.Read())
                    {
                        if(r.NodeType == XmlNodeType.Element && r.Name == "Engine")
                        {
                            r.Read();
                            tempName = r.ReadElementContentAsString();
                            tempMake = r.ReadElementContentAsString();
                            tempModel = r.ReadElementContentAsString();
                            tempStartYear = r.ReadElementContentAsString();
                            tempEndYear = r.ReadElementContentAsString();
                            tempStock = r.ReadElementContentAsString();
                            tempBinNumber = r.ReadElementContentAsString();
                            tempDescription = r.ReadElementContentAsString();
                            Engine tempEngine = new Engine(tempName, tempMake, tempModel, int.Parse(tempStartYear), int.Parse(tempEndYear), int.Parse(tempStock), tempBinNumber);
                            tempEngine.Description = tempDescription;
                            Engines.Add(tempEngine);
                        }
                    }
                }
            }
            catch(FileNotFoundException)
            {
                Engines = new List<Engine>();
            }

            catch(XmlException e)
            {
                MessageBox.Show("It appears your Inventory.xml file has become corrupt. Please contact the developer. Line number: " + e.LineNumber + " Line Position: " + e.LinePosition,
                    "Corrupt Inventory File",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
          
            catch(Exception e)
            {
                MessageBox.Show("It appears your Inventory.xml file has become corrupt. Please contact the developer.",
                    "Corrupt Inventory File",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
        
        // runs threw each engine on Engines and picks out the ones that meet serch criteria
        //then adds those to the SerchedSelectedEngines list
        // if one of the Engines is selected make sure the Serched Selected Index is updated
        public static void UpdateSerchedList()
        {
            SearchedEngines = new List<Engine>();

            for (int i = 0; i < Engines.Count; i++)
            {
                if (((Engines[i].Name.ToUpper().Contains(SearchParams.Name.ToUpper())) || (SearchParams.Name == "")) &&
                    ((Engines[i].Make.ToUpper().Contains(SearchParams.Make.ToUpper())) || (SearchParams.Make == "")) &&
                    ((Engines[i].Model.ToUpper().Contains(SearchParams.Model.ToUpper())) || (SearchParams.Model == "")) &&
                    ((Engines[i].StartYearsOfUse <= SearchParams.Year) && (Engines[i].EndYearsOfUse >= SearchParams.Year) || (SearchParams.Year == -1)))
                {
                    SearchedEngines.Add(Engines[i]);
                    if(SelectedIndex == null)
                    {
                        SearchedSelectedIndex = null;
                    }
                    else
                    {
                        SearchedSelectedIndex = SearchedEngines.Count - 1;
                    }
                }
                
            }
        }
    }

    struct SearchParameters
    {
        public string Name;
        public string Make;
        public string Model;
        public int Year;

        public SearchParameters (string nameIn, string makeIn, string modelIn, int yearIn)
        {
            Name = nameIn;
            Make = makeIn;
            Model = modelIn;
            Year = yearIn;
        }
    }
}
