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
using System.Windows.Threading;

namespace HFIV_6_BeehiveManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Queen queen; //nie tworzymy nowego obiektu.
                                      //Wykorzystamy ten utworzony w MainWindow.xaml a przechowywany w słowniku Resources
        private DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            queen = Resources["queen"] as Queen; //Wiązanie danych pozwala usunąć wiersze poniżej, ujęte w komentarz
                                                 //które aktualizowały właściwość statusReport.Text
            //statusReport.Text = queen.StatusReport;
            timer.Tick += Timer_Tick;
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            workShift_Click(this, new RoutedEventArgs());
        }

        private void assignJob_Click(object sender, RoutedEventArgs e)
        {
            queen.AssignBee(jobSelector.Text);
            //statusReport.Text = queen.StatusReport;
        }

        private void workShift_Click(object sender, RoutedEventArgs e)
        {
            queen.WorkNextShift();
            //statusReport.Text = queen.StatusReport;
        }
    }
}
