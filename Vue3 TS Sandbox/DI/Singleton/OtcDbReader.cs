using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Vue3_TS_Sandbox.Extension;

namespace Vue3_TS_Sandbox.DI.Singleton {
	public class OtcDbReader {
		SspApp app { get; set; }

		public OtcDbReader(SspApp app) {
			this.app = app;
		}

		public SqlConnection CreateConnection() {
			var connection = new SqlConnection(app.Settings.DB.ConnectionString);
			return connection;
		}



		public DataTable FillTable(string command, SqlConnection connect) {
			var sqlCommand = CreateCommand(command, connect);
			using (sqlCommand) {
				//_logger.LogInformation(command);
				DataTable table = new DataTable();
				SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
				adapter.Fill(table);
				return table;
			}
		}

		public IEnumerable<DataRow> GetRows(string command, SqlConnection connect) {
			return FillTable(command, connect).AsEnumerable();
		}

		public DataRow GetRow(string command, SqlConnection connect) {
			return FillTable(command, connect).AsEnumerable().FirstOrDefault();
		}
		public T GetCell<T>(string command, SqlConnection connect) {
			var row = FillTable(command, connect).AsEnumerable().FirstOrDefault();
			if (row == null)
				return default(T);
			var col = row.Table.Columns[0];
			return row.Field<T>(col);
		}
		public T GetRow<T>(string command, Func<DataRow, T> selector, SqlConnection connect) {
			return FillTable(command, connect)
			  .AsEnumerable()
			  .Select(selector)
			  .FirstOrDefault();
		}
		//

		public DataSet FillDataSet(string command, SqlConnection connect) {
			SqlCommand sqlCommand = CreateCommand(command, connect);
			using (sqlCommand) {
				SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
				DataSet dataset = new DataSet();
				adapter.Fill(dataset);
				return dataset;
			}
		}
		public DataTable FillTable(string command) {
			SqlConnection connection = CreateConnection();
			return FillTable(command, connection);
		}
		public int GetFolderIDByRequestID(int requestID, SqlConnection connection) {
			string command = $"select area from problemreport where id = {requestID}";
			return GetRow(command, r => r.Field<int?>("area"), connection).GetValueOrDefault();
		}

		public Dictionary<int, int> GetFoldersIDByRequestID(IEnumerable<int> requestID, SqlConnection connection) {
			string command = $"select id, area from problemreport where id IN( {requestID.Join()})";
			return GetRows(command, connection).ToDictionary(k => k.GetInt("id"), v => v.GetInt("area"));
		}

		SqlCommand CreateCommand(string sql, SqlConnection connection) {
			SqlCommand sqlCommand = new SqlCommand(sql, connection);
			sqlCommand.CommandTimeout = app.Settings.DB.CommandTimeout;
			return sqlCommand;
		}
	}
}
