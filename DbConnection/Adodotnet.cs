// See https://aka.ms/new-console-template for more information
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Xml.Linq;

namespace Networking
{
	class CodeRunner
	{
		static async Task<string> GetContent(string url)
		{
			using HttpClient httpClient = new HttpClient();
			HttpResponseMessage httpResponse = await httpClient.GetAsync(url);
			try
			{
				foreach (var header in httpResponse.Headers)
				{
					Console.WriteLine($"{header.Key}: {header.Value}");
				}
				string html = await httpResponse.Content.ReadAsStringAsync();
				return html;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message); ;
				return "Error";
			}
		}
		static async Task DownloadWeb(string url, string filename)
		{
			using HttpClient httpClient = new HttpClient();
			try
			{
				HttpResponseMessage Response = await httpClient.GetAsync(url);
				using var stream = await Response.Content.ReadAsStreamAsync();
				using var streamWrite = File.OpenWrite(filename);
				int size = 500;
				byte[] buffer = new byte[size];
				bool end = false;
				do
				{
					int byteNum = await stream.ReadAsync(buffer, 0, size);
					if (byteNum == 0)
					{
						end = true;
					}
					else
					{
						await streamWrite.WriteAsync(buffer, 0, byteNum);
					}
				} while (!end);

			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);

			}
		}
		static async Task Main1(string[] args)
		{
			string url = new string("https://www.google.com/search?q=showmaker");
			var url1 = "https://postman-echo.com/post";
			//string content = await GetContent(url);
			//Console.WriteLine(content);
			using var handler = new SocketsHttpHandler();
			handler.AllowAutoRedirect = true;
			var cookies = new CookieContainer();
			handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
			handler.UseCookies = true;
			handler.CookieContainer = cookies;
			using var httpClient = new HttpClient(handler);
			using var httpRequestMsg = new HttpRequestMessage();
			httpRequestMsg.Method = HttpMethod.Post;
			httpRequestMsg.RequestUri = new Uri(url1);
			httpRequestMsg.Headers.Add("User-Agent", "Mozilla/5.0");
			var parameters = new List<KeyValuePair<string, string>>();
			parameters.Add(new KeyValuePair<string, string>("key1", "value1"));
			parameters.Add(new KeyValuePair<string, string>("key2", "value2"));
			httpRequestMsg.Content = new FormUrlEncodedContent(parameters);
			var response = await httpClient.SendAsync(httpRequestMsg);
			cookies.GetCookies(new Uri(url1)).ToList().ForEach((Cookie o) =>
			{
				Console.WriteLine($"{o.Name}: {o.Value}");
			});
			string html = await response.Content.ReadAsStringAsync();
			Console.WriteLine(html);
		}
	}
}
namespace networking3
{
	class Program
	{
		static async Task Main1(string[] args)
		{
			if (HttpListener.IsSupported)
			{
				Console.WriteLine("HttpListener is supported");
			}
			else
			{
				Console.WriteLine("HttpListener is not supported");
				throw new Exception("Not support HttpListener");
			}
		}
	}
}
namespace ADO
{
	class Exercuter
	{
		static void Ado1()
		{
			Console.OutputEncoding = Encoding.Unicode;
			string sqlConnection = "server = DESKTOP-KJRTV58\\SQLEXPRESS; database = xtlab; integrated security = true ";
			using SqlConnection connection = new SqlConnection(sqlConnection);
			connection.Open();
			using DbCommand command = new SqlCommand();
			command.Connection = connection;
			command.CommandText = "SELECT TOP 10 * FROM SANPHAM";
			using var datareader = command.ExecuteReader();
			while (datareader.Read())
			{
				Console.WriteLine($"Ten san pham: {datareader["TenSanPham"]}, Gia: {datareader["Gia"]}");
			}

			connection.Close();
		}
		static void Ado2()
		{
			Console.OutputEncoding = Encoding.UTF8;
			string sqlConnection = "server = DESKTOP-KJRTV58\\SQLEXPRESS; database = xtlab; integrated security = true";
			using var dbConnection = new SqlConnection(sqlConnection);
			dbConnection.Open();
			using var command = new SqlCommand();
			command.Connection = dbConnection;
			command.CommandText = "INSERT INTO [Shippers] (Hoten, Sodienthoai) VALUES (@Hoten, @Sdt)";
			//command.CommandText = "DELETE FROM [Shippers] WHERE Hoten = @Hoten AND Sodienthoai = @";
			//var danhmucid = new SqlParameter("@danhmucid", 5);
			//command.Parameters.Add(danhmucid);
			//danhmucid.Value = 3;
			//var danhmuctrave = command.Parameters.AddWithValue("@danhmucid", 5);
			//danhmuctrave.Value = 3;
			var Hoten = command.Parameters.AddWithValue("@Hoten", null);
			var Sdt = command.Parameters.AddWithValue("@Sdt", null);
			Hoten.Value = "Xuan Thu Lab";
			Sdt.Value = "09321786133";
			Console.WriteLine(command.ExecuteNonQuery());
			//using var sqlReader = command.ExecuteReader();
			//var dataTable = new DataTable();
			//dataTable.Load(sqlReader);

			//if (sqlReader.HasRows)
			//{
			//	while (sqlReader.Read())
			//	{
			//		Console.WriteLine($"{sqlReader["DanhmucID"]} - {sqlReader["TenDanhMuc"]} - {sqlReader["Mota"]}");
			//	}

			//}
			//else
			//{
			//	Console.WriteLine("No data found");
			//}
			//dbConnection.Close();
		}
		static void Print(DataTable table)
		{
			Console.WriteLine($"Ten bang: {table.TableName}");
			foreach (DataColumn c in table.Columns)
			{
				Console.Write($"{c.ColumnName,15}");
			}
			Console.WriteLine();
			foreach (DataRow r in table.Rows)
			{
				for (int i = 0; i < table.Columns.Count; i++)
				{
					Console.Write($"{r[i],15}");
				}
				Console.WriteLine();
			}
		}
		static void Ado3()
		{
			Console.OutputEncoding = Encoding.UTF8;
			string sqlConnection = "server = DESKTOP-KJRTV58\\SQLEXPRESS; database = xtlab; integrated security = true";
			SqlConnection connect = new SqlConnection(sqlConnection);
			connect.Open();
			//var dataset = new DataSet();
			//var table = new DataTable("MyTable");
			//dataset.Tables.Add(table);
			//table.Columns.Add("STT");
			//table.Columns.Add("HoTen");
			//table.Columns.Add("Tuoi");
			//table.Rows.Add(1, "XuanThuLab", 25);
			//table.Rows.Add(2, "Nguyen Van A", 23);
			//table.Rows.Add(3, "Nguyen Van B", 20);
			//Console.WriteLine($"Ten bang: {table.TableName}");
			//Print(table);
			var adapter = new SqlDataAdapter();
			adapter.TableMappings.Add("Table", "NhanVien");
			adapter.SelectCommand = new SqlCommand("SELECT [NhanviennID], [Ten], [Ho] FROM [dbo].[NhanVien]", connect);
			DataSet dataset = new DataSet();
			adapter.Fill(dataset);
			DataTable table = dataset.Tables["NhanVien"];
			var row = table.Rows.Add();
			row["Ten"] = "Minh";
			row["Ho"] = "Vu";
			adapter.InsertCommand = new SqlCommand("INSERT INTO [dbo].[Nhanvien] (Ten, Ho) VALUES (@Ho, @Ten)", connect);
			adapter.InsertCommand.Parameters.Add("@Ho", SqlDbType.NVarChar, 255, "Ho");
			adapter.InsertCommand.Parameters.Add("@Ten", SqlDbType.NVarChar, 255, "Ten");
			var row10 = table.Rows[10];
			row10.Delete();
			adapter.DeleteCommand = new SqlCommand("DELETE FROM [dbo].[Nhanvien] WHERE [NhanviennID] = @NhanvienID", connect);
			var primary1 = adapter.DeleteCommand.Parameters.Add("@NhanvienID", SqlDbType.Int);
			primary1.SourceColumn = "NhanviennID";
			primary1.SourceVersion = DataRowVersion.Original;
			adapter.Update(dataset);
			connect.Close();
		}
		static void Ado4()
		{
			DataTable table = new DataTable("Nhanvien");
			SqlConnection connection;
			SqlDataAdapter adapter = new SqlDataAdapter();
			DataSet dataset = new DataSet();
			connection = new SqlConnection("server = DESKTOP-KJRTV58\\SQLEXPRESS; database = xtlab; integrated security = true");
			try
			{
				connection.Open();
				adapter = new SqlDataAdapter();
				adapter.TableMappings.Add("Table", "NhanVien");
				adapter.SelectCommand = new SqlCommand("SELECT [NhanviennID], [Ten], [Ho] FROM [dbo].[NhanVien]", connection);
				adapter.InsertCommand = new SqlCommand("INSERT INTO [dbo].[Nhanvien] (Ten, Ho) VALUES (@Ho, @Ten)", connection);
				adapter.InsertCommand.Parameters.Add("@Ho", SqlDbType.NVarChar, 255, "Ho");
				adapter.InsertCommand.Parameters.Add("@Ten", SqlDbType.NVarChar, 255, "Ten");
				adapter.DeleteCommand = new SqlCommand("DELETE FROM [dbo].[Nhanvien] WHERE [NhanviennID] = @NhanvienID", connection);
				var primary1 = adapter.DeleteCommand.Parameters.Add(new SqlParameter("@NhanvienID", SqlDbType.Int));
				primary1.SourceColumn = "NhanviennID";
				primary1.SourceVersion = DataRowVersion.Original;
				adapter.UpdateCommand = new SqlCommand("UPDATE [dbo].[Nhanvien] SET Ho = @Ho, Ten = @Ten WHERE NhanviennID = @NhanvienID");
				var primary2 = adapter.UpdateCommand.Parameters.Add(new SqlParameter("@NhanviennID", SqlDbType.Int));
				primary2.SourceColumn = "NhanviennID";
				primary2.SourceVersion = DataRowVersion.Original;
				adapter.UpdateCommand.Parameters.Add("@Ho", SqlDbType.NVarChar, 255, "Ho");
				adapter.UpdateCommand.Parameters.Add("@Ten", SqlDbType.NVarChar, 255, "Ten");
				dataset.Tables.Add(table);
				adapter.Fill(dataset);
				table = dataset.Tables["Nhanvien"];
				Print(table);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		static void Main12(string[] args)
		{
			Ado4();
		}
	}
}
