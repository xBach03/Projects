using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace TextAdapter
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		DataTable table = new DataTable("Nhanvien");
		SqlConnection connection;
		SqlDataAdapter adapter;
		DataSet dataset = new DataSet();
		public MainWindow()
		{
			InitAdapter();
			InitializeComponent();
		}
		void InitAdapter()
		{
			connection = new SqlConnection("server = DESKTOP-KJRTV58\\SQLEXPRESS; database = xtlab; integrated security = true; TrustServerCertificate = True");
			connection.Open();
			adapter = new SqlDataAdapter();
			adapter.TableMappings.Add("Table", "NhanVien");
			adapter.SelectCommand = new SqlCommand("SELECT [NhanviennID], [Ten], [Ho] FROM [dbo].[NhanVien]", connection);

			adapter.InsertCommand = new SqlCommand("INSERT INTO [dbo].[Nhanvien] (Ten, Ho) VALUES (@Ten, @Ho)", connection);
			adapter.InsertCommand.Parameters.Add("@Ten", SqlDbType.NVarChar, 255, "Ten");
			adapter.InsertCommand.Parameters.Add("@Ho", SqlDbType.NVarChar, 255, "Ho");

			adapter.DeleteCommand = new SqlCommand("DELETE FROM [dbo].[Nhanvien] WHERE [NhanviennID] = @NhanvienID", connection);
			var primary1 = adapter.DeleteCommand.Parameters.Add(new SqlParameter("@NhanvienID", SqlDbType.Int));
			primary1.SourceColumn = "NhanviennID";
			primary1.SourceVersion = DataRowVersion.Original;

			adapter.UpdateCommand = new SqlCommand("UPDATE [dbo].[Nhanvien] SET [Ho] = @Ho, [Ten] = @Ten WHERE NhanviennID = @NhanvienID");
			var primary2 = adapter.UpdateCommand.Parameters.Add(new SqlParameter("@NhanviennID", SqlDbType.Int));
			primary2.SourceColumn = "NhanviennID";
			primary2.SourceVersion = DataRowVersion.Original;
			adapter.UpdateCommand.Parameters.Add("@Ten", SqlDbType.NVarChar, 255, "Ten");
			adapter.UpdateCommand.Parameters.Add("@Ho", SqlDbType.NVarChar, 255, "Ho");
			dataset.Tables.Add(table);
		}
		void LoadDataTable()
		{
			table.Rows.Clear();
			adapter.Fill(dataset);
		}

		private void datagrid_Loaded(object sender, RoutedEventArgs e)
		{
			LoadDataTable();
			datagrid.DataContext = table.DefaultView;
		}

		private void btnLoad_Click(object sender, RoutedEventArgs e)
		{
			LoadDataTable();
			datagrid.DataContext = table.DefaultView;
		}

		private void btnSave_Click(object sender, RoutedEventArgs e)
		{
			adapter.Update(dataset);
			table.Rows.Clear();
			adapter.Fill(dataset);
		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{
			DataRowView selectedItem = (DataRowView)datagrid.SelectedItem;
			if (selectedItem != null)
			{
				selectedItem.Delete();
			}
		}
	}
}
