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

namespace MiniTC
{
    /// <summary>
    /// Logika interakcji dla klasy PanelTC.xaml
    /// </summary>
    public partial class PanelTC : UserControl
    {
        public PanelTC()
        {
            InitializeComponent();
        }

        //                                                                          TextBox
        //rejestracja zdarzenia tak, żeby możliwe było jego bindowanie
        public static readonly RoutedEvent PathChangedEvent =
        EventManager.RegisterRoutedEvent("TabItemSelected",
                     RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                     typeof(PanelTC));

        //definicja zdarzenia PathChanged
        public event RoutedEventHandler PathChanged
        {
            add { AddHandler(PathChangedEvent, value); }
            remove { RemoveHandler(PathChangedEvent, value); }
        }

        //Metoda pomocnicza wywołująca zdarzenie
        //przy okazji metoda ta tworzy obiekt argument przekazywany przez to zdarzenie
        void RaisePathChanged()
        {
            //argument zdarzenia
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(PanelTC.PathChangedEvent);
            //wywołanie zdarzenia
            RaiseEvent(newEventArgs);
        }



        //zarejestrowanie własności zależenej - taki mechanizm konieczny jest
        // aby możliwe było Bindowanie tej właśności z innnymi obiektami
        public static readonly DependencyProperty DirectoryProperty =
            DependencyProperty.Register("Directory", typeof(string),
                typeof(PanelTC),
                new FrameworkPropertyMetadata(null));

        //"czysta" właściwość powiązania z właściwości zależną
        //do niej będziemy się odnosić w XAMLU
        public string Directory
        {
            get { return (string)GetValue(DirectoryProperty); }
            set { SetValue(DirectoryProperty, value); }
        }

        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            //przy każdej zmianie tekstu w polu textBox
            //wyrzucamy zdarzenie, które informuje o tym,
            //że zmieniła się ścieżka
            RaisePathChanged();
        }



        //                                                                          ComboBox
        public static readonly RoutedEvent DrivesFocusedEvent =
        EventManager.RegisterRoutedEvent("DrivesComboBox",
                     RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                     typeof(PanelTC));

        public event RoutedEventHandler DrivesFocused
        {
            add { AddHandler(DrivesFocusedEvent, value); }
            remove { RemoveHandler(DrivesFocusedEvent, value); }
        }

        void RaiseDrivesFocused()
        {
            //argument zdarzenia
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(PanelTC.DrivesFocusedEvent);
            //wywołanie zdarzenia
            RaiseEvent(newEventArgs);
        }

        public static readonly DependencyProperty DrivesNamesProperty =
            DependencyProperty.Register(
                nameof(DrivesNames),
                typeof(string[]),
                typeof(PanelTC)
            );

        public string[] DrivesNames
        {
            get { return (string[])GetValue(DrivesNamesProperty); }
            set { SetValue(DrivesNamesProperty, value); }
        }

        private void cb_GotMouseCapture(object sender, RoutedEventArgs e)
        {
            //przy rozwinięciu comboboxa sprawdza dyski
            RaiseDrivesFocused();
        }



        //                                                                          SelectedComboBox

        public static readonly RoutedEvent DriveSelectedEvent =
        EventManager.RegisterRoutedEvent("DriveComboBox",
                     RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                     typeof(PanelTC));

        public event RoutedEventHandler DriveSelected
        {
            add { AddHandler(DriveSelectedEvent, value); }
            remove { RemoveHandler(DriveSelectedEvent, value); }
        }

        void RaiseDriveSelected()
        {
            //argument zdarzenia
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(PanelTC.DriveSelectedEvent);
            //wywołanie zdarzenia
            RaiseEvent(newEventArgs);
        }

        public static readonly DependencyProperty CurrentDriveProperty =
            DependencyProperty.Register(
                nameof(CurrentDrive),
                typeof(string),
                typeof(PanelTC)
            );

        public string CurrentDrive
        {
            get { return (string)GetValue(CurrentDriveProperty); }
            set { SetValue(CurrentDriveProperty, value); }
        }

        private void cb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RaiseDriveSelected();
        }




        //                                                                          ListBox  

        public static readonly DependencyProperty DirectoriesProperty =
            DependencyProperty.Register(
                nameof(Directories),
                typeof(string[]),
                typeof(PanelTC)
            );

        public string[] Directories
        {
            get { return (string[])GetValue(DirectoriesProperty); }
            set { SetValue(DirectoriesProperty, value); }
        }


        //                                                                          SelectedListBox

        public static readonly DependencyProperty SelectedDirectoryProperty =
            DependencyProperty.Register(
                nameof(SelectedDirectory),
                typeof(string),
                typeof(PanelTC)
            );

        public string SelectedDirectory
        {
            get { return (string)GetValue(SelectedDirectoryProperty); }
            set { SetValue(SelectedDirectoryProperty, value); }
        }


        //                                                                          ListBox MouseDoubleClick
        public static readonly RoutedEvent DirectoryDoubleClickedEvent =
        EventManager.RegisterRoutedEvent("DirectoryListBoxDoubleClicked",
                     RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                     typeof(PanelTC));

        public event RoutedEventHandler DirectoryDoubleClicked
        {
            add { AddHandler(DirectoryDoubleClickedEvent, value); }
            remove { RemoveHandler(DirectoryDoubleClickedEvent, value); }
        }

        void RaiseDirectoryDoubleClicked()
        {
            //argument zdarzenia
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(PanelTC.DirectoryDoubleClickedEvent);
            //wywołanie zdarzenia
            RaiseEvent(newEventArgs);
        }
        private void lb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RaiseDirectoryDoubleClicked();
        }
    }
}
