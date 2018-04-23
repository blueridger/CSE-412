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
            string sql = String.Format(
  "select g.name, avg(r.rating) as avgrating "
+ "from genres g, movies m, hasagenre h, ratings r "
+ "where r.movieid = m.movieid and m.movieid = h.movieid and h.genreid = g.genreid "
+ "and m.movieid in "
+ "(select m2.movieid from movies m2, ratings r2 where m2.movieid = r2.movieid group by m2.movieid "
+ "having avg(r2.rating) >= {0} and avg(r2.rating) <= {1}) "
+ "and m.movieid in "
+ "(select m3.movieid from movies m3, tags t, taginfo ti "
+ "where m3.movieid = t.movieid and t.tagid = ti.tagid "
+ "and ti.content like concat('%', '{2}', '%') "
+ "and m3.title like concat('%', '{3}', '%')) "
+ "group by g.genreid order by g.name desc; "
+ "select g.name, count(distinct m.movieid) as count "
+ "from genres g, movies m, hasagenre h, ratings r "
+ "where r.movieid = m.movieid and m.movieid = h.movieid and h.genreid = g.genreid "
+ "and m.movieid in "
+ "(select m2.movieid from movies m2, ratings r2 where m2.movieid = r2.movieid group by m2.movieid "
+ "having avg(r2.rating) >= {0} and avg(r2.rating) <= {1}) "
+ "and m.movieid in "
+ "(select m3.movieid from movies m3, tags t, taginfo ti "
+ "where m3.movieid = t.movieid and t.tagid = ti.tagid "
+ "and ti.content like concat('%', '{2}', '%') "
+ "and m3.title like concat('%', '{3}', '%')) "
+ "group by g.genreid order by g.name desc; ", HiddenField1.Value, HiddenField2.Value, TextBox2.Text, TextBox1.Text);

            //sql = "select g.name, avg(r.rating) as avgrating from genres g, ratings r;"
                //+ "select g.name, count(r.rating) as count from genres g, ratings r;";
            // data adapter making request from our connection
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
            // i always reset DataSet before i do
            // something with it.... i don't know why :-)
            ds.Reset();
            // filling DataSet with result from NpgsqlDataAdapter
            da.Fill(ds);
            // since it C# DataSet can handle multiple tables, we will select first
            dt = ds.Tables[0];
            if (dt != null && dt.Rows.Count > 0)
            {
                Label5.Text = "";
                // connect grid to DataTable
                Chart1.DataSource = dt;
                Chart1.Series["Series1"].XValueMember = "name";
                Chart1.Series["Series1"].YValueMembers = "avgrating";
                Chart1.Series["Series1"].IsValueShownAsLabel = true;
                Chart1.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;
                Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                Chart1.DataBind();

                dt = ds.Tables[1];
                // connect grid to DataTable
                Chart2.DataSource = dt;
                Chart2.Series["Series1"].XValueMember = "name";
                Chart2.Series["Series1"].YValueMembers = "count";
                Chart2.Series["Series1"].IsValueShownAsLabel = true;
                Chart2.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 1;
                Chart2.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                Chart2.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                Chart2.DataBind();
            }
            else
            {
                Label5.Text = "No results found. Try changing your filters.";
            }
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