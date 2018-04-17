using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Phase3
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public Dictionary<string, int> AvgRatingsPerGenre(int minRating, int maxRating, string titleKeyword, string tagKeyword)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                // PostgeSQL-style connection string
                string connstring = String.Format("Server={0};Port={1};" +
                    //"User Id={2};Password={3};" +
                    "Database={4};",
                    "localhost", "5432", "postgres");
                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                // quite complex sql statement
                string sql = "SELECT g.name, AVG(r.rating) as avgrating FROM ratings r, movies m, hasagenre h, genres g"
                    + "WHERE m.movieid = r.movieid and m.movieid = h.movieid and h.genreid = g.genreid"
                    + "GROUP BY g.genreid;";
                // data adapter making request from our connection
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                // i always reset DataSet before i do
                // something with it.... i don't know why :-)
                ds.Reset();
                // filling DataSet with result from NpgsqlDataAdapter
                da.Fill(ds);
                // since it C# DataSet can handle multiple tables, we will select first
                dt = ds.Tables[0];
                // connect grid to DataTable
                //dataGridView1.DataSource = dt;
                // since we only showing the result we don't need connection anymore
                conn.Close();
            }
            catch (Exception msg)
            {
                // something went wrong, and you wanna know why
                //MessageBox.Show(msg.ToString());
                throw;
            }
        }

        public Dictionary<string, int> CountPerGenre(int minRating, int maxRating, string titleKeyword, string tagKeyword)
        {
            return null;
        }
    }
}
