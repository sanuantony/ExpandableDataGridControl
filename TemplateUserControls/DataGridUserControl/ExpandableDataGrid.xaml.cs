using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TemplateUserControls.DataGridUserControl
{
    /// <summary>
    /// Interaction logic for ExpandableDataGrid.xaml
    /// </summary>
    public partial class ExpandableDataGrid : UserControl
    {
        public ExpandableDataGrid()
        {
            ColumnsList = Columns;
            InitializeComponent();
            GenerateGridColumns();
        }

        #region Private Properties
        private double totalWidth = 0;
        private double currentWidth = 0;
        private ExpandableGridColumn currentVisibleColumn = new ExpandableGridColumn();
        private ExpandableGridColumn nextInlineColumn = new ExpandableGridColumn();
        #endregion Private Properties

        #region Dependency Properties
        /// <summary>
        /// Adding the list of column in the data grid 
        /// </summary>
        public List<ExpandableGridColumn> ColumnsList
        {
            get { return (List<ExpandableGridColumn>)GetValue(ColumnsListProperty); }
            set { SetValue(ColumnsListProperty, value); }
        }

        public static readonly DependencyProperty ColumnsListProperty =
            DependencyProperty.Register("ColumnsList", typeof(List<ExpandableGridColumn>), typeof(ExpandableDataGrid), new PropertyMetadata(new List<ExpandableGridColumn>()));


        /// <summary>
        /// ItemSource for the datagrid
        /// </summary>
        public object ItemSourceCollection
        {
            get { return (object)GetValue(ItemSourceCollectionProperty); }
            set { SetValue(ItemSourceCollectionProperty, value); }
        }

        public static readonly DependencyProperty ItemSourceCollectionProperty =
            DependencyProperty.Register("ItemSourceCollection", typeof(object), typeof(ExpandableDataGrid), new PropertyMetadata(new object()));

        #endregion Dependency Properties

        private void DataGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            if (ColumnsList != null && ColumnsList.Count > 0)
            {
                totalWidth = ColumnsList.Sum(x => x.Width);
                currentWidth = ColumnsList.Where(i => i.Visibility.Equals(true)).Sum(x => x.Width);
                FindCurrentAndNextColumn();
            }
            else
            {
                Columns = new List<ExpandableGridColumn>
            {
                new ExpandableGridColumn
                {
                    ColumnName ="FirstName",Header="First Name",Order=1,Visibility=true,Width=100
                },
                new ExpandableGridColumn
                {
                    ColumnName ="LastName",Header="Last Name",Order=2,Visibility=true,Width=100
                },
                new ExpandableGridColumn
                {
                    ColumnName ="MiddleName",Header="Middle Name",Order=3,Visibility=false,Width=100
                },
                new ExpandableGridColumn
                {
                    ColumnName ="EmailAddress",Header="Email Address",Order=4,Visibility=false,Width=100
                },
                new ExpandableGridColumn
                {
                    ColumnName ="PhoneNumber",Header="Phone Number",Order=5,Visibility=false,Width=100
                }
            };
                People = new ObservableCollection<Person>
            {
                new Person
                {
                    FirstName="Sanu",LastName="Antony",MiddleName="",EmailAddress="sanu@nextgen.com",PhoneNumber="9995537668"
                },
                new Person
                {
                    FirstName="Anupama",LastName="S",MiddleName="P",EmailAddress="anu@nextgen.com",PhoneNumber="9741553153"
                },
                new Person
                {
                    FirstName="C",LastName="Antony",MiddleName="F",EmailAddress="antony@nextgen.com",PhoneNumber="9895318686"
                }
            };
            }
            if (nextInlineColumn != null && nextInlineColumn.Width > 0)
                if ((currentWidth + nextInlineColumn.Width) <= DataGridExpander.ActualWidth)
                {
                    ColumnsList[ColumnsList.LastIndexOf(nextInlineColumn)].Visibility = true;
                }
                else if (currentWidth > ActualWidth)
                {
                    ColumnsList[ColumnsList.LastIndexOf(currentVisibleColumn)].Visibility = false;
                }
            currentWidth = DataGridExpander.ActualWidth;
            FindCurrentAndNextColumn();
            GenerateGridColumns();
            // DataGridExpander.UpdateLayout();

        }
        #region Methods

        /// <summary>
        /// Fetching the last visible column and next column in line to show.
        /// </summary>
        private void FindCurrentAndNextColumn()
        {
            nextInlineColumn = ColumnsList.OrderBy(x => x.Order).ToList().FirstOrDefault(i => i.Visibility.Equals(false));
            currentVisibleColumn = ColumnsList.OrderBy(x => x.Order).ToList().LastOrDefault(i => i.Visibility.Equals(true));
        }

        /// <summary>
        /// Generating the data grid columns.
        /// </summary>
        private void GenerateGridColumns()
        {

            DataGridExpander.Columns.Clear();
            DataGridExpander.ItemsSource = People;
            if (ColumnsList != null)
                foreach (var item in ColumnsList.Where(x => x.Visibility.Equals(true)))
                {
                    var column = new DataGridTextColumn
                    {
                        Header = item.Header,
                        Binding = new Binding(item.ColumnName),
                        Width = item.Width,
                        Visibility = item.Visibility ? Visibility.Visible : Visibility.Collapsed
                    };
                    DataGridExpander.Columns.Add(column);
                }
            if (false)
            {
                foreach (var item in ColumnsList.Where(x => x.Visibility.Equals(true)))
                {
                    for (int i = 0; i < DataGridExpander.Columns.Count; i++)
                    {

                        var z = DataGridExpander.Columns[i].ClipboardContentBinding.BindingGroupName;
                        var q = DataGridExpander.Columns[i].ClipboardContentBinding;
                        if (Convert.ToString(DataGridExpander.Columns[i].Header) == item.ColumnName)
                        {
                            DataGridExpander.Columns[i].Visibility = item.Visibility ? Visibility.Visible : Visibility.Collapsed;
                            break;
                        }
                    }
                }
            }
            System.IO.File.AppendAllText(@"D:\sanu.txt", "Count: " + DataGridExpander.Columns.Count.ToString() + " Width: " + DataGridExpander.ActualWidth + "::::\n\r");
        }
        #endregion Methods

        public ObservableCollection<Person> People { get; set; }
        public List<ExpandableGridColumn> Columns { get; set; }
    }
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

    }
}
