using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Yangaroo.Core
{
    public class DatabaseHelper : IDisposable, IDatabaseHelper
    {
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;

        public DatabaseHelper(string connectionString = null)
        {
            _connection = new SqlConnection(connectionString ?? ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString); 
            _connection.Open();
        }

        /// <summary>
        /// begin transaction without providing a name
        /// </summary>
        /// <returns>the SqlTransaction object that established the database connection</returns>
        public SqlTransaction BeginTransaction()
        {
            return _connection.BeginTransaction();
        }

        /// <summary>
        /// get the sql connection object
        /// </summary>
        /// <returns>return the sqlconnection object</returns>
        public SqlConnection GetConnection()
        {
            return _connection;
        }

        public void Close()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }

            _connection.Dispose();
        }

        /// <summary>
        /// This method opens (if necessary) and assigns a transaction, command type and parameters 
        /// to the provided command.
        /// </summary>
        /// <param name="command">the SqlCommand to be prepared</param>
        /// <param name="connection">a valid SqlConnection, on which to execute this command</param>
        /// <param name="transaction">a valid SqlTransaction, or 'null'</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParameters to be associated with the command or 'null' if no parameters are required</param>
        private void PrepareCommand(SqlCommand command, bool transaction, CommandType commandType, string commandText, IEnumerable<SqlParameter> commandParameters)
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();

            command.Connection = _connection;

            if (transaction)
            {
                if (_transaction == null)
                    _transaction = _connection.BeginTransaction();

                command.Transaction = _transaction;
            }

            command.CommandType = commandType;
            
            if (!commandText.ToLower().StartsWith("dbo."))
            {
                commandText = "dbo." + commandText;
            }

            command.CommandText = commandText;

            if (commandParameters != null)
                AttachParameters(command, commandParameters);
        }

        /// <summary>
        /// This method is used to attach array of SqlParameters to a SqlCommand.
        /// 
        /// This method will assign a value of DbNull to any parameter with a direction of
        /// InputOutput and a value of null.  
        /// 
        /// This behavior will prevent default values from being used, but
        /// this will be the less common case than an intended pure output parameter (derived as InputOutput)
        /// where the user provided no input value.
        /// </summary>
        /// <param name="command">The command to which the parameters will be added</param>
        /// <param name="commandParameters">an array of SqlParameters tho be added to command</param>
        private void AttachParameters(SqlCommand command, IEnumerable<SqlParameter> commandParameters)
        {
            foreach (SqlParameter p in commandParameters)
            {
                if ((p.Direction == ParameterDirection.InputOutput) && (p.Value == null))
                {
                    p.Value = DBNull.Value;
                }

                command.Parameters.Add(p);
            }
        }

        /// <summary>
        /// This method assigns an array of values to an array of SqlParameters.
        /// </summary>
        /// <param name="commandParameters">array of SqlParameters to be assigned values</param>
        /// <param name="parameterValues">array of objects holding the values to be assigned</param>
        private void AssignParameterValues(SqlParameter[] commandParameters, SqlParameter[] parameterValues)
        {
            if ((commandParameters == null) || (parameterValues == null))
            {
                return;
            }

            // we must have the same number of values as we pave parameters to put them in
            if (commandParameters.Length != parameterValues.Length)
            {
                throw new ArgumentException("Parameter count does not match Parameter Value count.");
            }

            // iterate through the SqlParameters, assigning the values from the corresponding position in the value array
            for (int i = 0; i < commandParameters.Length; i++)
            {
                commandParameters[i] = parameterValues[i];
            }
        }

        /// <summary>
        /// get a data table from executing a storedproc. use this static method only if you're 
        /// expecting exactly one data table, hence it's great to use this method in cases where 
        /// binding a data grid or dropdown list. 
        /// 
        /// </summary>
        /// <param name="spName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string spName, params SqlParameter[] parameterValues)
        {
            DataTable dt = null;

            DataSet ds = GetDataSet(spName, parameterValues);

            if (ds != null && ds.Tables.Count > 0)
                dt = ds.Tables[0];

            return dt;
        }

        /// <summary>
        /// get a data set from executing a storedproc. use this static method when you're 
        /// expecting a dataset or iterating over a datareader. 
        /// 
        /// </summary>
        /// <param name="spName">the name of the storedproc</param>
        /// <param name="parameterValues">the parameter array, either a sqlparameter array or multiple sqlparameter parameter</param>
        /// <returns>a dataset that contain the result of the query</returns>
        public static DataSet GetDataSet(string spName, params SqlParameter[] parameterValues)
        {
            using (var databaseHelper = new DatabaseHelper())
            {
                return databaseHelper.ExecuteDataset(false, spName, parameterValues);
            }
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset and takes no parameters) against the provided SqlConnection. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(CommandType.StoredProcedure, "PublishOrders");
        /// </remarks>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        private int ExecuteNonQuery(bool createTransaction, CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(createTransaction, commandType, commandText, null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns no resultset) against the specified SqlConnection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(CommandType.StoredProcedure, "PublishOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        private int ExecuteNonQuery(bool createTransaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            var cmd = new SqlCommand();
            PrepareCommand(cmd, createTransaction, commandType, commandText, commandParameters);

            int retval = cmd.ExecuteNonQuery();

            cmd.Parameters.CopyTo(commandParameters, 0);
            cmd.Parameters.Clear();

            return retval;
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns no resultset) against the specified SqlConnection 
        /// using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// e.g.:  
        ///  int result = ExecuteNonQuery("setPersonLanguage", parameterValues);
        ///  int result = ExecuteNonQuery("setPersonLanguage", new SqlParameter("@EventID", p.UserID), new SqlParameter("@Language", p.Language));
        /// </remarks>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public int ExecuteNonQuery(bool createTransaction, string spName, params SqlParameter[] parameterValues)
        {
            if (parameterValues == null || parameterValues.Length == 0)
            {
                return ExecuteNonQuery(createTransaction, CommandType.StoredProcedure, spName);
            }

            var commandParameters = new SqlParameter[parameterValues.Length];
            AssignParameterValues(commandParameters, parameterValues);

            return ExecuteNonQuery(createTransaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset and takes no parameters) against the provided SqlTransaction. 
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders");
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        private DataSet ExecuteDataset(bool transaction, CommandType commandType, string commandText)
        {
            return ExecuteDataset(transaction, commandType, commandText, null);
        }

        /// <summary>
        /// Execute a SqlCommand (that returns a resultset) against the specified SqlTransaction
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  DataSet ds = ExecuteDataset(trans, CommandType.StoredProcedure, "GetOrders", new SqlParameter("@prodid", 24));
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        private DataSet ExecuteDataset(bool transaction, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            var cmd = new SqlCommand();
            PrepareCommand(cmd, transaction, commandType, commandText, commandParameters);

            var da = new SqlDataAdapter(cmd);
            var ds = new DataSet();

            da.Fill(ds);

            if (commandParameters != null)
                cmd.Parameters.CopyTo(commandParameters, 0);
            cmd.Parameters.Clear();

            return ds;
        }

        /// <summary>
        /// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified 
        /// SqlTransaction using the provided parameter values.  This method will query the database to discover the parameters for the 
        /// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
        /// </summary>
        /// <remarks>
        /// This method provides no access to output parameters or the stored procedure's return value parameter.
        /// 
        /// </remarks>
        /// <param name="transaction">a valid SqlTransaction</param>
        /// <param name="spName">the name of the stored procedure</param>
        /// <param name="parameterValues">an array of objects to be assigned as the input values of the stored procedure</param>
        /// <returns>a dataset containing the resultset generated by the command</returns>
        public DataSet ExecuteDataset(bool transaction, string spName, params SqlParameter[] parameterValues)
        {
            if (parameterValues == null || parameterValues.Length <= 0)
            {
                return ExecuteDataset(transaction, CommandType.StoredProcedure, spName);
            }

            var commandParameters = new SqlParameter[parameterValues.Length];
            AssignParameterValues(commandParameters, parameterValues);

            return ExecuteDataset(transaction, CommandType.StoredProcedure, spName, commandParameters);
        }

        /// <summary>
        /// Create a new SqlConnection initialized with the connection string in Web.config
        /// </summary>
        /// <returns>A new connection object</returns>
        public static SqlConnection GetDatabaseConnection()
        {
            return new SqlConnection(ConfigurationManager.ConnectionStrings["dbConnectionString"].ConnectionString);
        }

        /// <summary>
        /// Open a new connection
        /// </summary>
        /// <returns>An open connection</returns>
        public static SqlConnection GetOpenDatabaseConnection()
        {
            SqlConnection oSqlConnection = GetDatabaseConnection();
            oSqlConnection.Open();
            return oSqlConnection;
        }
    }
}
