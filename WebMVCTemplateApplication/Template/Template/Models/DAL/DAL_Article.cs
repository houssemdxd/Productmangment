using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Template.Models.Entities;
using Template.Utilities;

namespace Template.Models.DAL
{
    public class DAL_Article
    {
        private static Article GetEntityFromDataRow(DataRow dataRow)
        {
            Article article = new Article();
            article.Id = (int)dataRow["Id"];
            article.Designation = dataRow["Designation"] == DBNull.Value ? null : (string)dataRow["Designation"];
            article.Categorie = dataRow["Categorie"] == DBNull.Value ? null : (string)dataRow["Categorie"];
            if (dataRow["Prix"] == DBNull.Value)
                article.Prix = null;
            else
                article.Prix = Convert.ToInt32(dataRow["Prix"]);
            article.DateFabrication = dataRow["DateFabrication"] == DBNull.Value ? null : (DateTime?)dataRow["DateFabrication"];
            return article;
        }
        private static List<Article> GetListFromDataTable(DataTable dt)
        {
            List<Article> list = new List<Article>();
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                    list.Add(GetEntityFromDataRow(dr));
            }
            return list;
        }
        public static void Add(Article article)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                string StrSQL = "INSERT INTO Article (Designation, Categorie, Prix, DateFabrication) VALUES (@Designation, @Categorie,@Prix, @DateFabrication)";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@Designation", article.Designation ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Categorie", article.Categorie ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Prix", article.Prix ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DateFabrication", article.DateFabrication ?? (object)DBNull.Value);
                DataBaseAccessUtilities.NonQueryRequest(command);
            }
        }
        public static void Update(int id, Article article)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                string StrSQL = "UPDATE Article SET Designation= @Designation, Categorie = @Categorie, Prix = @Prix, DateFabrication = @DateFabrication WHERE Id = @CurId";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@CurId", id);
                command.Parameters.AddWithValue("@Designation", article.Designation ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Categorie", article.Categorie ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Prix", article.Prix ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DateFabrication", article.DateFabrication ?? (object)DBNull.Value);
                DataBaseAccessUtilities.NonQueryRequest(command);
            }
        }
        public static void Delete(int EntityKey)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                string StrSQL = "DELETE FROM Article WHERE Id=@EntityKey";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@EntityKey", EntityKey);
                DataBaseAccessUtilities.NonQueryRequest(command);

            }
        }
        public static Article SelectById(int EntityKey)
        {
            using (SqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                string StrSQL = "SELECT * FROM Article WHERE Id = @EntityKey";
                SqlCommand command = new SqlCommand(StrSQL, con);
                command.Parameters.AddWithValue("@EntityKey", EntityKey);
                DataTable dt = DataBaseAccessUtilities.SelectRequest(command);
                if (dt != null && dt.Rows.Count != 0)
                    return GetEntityFromDataRow(dt.Rows[0]);
                else
                    return null;
            }
        }
        public static List<Article> SelectAll()
        {
            DataTable dataTable;
            using (SqlConnection con = DbConnection.GetConnection())
            {
                con.Open();
                string StrSQL = "SELECT * FROM Article";
                SqlCommand command = new SqlCommand(StrSQL, con);
                dataTable = DataBaseAccessUtilities.SelectRequest(command);
            }
            return GetListFromDataTable(dataTable);
        }
    }
}
