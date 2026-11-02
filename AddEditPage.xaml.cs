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

namespace farkhutdinov_autoservice
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary> 
    public partial class AddEditPage : Page
    {
        private Service _currentServiсe = new Service();
        public AddEditPage(Service SelectedService)
        {
            InitializeComponent();

            if (SelectedService != null)
                _currentServiсe = SelectedService;
            DataContext = _currentServiсe;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentServiсe.Title))
                errors.AppendLine("Укажите название услуги");
            if (_currentServiсe.Cost <= 0)
                errors.AppendLine("Укажите стоимость услуги");
            if (_currentServiсe.DiscountInt < 0 || _currentServiсe.DiscountInt > 100)
                errors.AppendLine("Укажите скидку услуги");
            if (_currentServiсe.Duration==0)
                errors.AppendLine("Укажите длительность услуги");
            if (_currentServiсe.Duration > 240 || _currentServiсe.Duration<0)
                errors.AppendLine("Длительность не может быть больше 240 минут или меньше 0");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var allServices = Farkhutdinov_avtoserviceEntities.GetContext().Service.ToList();
            allServices = allServices.Where(p => p.Title == _currentServiсe.Title).ToList();
            if(allServices.Count==0)
            {
                if (_currentServiсe.ID == 0)
                    Farkhutdinov_avtoserviceEntities.GetContext().Service.Add(_currentServiсe);
                try
                {
                    Farkhutdinov_avtoserviceEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена");
                    Manager.MainFrame.GoBack();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Уже существует такая услуга");
            }
            if (_currentServiсe.ID == 0)
                Farkhutdinov_avtoserviceEntities.GetContext().Service.Add(_currentServiсe);

            try
            {
                Farkhutdinov_avtoserviceEntities.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
