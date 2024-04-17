using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learn_mvc.Data.Database
{
	public class DBWorker : IDisposable
	{
		private readonly MyDBEntities context;

		public DBWorker()
		{
			context = new MyDBEntities();
		}

		#region Data Save Method with Rollback and Commit
		//Save method use for save change in database
		//If any problem on saving then rollback otherwise commit changes
		public void Save()
		{
			if (context.Database.Connection.State != System.Data.ConnectionState.Open)
				context.Database.Connection.Open();

			using (var tran = context.Database.BeginTransaction(System.Data.IsolationLevel.ReadCommitted))
			{
				try
				{
					context.SaveChanges();
					tran.Commit();
				}
				catch (DbEntityValidationException ex)
				{
					tran.Rollback();
					// Retrieve the error messages as a list of strings.
					var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
					// Join the list to a single string.
					var fullErrorMessage = string.Join("; ", errorMessages);
					// Combine the original exception message with the new one.
					var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
					// Throw a new DbEntityValidationException with the improved exception message.
					throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
				}
				catch (Exception ex)
				{
					var errorMessages = ex.Message;

					tran.Rollback();
				}
				finally
				{
					if (context.Database.Connection.State == System.Data.ConnectionState.Open)
						context.Database.Connection.Close();
				}
			}
		}

		#endregion

		#region [tbl_customer]
		private GenericRepository<tbl_customer> tblCustomerEntity;
		public GenericRepository<tbl_customer> TblCustomerEntity
		{
			get
			{
				if (this.tblCustomerEntity == null)
				{
					this.tblCustomerEntity = new GenericRepository<tbl_customer>(context);
				}
				return tblCustomerEntity;
			}
		}
		#endregion

		#region protected virtual void Dispose(bool disposing)

		private bool disposedValue;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					context?.Dispose();
				}

				disposedValue = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}
