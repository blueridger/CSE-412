using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        try
        {
            // PostgeSQL-style connection string
            string connstring = String.Format("Server={0};Port={1};Database={2};User Id={3};",
                // +"User Id={3};Password={4};",
                "localhost", "5432", "postgres", "postgres");
            // Making connection with Npgsql provider
            NpgsqlConnection conn = new NpgsqlConnection(connstring);
            conn.Open();
            // quite complex sql statement
            string sql = "SELECT g.name, AVG(r.rating) as avgrating FROM ratings r, movies m, hasagenre h, genres g"
                + " WHERE m.movieid = r.movieid and m.movieid = h.movieid and h.genreid = g.genreid"
                + " GROUP BY g.genreid; SELECT g.name, g.genreid FROM ratings r, movies m, hasagenre h, genres g"
                + " WHERE m.movieid = r.movieid and m.movieid = h.movieid and h.genreid = g.genreid"
                + " GROUP BY g.genreid;";
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
            Chart1.DataSource = dt;
            Chart1.Series["Series1"].XValueMember = "name";
            Chart1.Series["Series1"].YValueMembers = "avgrating";
            Chart1.Series["Series1"].IsValueShownAsLabel = true;
            Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;
            Chart1.DataBind();

            dt = ds.Tables[1];
            // connect grid to DataTable
            Chart2.DataSource = dt;
            Chart2.Series["Series1"].XValueMember = "name";
            Chart2.Series["Series1"].YValueMembers = "genreid";
            Chart2.Series["Series1"].IsValueShownAsLabel = true;
            Chart2.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;
            Chart2.DataBind();
            // since we only showing the result we don't need connection anymore
            conn.Close();
        }
        catch (Exception msg)
        {
            // something went wrong, and you wanna know why
            throw;
        }
    }
}