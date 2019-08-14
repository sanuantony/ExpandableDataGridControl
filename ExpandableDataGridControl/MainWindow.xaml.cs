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
using TemplateUserControls.DataGridUserControl;

namespace ExpandableDataGridControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //People = new List<Person>
            //{
            //    new Person
            //    {
            //        FirstName="Sanu",LastName="Antony",MiddleName="",EmailAddress="sanu@nextgen.com",PhoneNumber="9995537668"
            //    },
            //    new Person
            //    {
            //        FirstName="Anupama",LastName="S",MiddleName="P",EmailAddress="anu@nextgen.com",PhoneNumber="9741553153"
            //    },
            //    new Person
            //    {
            //        FirstName="C",LastName="Antony",MiddleName="F",EmailAddress="antony@nextgen.com",PhoneNumber="9895318686"
            //    }
            //};
            //Columns = new List<ExpandableGridColumn>
            //{
            //    new ExpandableGridColumn
            //    {
            //        ColumnName ="FirstName",Header="First Name",Order=1,Visibility=true,Width=100
            //    },
            //    new ExpandableGridColumn
            //    {
            //        ColumnName ="LastName",Header="Last Name",Order=2,Visibility=true,Width=100
            //    },
            //    new ExpandableGridColumn
            //    {
            //        ColumnName ="MiddleName",Header="Middle Name",Order=3,Visibility=true,Width=100
            //    },
            //    new ExpandableGridColumn
            //    {
            //        ColumnName ="EmailAddress",Header="Email Address",Order=4,Visibility=false,Width=100
            //    },
            //    new ExpandableGridColumn
            //    {
            //        ColumnName ="PhoneNumber",Header="Phone Number",Order=5,Visibility=false,Width=100
            //    }
            //};
        }

        public List<Person> People { get; set; }
        public List<ExpandableGridColumn> Columns { get; set; }

    }


}
