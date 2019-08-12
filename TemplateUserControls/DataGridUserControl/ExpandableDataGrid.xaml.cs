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
            InitializeComponent();
        }

        #region Private Properties
        private double totalWidth = 0;
        private double currentWidth = 0;
        private ExpandableGridColumn currentVisibleColumn = new ExpandableGridColumn();
        private ExpandableGridColumn nextInlineColumn = new ExpandableGridColumn();
        #endregion Private Properties

        public List<ExpandableGridColumn> ColumnsList
        {
            get { return (List<ExpandableGridColumn>)GetValue(ColumnsListProperty); }
            set { SetValue(ColumnsListProperty, value); }
        }

        public static readonly DependencyProperty ColumnsListProperty =
            DependencyProperty.Register("ColumnsList", typeof(List<ExpandableGridColumn>), typeof(ExpandableDataGrid), new PropertyMetadata(new List<ExpandableGridColumn>()));


        public ObservableCollection<object> ItemSourceCollection
        {
            get { return (ObservableCollection<object>)GetValue(ItemSourceCollectionProperty); }
            set { SetValue(ItemSourceCollectionProperty, value); }
        }

        public static readonly DependencyProperty ItemSourceCollectionProperty =
            DependencyProperty.Register("ItemSourceCollection", typeof(ObservableCollection<object>), typeof(ExpandableDataGrid), new PropertyMetadata(new ObservableCollection<object>()));

        private void DataGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (ColumnsList != null && ColumnsList.Count > 0)
            {
                totalWidth = ColumnsList.Sum(x => x.Width);
                currentWidth = ColumnsList.Where(i => i.Visibility.Equals(true)).Sum(x => x.Width);
            }
            if (nextInlineColumn != null && nextInlineColumn.Width > 0)
                if ((currentWidth + nextInlineColumn.Width) <= DataGridExpander.ActualWidth)
                {                    
                    ColumnsList[ColumnsList.LastIndexOf(nextInlineColumn)].Visibility = true;
                    FindCurrentAndNextColumn();
                }
                else if (currentWidth>ActualWidth)
                {
                    ColumnsList[ColumnsList.LastIndexOf(currentVisibleColumn)].Visibility = false;
                    FindCurrentAndNextColumn();
                }
            currentWidth = DataGridExpander.ActualWidth;
        }

        private void FindCurrentAndNextColumn()
        {
            nextInlineColumn = ColumnsList.OrderBy(x => x.Order).ToList().FirstOrDefault(i=>i.Visibility.Equals(false));
            currentVisibleColumn = ColumnsList.OrderBy(x => x.Order).ToList().LastOrDefault(i => i.Visibility.Equals(true));
        }
    }
}
