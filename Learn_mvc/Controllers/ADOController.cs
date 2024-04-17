
using Learn_mvc.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Learn_mvc.Controllers
{
	public class ADOController : Controller
	{
		//string connectionString = @"Server=DESKTOP-F92A9QM\SQLEXPRESS;Database=temp_db;User Id=sa;Password=admin@123;";
		string connectionString = @"Server=DESKTOP-F92A9QM\SQLEXPRESS;Database=MyDB;Trusted_Connection=True;";

		private const string RETRIEVE_ALL_QUERY = @"SELECT * FROM tbl_student";

		private const string RETRIEVE_QUERY = @"SELECT * FROM tbl_student WHERE StdId=@StdId;";

		private const string UPDATE_QUERY = @"UPDATE tbl_student
													SET
													StdName = @StdName,
													StdRollNumber = @StdRollNumber,
													StdClass = @StdClass,
													StdSubject = @StdSubject
													WHERE StdId = @StdId;";

		private const string INSERT_QUERY = @"INSERT INTO tbl_student
													(
														StdName,
														StdRollNumber,
														StdClass,
														StdSubject
													)
													VALUES
													(
														@StdName,
														@StdRollNumber,
														@StdClass,
														@StdSubject
													);";

		private const string DELETE_QUERY = @"DELETE FROM tbl_student WHERE StdId = @StdId;";

		public ActionResult Index()
		{
			List<Student2> lst_student = GetStudents();
			return View(lst_student);
		}

		[HttpGet]
		public ActionResult AddUpdate(int id)
		{
			Student2 student = GetStudent(id);
			if (student == null)
			{
				return View(new Student2());
			}
			else
			{
				return View(student);
			}
		}

		[HttpPost]
		public ActionResult AddUpdate(Student2 std)
		{
			if (ModelState.IsValid)
			{
				if (std.Std_id == 0) // Create
				{
					if (CreateStudent(std))
					{
						return RedirectToAction("Index");
					}
					else
					{
						ViewBag.errmsg = "Could not add record!!";
						return View();
					}
				}
				else // Update
				{
					if (UpdateStudent(std))
					{
						return RedirectToAction("Index");
					}
					else
					{
						ViewBag.errmsg = "Could not update record!!";
						return View();
					}
				}
			}
			return View();
		}

		public ActionResult Delete(int id)
		{
			if (id != 0)
			{
				if (DeleteStudent(id))
				{
					return RedirectToAction("Index");
				}
				else
				{
					ViewBag.errmsg = "Could not delete record!!";
					return RedirectToAction("Index");
				}
			}
			ViewBag.errmsg = "Could not delete record!!";
			return RedirectToAction("Index");
		}

		// ------------------------------------------------------------------------------------------ //

		public bool DeleteStudent(int id)
		{
			bool isSuccessful = false;

			var parameters = new Dictionary<string, object>()
				{
					{ "@StdId", id },
				};

			isSuccessful = ExecuteNonQuery(DELETE_QUERY, parameters);

			return isSuccessful;
		}

		public bool CreateStudent(Student2 std)
		{
			bool isSuccessful = false;

			var parameters = new Dictionary<string, object>()
				{
					{ "@StdName", std.Std_name },
					{ "@StdRollNumber", std.Std_rollnumber },
					{ "@StdClass", std.Std_class },
					{ "@StdSubject", std.Std_subject }
				};

			isSuccessful = ExecuteNonQuery(INSERT_QUERY, parameters);

			return isSuccessful;
		}
		public bool UpdateStudent(Student2 std)
		{
			bool isSuccessful = false;

			var parameters = new Dictionary<string, object>()
				{
					{ "@StdId", std.Std_id },
					{ "@StdName", std.Std_name },
					{ "@StdRollNumber", std.Std_rollnumber },
					{ "@StdClass", std.Std_class },
					{ "@StdSubject", std.Std_subject }
				};

			isSuccessful = ExecuteNonQuery(UPDATE_QUERY, parameters);

			return isSuccessful;
		}

		public Student2 GetStudent(int id)
		{
			Student2 Student = null;
			using (var studentData = ExecuteQuery(RETRIEVE_QUERY, new Dictionary<string, object>() { { "@StdId", id } }))
			{
				Student = GetToList(
								studentData,
								x => new Student2()
								{
									Std_id = x.Field<int>("StdId"),
									Std_name = x.Field<string>("StdName"),
									Std_rollnumber = x.Field<string>("StdRollNumber"),
									Std_class = x.Field<string>("StdClass"),
									Std_subject = x.Field<string>("StdSubject")
								}
							).FirstOrDefault();
			}

			return Student;
		}

		public List<Student2> GetStudents()
		{
			var Students = new List<Student2>();
			using (var studentData = ExecuteQuery(RETRIEVE_ALL_QUERY, new Dictionary<string, object>() { }))
			{
				Students = GetToList(
								studentData,
								x => new Student2()
								{
									Std_id = x.Field<int>("StdId"),
									Std_name = x.Field<string>("StdName"),
									Std_rollnumber = x.Field<string>("StdRollNumber"),
									Std_class = x.Field<string>("StdClass"),
									Std_subject = x.Field<string>("StdSubject")
								}
							);
			}

			return Students;
		}

		public bool ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
		{
			try
			{
				using (var Connection = new SqlConnection(connectionString))
				using (var cmd = new SqlCommand(query, Connection))
				{
					Connection.Open();

					if (parameters != null)
					{
						foreach (var parameter in parameters)
						{
							cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
						}
					}

					var reader = cmd.ExecuteNonQuery();

					Connection.Close();
				}
			}
			catch (Exception ex)
			{
				ex.Data["parameters"] = parameters;
				throw ex;
			}

			return true;
		}

		public DataSet ExecuteQuery(string query, Dictionary<string, object> parameters = null)
		{
			var dataset = new DataSet();
			var datatable = new DataTable();

			try
			{
				using (var Connection = new SqlConnection(connectionString))
				using (var cmd = new SqlCommand(query, Connection))
				{
					Connection.Open();

					if (parameters != null)
					{
						foreach (var parameter in parameters)
						{
							cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
						}
					}

					var reader = cmd.ExecuteReader();

					LoadData(datatable, reader);

					dataset.Tables.Add(datatable);

					Connection.Close();
				}
			}
			catch (Exception ex)
			{
				ex.Data["parameters"] = parameters;
				throw ex;
			}

			return dataset;
		}

		public List<T> GetToList<T>(DataSet ds, Func<DataRow, T> f, int tableIndex = 0)
		{
			if (ds.Tables == null || ds.Tables.Count < tableIndex + 1)
				return default(List<T>);

			return ds.Tables[tableIndex].AsEnumerable().Select(x => f.Invoke(x)).ToList();
		}

		public void LoadData(DataTable datatable, DbDataReader reader)
		{
			for (int i = 0; i < reader.FieldCount; i++)
			{
				if (!datatable.Columns.Contains(reader.GetName(i)))
					datatable.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
			}

			var values = new List<object>();

			while (reader.Read())
			{
				values.Clear();

				for (int i = 0; i < reader.FieldCount; i++)
				{
					values.Add(reader[i]);
				}
				try
				{
					datatable.LoadDataRow(values.ToArray(), true);
				}
				catch (Exception ex)
				{
					Debug.WriteLine(ex.Message);
				}
			}
		}
	}

}