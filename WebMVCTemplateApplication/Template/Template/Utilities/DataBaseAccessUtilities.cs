﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Template.Utilities
{
    public class DataBaseAccessUtilities
    {
        public static int NonQueryRequest(SqlCommand MyCommand)
        {
            try
            {
                try
                {
                    MyCommand.Connection.Open();
                }
                catch (SqlException e)
                {
                    throw new Exception("Database Connection Error", e);
                }

                return MyCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("DataBase Connection Error", e);
            }
            finally
            {
                MyCommand.Connection.Close();
            }

        }
        public static int NonQueryRequest(string StrRequest, SqlConnection MyConnection)
        {
            try
            {
                try
                {
                    MyConnection.Open();
                }
                catch (SqlException e)
                {
                    throw new Exception("DataBase Connection Error", e);
                }

                SqlCommand MyCommand = new SqlCommand(StrRequest, MyConnection);
                return MyCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Query Execution Error", e);
            }
            finally
            {
                MyConnection.Close();
            }

        }

        public static object ScalarRequest(SqlCommand MyCommand)
        {
            try
            {
                try
                {
                    MyCommand.Connection.Open();
                }
                catch (SqlException e)
                {
                    throw new Exception("Database Connection Error", e);
                }

                return MyCommand.ExecuteScalar();
            }
            catch (SqlException e)
            {
                throw new Exception("Query Execution Error", e);
            }
            finally
            {
                MyCommand.Connection.Close();
            }
        }
        public static object ScalarRequest(string StrRequest, SqlConnection MyConnection)
        {
            try
            {
                try
                {
                    MyConnection.Open();
                }
                catch (SqlException e)
                {
                    throw new Exception("Database Connection Error", e);
                }
                SqlCommand MyCommand = new SqlCommand(StrRequest, MyConnection);

                return MyCommand.ExecuteScalar();
            }
            catch (SqlException e)
            {
                throw new Exception("Query Execution Error", e);
            }
            finally
            {
                MyConnection.Close();
            }
        }


        public static DataTable SelectRequest(SqlCommand MyCommand)
        {
            try
            {
                DataTable Table;
                SqlDataAdapter SelectAdapter = new SqlDataAdapter(MyCommand);
                Table = new DataTable();
                SelectAdapter.Fill(Table);
                return Table;
            }
            catch (SqlException e)
            {
                throw new Exception("Query Execution Error", e);
            }
            finally
            {
                MyCommand.Connection.Close();
            }
        }
        public static DataTable SelectRequest(string StrSelectRequest, SqlConnection MyConnection)
        {
            try
            {
                DataTable Table;
                SqlCommand SelectCommand = new SqlCommand(StrSelectRequest, MyConnection);
                SqlDataAdapter SelectAdapter = new SqlDataAdapter(SelectCommand);
                Table = new DataTable();
                SelectAdapter.Fill(Table);
                return Table;
            }
            catch (SqlException e)
            {

                throw new Exception("Query Execution Error", e);
            }
            finally
            {
                MyConnection.Close();
            }
        }


        public static void ShowRequest(SqlCommand Cmd)
        {
            String ListPar = "\t\t****Texte de la Requete****\n";
            ListPar += Cmd.CommandText + "\n";
            ListPar += "\t\t****Liste des parmêtres : ****\n";
            foreach (SqlParameter Param in Cmd.Parameters)
            {
                ListPar += Param.ParameterName + "\t:\t\"" + Param.Value.ToString() + "\"\t:\t" + Param.DbType.ToString() + "\n";
            }
        }

        public static bool CheckFieldValueExistence(string TableName, string FieldName, SqlDbType FieldType, object FieldValue, SqlConnection MyConnection)
        {
            try
            {
                string StrRequest = "SELECT COUNT(" + FieldName + ") FROM " + TableName + " WHERE ((" + FieldName + " = @" + FieldName + ")";
                StrRequest += "OR ( (@" + (FieldName + 1).ToString() + " IS NULL)AND (" + FieldName + " IS NULL)))";
                SqlCommand Command = new SqlCommand(StrRequest, MyConnection);
                Command.Parameters.Add("@" + FieldName, FieldType).Value = FieldValue;
                Command.Parameters.Add("@" + FieldName + 1, FieldType).Value = FieldValue;
                return ((int)DataBaseAccessUtilities.ScalarRequest(Command) != 0);
            }
            catch (SqlException e)
            {
                throw new Exception("Field Value Existence Check Failed", e);
            }
            finally
            {
                MyConnection.Close();
            }

        }

        public static object GetMaxFieldValue(SqlConnection MyConnection, string TableName, string FieldName)
        {
            try
            {
                string StrMaxRequest = "SELECT MAX(" + FieldName + ") FROM " + TableName;

                SqlCommand Command = new SqlCommand(StrMaxRequest, MyConnection);
                return (DataBaseAccessUtilities.ScalarRequest(Command));

            }
            catch (SqlException e)
            {
                throw new Exception("Query Execution Error", e);
            }
            finally
            {
                MyConnection.Close();
            }
        }
    }
}
