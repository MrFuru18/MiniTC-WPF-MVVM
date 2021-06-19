using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MiniTC.ViewModel
{
    using BaseClass;
    using System.Windows;
    using System.Windows.Input;
    using Model;

    class MainViewModel : ViewModelBase
    {
        Files files = new Model.Files();
        ManageDirectories ldir = new Model.ManageDirectories();
        ManageDirectories rdir = new Model.ManageDirectories();


        //                                                              LEFT PANEL
        private string _leftPath;
        public string LeftPath
        {
            get { return _leftPath; }
            private set
            {
                _leftPath = value;
                onPropertyChanged(nameof(LeftPath));
            }
        }


        private string[] _leftDrives;
        public string[] LeftDrives
        {
            get { return _leftDrives; }
            private set
            {
                _leftDrives = value;
                onPropertyChanged(nameof(LeftDrives));
            }
        }

        public string LeftDrive
        {
            get; set;
        }

        private string[] _leftDirectories;
        public string[] LeftDirectories
        {
            get { return _leftDirectories; }
            private set
            {
                _leftDirectories = value;
                onPropertyChanged(nameof(LeftDirectories));
            }
        }

        public string LeftDirectory
        {
            get; set;
        }


        //                                                                              RIGHT PANEL
        private string _rightPath;
        public string RightPath
        {
            get { return _rightPath; }
            private set
            {
                _rightPath = value;
                onPropertyChanged(nameof(RightPath));
            }
        }


        private string[] _rightDrives;
        public string[] RightDrives
        {
            get { return _rightDrives; }
            private set
            {
                _rightDrives = value;
                onPropertyChanged(nameof(RightDrives));
            }
        }

        public string RightDrive
        {
            get; set;
        }

        private string[] _rightDirectories;
        public string[] RightDirectories
        {
            get { return _rightDirectories; }
            private set
            {
                _rightDirectories = value;
                onPropertyChanged(nameof(RightDirectories));
            }
        }

        public string RightDirectory
        {
            get; set;
        }

        //                                                                                     LEFT PANEL
        // textbox path
        private ICommand _checkPath;
        public ICommand CheckPath
        {
            get
            {
                return _checkPath ?? (_checkPath = new RelayCommand(gotodir, pathExist));
            }
        }

        private void gotodir(object param)
        {
            if (ldir.Only_Drive())
                LeftDirectories = files.Get_Paths(LeftPath, true);
            else
                LeftDirectories = files.Get_Paths(LeftPath, false);

            //MessageBox.Show(LeftPath);
        }
        private bool pathExist(object param)
        {
            return Directory.Exists(LeftPath);
        }


        private ICommand _checkDrives;
        public ICommand CheckDrives
        {
            get
            {
                return _checkDrives ?? (_checkDrives = new RelayCommand((p) => {LeftDrives = files.Get_Drives(); }, p => true));
            }
        }


        private ICommand _selectDrive;
        public ICommand SelectDrive
        {
            get
            {
                return _selectDrive ?? (_selectDrive = new RelayCommand((p) => 
                { 
                    ldir.Clear_Directories();
                    ldir.Add_Directory(LeftDrive);
                    LeftPath=LeftDrive;
                   
                            
                }, p => true));
            }
        }


        private ICommand _chooseDirectory;
        public ICommand ChooseDirectory
        {
            get
            {
                return _chooseDirectory ?? (_chooseDirectory = new RelayCommand((p) => 
                {
                         
                    if (ldir.Go_back(LeftDirectory))
                    {
                        ldir.Delete_Last_Directory();
                        LeftPath = ldir.Get_Last_Directory();
                    }
                    else
                    {
                        string newdir = ldir.Full_Name(LeftDirectory) + @"\";
                        ldir.Add_Directory(newdir);
                        LeftPath = newdir;
                    }
                        
                   
                }, p => true));
            }
        }


        //                                                                                      RIGHT PANEL
        private ICommand _rcheckPath;
        public ICommand RCheckPath
        {
            get
            {
                return _rcheckPath ?? (_rcheckPath = new RelayCommand(rgotodir, rpathExist));
            }
        }

        private void rgotodir(object param)
        {
            if (rdir.Only_Drive())
                RightDirectories = files.Get_Paths(RightPath, true);
            else
                RightDirectories = files.Get_Paths(RightPath, false);

        }
        private bool rpathExist(object param)
        {
            return Directory.Exists(RightPath);
        }


        private ICommand _rcheckDrives;
        public ICommand RCheckDrives
        {
            get
            {
                return _rcheckDrives ?? (_rcheckDrives = new RelayCommand((p) => { RightDrives = files.Get_Drives(); }, p => true));
            }
        }


        private ICommand _rselectDrive;
        public ICommand RSelectDrive
        {
            get
            {
                return _rselectDrive ?? (_rselectDrive = new RelayCommand((p) =>
                {
                    rdir.Clear_Directories();
                    rdir.Add_Directory(RightDrive);
                    RightPath = RightDrive;

                }, p => true));
            }
        }


        private ICommand _rchooseDirectory;
        public ICommand RChooseDirectory
        {
            get
            {
                return _rchooseDirectory ?? (_rchooseDirectory = new RelayCommand((p) =>
                {

                    if (rdir.Go_back(RightDirectory))
                    {
                        rdir.Delete_Last_Directory();
                        RightPath = rdir.Get_Last_Directory();
                    }
                    else
                    {
                        string newdir = rdir.Full_Name(RightDirectory) + @"\";
                        rdir.Add_Directory(newdir);
                        RightPath = newdir;
                    }


                }, p => true));
            }
        }


        //                                                                                      COPY
        private ICommand _copy;
        public ICommand Copy
        {
            get
            {
                return _copy ?? (_copy = new RelayCommand(copyFile, canCopy));
            }
        }

        private bool canCopy(object param)
        {
            return true;
        }

        private void copyFile(object param)
        {
            string f = LeftPath + @"\" + LeftDirectory;
            string d = RightPath + @"\" + LeftDirectory;
            if (RightPath == null)
            {
                MessageBox.Show("Proszę wybrać folder docelowy");
            }
            else
            {
                if (File.Exists(f))
                {
                    if (File.Exists(d))
                        MessageBox.Show("Plik został nadpisany");
                    files.CopyFile(f, d);
                    RightPath = "";
                    RightPath = rdir.Get_Last_Directory();
                }

                else
                    MessageBox.Show("Proszę wybrać poprawny plik");
            }

        }
        

    }
}

